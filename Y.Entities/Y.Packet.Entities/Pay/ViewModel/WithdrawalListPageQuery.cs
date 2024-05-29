using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Pay.ViewModel
{
    public class WithdrawalListPageQuery : PageModel
    {
        public int MerchantId { get; set; }
    }
}
