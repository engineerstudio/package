using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Merchants.ViewModels
{
    public class DomiansListQuery : PageModel
    {
        public int MerchantId { get; set; }

        public string DType { get; set; }

    }
}
