using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Encrypt;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Cache.Redis.PayService;
using Y.Packet.Entities.Pay;
using Y.Packet.Entities.Pay.ViewModel;
using Y.Packet.Repositories.IPay;
using Y.Packet.Services.IPay;

namespace Y.Packet.Services.Pay
{
    public class PayMerchantService : PayMerchantCacheService, IPayMerchantService
    {
        private readonly IPayMerchantRepository _repository;
        private readonly IPayCategoryRepository _payCategoryRepository;
        public PayMerchantService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory, IPayMerchantRepository repository, IPayCategoryRepository payCategoryRepository) : base(options, factory)
        {
            _repository = repository; _payCategoryRepository = payCategoryRepository;
        }

        public async Task<(IEnumerable<PayMerchant>, int)> GetPageListAsync(PayListPageQuery q)
        {
            var parms = new DynamicParameters();
            string conditions = $" WHERE 1=1 AND MerchantId={q.MerchantId} ";
            if (!string.IsNullOrEmpty(q.Name))
            {
                conditions += $" AND Name like @Name ";
                parms.Add("Name", $"%{q.Name}%");
            }

            var rt = await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", parms);
            return (rt, _repository.RecordCount(conditions, parms));
        }

        public async Task<(bool, string)> SavePayAsync(PayMerchantConfig d)
        {
            if (d.MerchantId == 0 || d.PayCategory == 0) return (false, "参数错误");

            var payCategory = await _payCategoryRepository.GetFromCacheAsync(d.PayCategory);

            var pay = new PayMerchant()
            {
                MerchantId = d.MerchantId,
                PayCategoryId = d.PayCategory,
                PayCategory = payCategory.PayType,
                PayTypeId = d.PayType,
                Name = d.Name,
                Enabled = d.Enabled,
                ConfigStr = d.ConfigStr,
                ValidationStr = d.ValidationStr,
                Img = d.Img.IsNullOrEmpty() ? "" : d.Img,
                Desc = d.Desc.IsNullOrEmpty() ? "" : d.Desc,
            };
            //var va = pay.Validation; 

            var rt = await _repository.InsertWithCacheAsync(pay);
            if (rt > 0) return (true, "保存成功");
            return (false, "保存失败");
        }


        public async Task<(bool, string)> UpdatePayAsync(PayMerchantConfig d)
        {
            if (d.Id == 0) return (false, "支付参数错误");

            var pay = await _repository.GetFromCacheAsync(d.Id);
            if (d.PayCategory != pay.PayCategoryId)
            {
                var payCategory = await _payCategoryRepository.GetFromCacheAsync(d.PayCategory);
                pay.PayCategory = payCategory.PayType;
            }
            pay.PayCategoryId = d.PayCategory;
            pay.Name = d.Name;
            pay.PayTypeId = d.PayType;
            pay.Enabled = d.Enabled;
            pay.ConfigStr = d.ConfigStr;
            pay.ValidationStr = d.ValidationStr;
            pay.Img = d.Img.IsNullOrEmpty() ? "" : d.Img;
            pay.Desc = d.Desc.IsNullOrEmpty() ? "" : d.Desc; ;

            var rt = await _repository.UpdateWithCacheAsync(pay);
            if (rt > 0) return (true, "保存成功");
            return (false, "保存失败");
        }


        public async Task<IEnumerable<PayMerchant>> GetListAsync(int merchantId)
        {
            //string conditions = $"WHERE Enabled=1 AND MerchantId={merchantId}";
            //return await _repository.GetListAsync(conditions);
            var list = await _repository.GetListAsync(merchantId);
            return list.Where(t => t.Enabled);
        }


        public async Task<IEnumerable<PayMerchant>> GetListAsync(int merchantId, string payMerchantIds)
        {
            int[] payMchIds = payMerchantIds.Split(",").ToIntArray();

            var list = await _repository.GetListAsync(merchantId);

            return list.Where(t => payMchIds.Contains(t.Id));
        }

        public async Task<string> GetPayNameByIdAsync(int payId)
        {
            var payMerchant = await _repository.GetFromCacheAsync(payId);
            return payMerchant.Name;
        }

        public async Task<PayMerchant> GetByIdAsync(int payId) => await _repository.GetFromCacheAsync(payId);

        public async Task<(Dictionary<int, string>, Dictionary<int, int>)> GetPayMerchantIdAndNameDicAsync(int merchantId)
        {
            if (merchantId == 0) return (null, null);
            string conditions = $"WHERE MerchantId={merchantId} ";
            //var rt = await _repository.GetListAsync(conditions);
            var rt = await _repository.GetListAsync(merchantId);
            return (rt.ToDictionary(t => t.Id, t => t.Name), rt.ToDictionary(t => t.Id, t => t.PayTypeId));
        }


        public async Task<(bool, string, PayMerchant)> GetAsync(int merchantId, int id)
        {
            if (merchantId == 0 || id == 0) return (false, "商户不存在", null);
            //string condition = $"SELECT * FROM PayMerchant WHERE  MerchantId={merchantId}  AND Id={id}";
            var rt = await _repository.GetFromCacheAsync(id);
            if (rt == null) return (false, "商户不存在", null);
            return (true, "", rt);
        }

        public async Task<(bool, string, dynamic)> GetChannelDetailsAsync(int merchantId, int id)
        {
            var rt = await GetAsync(merchantId, id);
            if (!rt.Item1) return rt;
            var d = rt.Item3.Validation;
            string des = string.Empty;
            if (string.IsNullOrEmpty(d.FixedRange))
                des = $"单次充值金额最低 {d.Price_Min} 元，最高 {d.Price_Max} 元";
            // TODO 入款优惠啊

            return (rt.Item1, rt.Item2, new
            {
                Des = des,
                FixAmount = d.FixedRange,
                HasFixAmount = !string.IsNullOrEmpty(d.FixedRange)
            });
        }




    }
}