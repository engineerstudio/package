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
            [Description("δ����")]
            None,
            [Description("֧����")]
            AliPay,
            [Description("΢��")]
            WechatPay,
            [Description("����")]
            Bank,
            [Description("������")]
            YunShanFu
        }

    }
}
