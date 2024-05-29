using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Packet.Entities.Games
{
    /// <summary>
    /// Aaron
    /// 2020-09-08 23:59:35
    /// 投注日志表
    /// </summary>
    public partial class GameLogs
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
        [MaxLength(3)]
        public GameCategory GameCategory { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(18)]
        public string GameTypeStr { get; set; }

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
        [MaxLength(32)]
        public String PlayerName { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(64)]
        public String SourceId { get; set; }


        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(3)]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// 投注金额
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal BetAmount { get; set; }

        /// <summary>
        /// 有效投注
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal ValidBet { get; set; }

        /// <summary>
        /// 盈亏
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal Money { get; set; }

        /// <summary>
        /// 盈亏
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal AwardAmount { get; set; }

        /// <summary>
        /// 订单创建时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime SourceOrderCreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime SourceOrderAwardTime { get; set; }

        /// <summary>
        /// 投注时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime OrderCreateTimeUtc8 { get; set; }

        /// <summary>
        /// 派奖时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime OrderAwardTimeUtc8 { get; set; }


        /// <summary>
        /// 平台创建时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime CreateTimeUtc8 { get; set; }

        /// <summary>
        /// 平台结算时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime SettlementTimeUtc8 { get; set; }


    }
}
