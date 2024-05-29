using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Merchants
{
    /// <summary>
    /// Aaron
    /// 2021-02-26 13:53:38
    /// 
    /// </summary>
    public partial class SectionKey
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }


        /// <summary>
        /// 商户ID
        /// </summary>
        [Required]
        [MaxLength(10)]
        public int MerchantId { get; set; }

        /// <summary>
        /// Key类型 手机/PC
        /// </summary>
        [Required]
        [MaxLength(3)]
        public KeyType Type { get; set; }

        /// <summary>
        /// Multi / Banner
        /// </summary>
        [Required]
        [MaxLength(3)]
        public KeyDetailType DetailType { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String SKey { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String Description { get; set; }


    }
}
