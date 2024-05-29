using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Promotions
{
    /// <summary>
    /// Aaron
    /// 2020-07-26 00:31:01
    /// 
    /// </summary>
    public partial class PromotionsConfig
    {
        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public BConfig BaseConfig
        {
            get
            {
                switch (this.AType)
                {
                    case ActivityType.Commission:
                        return JsonConvert.DeserializeObject<CommissionConfig>(this.Config);
                    case ActivityType.Contract:
                        return JsonConvert.DeserializeObject<ContractConfig>(this.Config);
                    case ActivityType.Rebate:
                        return JsonConvert.DeserializeObject<RebateConfig>(this.Config);
                    case ActivityType.Register:
                        return JsonConvert.DeserializeObject<RegisterConfig>(this.Config);
                    case ActivityType.DailyCheckIn:
                        return JsonConvert.DeserializeObject<DailyCheckInConfig>(this.Config);
                    case ActivityType.WeeklyCheckIn:
                        return JsonConvert.DeserializeObject<WeeklyCheckInConfig>(this.Config);
                    case ActivityType.BankCard:
                        return JsonConvert.DeserializeObject<BankCarkConfig>(this.Config);
                    case ActivityType.Recharge:
                        return JsonConvert.DeserializeObject<RechargeConfig>(this.Config);
                    case ActivityType.TurnTable:
                        return JsonConvert.DeserializeObject<TurnTableConfig>(this.Config);
                    case ActivityType.Invite:
                        return JsonConvert.DeserializeObject<InviteConfig>(this.Config);
                    case ActivityType.SeriesCheckIn:
                        return JsonConvert.DeserializeObject<SeriesCheckInConfig>(this.Config);
                    case ActivityType.AgentRebate:
                        return JsonConvert.DeserializeObject<AgentRebateConfig>(this.Config);
                    case ActivityType.None:
                    default:
                        return JsonConvert.DeserializeObject<BConfig>(this.Config);
                }
            }
        }

        [NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public BConfig GetConfigInstance
        {
            get
            {
                switch (this.AType)
                {
                    default:
                    case ActivityType.None:
                        return new BConfig();
                    case ActivityType.Commission:
                        return new CommissionConfig();
                    case ActivityType.Contract:
                        return new ContractConfig();
                    case ActivityType.Rebate:
                        return new RebateConfig();
                    case ActivityType.Register:
                        return new RegisterConfig();
                    case ActivityType.DailyCheckIn:
                        return new DailyCheckInConfig();
                    case ActivityType.WeeklyCheckIn:
                        return new WeeklyCheckInConfig();
                    case ActivityType.BankCard:
                        return new BankCarkConfig();
                    case ActivityType.Recharge:
                        return new RechargeConfig();
                    case ActivityType.TurnTable:
                        return new TurnTableConfig();
                    case ActivityType.Invite:
                        return new InviteConfig();
                    case ActivityType.SeriesCheckIn:
                        return new SeriesCheckInConfig();
                    case ActivityType.AgentRebate:
                        return new AgentRebateConfig();
                }
            }

        }

        /// <summary>
        /// 规则基础配置
        /// </summary>
        [NotMapped]
        public class BConfig
        {
            /// <summary>
            /// 是否开启奖金发放类型
            /// </summary>
            public bool EnabledBonusType { get; set; }
            /// <summary>
            /// 奖金发放类型/自动、手动
            /// </summary>
            public BonusType BonusType { get; set; }

            /// <summary>
            /// 是否开启奖金计算方式
            /// </summary>
            public bool EnabledBonusCalType { get; set; }
            /// <summary>
            /// 奖金计算方式
            /// </summary>
            public BonusCalType BonusCalType { get; set; }
            /// <summary>
            /// 奖金(百分比/固定金额)
            /// </summary>
            public decimal BonusCalTypeValue { get; set; }

            /// <summary>
            /// 是否开启IP检查
            /// </summary>
            public bool EnabledIpCcheck { get; set; }

            public IPCheckType IPCheckType { get; set; }

            /// <summary>
            /// 奖金出款流水规则
            /// </summary>
            public WashType Wash { get; set; }

            /// <summary>
            /// 0 代表不计算流水，可以直接提款
            /// </summary>
            public decimal WashValue { get; set; }

        }

        /// <summary>
        /// 代理返点
        /// </summary>
        public class CommissionConfig : BConfig
        {
            /// <summary>
            /// 游戏反水配置
            /// </summary>
            public string GameConfig { get; set; }
            /// <summary>
            /// 返水类型
            /// </summary>
            public RebateType RebateType { get; set; }
        }

        public class ContractConfig : BConfig
        {
            public List<int> UserIds { get; set; }
            public RebateType RebateType { get; set; }
            public string RuleConfig { get; set; }
        }
        public enum RebateType
        {
            [Description("按周返水")]
            Weekly = 0,
            [Description("按月返水")]
            Monthly = 1
        }

        public class GradeRuleBase
        {
            /// <summary>
            /// 最低负盈利
            /// </summary>
            public decimal Min { get; set; }
            /// <summary>
            /// 最高负盈利
            /// </summary>
            public decimal Max { get; set; }
            /// <summary>
            /// 洗码倍数
            /// </summary>
            public int WashTimes { get; set; }
            /// <summary>
            /// 返利百分比/固定金额
            /// </summary>
            public int Percentage { get; set; }
        }

        /// <summary>
        /// 代理契约
        /// </summary>
        public class ContractRule: GradeRuleBase
        {
            public string Name { get; set; }
            /// <summary>
            /// 单独奖励金额
            /// </summary>
            public decimal Reward { get; set; }
        }


        /// <summary>
        /// 反水
        /// </summary>
        public class RebateConfig : BConfig
        {
            public string EnabledConfigStr { get; set; } // GameConfig or RuleConfig
            /// <summary>
            /// 所属会员分组
            /// </summary>
            public string GroupConfig { get; set; }
            /// <summary>
            /// 游戏反水配置
            /// </summary>
            public string GameConfig { get; set; }
            /// <summary>
            /// 返水区间规则配置
            /// </summary>
            public string RuleConfig { get; set; }  // GradeRuleBase
        }

        public class RegisterConfig : BConfig
        {

        }

        public class DailyCheckInConfig : BConfig
        {

        }
        // [周签到]
        public class WeeklyCheckInConfig : BConfig
        {
            /// <summary>
            /// 开启充值金额规则，进行奖金发放
            /// </summary>
            public bool RechargeAmountRule { get; set; }
            /// <summary>
            /// 开启流水金额规则，进行奖金发放
            /// </summary>
            public bool WashAmountRule { get; set; }
            public string RuleConfig { get; set; }

        }
        /// <summary>
        /// 配置规则
        /// </summary>
        public class RuleConfigModel
        {
            /// <summary>
            /// 天数
            /// </summary>
            public int DayCount { get; set; }
            /// <summary>
            /// 充值金额
            /// </summary>
            public decimal RechargeAmount { get; set; }
            /// <summary>
            /// 流水
            /// </summary>
            public decimal WashAmount { get; set; }
            /// <summary>
            /// 派奖
            /// </summary>
            public decimal Reward { get; set; }
        }

        public class BankCarkConfig : BConfig
        { }

        public class RechargeConfig : BConfig
        {
            /// <summary>
            /// 所属会员分组
            /// </summary>
            public string GroupConfig { get; set; }
            public string RechargeRule { get; set; }
        }

        /// <summary>
        /// 金额充值奖励规则
        /// </summary>
        public class RechargeRule
        {
            /// <summary>
            /// 充值最小值
            /// </summary>
            public decimal Min { get; set; }
            /// <summary>
            /// 充值最大值
            /// </summary>
            public decimal Max { get; set; }
            /// <summary>
            /// 洗码倍数 根据奖金计算的
            /// </summary>
            public int WashAmount { get; set; }
            /// <summary>
            /// 奖励 金额/倍数
            /// </summary>
            public decimal Reward { get; set; }
        }

        public class TurnTableConfig : BConfig
        { }

        public class InviteConfig : BConfig
        { }

        public class SeriesCheckInConfig : BConfig
        { }


        public class AgentRebateConfig : BConfig
        {
            /// <summary>
            /// 游戏反水配置
            /// </summary>
            public string GameConfig { get; set; }
        }


        #region 枚举类型

        /// <summary>
        /// 奖励派发类型
        /// </summary>
        public enum BonusType
        {
            [Description("不选择派奖方式")]
            None,
            [Description("自动派奖")]
            Auto = 1,
            [Description("手动派奖")]
            Manul = 2
        }

        /// <summary>
        /// 打码计算
        /// </summary>
        public enum WashType
        {
            [Description("不计算打码")]
            None,
            [Description("固定金额")]
            Fixed,
            [Description("倍数")]
            Multiple
        }


        /// <summary>
        /// 奖金计算方式
        /// </summary>
        public enum BonusCalType
        {
            [Description("不选择计算方式")]
            None,
            /// <summary>
            /// 现金金额
            /// </summary>
            [Description("现金金额")]
            Cash,
            /// <summary>
            /// 金额百分比
            /// </summary>
            [Description("金额百分比")]
            CashRate
        }

        /// <summary>
        /// IP检查类型
        /// </summary>
        public enum IPCheckType
        {
            [Description("不检查")]
            None,
            [Description("同IP检查")]
            SameIP,
            [Description("IP前三段")]
            IP3,
        }

        #endregion

    }
}
