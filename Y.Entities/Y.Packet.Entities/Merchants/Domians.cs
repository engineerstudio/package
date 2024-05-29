using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Merchants
{
    /// <summary>
    /// Aaron
    /// 2021-04-14 13:42:14
    /// 
    /// </summary>
    public partial class Domains
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
        ///域名类型
        /// </summary>
        [Required]
        [MaxLength(3)]
        public DomainsType DoType { get; set; }

        /// <summary>
        /// 域名名称
        /// </summary>
        [Required]
        [MaxLength(64)]
        public String Name { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean Enabled { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsHttps { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String Marks { get; set; }


    }
}
