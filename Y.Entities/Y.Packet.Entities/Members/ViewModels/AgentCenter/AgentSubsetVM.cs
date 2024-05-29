using System;
using System.Collections.Generic;
using System.Text;
using static Y.Packet.Entities.Members.Users;

namespace Y.Packet.Entities.Members.ViewModels.AgentCenter
{
    public class AgentSubsetVM
    {
        public int Id { get; set; }

        public UserType Type { get; set; }

        public decimal Balance { get; set; }

        public string GroupName { get; set; }
    }
}
