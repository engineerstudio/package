using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Members.ViewModels
{
    public class UserRegisterModel
    {

        public Int32 MerchantId { get; set; }
        /// <summary>
        /// 账户名称
        /// </summary>
        public String AccountName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public String Pasw { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string IdName { get; set; }
        /// <summary>
        /// 资金密码
        /// </summary>
        public String FPasw { get; set; }
        public Int32 AgentId { get; set; }
        public String Mobile { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidCode { get; set; }
        /// <summary>
        /// 代理Code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 注册URL
        /// </summary>
        public string RegisterUrl { get; set; }
        /// <summary>
        /// 默认用户组
        /// </summary>
        public int DefaultGroupId { get; set; }

    }
}
