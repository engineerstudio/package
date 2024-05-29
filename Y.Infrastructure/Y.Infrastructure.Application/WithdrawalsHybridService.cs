using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.IApplication;
using Y.Infrastructure.Library;
using Y.Packet.Repositories.IPay;
using Y.Packet.Repositories.IVips;
using Y.Packet.Services.IMembers;
using Y.Packet.Services.IMerchants;
using Y.Packet.Services.IPay;
using Y.Packet.Services.IVips;


namespace Y.Infrastructure.Application
{
    public class WithdrawalsHybridService : IWithdrawalsHybridService
    {

        private readonly IWithdrawalLibraryService _fac;
        private readonly IWithdrawOrderRepository _repository;
        private readonly IWithdrawMerchantRepository _withdrawMerchantRepository;
        private readonly IInfrastructurePayService _infrastructurePayService;
        private readonly IUsersBankService _usersBankService;
        private readonly IDomiansService _domiansService;
        private readonly IWithdrawOrderService _withdrawOrderService;
        private readonly IUsersFundsService _usersFundsService;
        private readonly IWashOrderRepository _washOrderRepository;
        private readonly IWashOrderService _washOrderService;

        public WithdrawalsHybridService(IWithdrawalLibraryService withdrawals, IWithdrawOrderRepository repository, IWithdrawMerchantRepository withdrawMerchantRepository, IInfrastructurePayService infrastructurePayService, IUsersBankService usersBankService, IDomiansService domiansService, IWithdrawOrderService withdrawOrderService, IUsersFundsService usersFundsService, IWashOrderRepository washOrderRepository, IWashOrderService washOrderService)
        {
            _fac = withdrawals;
            _repository = repository;
            _withdrawMerchantRepository = withdrawMerchantRepository;
            _infrastructurePayService = infrastructurePayService;
            _usersBankService = usersBankService;
            _domiansService = domiansService;
            _withdrawOrderService = withdrawOrderService;
            _usersFundsService = usersFundsService;
            _washOrderRepository = washOrderRepository;
            _washOrderService = washOrderService;
        }

        /// <summary>
        /// 确认提交订单出款
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="orderId"></param>
        /// <param name="withdrawalsMerchantId"></param>
        /// <returns></returns>
        public async Task<(bool, string)> ConfirmOrderAsync(int merchantId, int memberId, int orderId, int withdrawalsMerchantId)
        {
            if (merchantId == 0 || memberId == 0 || orderId == 0 || withdrawalsMerchantId == 0) return (false, "数据不存在");

            // 0. 用户是否有足够金额出款
            //var memberAvailableFunds = _usersFundsService.GetUserAvailableFunds(memberId);

            // 1.判断订单信息是否存在
            var order = await _repository.GetAsync(orderId);

            if (order == null) return (false, "数据不存在");

            if (order.MemberId != memberId || order.MerchantId != merchantId) return (false, "数据不存在");

            if (order.Status != PayOrderStatus.Audited) return (false, "订单状态异常");

            // 2. 判断出款渠道信息是否存在
            var channel = await _withdrawMerchantRepository.GetByIdAsync(merchantId, withdrawalsMerchantId); // 获取出款接口信息

            if (channel == null) return (false, "出款接口不存在");

            // 3. 判断银行卡信息是否存在
            var bankRt = await _usersBankService.GetAsync(order.UserBankId);
            if (!bankRt.Item1) return (false, "银行卡错误");

            // 4. 出款渠道
            order.WithdrawMerchantId = withdrawalsMerchantId;

            if (channel.IsDefault) // 手动出款
            {
                // 修改订单状态
                order.IsFinish = true;
                order.Status = PayOrderStatus.Sucess;
                order.ConfirmTime = DateTime.UtcNow.AddHours(8);

            }
            else // 代付渠道出款
            {
                // 1. 获取到当前的渠道的配置
                string conf = channel.ConfigStr;
                // 2. 获取到聚到的所有配置
                var attrs = _infrastructurePayService.GetMerchantMembersAndAttrFromInterface(channel.TypeStr, false);
                // 3. 获取地址里面配置的回调地址
                string callbackUrl = $"{ await _domiansService.GetCallbackUrlAsync(merchantId)}/apis/api/callback/withdrawals/{withdrawalsMerchantId}";

                Dictionary<string, string> dicValus = new Dictionary<string, string>();
                dicValus.Add("OrderId", orderId.ToString());
                dicValus.Add("Amount", order.ReqWithdrawAmount.ToString());
                dicValus.Add("IP", "127.0.0.1");
                dicValus.Add("BankName", bankRt.Item3.BankName);
                dicValus.Add("BankAccount", bankRt.Item3.AccountNo);
                dicValus.Add("BankAccountName", bankRt.Item3.AccountName);
                dicValus.Add("CallbackUrl", callbackUrl);

                Dictionary<string, string> configs = _infrastructurePayService.ToWithdrawalsConfig(channel.ConfigStr, attrs, dicValus);


                //Console.WriteLine(configs.ToJson());
                // 3. 初始化当前的配置
                //IWithdrawalMerchant pay = _fac.CreateInstance(channel.TypeStr, configs);

                // 4.请求数据
                var rt = _fac.CreateRequestData(channel.TypeStr, configs);
                //Console.WriteLine(rt.ToJson());
                // 5. 修改状态
                if (rt.RequstSucess)
                    order.Status = PayOrderStatus.Processing;
                else
                    order.Status = PayOrderStatus.Audited;


                order.WithdrawMerchantOrderId = rt.WithdrawalsMerchantOrderId.IsNullOrEmpty() ? "" : rt.WithdrawalsMerchantOrderId;
            }

            await _repository.UpdateAsync(order);

            return (true, "操作成功");

        }



        /// <summary>
        /// 处理第三方回调信息
        /// </summary>
        /// <param name="payId"></param>
        /// <param name="callbackDic"></param>
        /// <returns>string：第三方返回字段</returns>
        public async Task<(bool, string)> ProcessingCallbackAsync(int payId, Dictionary<string, string> callbackDic)
        {

            var payChannel = await _withdrawMerchantRepository.GetAsync(payId);
            if (payChannel == null) return (false, "未找到出款渠道");

            //payChannel.ConfigStr
            //IWithdrawalMerchant pay = _fac.CreateInstance(payChannel.TypeStr, new Dictionary<string, string>());

            //Console.WriteLine(callbackDic.ToJson());
            var rt = _fac.ProcessCallback(payChannel.TypeStr, payChannel.ConfigStr.JsonToDictionary(), callbackDic);

            // TODO 增加回调日志

            if (!rt.RequstSucess) return (false, "数据请求失败");

            var orderRt = await _withdrawOrderService.GetAsync(rt.OrderId);
            var order = orderRt.Item3;
            
            //Console.WriteLine(order.ToJson());
            //   ！Importmant 防止重复回调     判断订单是否已经解锁了  |   根据返回数据获取订单ID，获取订单信息，判断订单状态 
            if (order.Status != PayOrderStatus.Processing || order.IsFinish) return (false, "订单状态异常");


            // 状态为未处理,已审核,处理中 不做改变
            switch (rt.Status)
            {
                case PayOrderStatus.None:
                case PayOrderStatus.Audited:
                case PayOrderStatus.Processing:
                    break;
                case PayOrderStatus.Sucess: // 减去总资金， 减去锁定资金，增加日志记录
                    await _usersFundsService.WithdrawalUnlockAsync(order.MerchantId, order.MemberId, order.Id, order.ReqWithdrawAmount, true);
                    // 修改状态
                    await _withdrawOrderService.ProcessSucessStatusAsync(order.MerchantId, order.Id, order.ReqWithdrawAmount, rt.WithdrawalsMerchantOrderId);
                    break;
                case PayOrderStatus.Failed:
                    await _usersFundsService.WithdrawalUnlockAsync(order.MerchantId, order.MemberId, order.Id, order.ReqWithdrawAmount, false);
                    break;
            }
            return (true, rt.ResponseStr);
        }



        /// <summary>
        /// 提交出款前，获取的用户信息核对
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public async Task<(bool, dynamic)> GetCheckInfoAsync(int merchantId, int memberId)
        {
            var userAviableMoney = await _usersFundsService.GetUserAvailableFundsAsync(merchantId,merchantId);
            var bankList = await _usersBankService.GetListAsync(merchantId, memberId);
            return (true, new
            {
                Amount = userAviableMoney,
                memberId,
                Bank = bankList.Select(t => new
                {
                    Info = $"{t.AccountName}<{t.AccountNo}>",
                    Id = t.Id
                })
            });
        }


        /// <summary>
        /// 创建出款订单
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="money"></param>
        /// <param name="bankId"></param>
        /// <param name="spswd"></param>
        /// <param name="memberName"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public async Task<(bool, string)> CreateWithdrawalsOrder(int merchantId, int memberId, decimal money, int bankId, string spswd, string memberName, string ip)
        {

            if (money < 100) return (false, "最低提现额度为100元");

            // 0. 判断银行账号是否是该用户的
            var bankRt = await _usersBankService.GetAsync(bankId);
            if (!bankRt.Item1) return (bankRt.Item1, bankRt.Item2);

            // 0.0.1 判断打码量是否足够
            var exist = await _washOrderRepository.IsFinishedAllWashOrderAsync(memberId);
            if (exist)
            {
                var unfinishedAmount = await _washOrderRepository.GetUnfinishedAmountAsync(merchantId, memberId);
                return (false,$"当前未完成打码量为{unfinishedAmount},请继续下单哦");
            }


            // 0.1 判断用户是否有足够的提现金额
            var userAviableMoney =  await _usersFundsService.GetUserAvailableFundsAsync(merchantId, memberId);
            if (money < 0 || userAviableMoney < money) return (false, "金额不足");

            // 0.2 判断用户是否已经存在未处理完成的提现
            var existNotfinishedOrder = await _withdrawOrderService.ExistNotfinishedOrderAsync(memberId);
            if (existNotfinishedOrder) return (false, "存在未处理的出款订单,请联系客服处理后再提出款单");

            // 1. 创建提现订单
            var rt = await _withdrawOrderService.CreateOrderAsync(merchantId, memberId, money, bankId, memberName, ip);

            // 3. 写入锁定数据
            if (rt.Item1)
                await _usersFundsService.WithdrawalLockAsync(merchantId, memberId, rt.Item3, money);
            
            return (rt.Item1, rt.Item2);
        }

    }
}
