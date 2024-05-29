using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Vips.ViewModels
{
    public class WashOrderListQuery : PageModel
    {
        public int MerchantId { get; set; }
        public int? MemberId { get; set; }
    }
}
