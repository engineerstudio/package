using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Promotions.ViewModels
{
    public class ActivityOrderAddModel
    {
        public int MerchantId { get; set; }
        public string AType { get; set; }
        public int ProId { get; set; }
        public int MemberId { get; set; }
        public Decimal Reward { get; set; }
        public string IP { get; set; }
        public string Description { get; set; }
    }
}
