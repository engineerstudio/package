using System;
using System.ComponentModel;
using Y.Infrastructure.Library.Core.Extensions;

namespace Y.Infrastructure.Library.Core.YEntity
{
    /// <summary>
    /// 用户资金类型  手动加减款的
    /// </summary>
    public enum FundLogType
    {
        [Description("存款")] Recharge = 1,
        [Description("取款")] WithDraw = 2,
        [Description("活动")] Promotions = 3,
        [Description("游戏")] Games = 4
    }

    ///// <summary>
    ///// 转账类型
    ///// </summary>
    //public enum TransType
    //{
    //    [Description("转入")] // 加款
    //    In,
    //    [Description("转出")] // 减款
    //    Out
    //}

    /// <summary>
    /// 游戏转账状态
    /// </summary>
    public enum TransferStatus
    {
        [Description("等待解锁")] None = 1,
        [Description("转账成功")] Sucess = 2,
        [Description("转账失败")] Failed = 3
    }


    #region 资金标签

    public class RechargeAttribute : Attribute
    {
    }

    public class WithDrawAttribute : Attribute
    {
    }

    public class PromotionsAttribute : Attribute
    {
    }

    public class GamesAttribute : Attribute
    {
    }

    #endregion

    [Recharge]
    public enum FundLogType_Recharge
    {
        /// <summary>
        /// 手动加款
        /// </summary>
        [Description("手动加款")] Manual = 101,

        /// <summary>
        /// 渠道充值
        /// </summary>
        [Description("渠道充值")] Channel = 102
    }

    [WithDraw]
    public enum FundLogType_WithDraw
    {
        /// <summary>
        /// 手动减款
        /// </summary>
        [Description("手动减款")] Manual = 131,

        /// <summary>
        /// 渠道提现
        /// </summary>
        [Description("渠道提现")] Channel = 132
    }

    [Promotions]
    public enum FundLogType_Promotions
    {
        [Description("手动活动")] None = 151,
        [Description("代理返佣")] Commission = 152,
        [Description("代理契约")] Contract = 153,
        [Description("会员返水")] Rebate = 154,
        [Description("注册优惠")] Register = 155,
        [Description("每日签到")] DailyCheckIn = 156,
        [Description("周周签到")] WeeklyCheckIn = 157,
        [Description("连续签到")] SeriesCheckIn = 158,
        [Description("绑定银行卡")] BankCark = 159,
        [Description("充值优惠")] Recharge = 160,
        [Description("幸运转盘")] TurnTable = 161,
        [Description("邀请有礼")] Invite = 162,
        [Description("救援金")] Rescue = 163,
        [Description("代理返点")] AgentRebate = 164,
        [Description("生日礼金")] BirthGift = 170,
        [Description("VIP俸禄")] MonthSalary = 172,
        [Description("晋级礼包")] UpGradePromo = 173,
    }

    [Games]
    public enum FundLogType_Games
    {
        [Description("未设定")] NotSet = 0,

        #region ESport

        [Description("泛亚电竞")] ESport_AVIA = 181,


        [Description("东森电竞")] ESport_DS = 182,


        [Description("IM电竞")] ESport_IM = 183,


        [Description("IA电竞")] ESport_IA = 184,


        [Description("雷火电竞")] ESport_TF = 185,

        #endregion

        #region Sport

        [Description("沙巴体育")] Sport_Onebook = 186,


        [Description("IM体育")] Sport_IM = 187,


        [Description("CMD体育")] Sport_CMD = 188,


        [Description("SBTech体育")] Sport_SBTech = 189,


        [Description("平博体育")] Sport_Pinnacle = 190,


        [Description("UG体育")] Sport_UG = 191,


        [Description("小金体育")] Sport_XJ = 192,

        #endregion

        #region Slot

        [Description("泛亚电竞")] Slot_AVIA = 193,

        #endregion

        #region Live

        [Description("AG真人")] Live_AG = 194,

        [Description("OG东方真人")] Live_OG = 195,

        [Description("欧博真人")] Live_AllBet = 196,

        #endregion


        #region Lottery

        [Description("VR彩票")] Lottery_VR = 197,

        [Description("IG彩票")] Lottery_IG = 198,

        [Description("TCG彩票")] Lottery_TCG = 199,

        #endregion


        #region Chess

        [Description("开元棋牌")] Chess_KY = 200,

        [Description("GM棋牌")] Chess_GM = 201,

        [Description("JDB棋牌")] Chess_JDB = 202,

        [Description("天美棋牌")] Chess_TM = 203,

        #endregion


        #region Hunt

        [Description("AG捕鱼")] Hunt_AG = 204,

        [Description("KS捕鱼")] Hunt_KSAviaFish = 205,

        #endregion


        #region 2021.10.21 后

        [Description("泛亚体育")] Sport_AVIA = 206,

        [Description("皇冠体育")] Sport_HG = 207,


        [Description("AG电子")] Slot_AG = 208,

        [Description("JDB电子")] Slot_JDB = 209,

        [Description("MG电子")] Slot_MG = 210,

        [Description("AG街机")] Slot_AG_YOPLAY = 211,


        [Description("BBIN真人")] Live_BBIN = 212,

        [Description("BG真人")] Live_BG = 213,


        [Description("OB彩票")] Lottery_OB = 214,

        [Description("蚂蚁彩票")] Lottery_MY = 215,


        [Description("OB棋牌")] Chess_OB = 216,

        [Description("天游棋牌")] Chess_TY = 217,

        [Description("YG棋牌")] Chess_YG = 218,

        [Description("泛牛汇")] Finance_FNH = 219,

        [Description("泛牛汇彩票")] Lottery_FNH = 220,

        [Description("猜涨跌彩票")] Lottery_CZD = 221,
        #endregion
    }

    public static class FundsTypeTransUtility
    {
        public static FundLogType_Games TransToFundLogType(this GameType gameType)
        {
            var m = gameType.ToString().ToEnum<FundLogType_Games>();
            if (m == null) return FundLogType_Games.NotSet;
            return m.Value;
        }


        public static (int Value, string StrValue, string DescValue) TransToFundLogType(int intValue)
        {
            if (intValue.ToString().ToEnum<FundLogType_Recharge>().HasValue)
            {
                var typed = intValue.ToString().ToEnum<FundLogType_Recharge>().Value;
                return (intValue, typed.ToString(), typed.GetDescription());
            }
            if (intValue.ToString().ToEnum<FundLogType_Games>().HasValue)
            {
                var typed = intValue.ToString().ToEnum<FundLogType_Games>().Value;
                return (intValue, typed.ToString(), typed.GetDescription());
            }
            if (intValue.ToString().ToEnum<FundLogType_Promotions>().HasValue)
            {
                var typed = intValue.ToString().ToEnum<FundLogType_Promotions>().Value;
                return (intValue, typed.ToString(), typed.GetDescription());
            }
            if (intValue.ToString().ToEnum<FundLogType_WithDraw>().HasValue)
            {
                var typed = intValue.ToString().ToEnum<FundLogType_WithDraw>().Value;
                return (intValue, typed.ToString(), typed.GetDescription());
            }
            throw new Exception("未查询到对应数据");
        }




    }
}