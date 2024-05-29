using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Pay
{
    /// <summary>
    /// Aaron
    /// 2020-09-30 16:19:19
    /// 支付的类别  比如 晴晴支付(支付宝) ，晴晴支付(微信)
    /// </summary>
    public partial class PayCategory
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        /// 类名字段，支付类
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String PayType { get; set; }

        /// <summary>
        /// 支付类的描述字段
        /// </summary>
        [Required]
        [MaxLength(12)]
        public String Name { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean Enabled { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(2048)]
        public String ConfigStr { get; set; }

        /// <summary>
        /// 回调加白IP
        /// </summary>
        [Required]
        [MaxLength(128)]
        public String IpWhiteList { get; set; }


        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsVirtualCurrency { get; set; }

    }
}
