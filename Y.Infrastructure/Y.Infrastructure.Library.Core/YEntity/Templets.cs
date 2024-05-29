using System;
using System.ComponentModel;

namespace Y.Infrastructure.Library.Core.YEntity
{
    public class Templets
    {
    }

    public enum Templet
    {
        [Description("One[赢天下]")] One = 1,

        [Description("Two[泛博股赢]")] // fanbogushi  fanboguying
        Two = 2,
        [Description("Three[赢一体育]")] Three = 3,
        [Description("还没开始04")] Four = 4,
        [Description("还没开始05")] Five = 5,
        [Description("还没开始06")] Six = 6,
        [Description("还没开始07")] Seven = 7,
        [Description("还没开始08")] Eight = 8,
        [Description("还没开始09")] Nine = 9,
        [Description("还没开始10")] Ten = 10,
        [Description("还没开始11")] Eleven = 11,
        [Description("还没开始12")] Twelve = 12
    }

    public enum HEADERMENU
    {
        [Description("主页")] [MenuKey("")] Home,

        [Description("体育竞技")] [MenuKey("HEADER-SUB-MENU-SPORT")]
        Sport,

        [Description("真人娱乐")] [MenuKey("HEADER-SUB-MENU-LIVE")]
        Live,

        [Description("电子竞技")] [MenuKey("HEADER-SUB-MENU-ESPORT")]
        Esport,

        [Description("彩票娱乐")] [MenuKey("HEADER-SUB-MENU-LOTTERY")]
        Lottery,

        [Description("棋牌游艺")] [MenuKey("HEADER-SUB-MENU-CHESS")]
        Chess,

        [Description("电子游艺")] [MenuKey("HEADER-SUB-MENU-SLOT")]
        Slot,

        [Description("捕鱼")] [MenuKey("HEADER-SUB-MENU-HUNT")]
        Hunt,

        [Description("市场游艺")] [MenuKey("HEADER-SUB-MENU-FINANCE")]
        Finance,

        [Description("优惠活动")] [MenuKey("")] Promo,

        [Description("合营")] [MenuKey("")] Agent,

        [Description("赞助")] [MenuKey("")] Sponsor,

        [Description("App下载")] [MenuKey("")] AppDownload
    }
}

public class MenuKeyAttribute : Attribute
{
    private string _desc;

    public MenuKeyAttribute(string desc)
    {
        _desc = desc;
    }

    public string Desc
    {
        get { return _desc; }
    }
}