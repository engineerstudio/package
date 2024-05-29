using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Promotions.ViewModels
{
    public class ActivityOrderSummary
    {

        public int UserId { get; set; }
        public int MerchantId { get; set; }
        /// <summary>
        /// 已派发金额
        /// </summary>
        public decimal Reward { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime RewardDate { get; set; }

    }
}
