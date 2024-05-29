////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //
//                         O\  =  /O                              //
//                      ____/`---'\____                           //
//                    .'  \\|     |//  `.                         //
//                   /  \\|||  :  |||//  \                        //
//                  /  _||||| -:- |||||-  \                       //
//                  |   | \\\  -  /// |   |                       //
//                  | \_|  ''\---/''  |   |                       //
//                  \  .-\__  `-`  ___/-. /                       //
//                ___`. .'  /--.--\  `. . ___                     //
//              ."" '<  `.___\_<|>_/___.'  >'"".                  //
//            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
//            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
//      ========`-.____`-.___\_____/___.-`____.-'========         //
//                           `=---='                              //
//      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
//                   佛祖保佑       永不宕机     永无BUG          //
////////////////////////////////////////////////////////////////////

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：Aaron                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-08-24 02:23:05                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Y.Packet.Entities.Merchants                                  
*│　类    名：SysMenu                                     
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Merchants
{
	/// <summary>
	/// Aaron
	/// 2020-08-24 02:23:05
	/// 
	/// </summary>
	public partial class SysMenu
	{
		/// <summary>
		///  
		/// </summary>
		[Key]
		public Int32 Id {get;set;}

		/// <summary>
		/// 父菜单ID
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 ParentId {get;set;}

		/// <summary>
		/// 名称
		/// </summary>
		[Required]
		[MaxLength(32)]
		public String Name {get;set;}

		/// <summary>
		/// 显示名称
		/// </summary>
		[Required]
		[MaxLength(32)]
		public String DisplayName {get;set;}

		/// <summary>
		/// 图标地址
		/// </summary>
		[Required]
		[MaxLength(128)]
		public String IconClass {get;set;}

		/// <summary>
		/// 链接地址
		/// </summary>
		[Required]
		[MaxLength(128)]
		public String LinkUrl {get;set;}

		/// <summary>
		/// 排序数字
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 Sort {get;set;}

		/// <summary>
		/// 操作权限（按钮权限时使用）
		/// </summary>
		[Required]
		[MaxLength(2048)]
		public String Permission {get;set;}

		/// <summary>
		/// 是否显示
		/// </summary>
		[Required]
		[MaxLength(1)]
		public Boolean IsDisplay {get;set;}

		/// <summary>
		/// 是否系统默认
		/// </summary>
		[Required]
		[MaxLength(1)]
		public Boolean IsSystem {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(1)]
		public Boolean IsDelete {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 ModifyId {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(23)]
		public DateTime ModifyTime {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 CreatedId {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(23)]
		public DateTime CreatedTime {get;set;}


	}
}
