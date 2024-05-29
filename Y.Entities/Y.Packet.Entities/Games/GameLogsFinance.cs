using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Packet.Entities.Games
{
	/// <summary>
	/// Aaron
	/// 2021-12-27 14:37:53
	/// 
	/// </summary>
	public partial class GameLogsFinance
	{
		/// <summary>
		///  
		/// </summary>
		[Key]
		public Int32 Id {get;set;}

		/// <summary>
		/// 游戏类别
		/// </summary>
		[Required]
		[MaxLength(3)]
		public Byte GameCategory {get;set;}

		/// <summary>
		/// 游戏
		/// </summary>
		[Required]
		[MaxLength(18)]
		public String GameTypeStr {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 MerchantId {get;set;}

		/// <summary>
		/// 用户ID
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 UserId {get;set;}

		/// <summary>
		/// 游戏用户名称
		/// </summary>
		[Required]
		[MaxLength(32)]
		public String PlayerName {get;set;}

		/// <summary>
		/// 来源Id
		/// </summary>
		[Required]
		[MaxLength(32)]
		public String SourceId {get;set;}

		/// <summary>
		/// 投注比赛ID
		/// </summary>
		[Required]
		[MaxLength(128)]
		public String MatchName {get;set;}

		/// <summary>
		/// 投注项
		/// </summary>
		[Required]
		[MaxLength(50)]
		public String BetItem {get;set;}

		/// <summary>
		/// 投注内容
		/// </summary>
		[Required]
		[MaxLength(50)]
		public String BetContent {get;set;}

		/// <summary>
		/// 开奖结果
		/// </summary>
		[Required]
		[MaxLength(50)]
		public String Results {get;set;}

		/// <summary>
		/// 投注金额
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal BetAmount {get;set;}

		/// <summary>
		/// 有效投注
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal ValidBet {get;set;}

		/// <summary>
		/// 盈亏
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal Money {get;set;}

		/// <summary>
		/// 派奖金额
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal AwardAmount {get;set;}

		/// <summary>
		/// 订单创建时间
		/// </summary>
		[Required]
		[MaxLength(19)]
		public DateTime SourceOrderCreateTime {get;set;}

		/// <summary>
		/// 订单派奖时间
		/// </summary>
		[Required]
		[MaxLength(19)]
		public DateTime SourceOrderAwardTime {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(19)]
		public DateTime OrderCreateTimeUtc8 {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(19)]
		public DateTime OrderAwardTimeUtc8 {get;set;}

		/// <summary>
		/// 订单状态
		/// </summary>
		[Required]
		[MaxLength(3)]
		public Byte Status {get;set;}

		/// <summary>
		/// 1.早盘 2.滚球 3.其它
		/// </summary>
		[Required]
		[MaxLength(3)]
		public Stage Stage {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal SourceOdds {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(3)]
		public Byte SourceOddsType {get;set;}

		/// <summary>
		/// 欧赔
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal DEOdds {get;set;}

		/// <summary>
		/// 订单在平台创建时间
		/// </summary>
		[Required]
		[MaxLength(19)]
		public DateTime CreateTime {get;set;}

		/// <summary>
		/// 订单最后更新时间
		/// </summary>
		[Required]
		[MaxLength(19)]
		public DateTime UpdateTime {get;set;}

		/// <summary>
		/// 包网平台结算时间
		/// </summary>
		[Required]
		[MaxLength(19)]
		public DateTime SettlementTime {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		public String Raw {get;set;}


	}
}
