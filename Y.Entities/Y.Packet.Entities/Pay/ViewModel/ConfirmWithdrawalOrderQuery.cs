using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Pay.ViewModel
{
    public class ConfirmWithdrawalOrderQuery
    {
        public int MerchantId { get; set; }
        public int MemberId { get; set; }

        public string Marks { get; set; }

        public int OrderId { get; set; }

        public int WithdrawMerchantId { get; set; }

    }
}
