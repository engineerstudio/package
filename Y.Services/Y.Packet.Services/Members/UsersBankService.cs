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
*│　描    述：用户银行卡绑定                                                    
*│　作    者：Aaron                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-08-30 15:00:55                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Members                                  
*│　类    名： UsersBankService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Helper;
using Y.Packet.Entities.Members;
using Y.Packet.Repositories.IMembers;
using Y.Packet.Services.IMembers;

namespace Y.Packet.Services.Members
{
    public class UsersBankService : IUsersBankService
    {
        private readonly IUsersBankRepository _repository;

        public UsersBankService(IUsersBankRepository repository)
        {
            _repository = repository;
        }

        public async Task<(bool, string, UsersBank)> GetAsync(int id)
        {
            if (id == 0) return (false, "Id不存在", null);
            var rt = await _repository.GetFromCacheAsync(id);
            if (rt == null) return (false, "银行卡数据不存在", null);
            return (true, "查询银行卡成功", rt);
        }


        public async Task<IEnumerable<UsersBank>> GetListAsync(int merchantId, int memberId)
        {
            //string conditions = $"WHERE MerchantId={merchantId} AND UserId={memberId}";
            //return await _repository.GetListAsync(conditions);
            return await _repository.GetUsersBanksAsync(merchantId, memberId);
        }


        public async Task<(IEnumerable<UsersBank>, int)> GetPageListAsync(int merchantId, int memberId)
        {
            //string conditions = $"WHERE MerchantId={merchantId} AND UserId={memberId}";
            //return (await _repository.GetListPagedAsync(1, 20, conditions, "Id desc", null), await _repository.RecordCountAsync(conditions));

            var list = await _repository.GetUsersBanksAsync(merchantId, memberId);
            return (list, list.Count());
        }


        public async Task<(bool, string)> InsertAsync(int merchantId, int userId, string accountName, string accountNumber)
        {
            if (merchantId == 0 || userId == 0) return (false, "请登录后操作");

            if (!BankHelper.ValidateCardNoByLuhn(accountNumber)) return (false, "银行卡错误");

            var count = await _repository.RecordCountAsync($"WHERE UserId={userId} AND Deleted=0");
            if (count > 1) return (false, "只能绑定两张银行卡");

            var userBank = new UsersBank()
            {
                MerchantId = merchantId,
                UserId = userId,
                AccountName = accountName,
                AccountNo = accountNumber,
                BankName = "中国建设银行", // TODO 根据银行卡获取银行名字
                CreateTime = DateTime.UtcNow.AddHours(8),
                Enabled = true,
                Deleted = false
            };

            var rt = await _repository.InsertWithCacheAsync(userBank);
            if (rt != null && rt.Value > 0) return (true, "保存成功");
            return (false, "保存失败");
        }



        /// <summary>
        /// [绑定银行卡活动]获取过去8分钟内绑定银行卡的会员
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UsersBank>> GetTaskListAsync()
        {
            return await _repository.GetTaskListAsync();
        }



    }
}