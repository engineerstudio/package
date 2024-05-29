using System.ComponentModel;

namespace Y.Infrastructure.Library.Core.LuckyEntity
{
    public class LuckyGames
    {
        public enum GamesStatus
        {
            [Description("未开启")] NotSet = 0,
            [Description("进行中")] Normal = 1,
            [Description("结算中")] Settlement = 2,
            [Description("已结束")] Finished = 3
        }
    }


    public enum LanguageType
    {
        [Description("中文")] CN = 0,
        [Description("英文")] EN = 1
    }
}