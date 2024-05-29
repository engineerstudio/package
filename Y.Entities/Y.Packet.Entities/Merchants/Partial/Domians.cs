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
            [Description("������")]
            Main,
            [Description("��������")]
            Agent,
            [Description("APP����")]
            App,
            [Description("�ص�����")]
            Callback
        }

    }
}
