using System.Collections.Generic;

namespace Y.Infrastructure.Library.Core.WebInfrastructure.Entity
{
    public class AccountInfo
    {
        public int MerchantId { get; set; }
        public int AccountId { get; set; }
        public int RoleId { get; set; }
        public string AccountName { get; set; }
        public string NickName { get; set; }
        public List<string> Permissions { get; set; }
    }
}