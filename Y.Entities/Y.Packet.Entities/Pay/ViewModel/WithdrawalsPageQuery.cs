using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Pay.ViewModel
{
    public class WithdrawalsPageQuery : PageModel
    {
        public int MerchantId { get; set; }
        public int Id { get; set; }
        public int MemberId { get; set; }

        public bool? IsFinished { get; set; }

        public string WithdrawMerchantOrderId { get; set; }
        public string AccountName { get; set; }

        public DateTime? WithdrawStartTime { get; set; }
        public DateTime? WithdrawEndTime { get; set; }


        public DateTime? FinishStartTime { get; set; }
        public DateTime? FinishEndTime { get; set; }

    }
}
