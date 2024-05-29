using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Games.ViewModels.Sys
{
    public class GameListQuery : PageModel
    {
        public int MerchantId { get; set; }
        public string GameCategory { get; set; }
    }
}
