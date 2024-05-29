using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.IApplication;
using Y.Infrastructure.Library;
using Y.Packet.Entities.Pay;
using Y.Packet.Entities.Pay.ViewModel;
using Y.Packet.Repositories.IPay;
using Y.Packet.Services.IGames;
using Y.Packet.Services.IMembers;
using Y.Packet.Services.IMerchants;
using Y.Packet.Services.IPay;
using Y.Packet.Services.IVips;

namespace Y.Infrastructure.Application
{
    public class PaymentHybridService : IPaymentHybridService
    {
        private readonly IPayTypeCategoryService _payTypeCategoryService;
        private readonly IPayMerchantService _payMerchantService;
        private readonly IPayMerchantRepository _payMerchantRepository;
        private readonly IPaymentLibraryService _fac;
        private readonly IPayCategoryService _payCategoryService;
        private readonly IPayOrderService _payOrderService;
        private readonly IUsersFundsService _usersFundsService;
        private readonly IPayOrderRepository _payOrderRepository;
        private readonly IInfrastructurePayService _infrastructurePayService;
        private readonly IDomiansService _domiansService;
        private readonly IHybridTaskService _hybridTaskService;
        private readonly IVipGroupsService _vipGroupsService;
        private readonly IWashOrderService _washOrderService;
        private readonly IGameMerchantService _gameMerchantService;
        private readonly IGameUsersService _gameUsersService;
        public PaymentHybridService(IPaymentLibraryService withdrawals, IPayMerchantRepository payMerchantRepository, IPayCategoryService payCategoryService, IPayOrderService payOrderService, IUsersFundsService usersFundsService, IPayOrderRepository repository, IInfrastructurePayService infrastructurePayService, IDomiansService domiansService, IHybridTaskService hybridTaskService, IPayTypeCategoryService payTypeCategoryService, IPayMerchantService payMerchantService, IVipGroupsService vipGroupsService, IWashOrderService washOrderService, IGameMerchantService gameMerchantService, IGameUsersService gameUsersService)
        {
            _fac = withdrawals;
            _payMerchantRepository = payMerchantRepository;
            _payCategoryService = payCategoryService;
            _payOrderService = payOrderService;
            _usersFundsService = usersFundsService;
            _payOrderRepository = repository;
            _infrastructurePayService = infrastructurePayService;
            _domiansService = domiansService;
            _hybridTaskService = hybridTaskService;
            _payTypeCategoryService = payTypeCategoryService;
            _payMerchantService = payMerchantService;
            _vipGroupsService = vipGroupsService;
            _washOrderService = washOrderService;
            _gameMerchantService = gameMerchantService;
            _gameUsersService = gameUsersService;
        }


        /// <summary>
        /// 支付创建订单 并 提交到第三方支付机构，返回可操作的支付页面
        /// </summary>
        /// <param name="payId"></param>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="depositAmount"></param>
        /// <param name="userName"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public async Task<(bool Sucess, string Error, string Url)> CreateOrderAsync(int payId, int merchantId, int memberId, decimal depositAmount, string userName, string ip)
        {

            if (merchantId == 0 || memberId == 0) return (false, "请登录", string.Empty);
            if (depositAmount <= 0) return (false, "金额错误", string.Empty);
            var paymerchant = await _payMerchantRepository.GetFromCacheAsync(payId);
            if (paymerchant == null) return (false, "请选择支付", string.Empty);
            // 验证是否是其它站点胡乱提交
            if (paymerchant.MerchantId != merchantId) return (false, "请选择支付", string.Empty);

            // 2. 是否存在未完成订单
            string existUnfinishedOrder = $" WHERE MerchantId={merchantId} AND MemberId={memberId} AND IsFinish=0 AND Status!={PayOrderStatus.Failed.GetEnumValue()}";
            int count = await _payOrderRepository.RecordCountAsync(existUnfinishedOrder);
            if (count > 0) return (false, "存在未完成订单,请联系客服进行处理", string.Empty);

            // 1. 验证
            if (!string.IsNullOrEmpty(paymerchant.Validation.FixedRange))
            {
                var l = paymerchant.Validation.FixedRange.Split(",").ToList().ConvertAll<decimal>(delegate (string x) { return Convert.ToDecimal(x); });
                if (!l.Contains(depositAmount)) return (false, "金额错误", string.Empty);

            }
            else if (paymerchant.Validation.Price_Max != 0 && paymerchant.Validation.Price_Min != 0)
            {
                if (depositAmount < paymerchant.Validation.Price_Min || depositAmount > paymerchant.Validation.Price_Max)
                    return (false, $"金额区间{paymerchant.Validation.Price_Min}-{paymerchant.Validation.Price_Max} ", string.Empty);
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
                Status = PayOrderStatus.None,
                CreateTime = DateTime.UtcNow.AddHours(8),
                ConfirmTime = DefaultString.DefaultDateTime,
                IP = ip,
                PayMerchantOrderId = ""
            };

            var rt_order = await _payOrderRepository.InsertByTransAsync(payorder);
            if (rt_order == null || rt_order.Value < 1) return (false, "保存订单失败", string.Empty);
            payorder.Id = rt_order.Value;

            // 2. 请求

            var attrs = _infrastructurePayService.GetMerchantMembersAndAttrFromInterface(paymerchant.PayCategory, true);
            // 3. 获取地址里面配置的回调地址
            string callbackUrl = $"{await _domiansService.GetCallbackUrlAsync(merchantId)}/apis/api/callback/deposit/{payId}";

            //Console.WriteLine(payorder.Id);

            Dictionary<string, string> dicValus = new Dictionary<string, string>();
            dicValus.Add("OrderId", payorder.Id.ToString());
            dicValus.Add("Amount", depositAmount.ToString());
            dicValus.Add("IP", "127.0.0.1");
            //dicValus.Add("BankName", bankRt.Item3.BankName);
            //dicValus.Add("BankAccount", bankRt.Item3.AccountNo);
            //dicValus.Add("BankAccountName", bankRt.Item3.AccountName);
            dicValus.Add("CallbackUrl", callbackUrl);
            //Console.WriteLine(paymerchant.ConfigStr);
            Dictionary<string, string> configs = _infrastructurePayService.ToWithdrawalsConfig(paymerchant.ConfigStr, attrs, dicValus);


            //Console.WriteLine(configs.ToJson());
            // 3. 初始化当前的配置
            //IDepositMerchant pay = _fac.CreateInstance(paymerchant.PayCategory, configs);

            // 4. 请求数据
            var rt = _fac.CreateRequestData(paymerchant.PayCategory,configs);  // 支付的请求返回两种状态   Faild 与 Sucess， 其中ResponseStr 是需要前台页面解析的内容

            // 5. TODO 创建请求日志
            if (!rt.RequstSucess) return (false, "通讯异常", string.Empty);

            if (rt.Status == PayOrderStatus.Failed) return (false, rt.Msg, string.Empty);

            // 回调成功,那么更改订单状态
            var order = await _payOrderRepository.GetAsync(rt_order.Value);
            order.Status = PayOrderStatus.Processing;
            order.PayMerchantOrderId = rt.SysId;
            await _payOrderRepository.UpdateAsync(order);

            return (true, string.Empty, rt.ResponseStr);

        }


        /// <summary>
        /// 处理回调信息, 成功则自动上分，失败则不做处理
        /// </summary>
        /// <param name="payId"></param>
        /// <param name="callbackDic"></param>
        /// <returns></returns>
        public async Task<(bool, string)> ProcessingCallbackAsync(int payId, Dictionary<string, string> callbackDic)
        {
            var payChannel = await _payMerchantRepository.GetFromCacheAsync(payId);
            if (payChannel == null) return (false, "未找到渠道");

            var payCate = await _payCategoryService.GetByIdAsync(payChannel.PayCategoryId);
            if (!payCate.Item1) return (false, payCate.Item2);

            //IDepositMerchant pay = _fac.CreateInstance(payCate.Item3.PayType, new Dictionary<string, string>());

            //Console.WriteLine($"callbackdata: {callbackDic.ToJson()}");

            var rt = _fac.ProcessCallback(payCate.Item3.PayType, payChannel.ConfigStr.JsonToDictionary(), callbackDic);

            // TODO 增加回调日志
            if (!rt.RequstSucess) return (false, "数据请求失败");

            var orderRt = await _payOrderService.GetAsync(rt.OrderId);
            var order = orderRt.Item3;
            if (order.Status != PayOrderStatus.Processing) return (false, "订单状态错误");
            if (order.IsFinish || order.Status == PayOrderStatus.Sucess || order.Status == PayOrderStatus.Failed) return (false, "订单已经入款");

            // 状态为未处理,已审核,处理中 不做改变
            switch (rt.Status)
            {
                case PayOrderStatus.None:
                case PayOrderStatus.Audited:
                case PayOrderStatus.Processing:
                    break;
                case PayOrderStatus.Sucess: // 入款成功 增加金额
                    // 更改订单的状态
                    await _payOrderService.ProcessOrderAsync(order.Id, order.MerchantId, order.MemberId, order.DepositAmount);

                    await _usersFundsService.RechargeAsync(order.MerchantId, order.MemberId, order.Id, order.ReqDepositAmount, payChannel.Name);
                    // 入款成功后回调
                    await _hybridTaskService.VipsUpgradeTask(order.MerchantId, order.MemberId);
                    // 入款成功后增加打码量
                    {
                        //充值时候是否有余额，如果有余额，那么不重新计算打码
                        // 1.1 用户是否有余额
                        //var money = await _usersFundsService.GetUserFundsAsync(order.MemberId);
                        // 1.2 用户游戏是否存在余额

                        //如果没有余额（并且没有未结算的订单），那么重新计算打码
                        // 2.1 是否存在未结算订单

                        //如果有未结算的订单，那么不重新计算打码量



                        await _washOrderService.InsertAsync(order.MemberId, FundLogType.Recharge, order.ReqDepositAmount, order.ReqDepositAmount, "入款成功,增加洗码");

                    }
                    break;
                case PayOrderStatus.Failed:  // TODO 回调失败  那么更新该订单状态
                    break;
            }
            return (true, rt.ResponseStr);
        }



        /// <summary>
        /// 支付页面列表数据,包含支付类别，支付类别详细内容
        /// </summary>
        /// <returns></returns>
        public async Task<(bool, string, dynamic)> GetPayListAsync(int merchantId, int vipId)
        {
            if (merchantId == 0 || vipId == 0) return (false, "请先登陆", null);

            // 获取站点的支付类别及类别下的所有支付渠道然后返回给前端
            // TODO 如果用户未登录  那么返回错误
            var payTypes = await _payTypeCategoryService.GetListAsync(merchantId);
            var vipPayGroup = await _vipGroupsService.GetPaySettingAsync(merchantId);
            string payMerchantIds = vipPayGroup.Where(t => t.Key == vipId).Single().Value;

            var payMerchant = await _payMerchantService.GetListAsync(merchantId, payMerchantIds);

            var tt = payTypes.Select(t => new PayInfoViewModel()
            {
                Name = t.Name,
                Url = t.PicUrl,
                IsVC = t.IsVirtualCurrency ? 1 : 0,
                Merchants = payMerchant.Where(p => p.PayTypeId == t.Id).Select(p => new PayInfoSubMerchant
                {
                    Id = p.Id,
                    Name = p.Name,
                    Img = p.Img,
                    Rule = p.ValidationStr,
                    Desc = p.Desc
                }).ToList()
            });
            return (true, "", tt);
        }






    }
}
