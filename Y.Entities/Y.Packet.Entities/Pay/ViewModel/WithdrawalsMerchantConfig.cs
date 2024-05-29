using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Pay.ViewModel
{
    public class WithdrawalsMerchantConfig
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 支付配置
        /// </summary>
        public string ConfigStr { get; set; }

        public int Id { get; set; }

        /// <summary>
        /// 支付名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 支付渠道/类别的ID
        /// </summary>
        public string PayCategory { get; set; }
        /// <summary>
        ///  是否开启
        /// </summary>
        public bool Enabled { get; set; }

    }
}
