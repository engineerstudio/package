using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Games
{
	/// <summary>
	/// Aaron
	/// 2020-09-08 23:59:35
	/// 
	/// </summary>
	public partial class GameDailyReportStatisticByGameRules
	{
		/// <summary>
		///  
		/// </summary>
		[Key]
		public Int32 Id {get;set;}

		/// <summary>
		/// 统计日期
		/// </summary>
		[Required]
		[MaxLength(10)]
		public DateTime Date {get;set;}

		/// <summary>
		/// 游戏类型
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 Type {get;set;}

		/// <summary>
		/// 站点ID
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 SiteId {get;set;}

		/// <summary>
		/// 玩家名称
		/// </summary>
		[Required]
		[MaxLength(32)]
		public String PlayerName {get;set;}

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


	}
}
