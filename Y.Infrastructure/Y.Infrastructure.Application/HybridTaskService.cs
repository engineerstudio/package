using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.LogsCoreController.IService;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.IApplication;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Members;
using Y.Packet.Entities.Promotions;
using Y.Packet.Entities.Vips;
using Y.Packet.Repositories.IGames;
using Y.Packet.Repositories.IMembers;
using Y.Packet.Repositories.IPay;
using Y.Packet.Repositories.IPromotions;
using Y.Packet.Repositories.IVips;
using Y.Packet.Services.IGames;
using Y.Packet.Services.IMembers;
using Y.Packet.Services.IMerchants;
using Y.Packet.Services.IPay;
using Y.Packet.Services.IPromotions;
using Y.Packet.Services.IVips;
using static Y.Packet.Entities.Promotions.PromotionsConfig;

namespace Y.Infrastructure.Application
{
    public class HybridTaskService : IHybridTaskService
    {
        private readonly IServiceProvider _serviceProvider;
        public HybridTaskService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        /// <summary>
        /// 创建代理日报表
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task CreateAgentDailyStatisticAsync(DateTime date)
        {
            string dateStr = date.ToDateString();
            // 1. 获取所有站点
            IUsersRepository usersRepository = (IUsersRepository)_serviceProvider.GetRequiredService(typeof(IUsersRepository));
            var dic = await usersRepository.GetAgentIdAndMerchantIdDicAsync();
            // 2. 获取代理的数据
            IUserHierarchyRepository usersHierarchyRepository = (IUserHierarchyRepository)_serviceProvider.GetRequiredService(typeof(IUserHierarchyRepository));
            Dictionary<KeyValuePair<int, int>, IEnumerable<int>> dd = new Dictionary<KeyValuePair<int, int>, IEnumerable<int>>();
            foreach (var d in dic)
            {
                var subIds = await usersHierarchyRepository.GetSubMemberIdsFromCacheAsync(d.Value, d.Key);
                dd.Add(d, subIds);
            }
            // 3. 遍历代理
            IGameLogsRepository gameLogsRepository = (IGameLogsRepository)_serviceProvider.GetRequiredService(typeof(IGameLogsRepository));
            IPayOrderRepository payOrderRepository = (IPayOrderRepository)_serviceProvider.GetRequiredService(typeof(IPayOrderRepository));
            IWithdrawOrderRepository withdrawOrderRepository = (IWithdrawOrderRepository)_serviceProvider.GetRequiredService(typeof(IWithdrawOrderRepository));
            IActivityOrdersDetailsRepository activityOrdersDetailsRepository = (IActivityOrdersDetailsRepository)_serviceProvider.GetRequiredService(typeof(IActivityOrdersDetailsRepository));
            List<AgentDailyReportStatistic> ll = new List<AgentDailyReportStatistic>();

            foreach (var d in dd)
            {
                if (d.Value == null || d.Value.Count() == 0) continue;
                // 3.1获取代理下游戏日报表统计数据
                var gameData = await gameLogsRepository.CreateDailyAgentDataAsync(d.Key.Value, dateStr, d.Value);
                // 3.2 获取代理下充值日报表统计数据
                var payData = await payOrderRepository.CreateDailyAgentDataAsync(dateStr, d.Value);
                var withdrawalsData = await withdrawOrderRepository.CreateDailyAgentDataAsync(dateStr, d.Value);
                // 3.3 获取代理下活动统计的数据，返回相关类别统计的json
                if (activityOrdersDetailsRepository == null) throw new Exception("activityOrdersDetailsRepository null");
                var promoData = await activityOrdersDetailsRepository.CreateDailyAgentDataAsync(dateStr, d.Value);

                ll.Add(ToAgentReportModel(date, d.Key.Key, d.Key.Value, payData.Item2, payData.Item1, withdrawalsData.Item2, withdrawalsData.Item1, gameData["BetAmount"].ToString().ToDecimal().Value, gameData["ValidBet"].ToString().ToDecimal().Value, gameData["Money"].ToString().ToDecimal().Value, promoData.Item2));
            }
            // 4. 遍历上述数据，插入表
            IAgentDailyReportStatisticRepository agentRepository = (IAgentDailyReportStatisticRepository)_serviceProvider.GetRequiredService(typeof(IAgentDailyReportStatisticRepository));
            foreach (var l in ll)
                await agentRepository.InsertAsync(l);

        }

        private AgentDailyReportStatistic ToAgentReportModel(DateTime date, int merchantId, int memeberId, decimal pay, int payNo, decimal withdrawals, int withdrawalsNo, decimal gamebetAmount, decimal gameValidBet, decimal gameMoney, decimal promoMoney, string promoJson = null)
        {
            return new AgentDailyReportStatistic()
            {
                Date = date,
                MerchantId = merchantId,
                MemberId = memeberId,
                Pay = pay,
                PayNo = payNo,
                Withdrawals = withdrawals,
                WithdrawalsNo = withdrawalsNo,
                GameBetAmount = gamebetAmount,
                GameValidBet = gameValidBet,
                GameLoss = gameMoney,
                PromoMoney = promoMoney
            };
        }


        #region 活动任务



        /// <summary>
        /// [会员返水] 思路如下. 先生成一条发布日期的信息到ActivityOrders, 然后把详细的会员生成信息放在ActivityOrdersDetails. 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<string> ExecRebatePromoAsync(DateTime date, int merchantId)
        {
            IActivityOrdersService _activityOrdersService = (IActivityOrdersService)_serviceProvider.GetRequiredService(typeof(IActivityOrdersService));
            IPromotionsConfigService _promotionsConfigService = (IPromotionsConfigService)_serviceProvider.GetRequiredService(typeof(IPromotionsConfigService));
            IActivityOrdersDetailsService _activityOrdersDetailsService = (IActivityOrdersDetailsService)_serviceProvider.GetRequiredService(typeof(IActivityOrdersDetailsService));
            IGameUsersDailyReportStatisticService _gameUsersDailyReportStatisticService = (IGameUsersDailyReportStatisticService)_serviceProvider.GetRequiredService(typeof(IGameUsersDailyReportStatisticService));
            IReqLogsService _reqLogsService = (IReqLogsService)_serviceProvider.GetService(typeof(IReqLogsService));

            DateTime dt = DateTime.UtcNow.AddHours(8);
            string rebate_date = DateTime.UtcNow.AddHours(8).Date.ToString("yyyy-MM-dd");
            // 0. 不能生成当日返水
            if (date.Date == DateTime.UtcNow.AddHours(8).Date) return (false, "不能生成当日返水").ToJsonResult();

            string rebateDateSourceId = $"rebate.{dt.AddDays(-1).ToDateTimeString4()}";
            // 1. 该日期返水是否存在
            var exist = await _activityOrdersService.ExistSourceIdAsync(rebateDateSourceId);
            if (exist) return (false, $"{rebateDateSourceId}返水已存在").ToJsonResult();

            // 2. 查看返水开启的活动
            var proList = await _promotionsConfigService.GetListAsync(ActivityType.Rebate, merchantId);
            if (proList.count == 0) return (false, "请开启/配置返水").ToJsonResult();

            // 3. 获取站点该日期内的所有有效投注 , 从用户游戏日报表独取
            var rt = await _gameUsersDailyReportStatisticService.GetByMerchantAsync(merchantId, date);
            if (rt.Item2 == 0) return (false, "没有需要生成的返水记录").ToJsonResult();

            // 4. 开始准备生成返水
            List<GameUsersDailyReportStatistic.RebateTempModel> gameRebate = new List<GameUsersDailyReportStatistic.RebateTempModel>();
            Dictionary<string, int> dic_config = null;
            Dictionary<PromotionsConfig.RebateConfig, List<GameUsersDailyReportStatistic.RebateTempModel>> rebateCol = new Dictionary<RebateConfig, List<GameUsersDailyReportStatistic.RebateTempModel>>();
            IGameUsersDailyReportStatisticRepository gameUsersDailyReportStatisticRepository = (IGameUsersDailyReportStatisticRepository)_serviceProvider.GetRequiredService(typeof(IGameUsersDailyReportStatisticRepository));
            var userValidateDataDic = await gameUsersDailyReportStatisticRepository.GetRebateGradeDataAsync(merchantId, date, date);

            foreach (var pro in proList.promotions)
            {
                var pro_config = (PromotionsConfig.RebateConfig)pro.BaseConfig;
                if (pro_config.EnabledConfigStr == "GameConfig")
                {
                    // 5. 会员组/游戏返点 进行判断
                    dic_config = JsonConvert.DeserializeObject<Dictionary<string, int>>(pro_config.GameConfig);
                    GameType type;
                    foreach (var d in dic_config)
                    {
                        type = d.Key.ToEnum<GameType>().Value;
                        var dic_rr = rt.Item1.Where(t => t.GameTypeStr == type.ToString()).Select(t => new GameUsersDailyReportStatistic.RebateTempModel
                        {
                            UserId = t.MemberId,
                            Type = t.GameTypeStr.ToEnum<GameType>().Value,
                            Reward = t.ValidBet * (d.Value / 100),
                            MerchantId = merchantId,
                            PromotionId = pro.Id,
                            SourceId = $"{t.MerchantId}:{t.MemberId}:{t.GameTypeStr.ToString()}:{rebate_date}"
                        });
                        gameRebate.AddRange(dic_rr);
                    }
                    rebateCol.Add(pro_config, gameRebate);
                }
                else
                {
                    // 6. 负盈利多少进行判断
                    var pro_grade_rule_config = JsonConvert.DeserializeObject<List<GradeRuleBase>>(pro_config.RuleConfig);
                    GradeRuleBase grb = null;
                    foreach (var ud in userValidateDataDic)
                    {
                        // 获取用户该有效投注的配置
                        foreach (var p in pro_grade_rule_config)
                        {
                            if (ud.Value > p.Min && ud.Value <= p.Max)
                            {
                                grb = p; break;
                            }
                        }
                        var tmp = new GameUsersDailyReportStatistic.RebateTempModel()
                        {
                            UserId = ud.Key,
                            Type = GameType.Lottery_FNH, //GameType.Finance_FNH,
                            Reward = ud.Value * grb.Percentage,
                            MerchantId = merchantId,
                            PromotionId = pro.Id,
                            SourceId = $"{merchantId}:{ud.Key}:None:{rebate_date}"
                        };
                        gameRebate.Add(tmp);
                    }
                    rebateCol.Add(pro_config, gameRebate);
                }
            }
            // 根据返水点位生成对应的用户返水金额，写入优惠活动表，优惠活动详细表,用户账变记录表,用户增加余额

            // 5. 判断是否存在已经生成的返水日记录

            // 6. 写入返水记录
            var activityOrders = new ActivityOrders()
            {
                MerchantId = merchantId,
                UserId = -1,
                PromotionId = proList.promotions.First().Id,
                AType = ActivityType.Rebate,
                Reward = 0,
                Status = ActivityOrders.ActivityOrderStatus.None,
                Ip = "127.0.0.1",
                Description = $"{rebate_date} 返水发放",
                CreateTime = dt,
                RewardTime = DefaultString.DefaultDateTime,
                CreateDate = rebate_date,
                SourceId = rebateDateSourceId
            };

            var rt_order = await _activityOrdersService.InsertAsync(activityOrders);
            if (!rt_order.Item1) return (rt_order.Item1, rt_order.Item2).ToJsonResult();
            int orderId = rt_order.Item3;

            List<string> error_log = new List<string>();
            IUsersFundsService usersFundsService = (IUsersFundsService)_serviceProvider.GetRequiredService(typeof(IUsersFundsService));
            IWashOrderService washOrderService = (IWashOrderService)_serviceProvider.GetRequiredService(typeof(IWashOrderService));
            ActivityOrders.ActivityOrderStatus status = ActivityOrders.ActivityOrderStatus.None;
            foreach (var d in rebateCol)
            {
                foreach (var gr in d.Value)
                {
                    // 是否自动返水发放
                    if (d.Key.BonusType == BonusType.Auto)
                        status = ActivityOrders.ActivityOrderStatus.Ok;

                    var result = await _activityOrdersDetailsService.InsertAsync(orderId, gr.MerchantId, gr.UserId, gr.PromotionId, ActivityType.Rebate, gr.Reward, gr.SourceId, rebate_date, dt, status);
                    if (!result.sucess) error_log.Add(result.msg);

                    if (d.Key.BonusType == BonusType.Auto && result.sucess)
                    {
                        // 写入用户账变
                        var fundResult = await usersFundsService.PromoRewardAsync(merchantId, gr.UserId, result.activityOrderId, gr.Reward, $"奖金发放:{ActivityType.Rebate.GetDescription()}", FundLogType_Promotions.Rebate);
                        if (fundResult.Item1)// return $"用户{gr.UserId},{ActivityType.Rebate.GetDescription()}奖金发放成功";
                        {
                            // 出款要求流水
                            decimal wash = CalcWash(d.Key.Wash, gr.Reward, d.Key.WashValue);
                            await washOrderService.InsertAsync(gr.UserId, FundLogType.Promotions, gr.Reward, wash, $"[{ActivityType.WeeklyCheckIn.GetDescription()}] 活动奖金");
                        }
                    }
                }
            }


            // 7. 写入用户记录 (暂未实现，用户手动审核返水发放)

            // 8. 写入日志表


            return (true, "返水日志已生成").ToJsonResult();
        }


        /// <summary>
        /// [绑定银行卡活动]
        /// </summary>
        /// <returns></returns>
        public async Task<string> ExecBankCardPromo()
        {
            // 1. 获取开启活动的站点
            IPromotionsConfigService promotionsConfigService = (IPromotionsConfigService)_serviceProvider.GetRequiredService(typeof(IPromotionsConfigService));
            var promos = await promotionsConfigService.GetTaskListAsync(ActivityType.BankCard);
            if (promos.Count() == 0) return "未有展开开启该活动";
            var ids = promos.Select(t => t.MerchantId).ToList();
            // 2. 获取过去8分钟内绑定银行卡的会员 
            // TODO 以后可把绑定银行卡的会员放进缓存
            IUsersBankService usersBankService = (IUsersBankService)_serviceProvider.GetRequiredService(typeof(IUsersBankService));
            var cards = await usersBankService.GetTaskListAsync();
            if (cards.Count() == 0) return "不存在[绑定银行卡活动]的订单";
            var activityObjectDic = cards.Where(t => ids.Contains(t.MerchantId)).ToDictionary(t => t.MerchantId, t => t.UserId);

            IActivityOrdersService activityOrdersService = (IActivityOrdersService)_serviceProvider.GetRequiredService(typeof(IActivityOrdersService));
            foreach (var it in activityObjectDic)
            {
                // 1. 判断该用户是否已经参加过活动 根据活动订单进行判断
                bool exist = await activityOrdersService.ExistOrdersAsync(it.Key, it.Value, ActivityType.BankCard);

                // 2. 按照条件进行活动发放
                if (exist) return "已参与该活动";
                var pro = promos.Where(t => t.MerchantId == it.Key).Single();
                decimal bonus = pro.BaseConfig.BonusCalTypeValue;
                await CreateActivityFunds(it.Key, it.Value, pro.Id, bonus, $"奖金发放:{ActivityType.BankCard.GetDescription()}", ActivityType.BankCard, FundLogType_Promotions.BankCark, $"{it.Value}|{ActivityType.BankCard.ToString()}");
            }
            return string.Empty;
        }


        public Task<string> ExecAgentRebatePromoAsync()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// [代理返佣]   每个月/每周 生辰过的转账金额 固定
        /// 1. 每周一执行
        /// 2. 每个月1号执行
        /// </summary>
        /// <returns></returns>
        public async Task<string> ExecAgentRebatePromoAsync(DateTime date, int merchantId)
        {
            IActivityOrdersService _activityOrdersService = (IActivityOrdersService)_serviceProvider.GetRequiredService(typeof(IActivityOrdersService));
            IPromotionsConfigService _promotionsConfigService = (IPromotionsConfigService)_serviceProvider.GetRequiredService(typeof(IPromotionsConfigService));
            IActivityOrdersDetailsService _activityOrdersDetailsService = (IActivityOrdersDetailsService)_serviceProvider.GetRequiredService(typeof(IActivityOrdersDetailsService));
            DateTime dt = DateTime.UtcNow.AddHours(8);
            string rebate_date = DateTime.UtcNow.AddHours(8).Date.ToString("yyyy-MM-dd");
            // 0. 不能生成当日返水
            if (date.Date == DateTime.UtcNow.AddHours(8).Date) return (false, "不能生成当日返水").ToJsonResult();


            // 1. 获取该代理的负盈利

            // 2. 负盈利 * 返佣点位

            // 3. 日志生成




            return string.Empty;
        }

        /// <summary>
        /// [代理契约]  每个月/每周 生成的转账金额  阶梯式
        /// </summary>
        /// <returns></returns>
        public async Task<string> ExecAgentContractRebatePromo()
        {
            // 1. 获取所有站点
            IUsersRepository usersRepository = (IUsersRepository)_serviceProvider.GetRequiredService(typeof(IUsersRepository));
            var dic = await usersRepository.GetAgentIdAndMerchantIdDicAsync();

            IAgentDailyReportStatisticRepository agentRepository = (IAgentDailyReportStatisticRepository)_serviceProvider.GetRequiredService(typeof(IAgentDailyReportStatisticRepository));
            IAgentShareProfitLogsRepository agentShareProfitRepository = (IAgentShareProfitLogsRepository)_serviceProvider.GetRequiredService(typeof(IAgentShareProfitLogsRepository));
            IPromotionsConfigService promotionsConfigService = (IPromotionsConfigService)_serviceProvider.GetRequiredService(typeof(IPromotionsConfigService));
            DateTime startAt = default(DateTime), endAt = default(DateTime);
            // 2. 获取代理的数据
            foreach (var d in dic)
            {
                // 获取到合约内容 ContractId
                var agent = await usersRepository.GetAsync(d.Key);
                var contactId = agent.AgentSettingEntity.ContractId;
                var pro = await promotionsConfigService.GetAsync(d.Value, contactId);
                if (!pro.sucess)
                {
                    // TODO 记录日志
                    continue;
                }
                var pro_rule_config = (PromotionsConfig.ContractConfig)pro.entity.BaseConfig;
                var pro_rule = JsonHelper.JSONToObject<List<ContractRule>>(pro_rule_config.RuleConfig);
                // 获取到统计日期区间
                if (pro_rule_config.RebateType == RebateType.Weekly)
                {
                    startAt = DateTime.UtcNow.AddHours(8).GetLastWeekMondayDate();
                    endAt = DateTime.UtcNow.AddHours(8).GetThisWeekMondayDate().AddDays(-1);
                }
                else if (pro_rule_config.RebateType == RebateType.Monthly)
                {
                    startAt = DateTime.UtcNow.AddHours(8).GetLastMonthFirstDayDate();
                    endAt = DateTime.UtcNow.AddHours(8).GetLastMonthLastDayDate();
                }
                else
                    throw new Exception("请配置代理");
                // 获取到代理下所有用户的亏损情况
                var agentMoney = await agentRepository.GetstatisticsAsync(d.Value, d.Value, startAt, endAt);
                // 获取到代理绑定的契约
                ContractRule rule = null;
                foreach (var r in pro_rule.OrderBy(t => t.Min))
                {
                    if (agentMoney > r.Min && agentMoney < r.Max)
                    {
                        rule = r;
                        break;
                    }
                }
                // 代理分润
                var agentShareProfit = agentMoney * rule.Percentage / 100;
                // 写入数据库
                var agentShareProfitLogs = new AgentShareProfitLogs()
                {
                    MerchantId = d.Value,
                    MemberId = d.Key,
                    GameLossTotal = agentMoney,
                    Rate = rule.Percentage / 100,
                    Agentbonus = agentShareProfit,
                    StartDate = startAt,
                    EndDate = endAt,
                    Des = "",
                    CreateAt = DateTime.Now.AddHours(8),
                    Status = AgentShareProfitLogs.AgentPayLogStatus.None
                };
                var exist = await agentShareProfitRepository.IsExistLogsAsync(d.Key, startAt, endAt);
                if (!exist)
                {
                    await agentShareProfitRepository.InsertAsync(agentShareProfitLogs);
                }
            }
            return "执行完毕";
        }

        /// <summary>
        /// [注册优惠]
        /// </summary>
        /// <returns></returns>
        public async Task<string> ExecRegisterPromo()
        {

            return string.Empty;
        }

        /// <summary>
        /// [每日签到]
        /// </summary>
        /// <returns></returns>
        public async Task<string> ExecDailyCheckInPromo()
        {


            return string.Empty;
        }

        /// <summary>
        /// [周周签到]
        /// 思路: 获取开启活动的信息， 获取指定站点参与的人数信息，  筛选符合条件的人数 ， 根据规则发放数据
        /// </summary>
        /// <returns></returns>
        public async Task<string> ExecWeeklyCheckInPromo()
        {
            // 1. 获取开启活动的站点
            IPromotionsConfigService promotionsConfigService = (IPromotionsConfigService)_serviceProvider.GetRequiredService(typeof(IPromotionsConfigService));
            var promos = await promotionsConfigService.GetTaskListAsync(ActivityType.WeeklyCheckIn);
            if (promos.Count() == 0) return "未有展开开启该活动";


            IGameLogsService gameLogsService = (IGameLogsService)_serviceProvider.GetRequiredService(typeof(IGameLogsService));
            IPayOrderService payOrderService = (IPayOrderService)_serviceProvider.GetRequiredService(typeof(IPayOrderService));
            IWashOrderService washOrderService = (IWashOrderService)_serviceProvider.GetRequiredService(typeof(IWashOrderService));

            string date = $"{DateTime.UtcNow.AddHours(8).GetLastWeekMondayDate().ToDateTimeString4()}-{DateTime.UtcNow.AddHours(8).GetThisWeekMondayDate().AddDays(-1).ToDateTimeString4()}";
            foreach (var pro in promos)
            {
                // 2. 获取站点上周参与签到的会员数量统计
                ISignInLogService signInLogService = (ISignInLogService)_serviceProvider.GetRequiredService(typeof(ISignInLogService));
                var signMembersdic = await signInLogService.GetTasksDicAsync(pro.MerchantId);

                // 3. 发放规则
                var config = (PromotionsConfig.WeeklyCheckInConfig)pro.BaseConfig;
                var rule = JsonHelper.JSONToObject<List<RuleConfigModel>>(config.RuleConfig);

                // 4. 设置的所有签到天数
                var dayKey = rule.GroupBy(t => t.DayCount).Select(t => t.Key);
                foreach (var day in dayKey)
                {
                    // 5. 获取所有这一天签到的会员
                    var membersIds = signMembersdic.Values.Where(t => t == day);
                    // 6. 获取设置的内容进行排序
                    var ruleSettings = rule.Where(t => t.DayCount == day).OrderBy(t => t.Reward);

                    foreach (var membersId in membersIds)
                    {
                        decimal reward = decimal.Zero;
                        // 7. 获取会员的上周有效投注
                        // 8. 获取会员的上周的充值金额
                        // 9. 根据发放方式发放奖金
                        if (config.RechargeAmountRule && config.WashAmountRule)
                        {
                            var validBet = await gameLogsService.GetTaskLaskWeekValidBet(pro.MerchantId, membersId);
                            var recharge = await payOrderService.GetTaskRechargeAmountAsync(pro.MerchantId, membersId);

                            var sets = ruleSettings.Where(t => t.RechargeAmount > recharge && t.WashAmount > validBet); // 同时大于充值金额与有效投注
                            if (sets.Count() == 0) continue;
                            var set = sets.OrderBy(t => t.Reward).First();
                            reward = set.Reward;
                        }
                        else if (config.RechargeAmountRule)
                        {
                            var recharge = await payOrderService.GetTaskRechargeAmountAsync(pro.MerchantId, membersId);
                            var sets = ruleSettings.Where(t => t.RechargeAmount > recharge);
                            if (sets.Count() == 0) continue;

                        }
                        else if (config.WashAmountRule)
                        {
                            var validBet = await gameLogsService.GetTaskLaskWeekValidBet(pro.MerchantId, membersId);
                            var sets = ruleSettings.Where(t => t.WashAmount > validBet);
                            if (sets.Count() == 0) continue;
                        }

                        // 10. 保存派发金额
                        await CreateActivityFunds(pro.MerchantId, membersId, pro.Id, reward, $"奖金发放:{ActivityType.WeeklyCheckIn.GetDescription()}", ActivityType.WeeklyCheckIn, FundLogType_Promotions.WeeklyCheckIn, $"{membersId}|{ActivityType.WeeklyCheckIn.ToString()}|{date}");

                        // 11. 出款要求流水
                        decimal wash = CalcWash(config.Wash, reward, config.WashValue);
                        await washOrderService.InsertAsync(membersId, FundLogType.Promotions, reward, wash, $"[{ActivityType.WeeklyCheckIn.GetDescription()}] 活动奖金");

                    }
                }
            }

            return "OK";
        }

        /// <summary>
        /// [连续签到]
        /// </summary>
        /// <returns></returns>
        public async Task<string> ExecSeriesCheckInPromo()
        {
            return string.Empty;
        }

        /// <summary>
        /// [充值优惠] 
        /// </summary>
        /// <returns></returns>
        public async Task<string> ExecRechargePromo()
        {
            // 1. 获取开启活动的站点
            IPromotionsConfigService promotionsConfigService = (IPromotionsConfigService)_serviceProvider.GetRequiredService(typeof(IPromotionsConfigService));
            var promos = await promotionsConfigService.GetTaskListAsync(ActivityType.Recharge);
            if (promos.Count() == 0) return "未有展开开启该活动";
            var merchantIds = promos.Select(t => t.MerchantId).ToList();
            // 2. 获取获取两分钟分钟内充值成功的所有用户
            // TODO 增加充值成功的缓存队列
            IPayOrderService payOrderService = (IPayOrderService)_serviceProvider.GetRequiredService(typeof(IPayOrderService));

            var orders = await payOrderService.GetTaskListAsync();
            if (orders.Count() == 0) return "不存在[充值优惠]任务订单";
            orders = orders.Where(t => merchantIds.Contains(t.MerchantId));//过滤开启活动的站点

            // 3. 遍历开启的站点
            IUsersService usersService = (IUsersService)_serviceProvider.GetRequiredService(typeof(IUsersService));
            IWashOrderService washOrderService = (IWashOrderService)_serviceProvider.GetRequiredService(typeof(IWashOrderService));
            List<string> msgList = new List<string>();
            foreach (var pro in promos)
            {
                var config = (PromotionsConfig.RechargeConfig)pro.BaseConfig;
                if (config.GroupConfig.IsNullOrEmpty())
                {
                    msgList.Add($"活动Id{pro.Id},未开启该用户组");
                    continue;
                }
                var groups = JsonConvert.DeserializeObject<List<int>>(config.GroupConfig); // 活动允许的会员组
                var ods = orders.Where(t => t.MerchantId == pro.MerchantId);
                foreach (var od in ods)
                {
                    var rt = await usersService.GetUserById(od.MerchantId, od.MemberId);
                    if (!rt.Item1) continue;
                    if (!groups.Contains(rt.Item3.GroupId))
                    {
                        msgList.Add($"活动Id{pro.Id},未开启该用户组。用户{od.MemberId} 用户未达到返点用户组要求,站点{od.MerchantId} 未进行返点");
                        continue;
                    }

                    // 活动金额发放
                    var rechargeRuleConf = JsonHelper.JSONToObject<List<RechargeRule>>(config.RechargeRule);
                    RechargeRule rule = null;
                    foreach (var c in rechargeRuleConf)
                    {
                        if (c.Min <= od.DepositAmount && od.DepositAmount <= c.Max)
                        {
                            rule = c;
                            break;
                        }
                    }
                    if (rule == null)
                    {
                        msgList.Add($"活动Id{pro.Id},未找到规则. 充值金额{od.DepositAmount}. 用户{od.MerchantId}");
                        break;
                    }

                    decimal reward = decimal.Zero;
                    //if (config.BonusCalType == PromotionsConfig.BonusCalType.Cash)
                    //    reward = config.BonusCalTypeValue;
                    //else if (config.BonusCalType == PromotionsConfig.BonusCalType.CashRate)
                    //    reward = od.DepositAmount * config.BonusCalTypeValue / 100;
                    if (config.BonusCalType == PromotionsConfig.BonusCalType.Cash)
                        reward = rule.Reward;
                    else if (config.BonusCalType == PromotionsConfig.BonusCalType.CashRate)
                        reward = od.DepositAmount * rule.Reward / 100;


                    await CreateActivityFunds(od.MerchantId, od.MemberId, pro.Id, reward, $"奖金发放:{ActivityType.Recharge.GetDescription()}", ActivityType.Recharge, FundLogType_Promotions.Recharge, $"{od.MerchantId}|{ActivityType.Recharge.ToString()}|{od.Id}");

                    // 流水增加
                    decimal wash = reward * rule.WashAmount;
                    await washOrderService.InsertAsync(od.MemberId, FundLogType.Promotions, reward, wash, $"[{ActivityType.Recharge.GetDescription()}] 活动奖金");

                    msgList.Add($"{DateTime.UtcNow.AddHours(8).ToDateTimeString2()} 站点{od.MerchantId},用户{od.MemberId},活动{pro.Id},奖金{reward},流水{wash}");
                }
            }

            return msgList.ArrayToString();
        }

        /// <summary>
        /// [幸运转盘]
        /// </summary>
        /// <returns></returns>
        public async Task<string> ExecTurnTablePromo()
        {
            return string.Empty;
        }

        /// <summary>
        /// [邀请有礼]
        /// </summary>
        /// <returns></returns>
        public async Task<string> ExecInvitePromo()
        {
            return string.Empty;
        }

        /// <summary>
        /// [救援金]
        /// </summary>
        /// <returns></returns>
        public async Task<string> ExecRescuePromo()
        {



            return string.Empty;
        }


        /// <summary>
        /// 代理返点
        /// </summary>
        /// <returns></returns>
        public async Task<string> ExecAgentRebate()
        {

            // 0. 获取开启活动的站点
            IPromotionsConfigService promotionsConfigService = (IPromotionsConfigService)_serviceProvider.GetRequiredService(typeof(IPromotionsConfigService));
            var promos = await promotionsConfigService.GetTaskListAsync(ActivityType.Recharge);
            if (promos.Count() == 0) return "未有展开开启该活动";
            var merchantIds = promos.Select(t => t.MerchantId).ToList();
            // 1. 获取所有站点的代理
            IUsersRepository usersRepository = (IUsersRepository)_serviceProvider.GetRequiredService(typeof(IUsersRepository));
            var dic = await usersRepository.GetMerchantsAndAgentsDicAsync(merchantIds.ToArray()); // 获取站点下的代理

            IUserHierarchyRepository usersHierarchyRepository = (IUserHierarchyRepository)_serviceProvider.GetRequiredService(typeof(IUserHierarchyRepository));
            Dictionary<KeyValuePair<int, int>, IEnumerable<int>> dd = new Dictionary<KeyValuePair<int, int>, IEnumerable<int>>();
            foreach (var d in dic)
            {
                var subIds = await usersHierarchyRepository.GetSubMemberIdsFromCacheAsync(d.Key, d.Value);
                dd.Add(d, subIds);
            }


            // 2. 获取站点代理打码量


            // 3. 按照派发规则下发活动资金


            return string.Empty;
        }


        /// <summary>
        /// [活动的打码金额计算]
        /// </summary>
        /// <param name="washType"></param>
        /// <param name="reward"></param>
        /// <param name="washValue"></param>
        /// <returns></returns>
        private decimal CalcWash(PromotionsConfig.WashType washType, decimal reward, decimal washValue)
        {
            decimal wash = decimal.Zero;
            if (washType == PromotionsConfig.WashType.Fixed)
                wash = washValue;
            else if (washType == PromotionsConfig.WashType.Multiple)
                wash = (1 + washValue) * reward;
            return wash;
        }


        /// <summary>
        /// [保存活动金额统一方法]
        /// </summary>
        /// <returns></returns>
        private async Task<string> CreateActivityFunds(int merchantId, int userId, int promotionId, decimal reward, string description, ActivityType activityType, FundLogType_Promotions fundsProType, string sourceId)
        {
            // 保存订单
            IActivityOrdersService activityOrdersService = (IActivityOrdersService)_serviceProvider.GetRequiredService(typeof(IActivityOrdersService));

            // 判断是否已经存在发放的奖金 merchantId,userId,promotionId,sourceId,activityType,sourceId| 设定sourceId命名方式为  activityType|orderId 有订单的根据订单模式,没有订单的 orderId=会员Id+日期
            // todo 增加个sourceId字段
            var exist = await activityOrdersService.ExistSourceIdAsync(sourceId);
            if (exist) return $"{sourceId}已存在";

            DateTime dt = DateTime.UtcNow.AddHours(8);
            var activityOrders = new ActivityOrders()
            {
                MerchantId = merchantId,
                UserId = userId,
                PromotionId = promotionId,
                AType = activityType,
                Reward = reward,
                Status = ActivityOrders.ActivityOrderStatus.Ok,
                Ip = "127.0.0.1",
                Description = description,
                CreateTime = dt,
                RewardTime = dt,
                CreateDate = dt.Date.ToString("yyyy-MM-dd"),
                SourceId = sourceId ?? ""
            };
            var orderResult = await activityOrdersService.InsertAsync(activityOrders);

            // 保存金额账变
            if (orderResult.Item1)
            {
                IUsersFundsService usersFundsService = (IUsersFundsService)_serviceProvider.GetRequiredService(typeof(IUsersFundsService));
                var rt = await usersFundsService.PromoRewardAsync(merchantId, userId, orderResult.Item3, reward, description, fundsProType);
                if (rt.Item1) return $"用户{userId},{activityType.GetDescription()}奖金发放成功";
            }
            return $"用户{userId},{activityType.GetDescription()}奖金发放失败";
        }





        #endregion




        #region VIP自动升级任务

        /// <summary>
        /// 用户充值后升级
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public async Task<(bool, string)> VipsUpgradeTask(int merchantId, int memberId)
        {
            IUsersRepository _usersRepository = (IUsersRepository)_serviceProvider.GetRequiredService(typeof(IUsersRepository));
            IUsersFundsRepository _usersFundsRepository = (IUsersFundsRepository)_serviceProvider.GetRequiredService(typeof(IUsersFundsRepository));
            IVipGroupsRepository _vipGroupsRepository = (IVipGroupsRepository)_serviceProvider.GetRequiredService(typeof(IVipGroupsRepository));
            // 0. 获取用户数据
            var memberFunds = await _usersFundsRepository.GetAsync(merchantId, memberId);
            var memberInfo = await _usersRepository.GetAsync(memberId);

            // 1. 获取所有的启用的分组
            string conditions = $" WHERE MerchantId={merchantId} AND Enabled = 1 ORDER BY SortNo ASC ";
            //var list = (await _vipGroupsRepository.GetListAsync(conditions)).OrderBy(t => t.SortNo);  // 升序排列分组内容
            var list = (await _vipGroupsRepository.GetListFromCacheAsync(merchantId)).Where(t => t.Enabled).OrderBy(t => t.SortNo);

            // 2. 获取排序的数据排序信息
            GroupSetting g;
            int groupId = 0;
            decimal reward = 0;
            foreach (var l in list)
            {
                g = l.GroupSettingModel;
                // 3.判断用户是否升级  
                // 3.1 用户的充值/有效投注  都小于 会员组设定
                if (memberFunds.TotalRechargedFunds >= g.AccumulatedRechargeAmount && memberFunds.TotalBetFunds >= g.AccumulatedEffectiveAmount)
                {
                    groupId = l.Id;
                    reward = g.ProAmount;
                }
            }

            if (memberInfo.GroupId != groupId)
            {
                memberInfo.GroupId = groupId;
                await _usersRepository.UpdateAsync(memberInfo);
                await VipUpgradeTask(memberInfo.MerchantId, memberInfo.Id, reward);
            }
            return (true, null);
        }

        public async Task<(bool, string)> VipsUpgradeTaskAsync()
        {
            return (false, string.Empty);
        }

        private async Task VipUpgradeTask(int merchantID, int memberId, decimal reward)
        {
            IActivityOrdersService activityOrdersService = (IActivityOrdersService)_serviceProvider.GetRequiredService(typeof(IActivityOrdersService));
            string desc = "VIP奖金发放:晋级彩金";
            await CreateActivityFunds(merchantID, memberId, 0, reward, desc, ActivityType.None, FundLogType_Promotions.UpGradePromo, $"{memberId}|VIP.UpGradePromo|{DateTime.UtcNow.AddHours(8).ToDateTimeString4()}");
        }

        /// <summary>
        /// [生日礼金]每天凌晨三点执行一次
        /// </summary>
        /// <returns></returns>
        public async Task VipBirthTask()
        {
            IActivityOrdersService activityOrdersService = (IActivityOrdersService)_serviceProvider.GetRequiredService(typeof(IActivityOrdersService));
            string desc = "VIP奖金发放:生日礼金";
            var dic = await ProcessVips("BirthTask");
            foreach (var d in dic)
            {
                foreach (var dd in d.Value)
                {
                    //  判断生日礼金在过去的11个月内是否发放过
                    bool exist = await activityOrdersService.ExistBirthOrdersAsync(d.Key, dd.Key, DateTime.UtcNow.AddHours(8).AddDays(-360), desc);

                    // 2. 按照条件进行活动发放
                    if (exist) continue;

                    await CreateActivityFunds(d.Key, dd.Key, 0, dd.Value, desc, ActivityType.None, FundLogType_Promotions.BirthGift, $"{dd.Key}|VIP.BirthGift|{DateTime.UtcNow.AddHours(8).ToDateTimeString4()}");
                }
            }
        }

        /// <summary>
        /// [每月俸禄]每个月固定日期执行一次
        /// </summary>
        /// <returns></returns>
        public async Task VipSalaryTask()
        {
            IActivityOrdersService activityOrdersService = (IActivityOrdersService)_serviceProvider.GetRequiredService(typeof(IActivityOrdersService));
            string desc = "VIP奖金发放:每月俸禄";
            var dic = await ProcessVips("SalaryTask");
            string date = $"{DateTime.UtcNow.AddHours(8).GetLastMonthFirstDayDate().ToDateTimeString4()}-{DateTime.UtcNow.AddHours(8).GetLastMonthLastDayDate().ToDateTimeString4()}";
            foreach (var d in dic)
            {
                foreach (var dd in d.Value)
                {
                    //  判断生日礼金在过去的28日内是否发放过
                    bool exist = await activityOrdersService.ExistBirthOrdersAsync(d.Key, dd.Key, DateTime.UtcNow.AddHours(8).AddDays(-28), desc);

                    // 2. 按照条件进行活动发放
                    if (exist) continue;

                    await CreateActivityFunds(d.Key, dd.Key, 0, dd.Value, desc, ActivityType.None, FundLogType_Promotions.MonthSalary, $"{dd.Key}|VIP.MonthSalary|{date}");
                }
            }
        }

        private async Task<Dictionary<int, Dictionary<int, decimal>>> ProcessVips(string task)
        {
            IVipGroupsRepository vipGroupsRepository = (IVipGroupsRepository)_serviceProvider.GetRequiredService(typeof(IVipGroupsRepository));
            IUsersRepository usersRepository = (IUsersRepository)_serviceProvider.GetRequiredService(typeof(IUsersRepository));
            IActivityOrdersService activityOrdersService = (IActivityOrdersService)_serviceProvider.GetRequiredService(typeof(IActivityOrdersService));
            // 0. 获取所有站点VIP的设置消息
            IMerchantService merchantService = (IMerchantService)_serviceProvider.GetRequiredService(typeof(IMerchantService));
            // TODO 初始化站点时候初始化 Merchant_VipsConfig 字符串
            var dic = (await merchantService.GetMerchantsAsync()).ToDictionary(t => t.Id, t => t.MerchantVipsConfig);
            Dictionary<int, Dictionary<int, decimal>> merchantIdUserIdRewardDic = new Dictionary<int, Dictionary<int, decimal>>();
            foreach (var d in dic.Where(t => t.Value.EnableBirthAmount == true))
            {
                // 1. 获取会员的 ID/vipId 字典
                var userIdVipIdDic = await usersRepository.GetBirthDictionaryAsync(d.Key, DateTime.UtcNow.AddHours(8));
                // 2. 获取站点设置的活动金额
                Dictionary<int, decimal> vipIdAmountDic = new Dictionary<int, decimal>();
                var vipList = (await vipGroupsRepository.GetListFromCacheAsync(d.Key)).Where(t => t.Enabled).OrderBy(t => t.SortNo);
                switch (task)
                {
                    case "BirthTask":
                        vipIdAmountDic = vipList.ToDictionary(t => t.Id, t => t.GroupSettingModel.BirthAmount);
                        break;
                    case "SalaryTask":
                        vipIdAmountDic = vipList.ToDictionary(t => t.Id, t => t.GroupSettingModel.MonthSalary);
                        break;
                    default:
                        break;
                }

                var result = from x in userIdVipIdDic
                             join y in vipIdAmountDic
                             on x.Value equals y.Key
                             select new KeyValuePair<int, decimal>(x.Key, y.Value); // 用户ID，应发放金额
                merchantIdUserIdRewardDic.Add(d.Key, (Dictionary<int, decimal>)result);

            }

            return merchantIdUserIdRewardDic;
        }
        #endregion






    }
}
