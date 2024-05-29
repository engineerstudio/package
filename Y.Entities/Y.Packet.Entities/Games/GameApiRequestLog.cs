using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Games
{
	/// <summary>
	/// Aaron
	/// 2021-04-15 19:06:56
	/// 
	/// </summary>
	public partial class GameApiRequestLog
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
		[MaxLength(32)]
		public String TypeStr {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		public String RequestData {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		public String ResultData {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(1)]
		public Boolean Status {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(19)]
		public DateTime CreateTime {get;set;}


	}
}
