using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Members.ViewModels.AgentCenter
{
    public class AgentReportQuery : PageModel
    {
        public int MerchantId { get; set; }
        public int MemberId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartAt { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndAt { get; set; }
    }
}
