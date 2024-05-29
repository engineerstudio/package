using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Pay
{
	/// <summary>
	/// Aaron
	/// 2020-09-30 16:19:19
	/// 
	/// </summary>
	public partial class PayCallbackLog
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
		/// 平台订单ID
		/// </summary>
		[Required]
		[MaxLength(32)]
		public String SysOrderId {get;set;}

		/// <summary>
		/// 第三方平台返回的订单Id
		/// </summary>
		[Required]
		[MaxLength(32)]
		public String PlatformOrderId {get;set;}

		/// <summary>
		/// 请求内容
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 Contents {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(23)]
		public DateTime CreateTme {get;set;}


	}
}
