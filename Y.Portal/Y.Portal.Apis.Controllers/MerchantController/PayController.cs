using Microsoft.AspNetCore.Mvc;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.IApplication;
using Y.Packet.Entities.Pay.ViewModel;
using Y.Packet.Services.IMembers;
using Y.Packet.Services.IPay;
using Y.Packet.Services.IVips;
using Y.Portal.Apis.Controllers.Helper;

namespace Y.Portal.Apis.Controllers.MerchantController
{
    [Route(RouteHelper.BaseMerchantRoute)]
    [ApiController]
    public class PayController : ControllerBase
    {

        private readonly IPayCategoryService _payCategoryService;
        private readonly IPayMerchantService _payMerchantService;
        private readonly IPayTypeCategoryService _payTypeCategoryService;
        private readonly IPayOrderService _payOrderService;
        private readonly IWithdrawOrderService _withdrawOrderService;
        private readonly IWithdrawMerchantService _withdrawMerchantService;
        private readonly IUsersFundsLogService _usersFundsLogService;
        private readonly IUsersBankService _usersBankService;
        private readonly IUsersService _usersService;
        private readonly IUsersFundsService _usersFundsService;
        private readonly IHybridTaskService _hybridTaskService;
        private readonly IBaseHandlerService _baseHandlerService;
        private readonly IWashOrderService _washOrderService;
        public PayController(IPayCategoryService payCategoryService, IPayMerchantService payService, IPayTypeCategoryService payTypeCategoryService, IPayOrderService payOrderService, IUsersFundsLogService usersFundsLogService, IWithdrawOrderService withdrawOrderService, IUsersBankService usersBankService, IWithdrawMerchantService withdrawMerchantService, IUsersService usersService, IUsersFundsService usersFundsService, IHybridTaskService hybridTaskService, IBaseHandlerService baseHandlerService, IWashOrderService washOrderService)
        {
            _payCategoryService = payCategoryService;
            _payMerchantService = payService;
            _payTypeCategoryService = payTypeCategoryService;
            _payOrderService = payOrderService;
            _usersFundsLogService = usersFundsLogService;
            _withdrawOrderService = withdrawOrderService;
            _usersBankService = usersBankService;
            _withdrawMerchantService = withdrawMerchantService;
            _usersService = usersService;
            _usersFundsService = usersFundsService;
            _hybridTaskService = hybridTaskService;
            _baseHandlerService = baseHandlerService;
            _washOrderService = washOrderService;
        }

        #region PayTypeCategory


        [HttpPost("loadPayType")]
        public async Task<string> PayTypeList()
        {
            // TODO 
            int merchantId = _baseHandlerService.MerchantId;
            var rt = await _payTypeCategoryService.GetPageListAsync(merchantId);
            if (rt.Item2 == 0)
            {
                await _payTypeCategoryService.CreatePayTypeCategoryAsync(merchantId);
                rt = await _payTypeCategoryService.GetPageListAsync(merchantId);
            }
            return (new TableDataModel
            {
                count = rt.Item2,
                data = rt.Item1.Select(t => new
                {
                    t.Id,
                    t.Name,
                    t.PicUrl,
                    t.Enabled,
                    t.IsVirtualCurrency
                })
            }).ToJson();
        }

        [HttpPost("paytypecatedic")]
        public async Task<string> PayTypeCategoryDic()
        {
            int merchantId = _baseHandlerService.MerchantId;
            var rt = await _payTypeCategoryService.GetPayTypeDicAsync(merchantId);
            return (true, rt.ToJson()).ToJsonResult();
        }


        [HttpPost("paytypelist")]
        public async Task<string> GetPayTypeList()
        {
            int merchantId = _baseHandlerService.MerchantId;
            var rt = await _payTypeCategoryService.GetListAsync(merchantId);
            if (rt.Count() == 0) return (false, "请先开启支付类别").ToJsonResult();
            var ojb = rt.Select(t => new
            {
                t.Id,
                t.MerchantId,
                t.Name,
                t.PicUrl,
                t.Enabled,
            });
            return (true, "", ojb).ToJsonResult();
        }

        /// <summary>
        /// 更新支付类别状态
        /// </summary>
        /// <returns></returns>
        [HttpPost("updatecatetorytypestatus")]
        public async Task<string> UpdatePayTypeStatus([FromForm] int id, [FromForm] bool enabled)
        {
            int merchantId = _baseHandlerService.MerchantId;
            return (await _payTypeCategoryService.UpdateStatusAsync(merchantId, id, enabled)).ToJsonResult();
        }


        /// <summary>
        /// 保存支付类型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpPost("savepaytypecat")]
        public async Task<string> SavePayTypeCategory([FromForm] int id, [FromForm] string name, [FromForm] string url, [FromForm] bool isVC)
        {
            return (await _payTypeCategoryService.SavePayTypeCategoryAsync(_baseHandlerService.MerchantId, id, name, url,isVC)).ToJsonResult();
        }


        #endregion



        #region PayMerchant

        /// <summary>
        /// 支付商户列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("paymerchantdic")]
        public async Task<string> GetPayMerchantDic()
        {
            int merchantId = _baseHandlerService.MerchantId;
            var rt = await _payMerchantService.GetPayMerchantIdAndNameDicAsync(merchantId);

            return (true, rt.Item1.ToJson()).ToJsonResult();
        }



        #endregion



        #region 支付类别 PayCategory


        [HttpPost("load")]
        public async Task<string> PayList([FromForm] PayListPageQuery q)
        {
            int merchantId = _baseHandlerService.MerchantId;
            q.MerchantId = merchantId;
            var rt = await _payMerchantService.GetPageListAsync(q);
            var dicPayType = await _payTypeCategoryService.GetPayTypeDicAsync(merchantId);
            return (new TableDataModel
            {
                count = rt.Item2,
                data = rt.Item1.Select(t => new
                {
                    t.Id,
                    t.PayCategory,
                    t.PayCategoryId,
                    PayTypeName = dicPayType[t.PayTypeId],
                    PayTypeId = t.PayTypeId,
                    t.Name,
                    t.Enabled,
                    t.ConfigStr,
                    t.ValidationStr
                })
            }).ToJson();
        }


        /// <summary>
        /// 同步支付类别列表
        /// </summary>
        /// <returns></returns>  
        [HttpGet("paycategory")]
        public async Task<string> GetPayCategoryAsync()
        {
            var rt = (await _payCategoryService.GetCategoryDicAsync());
            return (true, "", rt).ToJsonResult();
        }

        /// <summary>
        /// 获取支付类别的配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("paycategoryconfig")]
        public async Task<string> GetPayCategoryConfig([FromQuery] int id)
        {
            var rt = await _payCategoryService.GetByIdAsync(id);

            return rt.ToJsonResult();
        }



        [HttpPost("savemerchantconfig")]
        public async Task<string> SavePayMerchantConfig([FromForm] PayMerchantConfig d)
        {
            d.MerchantId = _baseHandlerService.MerchantId;

            (bool, string) rt;
            if (d.Id == 0)
                rt = await _payMerchantService.SavePayAsync(d);
            else
                rt = await _payMerchantService.UpdatePayAsync(d);

            return rt.ToJsonResult();
        }


        #endregion




        #region 支付订单

        /// <summary>
        /// 收款订单列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [HttpPost("orderload")]
        public async Task<string> OrderList([FromForm] OrderListPageQuery q)
        {
            int merchantId = _baseHandlerService.MerchantId;
            q.MerchantId = merchantId;
            var rt = await _payOrderService.GetPageList(q);
            var paymerchantDicRt = await _payMerchantService.GetPayMerchantIdAndNameDicAsync(merchantId);
            var payMerchantDic = paymerchantDicRt.Item1; // 支付渠道字典
            var payChannelTypeDic = paymerchantDicRt.Item2; // 支付类别字典
            var payTypeCategoryDic = await _payTypeCategoryService.GetPayTypeDicAsync(merchantId);

            Dictionary<int,string> dic = new Dictionary<int,string>();
            foreach (var order in rt.Item1)
            {
                string userName = await _usersService.GetAccountNameByMemberIdAsync(order.MemberId);
                dic.TryAdd(order.MemberId, userName);
            }

            List<object> data = new List<object>();
            foreach (var t in rt.Item1)
            {
                int payTypeCategoryId = payChannelTypeDic.GetByKey(t.PayMerchantId);
                string AccountName = dic.GetByKey(t.MemberId);
                data.Add(new
                {
                    t.Id,
                    t.MerchantId,
                    t.MemberId,
                    AccountName,
                    t.PayMerchantId,
                    PayMerchantName = payMerchantDic.GetByKey(t.PayMerchantId),
                    PayTypeCategory = payTypeCategoryDic.GetByKey(payTypeCategoryId),
                    t.PayMerchantOrderId,
                    t.ReqDepositAmount,
                    t.DepositAmount,
                    t.IsFinish,
                    StatusStr = t.IsFinish ? "是" : "否",
                    t.CreateTime,
                    t.ConfirmTime,
                    t.IP,
                    t.Status,
                    StatusDesc = t.Status.GetDescription()
                });
            }

            return (new TableDataModel
            {
                count = rt.Item2,
                data = data
            }).ToJson();
        }


        /// <summary>
        ///  手动确认支付订单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpPost("confirmpayorder")]
        public async Task<string> ConfirmOrder([FromForm] int id, [FromForm] int memberId)
        {

            // todo  手动入款增加打码量计算

            int merchantId = _baseHandlerService.MerchantId;
            decimal deposit = 0;
            // 处理订单状态,找出sourceId
            var rt = await _payOrderService.ProcessOrderAsync(id, merchantId, memberId, deposit);

            if (!rt.Item1)
                return (rt.Item1, rt.Item2).ToJsonResult();
            // 兼容deposit等于0
            if (deposit == 0)
            {
                var order = rt.Item4;
                deposit = order.DepositAmount;
            }

            // 获取支付名称
            string payName = await _payMerchantService.GetPayNameByIdAsync(rt.Item4.PayMerchantId);

            // 充值成功后的回调
            // TODO 确认订单这个功能会删除 ， 代付会增加
            var rt2 = await _usersFundsService.RechargeAsync(merchantId, memberId, id, deposit, $"{payName}(手动上分)");


            // 重置成功后的任务
            if (rt2.Item1)
            {
                await _hybridTaskService.VipsUpgradeTask(merchantId, memberId);
                // 入款成功后增加打码量
                await _washOrderService.InsertAsync(memberId, FundLogType.Recharge, deposit, deposit, "入款成功,增加洗码");
            }
            return (rt2.Item1, rt2.Item2).ToJsonResult();

        }


        /// <summary>
        /// 取消订单
        /// </summary>
        /// <returns></returns>
        [HttpPost("cannelpayorders")]
        public async Task<string> CannelOrder([FromForm] int id)
        {
            return (await _payOrderService.SetPayOrderStatusAsync(id, PayOrderStatus.Failed)).ToJsonResult();
        }



        #endregion



        #region 出款订单  withdrawal

        ///// <summary>
        ///// 出款订单列表
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost("withdrawalsorderload")]
        //public async Task<string> WithdrawalsOrderList([FromForm] WithdrawalsPageQuery q)
        //{
        //    q.MerchantId = 1000;
        //    q.MemberId = _usersService.GetMemberIdByAccountName(q.MemberId, q.AccountName);

        //    var rt = await _withdrawOrderService.GetPageList(q);

        //    return (new TableDataModel
        //    {
        //        count = rt.Item2,
        //        data = rt.Item1.Select(
        //          t =>
        //          {
        //              string AccountName = _usersService.GetAccountNameByMemberId(t.MemberId);
        //              return new
        //              {
        //                  t.Id,
        //                  t.MemberId,
        //                  AccountName,
        //                  t.ReqWithdrawAmount,
        //                  t.WithdrawAmount,
        //                  t.WithdrawMerchantId,
        //                  t.WithdrawMerchantOrderId,
        //                  t.IsFinish,
        //                  t.CreateTime,
        //                  t.ConfirmTime,
        //                  t.IP
        //              };
        //          })
        //    }).ToJson();

        //}


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


        #endregion


        #region 手动加减款



        [HttpPost("manualfunds")]
        public async Task<string> ManualAddFunds([FromForm] ManualFunds funds)
        {
            if (funds.Name.IsEqualNull()) return (false, "请输入操作用户名").ToJsonResult();

            var userId = await _usersService.GetMemberIdByAccountNameAsync(_baseHandlerService.MerchantId, funds.Name);

            int merchantId = _baseHandlerService.MerchantId;
            // 1. 获取用户是否存在

            if (funds.Money <= 0) return (false, "金额必须大于零").ToJsonResult();
            FundLogType? type = funds.FundsType.ToEnum<FundLogType>();
            if (type == null) return (false, "不存在的加减款类型").ToJsonResult();
            var transType = funds.FundsTransType.ToEnum<TransType>();
            if (transType == null) return (false, "不存在的账变类型").ToJsonResult();

            var subType = _usersFundsService.GetAttributeSubType($"FundLogType_{funds.FundsType}", funds.FundsSubType);

            //var subType = transType.Value == Members.Entity.TransType.In ? (byte)Members.Entity.FundLogType_Recharge.Manual : (byte)Members.Entity.FundLogType_WithDraw.Manual;
            //var descrpt = transType.Value == Members.Entity.TransType.In ? Members.Entity.FundLogType_Recharge.Manual.GetDescription() : Members.Entity.FundLogType_WithDraw.Manual.GetDescription();

            (bool, string) rt = (false, "失败");
            switch (transType.Value)
            {
                case TransType.In:
                    rt = await _usersFundsService.AddManualAddFundsAsync(type.Value, subType.Item1, subType.Item3, merchantId, userId, funds.Money);
                    if (rt.Item1)
                    {
                        await _hybridTaskService.VipsUpgradeTask(merchantId, userId);
                        await _washOrderService.InsertAsync(userId, FundLogType.Recharge, funds.Money, funds.Money, FundLogType_Recharge.Manual.ToString());
                    }
                    break;
                case TransType.Out:
                    rt = await _usersFundsService.AddManualMinusFundsAsync(type.Value, subType.Item1, subType.Item3, merchantId, userId, funds.Money);
                    break;
                default:
                    break;
            }
            return rt.ToJsonResult();
        }




        #endregion




    }
}