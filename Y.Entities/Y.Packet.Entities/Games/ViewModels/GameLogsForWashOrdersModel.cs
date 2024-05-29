using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Games.ViewModels
{
    public class GameLogsForWashOrdersModel
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public int MemberId { get; set; }
        public decimal ValidBet { get; set; }
        public string GameTypeStr { get; set; }
    }
}
