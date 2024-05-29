using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Members.ViewModels.AgentCenter
{
    public class AgentSubsetQuery : PageModel
    {

        public int MerchantId { get; set; }
        public int MemberId { get; set; }



    }
}
