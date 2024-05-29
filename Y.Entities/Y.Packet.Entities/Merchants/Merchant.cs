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
*│　类    名：Merchant                                     
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
    public partial class Merchant
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        /// 站点名称
        /// </summary>
        [Required]
        [MaxLength(12)]
        public String Name { get; set; }

        /// <summary>
        /// 游戏分数
        /// </summary>
        [Required]
        [MaxLength(10)]
        public decimal GameCredit { get; set; }

        /// <summary>
        /// 站点状态
        /// </summary>
        [Required]
        [MaxLength(3)]
        public MerStatus Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 页面配置
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public string PageSectionConfig { get; set; }

        /// <summary>
        /// VIP分组配置
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public string VipsConfig { get; set; }

        /// <summary>
        /// 登陆域名设置
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string Domains { get; set; }

        /// <summary>
        /// 后台加白IP
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public string IpWhitelist { get; set; }


        [Required]
        [MaxLength(32)]
        public string PcTempletStr { get; set; }

        [Required]
        [MaxLength(32)]
        public string H5TempletStr { get; set; }

        /// <summary>
        /// 登录页面配置
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public string SignupConfig { get; set; }


        /// <summary>
        /// 客户站点配置
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public string CustomerConfig { get; set; }

    }
}
