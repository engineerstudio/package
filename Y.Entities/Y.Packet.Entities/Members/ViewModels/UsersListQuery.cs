using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;
using static Y.Packet.Entities.Members.Users;

namespace Y.Packet.Entities.Members.ViewModels
{
    public class UsersListQuery : PageModel
    {
        public int MerchantId { get; set; }
        public int Id { get; set; }

        public string AccountName { get; set; }

        public int GroupId { get; set; }

        public string MemberType { get; set; }
        public int? AgentId { get;set; }

        public DateTime? RegisterAt { get; set; }
        public DateTime? RegisterEnd { get; set; }
    }
}
