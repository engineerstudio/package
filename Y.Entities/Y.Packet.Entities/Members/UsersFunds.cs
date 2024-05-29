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
*│　描    述：用户资金                                                    
*│　作    者：Aaron                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-08-30 15:00:55                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Y.Users.Entity                                  
*│　类    名：UsersFunds                                     
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Members
{
	/// <summary>
	/// Aaron
	/// 2020-08-30 15:00:55
	/// 用户资金
	/// </summary>
	public partial class UsersFunds
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
		public Int32 UserId {get;set;}

		/// <summary>
		/// 账户金额, 包含锁定
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal TotalFunds {get;set;}

		/// <summary>
		/// 苏定资金
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal LockFunds {get;set;}

		/// <summary>
		/// 总充值金额
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal TotalRechargedFunds {get;set;}

		/// <summary>
		/// 总充值笔数
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 TotalRechargedFundsCount {get;set;}

		/// <summary>
		///  
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal TotalWithdrawalFunds {get;set;}

		/// <summary>
		/// 用户总提现笔数目
		/// </summary>
		[Required]
		[MaxLength(10)]
		public Int32 TotalWithdrawalCount {get;set;}

		/// <summary>
		/// 总投注金额
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal TotalBetFunds {get;set;}

		/// <summary>
		/// 用户总盈亏
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal TotalProfitAndLoss {get;set;}

		/// <summary>
		/// 活动奖金
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal PromotionsFunds {get;set;}

		/// <summary>
		/// 包含  佣金/反水/活动/饭店/奖励
		/// </summary>
		[Required]
		[MaxLength(19)]
		public Decimal OtherFunds {get;set;}


	}
}
