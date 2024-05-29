using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Pay.ViewModel
{
    public class PayMerchantConfig
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
        public int PayCategory { get; set; }
        /// <summary>
        ///  是否开启
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 支付类别   微信/支付宝
        /// </summary>
        public int PayType { get; set; }
        /// <summary>
        /// 支付验证字符串
        /// </summary>
        public string ValidationStr { get; set; }
        public string Img { get; set; }
        public string Desc { get; set; }
    }
}
