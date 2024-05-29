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
*│　类    名：SysAccount                                     
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
    public partial class SysAccount
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }


        /// <summary>
        /// 商户ID
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MerchantId { get; set; }


        /// <summary>
        /// 角色ID
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 RoleId { get; set; }

        /// <summary>
        /// 账户名称
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String Name { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String Password { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Required]
        [MaxLength(256)]
        public String Avatar { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String NickName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        [MaxLength(16)]
        public String Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Required]
        [MaxLength(128)]
        public String Email { get; set; }

        /// <summary>
        /// 上次登录IP
        /// </summary>
        [Required]
        [MaxLength(64)]
        public String LastLoginIp { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsLock { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsDelete { get; set; }

        /// <summary>
        /// 账户备注
        /// </summary>
        [Required]
        [MaxLength(128)]
        public String Remark { get; set; }

        /// <summary>
        /// 创建该账户的ID
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 CreatedId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime CreatedTime { get; set; }


    }
}
