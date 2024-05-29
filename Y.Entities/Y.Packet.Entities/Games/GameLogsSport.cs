using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Packet.Entities.Games
{
    /// <summary>
    /// Aaron
    /// 2021-01-30 16:06:24
    /// 
    /// </summary>
    public partial class GameLogsSport
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
        public GameCategory GameCategory { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(18)]
        public String GameTypeStr { get; set; }

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
        ///  
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String PlayerName { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String SourceId { get; set; }

        /// <summary>
        ///  是否串关
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsSeries { get; set; }

        /// <summary>
        /// 串关内容
        /// </summary>
        [Required]
        public string SeriesInfo { get; set; }


        /// <summary>
        /// 事件 篮球 足球等类别
        /// </summary>
        [Required]
        [MaxLength(18)]
        public string Event { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(128)]
        public String LeagueName { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(128)]
        public String MatchName { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(128)]
        public String BetItem { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String BetContent { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String Results { get; set; }

        /// <summary>
        /// 下注金额
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
        /// 奖金 / 盈亏
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal Money { get; set; }

        /// <summary>
        /// 派奖金额
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
        /// 订单派奖时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime SourceOrderAwardTime { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime OrderCreateTimeUtc8 { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime OrderAwardTimeUtc8 { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [Required]
        [MaxLength(3)]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// 滚球/早盘
        /// </summary>
        [Required]
        [MaxLength(3)]
        public Stage Stage { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal SourceOdds { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(3)]
        public OddsType SourceOddsType { get; set; }

        /// <summary>
        /// 欧赔
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal DEOdds { get; set; }

        /// <summary>
        /// 订单在平台创建时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 订单最后更新时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 包网平台结算时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime SettlementTime { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(2048)]
        public String Raw { get; set; }


    }
}
