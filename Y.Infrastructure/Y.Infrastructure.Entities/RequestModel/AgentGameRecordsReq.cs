using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Infrastructure.Entities.RequestModel
{
    public class AgentGameRecordsReq:PageModel
    {
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
    }
}
