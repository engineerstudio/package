using System.ComponentModel;

namespace Y.Infrastructure.Library.Core.LuckyEntity.Enums
{
    /// <summary>
    /// 游戏类别
    /// </summary>
    public enum CateType
    {
        [Description("常规游戏")] NormalGames,
        [Description("上证分分乐")] MinutesGames,
        [Description("股市时时乐")] SlotsGames,
        [Description("奖池盘口")] JackpotGames,
        [Description("冠军盘口")] ChampionGames,
    }

    public enum MatchesCacheType
    {
        Today,
        Early,
        Jackpot,
        Champion,
        Minutes,
    }

    public enum GameBStatus
    {
        [Description("未开启")] NotSet = 0,
        [Description("进行中")] Normal = 1,
        [Description("结算中")] Settlement = 2,
        [Description("已结束")] Finished = 3
    }
}