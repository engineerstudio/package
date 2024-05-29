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
*│　命名空间： Y.Users.IServices                                   
*│　接口名称： IUsersFundsRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Members;

namespace Y.Packet.Services.IMembers
{

    public interface IUsersFundsService
    {

        Task<UsersFunds> GetAsync(int merchantId, int userId);
        Task<decimal> GetUserAvailableFundsAsync(int merchantId, int userId);
        Task<decimal> GetUserFundsAsync(int merchantId, int userId);
        Task<decimal> GetUserLockFundsAsync(int merchantId, int userId);

        Task<(bool, string)> TransInGameAsync(int merchantId, int memberId, int transferLogId, GameType gameType, FundLogType_Games subType, decimal amount, decimal lockAmount, bool sucess);
        Task<(bool, string)> TransOutGameAsync(int merchantId, int memberId, int transferLogId, GameType gameType, FundLogType_Games subType, decimal amount, decimal lockAmount, bool sucess);


        Task<(bool, string)> PromoRewardAsync(int merchantId, int memberId, int orderId, decimal amount, string mark, FundLogType_Promotions type);

        /// <summary>
        /// 取款资金解锁
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="orderId"></param>
        /// <param name="amount"></param>
        /// <param name="sucess">是否出款成功：true->是，false->否,仅解锁</param>
        /// <returns></returns>
        Task<(bool, string)> WithdrawalUnlockAsync(int merchantId, int memberId, int orderId, decimal amount, bool sucess);

        /// <summary>
        /// 取款资金锁定. 用户请求创建出款订单后锁定用户资金
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="orderId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        Task<(bool, string)> WithdrawalLockAsync(int merchantId, int memberId, int orderId, decimal amount);

        /// <summary>
        /// 取款  
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        Task<(bool, string)> WithdrawalAsync(int merchantId, int memberId, int orderId, decimal amount, string withDrawMerchantName);

        /// <summary>
        /// 加款
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="orderId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        Task<(bool, string)> RechargeAsync(int merchantId, int memberId, int orderId, decimal amount, string payMerchantName);


        /// <summary>
        /// 手动加款
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        Task<(bool, string)> AddManualAddFundsAsync(FundLogType type, byte subType, string subTypeDesc, int merchantId, int memberId, decimal amount, string sourceId = null, string marks = null);

        /// <summary>
        /// 手动减款
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        Task<(bool, string)> AddManualMinusFundsAsync(FundLogType type, byte subType, string subTypeDesc, int merchantId, int memberId, decimal amount, string sourceId = null, string marks = null);


        /// <summary>
        /// 获取资金类型的子类型 
        /// </summary>
        /// <param name="typeStr"></param>
        /// <param name="typeValue"></param>
        /// <returns></returns>
        (byte, string, string) GetAttributeSubType(string typeStr, string typeValue);

    }
}