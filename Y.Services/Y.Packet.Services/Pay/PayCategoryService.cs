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
*│　创建时间：2020-09-30 16:19:19                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Pay                                  
*│　类    名： PayCategoryService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Helper;
using Y.Packet.Entities.Pay;
using Y.Packet.Entities.Pay.ViewModel;
using Y.Packet.Repositories.IPay;
using Y.Packet.Services.IPay;

namespace Y.Packet.Services.Pay
{
    public class PayCategoryService : IPayCategoryService
    {
        private readonly IPayCategoryRepository _repository;

        public PayCategoryService(IPayCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<string>> GetAllPayTypeAsync()
        {
            return await _repository.GetAllPayTypeAsync();
        }

        public async Task<Dictionary<int, string>> GetCategoryDicAsync()
        {
            return (await _repository.GetListAsync()).ToDictionary(t => t.Id, t => t.Name);
        }

        public async Task<(IEnumerable<PayCategory>, int)> GetPageListAsync(PayListPageQuery q)
        {
            string conditions = "WHERE 1=1 ";
            return (await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", null), await _repository.RecordCountAsync());
        }

        public async Task<(bool, string)> InsertAsync(KeyValuePair<string, string> kvp)
        {
            var entity = new PayCategory();
            var kp = kvp.Key.Split('#');

            entity.PayType = kp[0];
            entity.Name = kp[1];
            entity.Enabled = true;
            entity.ConfigStr = kvp.Value;
            entity.IpWhiteList = "";
            entity.IsVirtualCurrency = false;
            var rt = await _repository.InsertWithCacheAsync(entity);
            if (rt != null && rt.HasValue && rt.Value > 0) return (true, "保存成功");
            return (false, "保存失败");
        }


        public async Task<(bool, string)> UpdateConfigAsync(int id, string configStr)
        {
            if (id == 0) return (false, "标识为空");
            string testJson = $"[{configStr}]";
            if (JsonHelper.IsJson(testJson)) return (false, "配置字符串不符合JSON格式");
            var entity = await _repository.GetFromCacheAsync(id);
            if (entity == null) return (false, "未找到支付类别");
            entity.ConfigStr = configStr;
            var rt = await _repository.UpdateWithCacheAsync(entity);
            if (rt > 0) return (true, "更新成功");
            return (false, "更新失败");
        }



        public async Task<(bool, string, PayCategory)> GetByIdAsync(int id)
        {
            if (id == 0) return (false, $"无此Id{id}", null);
            var rt = await _repository.GetFromCacheAsync(id);
            if (rt == null) return (false, $"未找到支付类别 Id{id}", null);
            return (true, null, rt);
        }


    }
}