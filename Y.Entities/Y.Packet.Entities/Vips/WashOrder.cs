using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Vips
{
    /// <summary>
    /// Aaron
    /// 2021-05-05 18:55:02
    /// 
    /// </summary>
    public partial class WashOrder
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        [Required]
        [MaxLength(10)]
        public int MemberId { get; set; }

        /// <summary>
        /// 资金类型
        /// </summary>
        [Required]
        [MaxLength(12)]
        public String FundsType { get; set; }

        /// <summary>
        /// 账变金额
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal Amount { get; set; }

        /// <summary>
        /// 账变对应的流水金额
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal WashAmount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String Mark { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否结束
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean Ended { get; set; }


    }
}
