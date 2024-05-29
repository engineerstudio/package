using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Packet.Entities.Members.ViewModels
{
    public class FundsLogQuery : PageModel
    {
        public int MerchantId { get; set; }

        public int MemberId { get; set; }

        public FundLogType? FundLogType { get; set; }

        public TransType? TransType { get; set; }

        public DateTime? StartAt { get; set; }

        public DateTime? EndAt { get; set; }
    }
}
