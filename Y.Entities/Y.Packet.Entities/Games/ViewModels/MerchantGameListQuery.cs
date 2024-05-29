using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Games.ViewModels
{
    public class MerchantGameListQuery : PageModel
    {
        public int MerchantId { get; set; }
        public string? GameName { get; set; }
        public Boolean? Enabled { get; set; }
        public Boolean? SysEnabled { get; set; }
    }
}
