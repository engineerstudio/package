using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Encrypt;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.Mapper;
using Y.Infrastructure.Cache.Redis.MerchantsService;
using Y.Infrastructure.ICache.IRedis.MerchantsService;
using Y.Packet.Entities.Merchants;
using Y.Packet.Entities.Merchants.ViewModels;
using Y.Packet.Repositories.IMerchants;
using Y.Packet.Services.IMerchants;

namespace Y.Packet.Services.Merchants
{
    public class NoticeAreaService : NoticeAreaCacheService, INoticeAreaService
    {
        private readonly INoticeAreaRepository _repository;

        public NoticeAreaService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory, INoticeAreaRepository repository) : base(options, factory)
        {
            _repository = repository;
        }


        public (IEnumerable<NoticeArea>, int) GetPageList(NoticeAreaListQuery q)
        {
            if (q.MerchantId == 0) return (null, 0);

            string conditions = $" WHERE 1=1 AND MerchantId={q.MerchantId} AND Deleted=0 ";
            if (q.Type != null)
                conditions += $" AND Type={q.Type.GetEnumValue()}";

            return (_repository.GetListPaged(q.Page, q.Limit, conditions, "Id desc", null), _repository.RecordCount(conditions));
        }


        public async Task<(IEnumerable<NoticeArea> value, int count)> GetPageListAsync(NoticeAreaListQuery q)
        {
            if (q.MerchantId == 0) return (null, 0);

            string md5 = MD5EncryptHelper.ToMD5(q.ToJson());
            if (await base.KeyExistsAsync($"NoticeAreaService.GetPageListAsync{q.MerchantId}_{md5}"))
            {
                var str = await base.StringGetAsync($"NoticeAreaService.GetPageListAsync{q.MerchantId}_{md5}");
                var cc = await base.StringGetAsync<int>($"NoticeAreaService.GetPageListAsync{q.MerchantId}_{md5}_c");
                return (JsonHelper.JSONToObject<IEnumerable<NoticeArea>>(str), cc);
            }


            string conditions = $" WHERE 1=1 AND MerchantId={q.MerchantId} AND Deleted=0 ";
            if (q.Type != null)
                conditions += $" AND Type={q.Type.GetEnumValue()}";

            var list = await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", null);
            var c = await _repository.RecordCountAsync(conditions);
            var cacheValue = JsonHelper.ToJson(list);
            await base.StringSetAsync($"NoticeAreaService.GetPageListAsync{q.MerchantId}_{md5}", cacheValue, TimeSpan.FromHours(2));
            await base.StringSetAsync<int>($"NoticeAreaService.GetPageListAsync{q.MerchantId}_{md5}_c", c, TimeSpan.FromHours(2));

            return (list,c);
        }

        async Task ClearCache(NoticeAreaListQuery q)
        {
            string md5 = MD5EncryptHelper.ToMD5(q.ToJson());
            if (await base.KeyExistsAsync($"NoticeAreaService.GetPageListAsync{q.MerchantId}_{md5}"))
            {
                await base.KeyDeleteAsync($"NoticeAreaService.GetPageListAsync{q.MerchantId}_{md5}");
                await base.KeyDeleteAsync($"NoticeAreaService.GetPageListAsync{q.MerchantId}_{md5}_c");
            }
        }


        public async Task<(bool, string, int)> SaveAsync(NoticeAreaInsertOrModifyModel m)
        {
            //  处理缓存  
            await ClearCache(new NoticeAreaListQuery() { MerchantId = m.MerchantId, Limit = 3 });

            int orderId = 0;
            if (m.MerchantId == 0) return (false, "商户错误", orderId);
            if (m.Title.IsNullOrEmpty()) return (false, "标题不能为空", orderId);
            if (m.Description.IsNullOrEmpty()) return (false, "内容不能为空", orderId);
            if (m.Type == NoticeArea.NoticeType.Notice && m.GroupId.IsNullOrEmpty()) return (false, "通知类别请选择通知的用户组", orderId);
            if (m.GroupId.IsNullOrEmpty()) m.GroupId = "";

            if (m.Id == 0)
            {
                var entity = ModelToEntity.Mapping<NoticeArea, NoticeAreaInsertOrModifyModel>(m);
                entity.GroupId = m.GroupId;
                entity.CreateTime = DateTime.UtcNow.AddHours(8);
                var rt = await _repository.InsertAsync(entity);
                if (rt == null || rt.Value < 1) return (false, "保存失败", orderId);
                orderId = rt.Value;
            }
            else
            {
                var entity = _repository.Get(m.Id);
                entity.GroupId = m.GroupId;
                entity.Title = m.Title;
                entity.Description = m.Description;
                entity.IsDisplay = m.IsDisplay;
                entity.Type = m.Type;
                var rt = await _repository.UpdateAsync(entity);
                if (rt < 1) return (false, "保存更新失败", orderId);
                orderId = m.Id;
            }
            // TODO 记录管理员操作日志

            return (true, "保存成功", orderId);
        }

        public async Task<(bool, string, NoticeArea)> GetAsync(int merchantId, int id)
        {
            if (merchantId == 0 || id == 0)
                return (false, "", new NoticeArea());
            var entity = await _repository.GetAsync(merchantId, id);
            if (entity == null) return (false, "数据不存在", new NoticeArea());
            return (true,string.Empty,entity);
        }

        public async Task<(bool, string)> UpdateDeletedStatusAsync(int merchantId, int noticeId)
        {
            if (merchantId == 0 || noticeId == 0) return (false, "参数错误");
            var entity = await _repository.GetAsync(merchantId, noticeId);
            if (entity == null) return (false, "数据不存在");
            entity.Deleted = true;
            var rt = await _repository.UpdateAsync(entity);
            if (rt == 0) return (false, "删除失败");

            //  处理缓存  
            await ClearCache(new NoticeAreaListQuery() { MerchantId = merchantId, Limit = 3 });

            return (true, "删除成功");
        }



        public async Task<IEnumerable<NoticeArea>> GetHomePageDisplayAsync(int merchantId)
        {
            if (merchantId == 0) return null;
            if (await base.KeyExistsAsync($"GetHomePageDisplayAsync{merchantId}"))
            {
                var str = await base.StringGetAsync($"GetHomePageDisplayAsync{merchantId}");
                return (JsonHelper.JSONToObject<IEnumerable<NoticeArea>>(str));
            }
            var list = await _repository.GetHomePageDisplayAsync(merchantId);
            var cacheValue = JsonHelper.ToJson(list);
            await base.StringSetAsync($"GetHomePageDisplayAsync{merchantId}", cacheValue, TimeSpan.FromHours(2));

            return list;
        }

    }
}