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
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-10-25 10:39:54                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.IPay                                   
*│　接口名称： IWithdrawOrderRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Packet.Entities.Pay;
using Y.Packet.Entities.Pay.ViewModel;

namespace Y.Packet.Services.IPay
{
    public interface IWithdrawOrderService
    {
        /// <summary>
        /// 用户创建提现申请订单，创建成功后锁定资金
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="amount"></param>
        /// <param name="bankId"></param>
        /// <param name="userName"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        Task<(bool, string, int)> CreateOrderAsync(int merchantId, int memberId, decimal amount, int bankId, string userName, string ip);

        Task<(IEnumerable<WithdrawOrder>, int)> GetPageListAsync(WithdrawalsPageQuery q);

        /// <summary>
        /// 根据记录ID查询用户出款记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<(bool, string, WithdrawOrder)> GetAsync(int id);

        /// <summary>
        /// 获取上一条用户出款记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<(bool, string, WithdrawOrder)> GetLastAsync(int userId);


        /// <summary>
        /// 用户是否存在未完成的订单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> ExistNotfinishedOrderAsync(int userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<(bool, string, WithdrawOrder)> GetAsync(int merchantId, int orderId);

        /// <summary>
        /// 修改订单状态为 已审核
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<(bool, string)> ProcessAuditedStatusAsync(int merchantId, int orderId);

        /// <summary>
        /// 修改订单状态为 失败
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<(bool, string)> ProcessFaildStatusAsync(int merchantId, int orderId);

        /// <summary>
        /// 修改订单状态为 成功
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="orderId"></param>
        /// <param name="amount"></param>
        /// <param name="WithdrawMerchantOrderId"></param>
        /// <returns></returns>
        Task<(bool, string)> ProcessSucessStatusAsync(int merchantId, int orderId, decimal amount, string WithdrawMerchantOrderId);
    }
}