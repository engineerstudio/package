using System;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Portal.Apis.Controllers.DtoModel.Merchant
{
    public class GameQuery
    {
        /// <summary>
        /// 会员名字
        /// </summary>
        public string MemberName { get; set; }
        /// <summary>
        /// 查询开始时间 // 默认昨天
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 查询结束时间 // 默认昨天
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 游戏类别
        /// </summary>
        public GameCategory GameCate { get; set; }
        /// <summary>
        /// 游戏类别
        /// </summary>
        public string[] GameTypes { get; set; }
        /// <summary>
        /// 查询方式，按包网平台时间，按游戏平台时间
        /// </summary>
        public string QueryType { get; set; }
    }


    public class GameD
    {
        public string GameName { get; set; }
        public int BetOrderCount { get; set; }
        public int SettlementOrderCount { get; set; }
        public decimal TotalBet { get; set; }
        public decimal TatalValidBet { get; set; }
        public decimal MerchantMoney { get; set; }
        public decimal GameMoney { get; set; }
    }



    public class MoneyD
    {
        /// <summary>
        /// 渠道名称
        /// </summary>
        public string ChannelName { get; set; }

        public decimal TotalRecharge { get; set; }

        public decimal TotalWithdrawal { get; set; }

    }
    public class AgentQuery
    {
        /// <summary>
        /// 查询代理名称
        /// </summary>
        public string AgentName { get; set; }
        /// <summary>
        /// 查询开始时间 // 默认昨天
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 查询结束时间 // 默认昨天
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    public class AgentD
    {
        public string AgentName { get; set; }
        public decimal SubMemberCount { get; set; }
        public decimal NewMemberCount { get; set; }
        public decimal TotalRecharge { get; set; }
        public decimal TotalWithdrawal { get; set; }
        public decimal TotalBet { get; set; }
        public decimal TatalValidBet { get; set; }
        public decimal Money { get; set; }
    }
}
