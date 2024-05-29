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
    public partial class WashOrderDetail
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        ///  会员ID
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MemberId { get; set; }

        /// <summary>
        ///  MemberId
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 OrderId { get; set; }

        /// <summary>
        /// 有效投注金额 账变金额
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal Amount { get; set; }

        /// <summary>
        /// 未完成的总流水
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal Balance { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [MaxLength(18)]
        public String Mark { get; set; }

        /// <summary>
        /// 打码数据的Id
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string SourceOrderId { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime CreateTime { get; set; }




    }
}
