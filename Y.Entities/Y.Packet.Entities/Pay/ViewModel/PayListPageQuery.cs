using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Pay.ViewModel
{
    public class PayListPageQuery : PageModel
    {
        public int MerchantId { get; set; }
        public string Name { get; set; }

    }
}
