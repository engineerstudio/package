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
*│　创建时间：2020-10-25 10:39:54                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Pay                                  
*│　类    名： WithdrawOrderService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using Dapper;
using System;
using System.Collections.Generic;
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
    public class WithdrawOrderService : IWithdrawOrderService
    {
        private readonly IPaymentLibraryService _fac;
        private readonly IWithdrawOrderRepository _repository;
        private readonly IWithdrawMerchantRepository _withdrawMerchantRepository;
        private readonly IInfrastructurePayService _infrastructurePayService;

        public WithdrawOrderService(IPaymentLibraryService withdrawals, IWithdrawOrderRepository repository, IWithdrawMerchantRepository withdrawMerchantRepository, IInfrastructurePayService infrastructurePayService)
        {
            _fac = withdrawals;
            _repository = repository;
            _withdrawMerchantRepository = withdrawMerchantRepository;
            _infrastructurePayService = infrastructurePayService;
        }

        public async Task<(bool, string, int)> CreateOrderAsync(int merchantId, int memberId, decimal amount, int bankId, string userName, string ip)
        {
            if (merchantId == 0 || memberId == 0) return (false, "用户未登录", 0);
            if (amount <= 0) return (false, "取款金额错误", 0);

            var m = new WithdrawOrder()
            {
                MemberId = memberId,
                ReqWithdrawAmount = amount,
                WithdrawAmount = amount,
                MerchantId = merchantId,
                IsFinish = false,
                CreateTime = DateTime.UtcNow.AddHours(8),
                ConfirmTime = DefaultString.DefaultDateTime,
                IP = ip,
                Status = PayOrderStatus.None,
                UserBankId = bankId,
                Marks = "",
                WithdrawMerchantOrderId = ""
            };

            var rt = await _repository.InsertAsync(m);


            if (rt == null || rt.Value == 0) return (false, "申请提现失败", 0);


            return (true, "申请提现成功", rt.Value);
        }

        public async Task<(bool, string, WithdrawOrder)> GetAsync(int id)
        {
            if (id == 0) return (false, "Id不存在", null);
            var m = await _repository.GetAsync(id);
            if (m == null) return (false, "数据不存在", null);
            return (true, "", m);
        }


        public async Task<(bool, string, WithdrawOrder)> GetAsync(int merchantId, int orderId)
        {
            if (merchantId == 0 || orderId == 0) return (false, "商户/订单号错误", null);
            var m = await _repository.GetAsync(merchantId, orderId);
            if (m == null) return (false, "订单不存在", null);
            return (true, null, m);
        }

        /// <summary>
        /// 转换为 已审核 状态
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<(bool, string)> ProcessAuditedStatusAsync(int merchantId, int orderId)
        {
            var rt = await GetAsync(merchantId, orderId);
            if (!rt.Item1) return (rt.Item1, rt.Item2);
            if (rt.Item3.Status != PayOrderStatus.None) return (false, "订单状态异常");

            rt.Item3.IsFinish = false;
            rt.Item3.ConfirmTime = DateTime.UtcNow.AddHours(8);
            rt.Item3.Status = PayOrderStatus.Audited;
            if (await _repository.UpdateAsync(rt.Item3) == 0) return (false, "审核状态失败");
            return (true, "审核保存成功");
        }

        /// <summary>
        /// 转换为 失败 状态
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<(bool, string)> ProcessFaildStatusAsync(int merchantId, int orderId)
        {
            var rt = await GetAsync(merchantId, orderId);
            if (!rt.Item1) return (rt.Item1, rt.Item2);
            rt.Item3.Status = PayOrderStatus.Failed;
            rt.Item3.Marks = "订单取消";

            rt.Item3.IsFinish = true;
            rt.Item3.ConfirmTime = DateTime.UtcNow.AddHours(8);
            if (await _repository.UpdateAsync(rt.Item3) == 0) return (false, "取消状态失败");
            return (true, "取消订单保存成功");
        }

        /// <summary>
        /// 转换为 成功 状态
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<(bool, string)> ProcessSucessStatusAsync(int merchantId, int orderId, decimal amount, string WithdrawMerchantOrderId)
        {
            var rt = await GetAsync(merchantId, orderId);
            if (!rt.Item1) return (rt.Item1, rt.Item2);
            if (rt.Item3.Status != PayOrderStatus.Audited && rt.Item3.Status != PayOrderStatus.Processing) return (false, "订单状态异常");

            rt.Item3.Status = PayOrderStatus.Sucess;
            if (string.IsNullOrEmpty(WithdrawMerchantOrderId)) WithdrawMerchantOrderId = "手动出款";
            rt.Item3.WithdrawMerchantOrderId = WithdrawMerchantOrderId;
            rt.Item3.IsFinish = true;
            rt.Item3.ConfirmTime = DateTime.UtcNow.AddHours(8);
            if (await _repository.UpdateAsync(rt.Item3) == 0) return (false, "审核状态失败");
            return (true, "审核保存成功");
        }

        /// <summary>
        /// 获取用户的上一条支付记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<(bool, string, WithdrawOrder)> GetLastAsync(int userId)
        {
            if (userId == 0) return (false, "Id不存在", null);
            var m = await _repository.GetFirstFindishedOrderAsync(userId);
            if (m == null) return (false, "暂无订单", null);
            return (true, "", m);
        }

        public async Task<(IEnumerable<WithdrawOrder>, int)> GetPageListAsync(WithdrawalsPageQuery q)
        {
            var parms = new DynamicParameters();
            string conditions = $"WHERE 1=1 AND MerchantId = {q.MerchantId} ";
            if (q.Id != 0)
                conditions += $" AND Id = {q.Id} ";
            if (!string.IsNullOrEmpty(q.WithdrawMerchantOrderId))
            {
                conditions += " AND WithdrawMerchantOrderId = @WithdrawMerchantOrderId";
                parms.Add("WithdrawMerchantOrderId", q.WithdrawMerchantOrderId);
            }

            if (!string.IsNullOrEmpty(q.AccountName) && q.MemberId != 0)
                conditions += $" AND MemberId = {q.MemberId} ";
            if (q.WithdrawStartTime != null)
                conditions += $" AND CreateTime Between '{q.WithdrawStartTime}' AND '{q.WithdrawEndTime}' ";
            if (q.FinishStartTime != null)
                conditions += $" AND ConfirmTime Between '{q.FinishStartTime}' AND '{q.FinishEndTime}' ";

            if (q.IsFinished != null)
                conditions += $" AND IsFinish = {(q.IsFinished.Value ? 1 : 0)}";


            return (await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", parms), _repository.RecordCount(conditions, parms));
        }




        /// <summary>
        /// 用户是否存在未完成的订单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>存在:true</returns>
        public async Task<bool> ExistNotfinishedOrderAsync(int userId)
        {
            string conditions = $"WHERE MemberId={userId} AND IsFinish=0";
            var count = await _repository.RecordCountAsync(conditions);
            return count > 0;
        }
    }
}