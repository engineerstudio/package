using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Promotions
{
    /// <summary>
    /// Aaron
    /// 2021-05-06 07:38:35
    /// 
    /// </summary>
    public partial class SignInLog
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MerchantId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MemberId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(19)]
        public DateTime CreateTime { get; set; }

    }
}
