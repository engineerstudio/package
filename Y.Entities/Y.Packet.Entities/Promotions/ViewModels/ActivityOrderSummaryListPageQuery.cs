using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Promotions.ViewModels
{
    public class ActivityOrderSummaryListPageQuery: PageModel
    {
        public int Id { get; set; }// 活动Id
        public DateTime CreateDateStartAt { get; set; }

        public DateTime CreateDateEndAt { get; set; }

        public DateTime RewardDateStartAt { get; set; }

        public DateTime RewardDateEndAt { get; set; }
    }
}
