using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Pay.ViewModel
{
    public class ManualFunds
    {
        //  Y.Packet.Entities.Members.FundLogType 
        public string FundsType { get; set; }
        public string FundsSubType { get; set; }
        public string Name { get; set; }
        public decimal Money { get; set; }
        public string Marks { get; set; }

        public string FundsTransType { get; set; }

    }
}
