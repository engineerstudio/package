using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Games
{
	/// <summary>
	/// Aaron
	/// 2020-09-08 23:59:35
	/// 游戏日志表
	/// </summary>
	public partial class GameDic
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
		[MaxLength(3)]
		public Byte Type {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(32)]
		public String keys {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(128)]
		public String vals {get;set;}


	}
}
