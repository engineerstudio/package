using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Promotions.ViewModels
{
    public class ActivityOrdersListPageQuery : PageModel
    {
        public int OrderId { get; set; }
        public int MerchantId { get; set; }
        public string AccountName { get; set; }
        public int PromotionId { get; set; }
        public int MemberId { get; set; }
        public string AType { get; set; }

        public string Status { get; set; }

    }
}
