using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Infrastructure.Entities.RequestModel
{
    public class AgentDailyReportQuery : PageModel
    {
        public int MerchantId { get;set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
    }
}
