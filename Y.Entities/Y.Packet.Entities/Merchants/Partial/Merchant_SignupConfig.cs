using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Merchants
{
    public class Merchant_SignupConfig
    {
        /// <summary>
        /// 开启验证码验证
        /// </summary>
        public bool EnableValidCode { get; set; }


        /// <summary>
        /// 开启代理模式，无代理验证码无法注册
        /// </summary>
        public bool EnableCode { get; set; }


        /// <summary>
        /// 开启支付密码验证
        /// </summary>
        public bool EnableFPasw { get; set; }


        /// <summary>
        /// 开启手机验证
        /// </summary>
        public bool EnablePhone { get; set; }


        /// <summary>
        /// 开启实名认证
        /// </summary>
        public bool EnableIdName { get; set; }

    }
}
