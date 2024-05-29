using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Merchants.ViewModels
{
    public class NoticeListQuery : PageModel
    {
        public int MerchantId { get; set; }
        public int Type { get;set; }
    }
}
