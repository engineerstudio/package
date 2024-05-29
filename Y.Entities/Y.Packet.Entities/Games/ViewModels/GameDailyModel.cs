using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Games.ViewModels
{
    public class GameDailyModel
    {
        public string Date { get; set; }
        public int MemberId { get; set; }
        public int MerchantId { get; set; }
        public string GameTypeStr { get; set; }
        public decimal BetAmount { get; set; }
        public decimal ValidBet { get; set; }
        public decimal Money { get; set; }
        public decimal AwardAmount { get; set; }
    }
}
