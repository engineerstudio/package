using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Members
{
	/// <summary>
	/// Aaron
	/// 2020-10-18 22:53:28
	/// 
	/// </summary>
	public partial class UsersSession
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
		public Int32 UserId {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(32)]
		public String Session {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(23)]
		public DateTime UpdateTime {get;set;}


	}
}
