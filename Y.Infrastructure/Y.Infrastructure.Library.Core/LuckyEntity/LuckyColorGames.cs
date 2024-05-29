using System.Collections.Generic;
using System.ComponentModel;

namespace Y.Infrastructure.Library.Core.LuckyEntity
{
    public partial class LuckyColorGames
    {
        public class Game
        {
            public string Issue { get; set; }
            public List<GameBets> GameBets { get; set; }
        }

        public class GameBets
        {
            public string Name { get; set; }
            public List<GameBetsItems> GameBetsItems { get; set; }
        }


        public class GameBetsItems
        {
            public PlayTypeItem Name { get; set; }
            public string Odds { get; set; }
        }
    }


    public partial class LuckyColorGames
    {
        public Game GetGames()
        {
            List<GameBetsItems> ll = new List<GameBetsItems>()
            {
                new GameBetsItems()
                {
                    Name = PlayTypeItem.Red,
                    Odds = "1.92"
                },
                new GameBetsItems()
                {
                    Name = PlayTypeItem.Green,
                    Odds = "1.92"
                }
            };
            GameBets gb = new GameBets()
            {
                Name = "涨涨分分乐",
                GameBetsItems = ll
            };
            Game game = new Game();
            game.GameBets.Add(gb);
            return game;
        }
    }


    public enum AwardStrategy
    {
        [Description("随机模式")] FreeMode,
        [Description("赢奖金模式")] BonusMode,
        [Description("偏差模式")] DeviationMode
    }
}