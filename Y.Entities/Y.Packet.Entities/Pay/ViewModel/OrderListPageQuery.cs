using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Pay.ViewModel
{
    public class OrderListPageQuery : PageModel
    {
        public int MerchantId { get; set; }

        public string AccountName { get; set; }

        public int PayMerchantId { get; set; }

        public int PayTypeCategoryId { get; set; }


        public DateTime? DepositStartTime { get; set; }
        public DateTime? DepositEndTime { get; set; }



        public DateTime? FinishStartTime { get; set; }
        public DateTime? FinishEndTime { get; set; }



    }
}
