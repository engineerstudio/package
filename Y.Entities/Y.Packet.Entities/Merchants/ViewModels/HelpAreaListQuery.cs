using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Merchants
{
    public class HelpAreaListQuery : PageModel
    {
        public int MerchantId { get; set; }
        public string Title { get; set; }
        public bool? ShowIndexPage { get; set; }
    }
}
