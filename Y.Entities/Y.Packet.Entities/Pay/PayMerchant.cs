using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Pay
{
    /// <summary>
    /// Aaron
    /// 2020-09-30 16:19:19
    /// 商户配置的支付
    /// </summary>
    public partial class PayMerchant
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        /// 商户Id
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MerchantId { get; set; }

        /// <summary>
        /// 支付类别Id， PayCategory， 就是支付的上级。 总后台配置 比如  一本支付，晴晴支付
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 PayCategoryId { get; set; }

        /// <summary>
        /// 类名字段，支付类
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String PayCategory { get; set; }

        /// <summary>
        /// 商户支付类别Id  微信 /  支付宝
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 PayTypeId { get; set; }

        /// <summary>
        /// 支付名称
        /// </summary>
        [Required]
        [MaxLength(12)]
        public String Name { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean Enabled { get; set; }

        /// <summary>
        /// 配置字符串
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public String ConfigStr { get; set; }

        /// <summary>
        /// 验证信息
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public string ValidationStr { get; set; }

        [Required]
        [MaxLength(256)]
        public string Img { get; set; }

        [Required]
        [MaxLength(256)]
        public string Desc { get; set; }

    }
}
