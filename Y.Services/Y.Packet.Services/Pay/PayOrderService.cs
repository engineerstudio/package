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
*│　创建时间：2020-10-16 23:12:38                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Pay                                  
*│　类    名： PayOrderService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using Dapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.Library;
using Y.Packet.Entities.Pay;
using Y.Packet.Entities.Pay.ViewModel;
using Y.Packet.Repositories.IPay;
using Y.Packet.Services.IPay;

namespace Y.Packet.Services.Pay
{
    public class PayOrderService : IPayOrderService
    {
        private readonly IPayOrderRepository _repository;
        private readonly IPayMerchantRepository _payMerchantRepository;
        private readonly IPaymentLibraryService _payFactory;

        public PayOrderService(IPayOrderRepository repository, IPayMerchantRepository payMerchantRepository, IPaymentLibraryService payFactory)
        {
            _repository = repository;
            _payMerchantRepository = payMerchantRepository;
            _payFactory = payFactory;
        }

        public async Task<(IEnumerable<PayOrder>, int)> GetPageList(OrderListPageQuery q)
        {
            var parm = new DynamicParameters();
            string conditions = $"WHERE 1=1 AND MerchantId={q.MerchantId} ";
            if (!string.IsNullOrEmpty(q.AccountName))
                conditions += $" AND MemberId={0} ";
            if (q.PayMerchantId != 0)
                conditions += $" AND PayMerchantId={q.PayMerchantId} ";
            if (q.DepositStartTime != null)
                conditions += $" AND CreateTime Between '{q.DepositStartTime}' AND '{q.DepositEndTime}' ";
            if (q.FinishStartTime != null)
                conditions += $" AND ConfirmTime Between '{q.FinishStartTime}' AND '{q.FinishEndTime}' ";

            return (await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", null), _repository.RecordCount(conditions));
        }

        public async Task<(bool, string, string, PayOrder)> ProcessOrderAsync(int orderId, int merchantId, int memberId, decimal depositAmount, string payMerchantOrderId = null)
        {

            var order = await _repository.GetAsync(orderId);
            if (order == null) return (false, "订单不存在", payMerchantOrderId, null);


            if (order.MerchantId != merchantId || order.MemberId != memberId)
                return (false, "错误", payMerchantOrderId, null);

            order.PayMerchantOrderId = string.IsNullOrEmpty(payMerchantOrderId) ? RandomHelper.GetString(32) : payMerchantOrderId;
            order.DepositAmount = depositAmount == 0 ? order.ReqDepositAmount : depositAmount;
            order.IsFinish = true;
            order.Status = Y.Infrastructure.Library.Core.YEntity.PayOrderStatus.Sucess;
            order.ConfirmTime = DateTime.UtcNow.AddHours(8);

            var rt = await _repository.UpdateAsync(order);

            if (rt > 0) return (true, "更新成功", payMerchantOrderId, order);
            return (false, "更新失败", payMerchantOrderId, null);

        }

        /// <summary>
        /// 仅仅创建请求订单，不增加用户金额与日志金额
        /// </summary>
        /// <param name="payId"></param>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="depositAmount"></param>
        /// <returns></returns>
        public async Task<(bool, string, string, PayOrder)> CreateOrderAsync(int payId, int merchantId, int memberId, decimal depositAmount, string userName, string ip)
        {
            if (merchantId == 0 || memberId == 0) return (false, "请登录", "", null);
            if (depositAmount <= 0) return (false, "金额错误", "", null);
            var paymerchant = await _payMerchantRepository.GetFromCacheAsync(payId);
            if (paymerchant == null) return (false, "请选择支付", "", null);
            // 验证是否是其它站点胡乱提交
            if (paymerchant.MerchantId != merchantId) return (false, "请选择支付", "", null);

            // 2. 是否存在未完成订单
            string existUnfinishedOrder = $" WHERE MerchantId={merchantId} AND MemberId={memberId} AND IsFinish=0";
            int count = await _repository.RecordCountAsync(existUnfinishedOrder);
            if (count > 0) return (false, "存在未完成订单,请联系客服进行处理", "", null);

            // 1. 验证
            if (!string.IsNullOrEmpty(paymerchant.Validation.FixedRange))
            {
                var l = paymerchant.Validation.FixedRange.Split(",").ToList().ConvertAll<decimal>(delegate (string x) { return Convert.ToDecimal(x); });
                if (!l.Contains(depositAmount)) return (false, "金额错误", "", null);

            }
            else if (paymerchant.Validation.Price_Max != 0 && paymerchant.Validation.Price_Min != 0)
            {
                if (depositAmount < paymerchant.Validation.Price_Min || depositAmount > paymerchant.Validation.Price_Max)
                    return (false, $"金额区间{paymerchant.Validation.Price_Min}-{paymerchant.Validation.Price_Max}", "", null);
            }


            // 1.0 写入日志
            var payorder = new PayOrder()
            {
                MerchantId = merchantId,
                MemberId = memberId,
                PayMerchantId = payId,
                ReqDepositAmount = depositAmount,
                DepositAmount = 0,
                IsFinish = false,
                Status = Y.Infrastructure.Library.Core.YEntity.PayOrderStatus.None,
                CreateTime = DateTime.UtcNow.AddHours(8),
                ConfirmTime = DefaultString.DefaultDateTime,
                IP = ip,
                PayMerchantOrderId = ""
            };

            var rt_order = await _repository.InsertByTransAsync(payorder);
            if (rt_order == null || rt_order.Value < 1) return (false, "保存订单失败", "", null);
            payorder.Id = rt_order.Value;
            // 2. 请求
            Dictionary<string, string> dic = new Dictionary<string, string>();
            JObject configJson = JObject.Parse(paymerchant.ConfigStr);
            foreach (var c in configJson)
            {
                dic.Add(c.Key, c.Value.ToString());
            }

            // 增加IP,用户名,金额
            dic.Add("Money", depositAmount.ToString());
            dic.Add("MemberName", userName);
            dic.Add("Ip", ip);

            // TODO 创建订单请求
            //IDepositMerchant onebook = _payFactory.CreateInstance(paymerchant.PayCategory, dic);

            //var rt = onebook.ProcessRequest();

            return (true, "请求成功", "rt.ToJson()", payorder);
        }


        /// <summary>
        /// 设定充值订单过期，从而可以进行继续下单付款
        /// </summary>
        /// <returns></returns>
        public async Task SetPayOrderExpireStatusAsync()
        {
            //var parm = new DynamicParameters();
            string conditions = $"WHERE  IsFinish = 0  AND CreateTime<=  N'{DateTime.UtcNow.AddHours(8).AddMinutes(-5)}' "; // CreateTime >= N'{DateTime.UtcNow.AddHours(8).AddMinutes(-6)}'  AND
            var d = await _repository.GetListAsync(conditions);
            foreach (var da in d)
            {
                da.Status =  PayOrderStatus.Failed;
                await _repository.UpdateAsync(da);
            }
        }


        public async Task<(bool, string)> SetPayOrderStatusAsync(int payId,PayOrderStatus status)
        {
            if (payId == 0) return (false,"Id错误");
            var pay = await _repository.GetAsync(payId);
            pay.Status = status;
            var rt =  await _repository.UpdateAsync(pay);
            if (rt > 0) return (true,"更新成功");
            return (false,"更新失败");
        }

        public async Task<(bool, PayOrder)> GetAsync(int merchantId, int memeberId, int orderId)
        {
            string where = $" SELECT * FROM PayOrder WHERE MerchantId={merchantId} AND MemberId={memeberId} AND Id={orderId}";
            var rt = await _repository.GetAsync(where);
            if (rt == null) return (false, null);
            return (true, rt);
        }

        public async Task<(bool, string, PayOrder)> GetAsync(int orderId)
        {
            if (orderId == 0) return (false, "订单ID异常", null);
            var rt = await _repository.GetAsync(orderId);
            if (rt == null) return (false, "数据不存在", null);
            return (true, "", rt);
        }


        /// <summary>
        /// [充值成功] 任务列表
        ///  获取过去两分钟内所有充值成功的订单信息
        ///  TODO 增加充值成功的缓存队列
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PayOrder>> GetTaskListAsync()
        {
            return await _repository.GetTaskListAsync();
        }


        /// <summary>
        /// [周签到活动] 获取上周总充值金额
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<decimal> GetTaskRechargeAmountAsync(int merchantId, int userId)
        {
            return await _repository.GetTaskRechargeAmountAsync(merchantId, userId);
        }


    }
}