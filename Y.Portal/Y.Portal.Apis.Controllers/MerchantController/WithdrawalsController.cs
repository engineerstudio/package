using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.IApplication;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Games.ViewModels;
using Y.Packet.Entities.Pay;
using Y.Packet.Entities.Pay.ViewModel;
using Y.Packet.Entities.Promotions;
using Y.Packet.Entities.Promotions.ViewModels;
using Y.Packet.Entities.Vips;
using Y.Packet.Entities.Vips.ViewModels;
using Y.Packet.Services.IGames;
using Y.Packet.Services.IMembers;
using Y.Packet.Services.IPay;
using Y.Packet.Services.IPromotions;
using Y.Packet.Services.IVips;
using Y.Portal.Apis.Controllers.DtoModel.Merchant;
using Y.Portal.Apis.Controllers.Helper;

namespace Y.Portal.Apis.Controllers.MerchantController
{
    [Route(RouteHelper.BaseMerchantRoute)]
    [ApiController]
    public class WithdrawalsController : ControllerBase
    {
        private readonly IWithdrawMerchantService _withdrawMerchantService;
        private readonly IWithdrawOrderService _withdrawOrderService;
        private readonly IInfrastructurePayService _infrastructurePayService;
        private readonly IBaseHandlerService _baseHandler;
        private readonly IUsersBankService _usersBankService;
        private readonly IUsersService _usersService;
        private readonly IUsersFundsService _usersFundsService;
        private readonly IWithdrawalsHybridService _withdrawalsHybridService;
        public WithdrawalsController(IWithdrawMerchantService withdrawMerchantService, IWithdrawOrderService withdrawOrderService, IInfrastructurePayService infrastructurePayService, IBaseHandlerService baseHandler, IUsersBankService usersBankService, IUsersService usersService, IUsersFundsService usersFundsService, IWithdrawalsHybridService withdrawalsHybridService)
        {
            _withdrawMerchantService = withdrawMerchantService;
            _withdrawOrderService = withdrawOrderService;
            _infrastructurePayService = infrastructurePayService;
            _baseHandler = baseHandler;
            _usersBankService = usersBankService;
            _usersService = usersService;
            _usersFundsService = usersFundsService;
            _withdrawalsHybridService = withdrawalsHybridService;
        }


        [HttpPost("loadmerchants")]
        public async Task<string> WithdrawalMerchantList([FromForm] WithdrawalListPageQuery q)
        {
            q.MerchantId = _baseHandler.MerchantId;
            var rt = await _withdrawMerchantService.GetPageListAsync(q);
            return (new TableDataModel
            {
                count = rt.Item2,
                data = rt.Item1.Select(t => new
                {
                    t.Id,
                    t.Name,
                    t.Enabled,
                    EnabledStr = t.Enabled ? "已开启" : "已关闭",
                    PayCategoryId = t.TypeStr,
                    t.TypeStr,
                    t.ConfigStr,
                    t.Description
                })
            }).ToJson();
        }

        [HttpPost("paycategory")]
        public async Task<string> GetWithdrawalsCategory()
        {
            return (true, await _infrastructurePayService.GetWithdrawalsMerchantFromInterfaceAsync()).ToJsonResult();
        }

        [HttpPost("paycategoryconfig")]
        public async Task<string> GetWithdrawalsConfig([FromForm] string id)
        {
            var conf = await _infrastructurePayService.GetMerchantConfigMembersFromInterfaceAsync(id, false);
            return (true, new { ConfigStr = conf }).ToJsonResult();
        }

        [HttpPost("savemerchantconfig")]
        public async Task<string> SaveMerchantConfig([FromForm] WithdrawalsMerchantConfig d)
        {
            (bool, WithdrawMerchant) rt2 = await _withdrawMerchantService.ExistOrInsertAsync(_baseHandler.MerchantId, d.PayCategory);
            if (!rt2.Item1)
            {
                d.Id = rt2.Item2.Id;
            }
            d.MerchantId = _baseHandler.MerchantId;
            var rt = await _withdrawMerchantService.UpdateConfigAsync(d);
            return rt.ToJsonResult();
        }


        /// <summary>
        /// 出款列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("withdrawaldic")]
        public async Task<string> WithdrawalDic()
        {
            int merchantId = _baseHandler.MerchantId;
            var data = (await _withdrawMerchantService.GetDicAsync(merchantId));
            data.Item3.Add(-1, "手动出款");
            return data.ToJsonResult();
        }

        #region 订单列表操作

        /// <summary>
        /// 订单审核，修改订单状态
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost("confirmwithdrawalsorder")]
        public async Task<string> ConfirmOrderOverview([FromForm] int orderId)
        {
            int merchantId = _baseHandler.MerchantId;
            return (await _withdrawOrderService.ProcessAuditedStatusAsync(merchantId, orderId)).ToJsonResult();
        }

        /// <summary>
        /// 提交订单到第三方出款 / 手动出款修改状态
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [HttpPost("submitorder")]
        public async Task<string> ConfirmWithdrawalOrder([FromForm] ConfirmWithdrawalOrderQuery q)
        {
            int merchantId = _baseHandler.MerchantId;

            if (q.WithdrawMerchantId == -1)
            {
                // 2. 减款
                var withdrawOrder = (await _withdrawOrderService.GetAsync(q.OrderId)).Item3;
                //var withdrawDic = (await _withdrawMerchantService.GetDicAsync(merchantId)).Item3;
                var unlockfunds = await _usersFundsService.WithdrawalAsync(merchantId, q.MemberId, q.OrderId, withdrawOrder.ReqWithdrawAmount, "手动出款");
                if (unlockfunds.Item1)
                    await _withdrawOrderService.ProcessSucessStatusAsync(merchantId, q.OrderId, withdrawOrder.ReqWithdrawAmount, string.Empty);
                return (true, "请核对用户出款记录").ToJsonResult();
            }
            else
            {
                // 1. 提交到第三方的确认订单
                var orderConfim = await _withdrawalsHybridService.ConfirmOrderAsync(merchantId, q.MemberId, q.OrderId, q.WithdrawMerchantId);
                return orderConfim.ToJsonResult();
            }
        }

        /// <summary>
        /// 取消出款
        /// </summary>
        /// <returns></returns>
        [HttpPost("cannelorder")]
        public async Task<string> CannelOrder([FromForm] int orderId)
        {
            int merchantId = _baseHandler.MerchantId;
            var orderRt = await _withdrawOrderService.GetAsync(orderId);
            var order = orderRt.Item3;
            await _usersFundsService.WithdrawalUnlockAsync(order.MerchantId, order.MemberId, order.Id, order.ReqWithdrawAmount, false);
            return (await _withdrawOrderService.ProcessFaildStatusAsync(merchantId, orderId)).ToJsonResult();
        }

        #endregion



        /// <summary>
        /// 出款订单列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("withdrawalsorderload")]
        public async Task<string> WithdrawalsOrderList([FromForm] WithdrawalsPageQuery q)
        {
            q.MerchantId = _baseHandler.MerchantId;
            q.MemberId = await _usersService.GetMemberIdByAccountNameAsync(q.MerchantId, q.AccountName);

            var rt = await _withdrawOrderService.GetPageListAsync(q);
            var dic = (await _withdrawMerchantService.GetDicAsync(q.MerchantId)).Item3;
            dic.Add(0, "无"); // 考虑未处理的订单

            List<object> data  = new List<object>();
            foreach (var t in rt.Item1)
            {
                string AccountName = await _usersService.GetAccountNameByMemberIdAsync(t.MemberId);
                data.Add(new
                {
                    t.Id,
                    t.MemberId,
                    AccountName,
                    t.ReqWithdrawAmount,
                    t.WithdrawAmount,
                    WithdrawMerchantName = dic[t.WithdrawMerchantId],
                    t.WithdrawMerchantOrderId,
                    t.IsFinish,
                    t.CreateTime,
                    t.ConfirmTime,
                    Status = t.Status.ToString(),
                    StatusDes = t.Status.GetDescription(),
                    t.IP
                });
            }

        

            return (new TableDataModel
            {
                count = rt.Item2,
                data = data
            }).ToJson();

        }


        #region 出款核对信息

        /// <summary>
        /// 获取出款信息
        /// </summary>
        /// <param name="id">出款Id</param>
        /// <returns></returns>
        [HttpPost("withdrawalsorderinfo")]
        public async Task<string> WithdrawalsInfo([FromForm] int id)
        {
            bool sameBankCard = true;  // 是否与之前提现账户一致
            bool fullBetAmount = true; // 是否打满流水
            decimal totalRecharge = 1000; // 总充值
            decimal totalWithdrawals = 900; // 总提现
            decimal gameMoney = 300; // 游戏账户金额汇总
            // 获取出款信息
            var info = await _withdrawOrderService.GetAsync(id);
            if (!info.Item1) return (false, info.Item2).ToJsonResult();
            var orderInfo = info.Item3;
            // TODO 获取流水状态


            // 核对出款银行卡是否与之前一致
            var lastInfo = await _withdrawOrderService.GetLastAsync(orderInfo.MemberId);
            if (lastInfo.Item1 && lastInfo.Item3.UserBankId != info.Item3.UserBankId) sameBankCard = false;

            // 获取银行卡号
            var bank = await _usersBankService.GetAsync(orderInfo.UserBankId);
            if (!bank.Item1) return bank.ToJsonResult();

            // TODO 获取用户盈利等相关判断  总充值/总提现/用户盈亏/体现后剩余金额



            return (true, "", new
            {
                orderInfo.MemberId,
                orderInfo.UserBankId,
                orderInfo.WithdrawMerchantId,
                orderInfo.ReqWithdrawAmount,
                orderInfo.WithdrawAmount,
                orderInfo.IsFinish,
                orderInfo.CreateTime,
                orderInfo.ConfirmTime,
                orderInfo.IP,
                bank.Item3.AccountName,
                bank.Item3.AccountNo,
                SameBankCard = sameBankCard,
                FullBetAmount = fullBetAmount,
                TotalRecharge = totalRecharge,
                TotalWithdrawals = totalWithdrawals,
                GameMoney = gameMoney,
                MemberProfit = totalRecharge - totalWithdrawals - gameMoney  // 用户盈利

            }).ToJsonResult();

        }


        [HttpPost("history")]
        public async Task<string> MemberWithdrawalHistory([FromForm] int memberId)
        {
            var q = new WithdrawalsPageQuery();
            q.MemberId = memberId;
            q.IsFinished = true;
            q.MerchantId = _baseHandler.MerchantId;

            var rt = await _withdrawOrderService.GetPageListAsync(q);

            var dic = (await _withdrawMerchantService.GetDicAsync(q.MerchantId)).Item3;

            return (new TableDataModel
            {
                count = rt.Item2,
                data = rt.Item1.Select(
                  t =>
                  {
                      return new
                      {
                          t.Id,
                          t.ReqWithdrawAmount,
                          t.WithdrawAmount,
                          WithdrawMerchantName = dic[t.WithdrawMerchantId],
                          t.WithdrawMerchantOrderId,
                          StatusDes = t.Status.GetDescription(),
                          t.CreateTime,
                          ConfirmTime = t.ConfirmTime.ToDateTimeString(),
                          t.IP,
                          t.Marks
                      };
                  })
            }).ToJson();

        }



        #endregion
    }
}
