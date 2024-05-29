using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Games.ViewModels
{
    public class GameLogsQuery : PageModel
    {
        public int MerchantId { get; set; }
        public string GameCategory { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
    }
}
