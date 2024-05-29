using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Pay
{
    /// <summary>
    /// Aaron
    /// 2021-01-10 19:22:45
    /// 
    /// </summary>
    public partial class WithdrawMerchant
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
        [MaxLength(32)]
        public string TypeStr { get; set; }
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
        [MaxLength(16)]
        public String Name { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean Enabled { get; set; }


        /// <summary>
        ///  手动出款
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsDefault { get; set; }


        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public String ConfigStr { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(64)]
        public String Description { get; set; }


        [Required]
        [MaxLength(23)]
        public DateTime CreateTime { get; set; }


    }
}
