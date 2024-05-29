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
*│　创建时间：2021-01-10 19:22:45                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Pay                                  
*│　类    名： WithdrawMerchantService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Packet.Entities.Pay;
using Y.Packet.Entities.Pay.ViewModel;
using Y.Packet.Repositories.IPay;
using Y.Packet.Services.IPay;


namespace Y.Packet.Services.Pay
{
    public class WithdrawMerchantService : IWithdrawMerchantService
    {
        private readonly IWithdrawMerchantRepository _repository;

        public WithdrawMerchantService(IWithdrawMerchantRepository repository)
        {
            _repository = repository;
        }

        public async Task<(IEnumerable<WithdrawMerchant>, int)> GetPageListAsync(WithdrawalListPageQuery q)
        {
            string conditions = "WHERE 1=1 ";
            if (q.MerchantId != 0)
                conditions += $"AND MerchantId={q.MerchantId}";
            return (await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", null), _repository.RecordCount(conditions));

        }

        public async Task<(bool, string, Dictionary<int, string>)> GetDicAsync(int merchantId)
        {
            if (merchantId == 0) return (false, "未获取到", new Dictionary<int, string>());
            //string coditions = $"WHERE MerchantId={merchantId}";
            var list = await _repository.GetListAsync(merchantId);

            var dic = list.ToDictionary(t => t.Id, t => t.Name);
            return (true, "sucess", dic);
        }

        public async Task<(bool, WithdrawMerchant)> ExistOrInsertAsync(int merchantId, string typeStr)
        {
            var m = await _repository.ExistAsync(merchantId, typeStr);
            if (m != null) return (true, m);

            m = new WithdrawMerchant()
            {
                TypeStr = typeStr,
                MerchantId = merchantId,
                Name = "",
                Enabled = true,
                IsDefault = false,
                ConfigStr = "{}",
                Description = "",
                CreateTime = DateTime.UtcNow.AddHours(8)
            };

            var rt = await _repository.InsertWithCacheAsync(m);
            m.Id = rt.Value;
            return (false, m);
        }


        public async Task<(bool, string)> UpdateConfigAsync(WithdrawalsMerchantConfig config)
        {
            var m = await _repository.GetFromCacheAsync(config.Id);
            if (m == null) return (false, "商户错误");
            if (m.MerchantId != config.MerchantId) return (false, "商户错误");

            m.MerchantId = config.MerchantId;
            m.Name = config.Name;
            m.Enabled = config.Enabled;
            m.ConfigStr = config.ConfigStr;

            var rt = await _repository.UpdateWithCacheAsync(m);
            if (rt == 0) return (false, "保存错误");

            return (true, "保存成功");
        }


        /// <summary>
        /// 获取出款渠道
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<(bool, WithdrawMerchant)> GetAsync(int id)
        {
            var m = await _repository.GetFromCacheAsync(id);
            if (m == null) return (false, null);
            return (true, m);
        }


    }
}