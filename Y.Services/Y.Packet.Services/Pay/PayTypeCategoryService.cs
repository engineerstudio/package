////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //
//                         O\  =  /O                              //
//                      ____/`---'\____                           //
//                    .'  \\|     |//  `.                         //
//                   /  \\|||  :  |||//  \                        //
//                  /  _||||| -:- |||||-  \                       //
//                  |   | \\\  -  /// |   |                       //
//                  | \_|  ''\---/''  |   |                       //
//                  \  .-\__  `-`  ___/-. /                       //
//                ___`. .'  /--.--\  `. . ___                     //
//              ."" '<  `.___\_<|>_/___.'  >'"".                  //
//            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
//            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
//      ========`-.____`-.___\_____/___.-`____.-'========         //
//                           `=---='                              //
//      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
//                   佛祖保佑       永不宕机     永无BUG          //
////////////////////////////////////////////////////////////////////

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：Aaron                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-10-04 19:55:59                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Pay                                  
*│　类    名： PayTypeCategoryService                                    
*└──────────────────────────────────────────────────────────────┘
*/
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
using Y.Infrastructure.Cache.Redis.PayService;
using Y.Packet.Entities.Pay;
using Y.Packet.Repositories.IPay;
using Y.Packet.Services.IPay;

namespace Y.Packet.Services.Pay
{
    public class PayTypeCategoryService : PayTypeCategoryCacheService, IPayTypeCategoryService
    {
        private readonly IPayTypeCategoryRepository _repository;

        public PayTypeCategoryService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory, IPayTypeCategoryRepository repository) : base(options, factory)
        {
            _repository = repository;
        }

        public async Task<bool> CreatePayTypeCategoryAsync(int merchantId)
        {
            var dic = PayTypeCategory.PayType.AliPay.GetEnumTypeDic();
            foreach (var kvp in dic)
            {
                var type = kvp.Value.ToEnum<PayTypeCategory.PayType>().Value;
                var entity = new PayTypeCategory()
                {
                    MerchantId = merchantId,
                    Name = type.GetDescription(),
                    PicUrl = "",
                    Enabled = false
                };
                await _repository.InsertAsync(entity);
            }
            return true;
        }

        public async Task<IEnumerable<PayTypeCategory>> GetListAsync(int merchantId)
        {
            //var cache =await  base.GetListCacheAsync(merchantId);
            //if(cache.Exist) return JsonHelper.JSONToObject<IEnumerable<PayTypeCategory>>(cache.Data);

            string conditons = $"WHERE Enabled=1 AND MerchantId={merchantId}";
            var list = await _repository.GetListAsync(conditons, null);

            var cacheValue = JsonHelper.ToJson(list);
            await base.SaveGetListCacheAsync(merchantId, cacheValue);

            return list;
        }

        public async Task<(IEnumerable<PayTypeCategory>, int)> GetPageListAsync(int merchantId)
        {
            string conditions = $"WHERE 1=1 AND MerchantId={merchantId}";
            var rt = await _repository.GetListPagedAsync(1, 10, conditions, "Id desc", null);
            return (rt, _repository.RecordCount());
        }

        public async Task<Dictionary<int, string>> GetPayTypeDicAsync(int merchantId)
        {
            string conditions = $"WHERE MerchantId={merchantId}";
            return (await _repository.GetListAsync(conditions)).ToDictionary(t => t.Id, t => t.Name);
        }

        public async Task<(bool, string)> UpdateStatusAsync(int merchantId, int cId, bool enabled)
        {
            if (merchantId == 0 || cId == 0) return (false, "商户不存在");
            var m = await _repository.GetAsync(cId);
            if (m.MerchantId != merchantId) return (false, "商户不存在");
            m.Enabled = enabled;
            var rt = await _repository.UpdateAsync(m);
            if (rt > 0) return (true, "更新成功");
            await base.DeleteGetListCacheAsync(merchantId);
            return (false, "状态修改失败");
        }


        public async Task<(bool, string)> SavePayTypeCategoryAsync(int merchantId, int id, string name, string url, bool isVc)
        {
            if (merchantId == 0) return (false, "商户错误");
            if (name.IsNullOrEmpty()) return (false, "请输入支付类别名称");
            if (url.IsNullOrEmpty()) return (false, "请上传支付类别的图片");

            if (id == 0)
            {
                var entity = new PayTypeCategory()
                {
                    Id = id,
                    MerchantId = merchantId,
                    Name = name,
                    PicUrl = url,
                    Enabled = true,
                    IsVirtualCurrency = isVc
                };
                var rt = await _repository.InsertAsync(entity);
                if (rt == null || rt.Value == 0) return (false, "添加失败");
            }
            else
            {
                var en = await _repository.GetAsync(merchantId, id);
                if (en == null) return (false, "数据不存在");
                en.Name = name;
                en.PicUrl = url;
                en.IsVirtualCurrency = isVc;
                if (_repository.Update(en) == 0) return (false, "更新失败");
            }
            await base.DeleteGetListCacheAsync(merchantId);
            return (true, "保存成功");
        }


    }
}