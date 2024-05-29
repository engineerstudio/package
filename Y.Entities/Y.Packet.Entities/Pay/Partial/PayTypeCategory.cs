using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Pay
{
    /// <summary>
    /// Aaron
    /// 2020-10-04 19:55:59
    /// 
    /// </summary>
    public partial class PayTypeCategory
    {

        public enum PayType
        {
            [Description("未定义")]
            None,
            [Description("支付宝")]
            AliPay,
            [Description("微信")]
            WechatPay,
            [Description("银行")]
            Bank,
            [Description("云闪付")]
            YunShanFu
        }

    }
}
