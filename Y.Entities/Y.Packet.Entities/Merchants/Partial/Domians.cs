using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Merchants
{
    /// <summary>
    /// Aaron
    /// 2021-04-14 13:42:14
    /// 
    /// </summary>
    public partial class Domains
    {

        public enum DomainsType
        {
            [Description("主域名")]
            Main,
            [Description("代理域名")]
            Agent,
            [Description("APP域名")]
            App,
            [Description("回调域名")]
            Callback
        }

    }
}
