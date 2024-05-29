using System;
using System.Collections.Generic;
using System.Text;
using static Y.Packet.Entities.Merchants.Domains;

namespace Y.Packet.Entities.Merchants.ViewModels
{
    public class DomiansInsertOrEditViewModel
    {
        public Int32 Id { get; set; }
        public Int32 MerchantId { get; set; }
        public String Name { get; set; }
        public DomainsType DoType { get; set; }
        public Boolean Enabled { get; set; }
        public Boolean IsHttps { get; set; }
        public String Marks { get; set; }
    }
}
