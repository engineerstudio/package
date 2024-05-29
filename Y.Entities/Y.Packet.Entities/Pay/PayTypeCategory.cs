using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Pay
{
    /// <summary>
    /// 支付分类类别   支付宝/微信/云闪付   等等
    /// 
    /// 
    /// </summary>
    public partial class PayTypeCategory
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }


        [Required]
        [MaxLength(10)]
        public Int32 MerchantId { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [Required]
        [MaxLength(12)]
        public string Name { get; set; }

        /// <summary>
        /// 类别图片地址
        /// </summary>
        [Required]
        [MaxLength(128)]
        public String PicUrl { get; set; }

        /// <summary>
        /// 是否虚拟币
        /// </summary>
        [Required]
        [MaxLength(1)]
        public bool IsVirtualCurrency { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        [MaxLength(1)]
        public bool Enabled { get; set; }
    }
}
