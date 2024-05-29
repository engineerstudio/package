using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;
using static Y.Packet.Entities.Merchants.NoticeArea;

namespace Y.Packet.Entities.Merchants.ViewModels
{
    public class NoticeAreaListQuery : PageModel
    {
        public int MerchantId { get; set; }
        public NoticeType? Type { get; set; }
    }
}
