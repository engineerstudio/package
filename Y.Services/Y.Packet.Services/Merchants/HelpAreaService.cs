using Dapper;
using Microsoft.Extensions.Options;
using Quartz.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Cache.Redis.MerchantsService;
using Y.Packet.Entities.Merchants;
using Y.Packet.Entities.Merchants.ViewModels;
using Y.Packet.Repositories.IMerchants;
using Y.Packet.Services.IMerchants;

namespace Y.Packet.Services.Merchants
{
    public class HelpAreaService : HelpAreaCacheService, IHelpAreaService
    {
        private readonly IHelpAreaRepository _repository;
        private readonly IHelpAreaTypeRepository _helpAreaTypeRepository;
        public HelpAreaService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory, IHelpAreaRepository repository, IHelpAreaTypeRepository helpAreaTypeRepository) : base(options, factory)
        {
            _repository = repository;
            _helpAreaTypeRepository = helpAreaTypeRepository;
        }


        public async Task<(bool, string)> GetByMerchantIdAsync(int merchantId, int typeId = 0)
        {
            //var typeIds = await _repository.GetTypeIdsAsync(merchantId);
            //string where = $" WHERE MerchantId={merchantId} AND Id in ({ string.Join(",", typeIds)  }) ";
            //var rt = (await _helpAreaTypeRepository.GetListAsync(where));

            string conditions = $" WHERE MerchantId={merchantId} AND IsOpen=1 ";
            if (typeId != 0)
                conditions += $" AND Id={typeId}";
            var types = await _helpAreaTypeRepository.GetListAsync(conditions);
            var helps = await _repository.GetListAsync($" WHERE MerchantId={merchantId} ");
            var list = new List<HelpAreaTypeVM>();
            HelpAreaTypeVM thVM;
            foreach (var t in types)
            {
                thVM = new HelpAreaTypeVM();
                thVM.Id = t.Id;
                thVM.Title = t.Title;
                thVM.IsHref = t.IsHref;
                thVM.Href = t.Href;
                if (!t.IsHref)
                {
                    foreach (var h in helps.Where(p => p.TypeId == t.Id))
                    {
                        var hh = new HelpAreaVM();
                        hh.Title = h.Title;
                        if (typeId != 0)
                            hh.Tcontent = h.Tcontent;
                        thVM.Sub.Add(hh);
                    }
                }
                list.Add(thVM);
            }

            return (true, list.ToJson());
        }

        public async Task<(bool, string)> GetByTypeIdAsync(int merchantId, int typeId)
        {
            if (typeId == 0) return (false, "类型ID错误");
            return await GetByMerchantIdAsync(merchantId, typeId);
        }


        public async Task<(IEnumerable<HelpArea>, int)> GetPageListAsync(HelpAreaListQuery q)
        {
            var parms = new DynamicParameters();
            string conditions = $"WHERE 1=1 AND MerchantId={q.MerchantId} ";
            if (!q.Title.IsNullOrEmpty())
            {
                conditions += $" AND Title LIKE @Key ";
                parms.Add("Key", $"%{q.Title}%");
            }
            if (q.ShowIndexPage.HasValue && q.ShowIndexPage.Value)
            {
                conditions += " AND ShowIndexPage = 1";
            }
            var list = await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", new { Key = q.Title });
            return (list, _repository.RecordCount(conditions, parms));
        }


        public async Task<(bool, string)> InsertOrModify(HelpAreaAOM m)
        {
            if (m.MerchantId == 0) return (false, "商户错误");
            if (string.IsNullOrEmpty(m.Title)) return (false, "请输入标题");
            if (string.IsNullOrEmpty(m.Tcontent)) return (false, "请输入内容");
            //if (string.IsNullOrEmpty(m.Alias)) m.Alias = "";
            if (m.Id == 0)
            {
                var d = new HelpArea();
                d.MerchantId = m.MerchantId;
                d.Title = m.Title;
                d.Tcontent = m.Tcontent;
                d.TypeId = m.TypeId;
                d.ShowIndexPage = m.ShowIndexPage;
                d.Alias = m.Alias;
                d.SortNo = m.SortNo;
                d.CreateTime = DateTime.UtcNow.AddHours(8);
                var rt = await _repository.InsertAsync(d);
                return rt.ToResult("保存成功", "保存失败");
            }
            else
            {
                var d = await _repository.GetAsync(m.Id);
                if (d == null || m.MerchantId != d.MerchantId) return (false, "对象不存在");
                d.Title = m.Title;
                d.Tcontent = m.Tcontent;
                d.TypeId = m.TypeId;
                d.Alias = m.Alias;
                d.SortNo = m.SortNo;
                d.ShowIndexPage = m.ShowIndexPage;
                var rt = await _repository.UpdateAsync(d);
                return rt.ToResult("保存成功", "保存失败");
            }
        }


        public async Task<(bool, string, HelpArea)> GetAsync(int merchantId, int id)
        {
            if (merchantId == 0 || id == 0) return (false, "数据异常", null);
            string sql = $"SELECT * FROM HelpArea WHERE MerchantId={merchantId} AND ID={id}";
            var e = await _repository.GetAsync(sql);
            if (e == null) return (false, "数据不存在", null);

            return (true, "", e);
        }

        public async Task<(bool, string)> DeleteAsync(int merchantId, int id)
        {
            if (merchantId == 0 || id == 0) return (false, "数据异常");
            var no = await _repository.GetDataCountAsync(merchantId, id);
            if (no == 0)
                return (false, "数据不存在");
            var rt = await _repository.DeleteAsync(id);
            return rt.ToResult("删除成功", "删除失败");
        }

        /// <summary>
        /// 获取首页显示项
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<HelpAreaVMV2>> GetShowIndexItemsAsync(int merchantId)
        {
            if (await base.KeyExistsAsync($"GetShowIndexItemsAsync{merchantId}"))
            {
                var str = await base.StringGetAsync($"GetShowIndexItemsAsync{merchantId}");
                return (JsonHelper.JSONToObject<IEnumerable<HelpAreaVMV2>>(str));
            }
            var list = await _repository.GetShowHelpAreaAsync(merchantId);
            if (list == null) return new List<HelpAreaVMV2>();

            IEnumerable<HelpAreaVMV2> vm = list.Select(t => new HelpAreaVMV2
            {
                //Id = t.Id,
                Title = t.Alias.IsNullOrEmpty() ? t.Title : t.Alias,
                Link = $@"/help/{t.TypeId}/{t.Id}",
                SortDesc = t.SortNo
            }).ToList();

            var cacheValue = JsonHelper.ToJson(vm);
            await base.StringSetAsync($"GetShowIndexItemsAsync{merchantId}", cacheValue, TimeSpan.FromHours(2));
            return vm;
        }

    }
}