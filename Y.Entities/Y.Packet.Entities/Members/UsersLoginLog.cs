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
*│　描    述：用户登录日志                                                    
*│　作    者：Aaron                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-08-30 15:00:55                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Y.Users.Entity                                  
*│　类    名：UsersLoginLog                                     
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
    /// 用户登录日志
    /// </summary>
    public partial class UsersLoginLog
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MerchantId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 UserId { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        [Required]
        [MaxLength(16)]
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [Required]
        [MaxLength(20)]
        public String IP { get; set; }

        /// <summary>
        /// IP中文地址
        /// </summary>
        [Required]
        [MaxLength(64)]
        public String IPCn { get; set; }

        /// <summary>
        /// 是否最后登录
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsLastLogin { get; set; }

        /// <summary>
        /// 是否成功登录
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean Sucess { get; set; }


        /// <summary>
        /// 请求登录数据
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public String RawData { get; set; }


    }
}
