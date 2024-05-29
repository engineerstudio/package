using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Members
{
	/// <summary>
	/// Aaron
	/// 2021-04-12 23:13:48
	/// 
	/// </summary>
	public partial class AgentDailyReportStatistic
	{
		/// <summary>
		///  
		/// </summary>
		[Key]
		public Int32 Id {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(10)]
		public DateTime Date {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 MerchantId {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 MemberId {get;set;}

		/// <summary>
		/// 充值金额
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal Pay {get;set;}
		/// <summary>
		/// 充值订单数量
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 PayNo { get; set; }

		/// <summary>
		/// 出款金额
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal Withdrawals {get;set;}

		/// <summary>
		/// 取款订单数量
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 WithdrawalsNo { get; set; }


		/// <summary>
		/// 投注金额
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal GameBetAmount {get;set;}

		/// <summary>
		/// 有效投注
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal GameValidBet {get;set;}

		/// <summary>
		/// 游戏盈亏
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal GameLoss {get;set;}

		/// <summary>
		/// 活动金额
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal PromoMoney {get;set;}

	}
}
