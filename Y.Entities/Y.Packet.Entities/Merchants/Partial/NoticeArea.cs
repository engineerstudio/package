using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Y.Packet.Entities.Merchants
{
    partial class NoticeArea
    {
        public enum NoticeType
        {
            /// <summary>
            /// 站点公告
            /// </summary>
            [Description("公告")]
            Announcement = 0,
            /// <summary>
            /// 用户私信
            /// </summary>
            [Description("通知")]
            Notice = 1,
            [Description("活动")]
            Activity = 2,
            [Description("充提")]
            Money = 3
        }
    }
}
