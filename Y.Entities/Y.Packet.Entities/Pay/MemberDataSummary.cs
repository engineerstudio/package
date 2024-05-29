using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Pay
{
	/// <summary>
	/// Aaron
	/// 2021-06-08 16:33:20
	/// ����ƽ̨����ʱ�����ͳ�Ƶ�����
	/// </summary>
	public partial class MemberDataSummary
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
		///  
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal PaymentTotal {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal WithdrawalTotal {get;set;}


	}
}
