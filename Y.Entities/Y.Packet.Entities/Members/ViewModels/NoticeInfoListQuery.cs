using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Members.ViewModels
{
    public class NoticeInfoListQuery : PageModel
    {
        public int MerchantId { get; set; }
        public int MemberId { get; set; }
    }
}
