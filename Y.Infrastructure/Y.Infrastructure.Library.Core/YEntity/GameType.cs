using System;
using System.Collections.Generic;
using System.ComponentModel;
using Y.Infrastructure.Library.Core.Extensions;

namespace Y.Infrastructure.Library.Core.YEntity
{
    public enum GameType
    {
        #region ESport

        [ESport][Description("泛亚电竞")] ESport_AVIA = 11,

        [ESport][Description("东森电竞")] ESport_DS = 12,

        [ESport][Description("IM电竞")] ESport_IM = 13,

        [ESport][Description("IA电竞")] ESport_IA = 14,

        [ESport][Description("雷火电竞")] ESport_TF = 15,

        #endregion

        #region Sport

        [Sport][Description("沙巴体育")] Sport_Onebook = 31,

        [Sport][Description("IM体育")] Sport_IM = 32,

        [Sport][Description("CMD体育")] Sport_CMD = 33,

        [Sport][Description("SBTech体育")] Sport_SBTech = 34,

        [Sport][Description("平博体育")] Sport_Pinnacle = 35,

        [Sport][Description("UG体育")] Sport_UG = 36,

        [Sport][Description("小金体育")] Sport_XJ = 37,

        [Sport][Description("泛亚体育")] Sport_AVIA = 38,

        [Sport][Description("皇冠体育")] Sport_HG = 39,

        #endregion

        #region Slot

        [Slot][Description("泛亚电竞")] Slot_AVIA = 51,

        [Slot][Description("AG电子")] Slot_AG = 52,

        [Slot][Description("JDB电子")] Slot_JDB = 53,

        [Slot][Description("MG电子")] Slot_MG = 54,

        [Slot][Description("AG街机")] Slot_AG_YOPLAY = 55,

        #endregion

        #region Live

        [Live][Description("AG真人")] Live_AG = 71,

        [Live][Description("OG东方真人")] Live_OG = 72,

        [Live][Description("欧博真人")] Live_AllBet = 73,

        [Live][Description("BBIN真人")] Live_BBIN = 74,

        [Live][Description("BG真人")] Live_BG = 75,

        #endregion

        #region Lottery

        [Lottery][Description("VR彩票")] Lottery_VR = 91,

        [Lottery][Description("IG彩票")] Lottery_IG = 92,

        [Lottery][Description("TCG彩票")] Lottery_TCG = 93,

        [Lottery][Description("OB彩票")] Lottery_OB = 94,

        [Lottery][Description("蚂蚁彩票")] Lottery_MY = 95,

        [Lottery][Description("泛牛彩票")] Lottery_FNH = 96,

        [Lottery][Description("猜涨跌")] Lottery_CZD = 97,

        #endregion

        #region Chess

        [Chess][Description("开元棋牌")] Chess_KY = 111,

        [Chess][Description("GM棋牌")] Chess_GM = 112,

        [Chess][Description("JDB棋牌")] Chess_JDB = 113,

        [Chess][Description("天美棋牌")] Chess_TM = 114,

        [Chess][Description("OB棋牌")] Chess_OB = 115,

        [Chess][Description("天游棋牌")] Chess_TY = 116,

        [Chess][Description("YG棋牌")] Chess_YG = 117,

        #endregion

        #region Hunt

        [Hunt][Description("AG捕鱼")] Hunt_AG = 131,

        [Hunt][Description("KS捕鱼")] Hunt_KSAviaFish = 132,

        #endregion


        //[Finance][Description("泛牛汇")] Finance_FNH = 150,
    }

    public class ESportAttribute : Attribute
    {
    }

    public class SportAttribute : Attribute
    {
    }

    public class SlotAttribute : Attribute
    {
    }

    public class LiveAttribute : Attribute
    {
    }

    public class LotteryAttribute : Attribute
    {
    }

    public class ChessAttribute : Attribute
    {
    }

    public class HuntAttribute : Attribute
    {
    }

    //public class FinanceAttribute : Attribute
    //{
    //}

    public enum GameCategory
    {
        [Description("无")] NotSet = 0,
        [Description("电竞")] ESport = 1,
        [Description("体育")] Sport = 2,
        [Description("真人")] Live = 3,
        [Description("电子")] Slot = 4,
        [Description("彩票")] Lottery = 5,
        [Description("棋牌")] Chess = 6,
        [Description("捕鱼")] Hunt = 7,
        //[Description("市场")] Finance = 8
    }

    /// <summary>
    /// 转账类型
    /// </summary>
    public enum TransType
    {
        [Description("转入")] In,
        [Description("转出")] Out
    }

    public enum ESport
    {
        [Description("泛亚电竞")] ESport_AVIA,

        [Description("东森电竞")] ESport_DS,

        [Description("IM电竞")] ESport_IM,

        [Description("IA电竞")] ESport_IA,

        [Description("雷火电竞")] ESport_TF,
    }

    public enum Sport
    {
        [Description("沙巴体育")] Sport_Onebook,

        [Description("IM体育")] Sport_IM,

        [Description("CMD体育")] Sport_CMD,

        [Description("SBTech体育")] Sport_SBTech,

        [Description("平博体育")] Sport_Pinnacle,

        [Description("UG体育")] Sport_UG,

        [Description("小金体育")] Sport_XJ,

        [Description("泛亚体育")] Sport_AVIA,

        [Description("皇冠体育")] Sport_HG,
    }

    public enum Slot
    {
        [Description("泛亚电竞")] Slot_AVIA,

        [Description("AG电子")] Slot_AG,

        [Description("JDB电子")] Slot_JDB,

        [Description("MG电子")] Slot_MG,

        [Description("AG街机")] Slot_AG_YOPLAY,
    }

    public enum Live
    {
        [Description("AG真人")] Live_AG,

        [Description("OG东方真人")] Live_OG,

        [Description("欧博真人")] Live_AllBet,

        [Description("BBIN真人")] Live_BBIN,

        [Description("BG真人")] Live_BG,
    }


    public enum Lottery
    {
        [Description("VR彩票")] Lottery_VR,

        [Description("IG彩票")] Lottery_IG,

        [Lottery][Description("TCG彩票")] Lottery_TCG,

        [Description("OB彩票")] Lottery_OB,

        [Description("蚂蚁彩票")] Lottery_MY,
    }


    public enum Chess
    {
        [Description("开元棋牌")] Chess_KY,

        [Description("GM棋牌")] Chess_GM,

        [Chess][Description("JDB棋牌")] Chess_JDB,

        [Chess][Description("天美棋牌")] Chess_TM,

        [Description("OB棋牌")] Chess_OB,

        [Description("天游棋牌")] Chess_TY,

        [Description("YG棋牌")] Chess_YG,
    }

    public enum Hunt
    {
        [Description("AG捕鱼")] Hunt_AG,
        [Description("KS捕鱼")] Hunt_KSAviaFish,
    }

    //public enum Finance
    //{
    //    [Description("泛牛汇")] Finance_FNH,
    //}


    /// <summary>
    /// 赔率类型
    /// </summary>
    public enum OddsType
    {
        /// <summary>
        /// 未找到赔率类型
        /// </summary>
        [Description("未找到赔率类型")] None = 0,

        /// <summary>
        /// 欧盘
        /// </summary>
        [Description("欧盘")] DE = 1,

        /// <summary>
        /// 美盘
        /// </summary>
        [Description("美盘")] AM = 2,

        /// <summary>
        /// 香港盘
        /// </summary>
        [Description("香港盘")] HK = 3,

        /// <summary>
        /// 马来盘
        /// </summary>
        [Description("马来盘")] MY = 4,

        /// <summary>
        /// 印尼盘
        /// </summary>
        [Description("印尼盘")] ID = 5,

        /// <summary>
        /// 缅甸盘
        /// </summary>
        [Description("缅甸盘")] MR = 6
    }

    /// <summary>
    /// 订单状态
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// 待开奖
        /// </summary>
        [Description("待开奖")] None = 0,

        /// <summary>
        /// 赢
        /// </summary>
        [Description("赢")] Win = 1,

        /// <summary>
        /// 输
        /// </summary>
        [Description("输")] Lose = 2,

        /// <summary>
        /// 和
        /// </summary>
        [Description("和")] Draw = 3,

        /// <summary>
        /// 半赢
        /// </summary>
        [Description("半赢")] HalfWin = 4,

        /// <summary>
        /// 半输
        /// </summary>
        [Description("半输")] HalfLose = 5,

        /// <summary>
        /// 平台拒单，投注无效
        /// </summary>
        [Description("平台拒单")] Reject = 6,

        /// <summary>
        /// 订单取消，退款
        /// </summary>
        [Description("订单取消")] Refund = 7,

        /// <summary>
        /// 玩家撤单
        /// </summary>
        [Description("玩家撤单")] Cannel = 8
    }


    public enum OrderStatusForCustomers
    {
        [Description("全部")]
        ALL = 0,
        [Description("已结算")]
        Settled = 1,
        [Description("未结算")]
        UnSettled = 2
    }


    /// <summary>
    /// 订单访问类型
    /// </summary>
    public enum DeviceType
    {
        [Description("未知")] None,
        [Description("站点网页")] WebSite,
        [Description("移动端访问")] Mobile,
        [Description("手机网页")] MobileWebsite,
        [Description("安卓手机")] MobileAppAndroid,
        [Description("苹果手机")] MobileAppIOS
    }

    /// <summary>
    /// 早盘  滚球 其它 
    /// </summary>
    public enum Stage
    {
        [Description("未知")] None = 0,
        [Description("早盘")] FirstHalf = 1,
        [Description("午盘")] SecondHalf = 2,
        [Description("滚球")] Live = 3,
        [Description("全场")] Full = 4,
    }


    public static class OrderUtility
    {
        /// <summary>
        /// 订单是否完成
        /// </summary>
        /// <param name="orderStatus"></param>
        /// <returns>true:订单已经完成</returns>
        public static bool IsFinishOrder(this OrderStatus orderStatus)
        {
            return orderStatus != OrderStatus.None;
        }


        public static List<OrderStatus> TransOrderStatusForCustomersToOrderStatus(this OrderStatusForCustomers status)
        {
            List<OrderStatus> list = new List<OrderStatus>();
            switch (status)
            {
                case OrderStatusForCustomers.ALL:
                    list.Add(OrderStatus.None);
                    list.Add(OrderStatus.Win);
                    list.Add(OrderStatus.Lose);
                    list.Add(OrderStatus.Draw);
                    list.Add(OrderStatus.HalfWin);
                    list.Add(OrderStatus.HalfLose);
                    list.Add(OrderStatus.Reject);
                    list.Add(OrderStatus.Refund);
                    list.Add(OrderStatus.Cannel);
                    break;
                case OrderStatusForCustomers.Settled:
                    list.Add(OrderStatus.None);
                    list.Add(OrderStatus.Win);
                    list.Add(OrderStatus.Lose);
                    list.Add(OrderStatus.Draw);
                    list.Add(OrderStatus.HalfWin);
                    list.Add(OrderStatus.HalfLose);
                    list.Add(OrderStatus.Reject);
                    list.Add(OrderStatus.Refund);
                    list.Add(OrderStatus.Cannel);
                    break;
                case OrderStatusForCustomers.UnSettled:
                    list.Add(OrderStatus.None);
                    break;
            }
            return list;
        }


        /// <summary>
        /// GameType 转换为 GameCategory
        /// </summary>
        /// <param name="gameType"></param>
        /// <returns> GameCategory 类型</returns>
        public static GameCategory TransToGameCate(this GameType gameType)
        {
            if (gameType.ExistAttributeOfType<ESportAttribute>())
                return GameCategory.ESport;
            if (gameType.ExistAttributeOfType<SportAttribute>())
                return GameCategory.Sport;
            if (gameType.ExistAttributeOfType<SlotAttribute>())
                return GameCategory.Slot;
            if (gameType.ExistAttributeOfType<LiveAttribute>())
                return GameCategory.Live;
            if (gameType.ExistAttributeOfType<LotteryAttribute>())
                return GameCategory.Lottery;
            if (gameType.ExistAttributeOfType<ChessAttribute>())
                return GameCategory.Chess;
            if (gameType.ExistAttributeOfType<HuntAttribute>())
                return GameCategory.Hunt;
            //if (gameType.ExistAttributeOfType<FinanceAttribute>())
            //    return GameCategory.Finance;

            return GameCategory.NotSet;
        }


        public static bool IsMobile(this DeviceType deviceType)
        {
            var mobileType = new List<DeviceType>() { DeviceType.Mobile, DeviceType.MobileWebsite, DeviceType.MobileAppAndroid, DeviceType.MobileAppIOS };
            return mobileType.Contains(deviceType);
        }
    }
}