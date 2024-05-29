using System.ComponentModel;

namespace Y.Infrastructure.Library.Core.LuckyEntity
{
    [Description("盘口类型")]
    public enum StockType
    {
        [Description("大盘指数")] StockMarketIndex,
        [Description("大盘涨跌幅")] StockMarketQuote,
        [Description("个股指数")] SingleStocksIndex,
        [Description("个股涨跌幅")] SingleStockstQuote,
    }

    [Description("场次")]
    public enum StockSchedule
    {
        [Description("全场")] Match = 0,
        [Description("早市")] FirstHalf = 1,
        [Description("午市")] SecondHalf = 2
    }

    public enum MarketType
    {
        [Description("上证指数")] CN_SH,
        [Description("深证成指")] CN_SZ,
        [Description("创业板指")] CN_CY,
        //[Description("沪深300")]
        //CN_HS300,
        //[Description("上证50")]
        //CN_SZ50,
        //[Description("中证500")]
        //CN_ZZ500,
        //[Description("科创50")]
        //CN_KC50,
        //[Description("富时A50")]
        //CN_FSA50,
        //[Description("富时A200")]
        //CN_FSA200,
        //[Description("深证100")]
        //CN_SZ100,
        //[Description("上证180")]
        //CN_SZ180,
        //[Description("上证380")]
        //CN_SZ380,
        //[Description("创业板50")]
        //CN_CYB50,
        //[Description("深证综指")]
        //CN_SZZZ,
        //[Description("B股指数")]
        //CN_BGZS,
        //[Description("新指数")]
        //CN_XZS,
        //[Description("央视50")]
        //CN_YS50,
        //[Description("深次新股")]
        //CN_SCXG,
        //[Description("深证300")]
        //CN_SZ300,
        //[Description("中小300")]
        //CN_ZX300,
        //[Description("创新成指")]
        //CN_CXCZ
    }

    [Description("玩法类型")]
    public enum PlayType
    {
        [Description("涨跌")] RiseFall = 0,
        [Description("单双")] SingleDouble = 1,
        [Description("尾号龙虎")] TailNo = 2,
        [Description("周跌天数")] WeekRiseFall = 3,
        [Description("大小")] Size = 4,

        [Description("周涨跌大小")] // >2.5 涨
        WeekRiseFallSize = 5,
        [Description("周涨天数")] WeekRiseDays = 6,
    }


    public enum PlayTypeItem
    {
        [Description("无")] NotSet = 99,
        [Description("零")] Zero = 0,
        [Description("一")] One = 1,
        [Description("二")] Two = 2,
        [Description("三")] Tree = 3,
        [Description("四")] Four = 4,
        [Description("五")] Five = 5,
        [Description("六")] Six = 6,
        [Description("七")] Seven = 7,
        [Description("八")] Eight = 8,
        [Description("九")] Nine = 9,
        [Description("十")] Ten = 10,
        [Description("单")] Single = 11,
        [Description("双")] Double = 12,
        [Description("涨")] Rise = 13,
        [Description("跌")] Fall = 14,
        [Description("大")] Large = 15,
        [Description("小")] Small = 16,
        [Description("红")] Red = 17,
        [Description("绿")] Green = 18
    }
}