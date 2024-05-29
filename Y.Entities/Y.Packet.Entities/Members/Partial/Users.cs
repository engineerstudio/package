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
*│　描    述：用户基本信息表                                                    
*│　作    者：Aaron                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-08-30 15:00:55                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间: Y.Users.Entity                                  
*│　类    名：Users                                     
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Y.Infrastructure.Library.Core.Extensions;

namespace Y.Packet.Entities.Members
{
    /// <summary>
    /// Aaron
    /// 2020-08-30 15:00:55
    /// 用户基本信息表
    /// </summary>
    public partial class Users
    {
        /// <summary>
        /// 用户类型
        /// </summary>
        public enum UserType
        {
            /// <summary>
            /// 未设定/删除的用户
            /// </summary>
            [Description("禁用用户")]
            None = 0,
            /// <summary>
            /// 用户
            /// </summary>
            [Description("普通会员")]
            User = 1,
            /// <summary>
            /// 代理
            /// </summary>
            [Description("代理")]
            Agent = 2
        }


        [NotMapped]
        public AgentSetting AgentSettingEntity
        {
            get
            {
                if (this.AgentSetting.IsNullOrEmpty()) return new AgentSetting();
                return this.AgentSetting.JsonStringToEntity<AgentSetting>();
            }
        }


    }
}
