using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Members
{
	/// <summary>
	/// Aaron
	/// 2023-01-09 17:58:29
	/// 
	/// </summary>
	public partial class AgentShareProfitLogs
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
		public Int32 MerchantId {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 MemberId {get;set;}

		/// <summary>
		/// 会员总输金额
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal GameLossTotal {get;set;}

		/// <summary>
		/// 费率
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal Rate {get;set;}

		/// <summary>
		/// 会员分红
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal Agentbonus {get;set;}

		/// <summary>
		/// 统计的开始日期
		/// </summary>
		[Required]
		[MaxLength(18)]
		public DateTime StartDate {get;set;}

		/// <summary>
		/// 统计的结束日期
		/// </summary>
		[Required]
		[MaxLength(18)]
		public DateTime EndDate {get;set;}

		/// <summary>
		/// 备注
		/// </summary>
		[Required]
		[MaxLength(128)]
		public String Des {get;set;}

		/// <summary>
		/// 创建时间
		/// </summary>
		[Required]
		[MaxLength(10)]
		public DateTime CreateAt {get;set;}

		/// <summary>
		/// 0 审核种 1 已下发
		/// </summary>
		[Required]
		[MaxLength(3)]
		public AgentPayLogStatus Status {get;set;}


	}
}
