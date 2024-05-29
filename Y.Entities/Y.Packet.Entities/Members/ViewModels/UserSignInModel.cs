using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Members.ViewModels
{
    public class UserSignInModel
    {
        public int MerchantId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ValidCode { get; set; }

    }
}
