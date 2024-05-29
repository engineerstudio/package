using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Packet.Entities.Games
{
    /// <summary>
    /// Aaron
    /// 2020-09-08 23:59:35
    /// 游戏转账日志
    /// </summary>
    public partial class GameTransferLogs
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
        public Int32 UserId { get; set; }

        /// <summary>
        /// 游戏类型
        /// </summary>
        [Required]
        [MaxLength(18)]
        public string TypeStr { get; set; }

        /// <summary>
        /// 转账状态
        /// </summary>
        [Required]
        [MaxLength(3)]
        public TransferStatus Status { get; set; }

        /// <summary>
        /// 转账时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 转账金额
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal Money { get; set; }

        /// <summary>
        /// 转入/转出
        /// </summary>
        [Required]
        [MaxLength(3)]
        public TransType TransType { get; set; }

        /// <summary>
        /// 转账数据
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public String Raw { get; set; }


    }
}
