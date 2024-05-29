using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Infrastructure.Entities.RequestModel
{
    public class AgentMembersReq : PageModel
    {

        public string MemberName { get; set; }
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public string MemberType { get; set; }
    }
}
