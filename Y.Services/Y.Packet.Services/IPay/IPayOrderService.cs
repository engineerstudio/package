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
*│　创建时间：2020-10-16 23:12:38                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.IPay                                   
*│　接口名称： IPayOrderRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Pay;
using Y.Packet.Entities.Pay.ViewModel;

namespace Y.Packet.Services.IPay
{
    public interface IPayOrderService
    {
        Task<(IEnumerable<PayOrder>, int)> GetPageList(OrderListPageQuery q);


        /// <summary>
        /// 更新第三方订单信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="depositAmount"></param>
        /// <param name="payMerchantOrderId"></param>
        /// <returns>是否保存成功，错误信息</returns>
        Task<(bool, string, string, PayOrder)> ProcessOrderAsync(int orderId, int merchantId, int memberId, decimal depositAmount, string payMerchantOrderId = null);


        /// <summary>
        /// 创建请求订单
        /// </summary>
        /// <param name="payId"></param>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="depositAmount"></param>
        /// <param name="userName"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        Task<(bool, string, string, PayOrder)> CreateOrderAsync(int payId, int merchantId, int memberId, decimal depositAmount, string userName, string ip);

        /// <summary>
        /// 设定提交订单5-6分钟未处理的为失败状态
        /// </summary>
        /// <returns></returns>
        Task SetPayOrderExpireStatusAsync();

        Task<(bool, string)> SetPayOrderStatusAsync(int payId, PayOrderStatus status);

        Task<(bool, PayOrder)> GetAsync(int merchantId, int memeberId, int orderId);

        /// <summary>
        /// 获取订单，支付回调专用
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<(bool, string, PayOrder)> GetAsync(int orderId);


        /// <summary>
        /// [充值成功] 任务列表
        ///  获取过去两分钟内所有充值成功的订单信息
        ///  TODO 增加充值成功的缓存队列
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<PayOrder>> GetTaskListAsync();

        /// <summary>
        /// [周签到活动] 获取上周总充值金额
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<decimal> GetTaskRechargeAmountAsync(int merchantId, int userId);
    }
}