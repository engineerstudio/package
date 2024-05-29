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
*│　描    述：用户资金                                                    
*│　作    者：Aaron                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-08-30 15:00:55                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Users.IRepository                                   
*│　接口名称： IUsersFundsRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Packet.Entities.Members;

namespace Y.Packet.Repositories.IMembers
{
    public interface IUsersFundsRepository : IBaseRepository<UsersFunds, Int32>
    {

        /// <summary>
        /// 获取用户余额  TotalFunds
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<decimal> GetUserFundsAsync(int merchantId, int userId);

        /// <summary>
        /// 获取用户可用余额  TotalFunds-LockFunds
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<decimal> GetUserAvailableFundsAsync(int merchantId, int userId);

        /// <summary>
        /// 获取锁定金额 LockedAmount
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<decimal> GetUserLockFundsAsync(int merchantId, int userId);

        /// <summary>
        /// 更新 UsersFunds ， 插入 UsersFundsLog
        /// </summary>
        /// <param name="uFunds"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        Task<bool> UpdateUserFundsAsync(UsersFunds uFunds, UsersFundsLog log);

        Task<UsersFunds> GetAsync(int merchantId, int memberId);
        Task<int?> InsertWithCacheAsync(UsersFunds usersFunds);
        Task MigrateSqlDbToRedisDbAsync();
    }
}