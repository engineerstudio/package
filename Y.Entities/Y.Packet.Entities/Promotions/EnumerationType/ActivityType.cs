using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Y.Packet.Entities.Promotions
{
    public enum ActivityType
    {
        [Description("手动活动")]
        None = 0,
        [Description("代理返佣")]
        Commission = 1,
        [Description("代理契约")]
        Contract = 2,
        [Description("会员返水")]
        Rebate = 3,
        [Description("注册优惠")]
        Register = 4,
        [Description("每日签到")]
        DailyCheckIn = 5,
        [Description("周周签到")]
        WeeklyCheckIn = 6,
        [Description("连续签到")]
        SeriesCheckIn = 7,
        [Description("绑定银行卡")]
        BankCard = 8,
        [Description("充值优惠")]
        Recharge = 9,
        [Description("幸运转盘")]
        TurnTable = 10,
        [Description("邀请有礼")]
        Invite = 11,
        [Description("救援金")]
        Rescue = 12,
        [Description("代理返点")]
        AgentRebate = 13
    }
}
