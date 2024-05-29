using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Packet.Entities.Members
{
    /// <summary>
    /// Aaron
    /// 2020-08-30 15:00:55
    /// 用户资金日志
    /// </summary>
    public partial class UsersFundsLog
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
        /// 账变金额
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal Amount { get; set; }

        /// <summary>
        /// 锁定金额
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal LockedAmount { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal Balance { get; set; }

        /// <summary>
        /// 账变类型
        /// </summary>
        [Required]
        [MaxLength(3)]
        public FundLogType FundsType { get; set; }

        /// <summary>
        /// 账变类型之子类型
        /// </summary>
        [Required]
        [MaxLength(3)]
        public byte SubFundsType { get; set; }

        /// <summary>
        /// 转入转出
        /// </summary>
        [Required]
        [MaxLength(3)]
        public TransType TransType { get; set; }

        /// <summary>
        /// 来源ID
        /// </summary>
        [Required]
        [MaxLength(64)]
        public String SourceId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(20)]
        public String IP { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(64)]
        public String Marks { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        [Required]
        [MaxLength(16)]
        public DateTime CreateTime { get; set; }

    }
}
