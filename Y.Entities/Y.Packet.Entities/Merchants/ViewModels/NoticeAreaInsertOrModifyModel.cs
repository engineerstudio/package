using System;
using System.Collections.Generic;
using System.Text;
using static Y.Packet.Entities.Merchants.NoticeArea;

namespace Y.Packet.Entities.Merchants.ViewModels
{
    public class NoticeAreaInsertOrModifyModel
    {
        public int MerchantId { get; set; }
        public int Id { get; set; }

        public NoticeType Type { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public Boolean IsDisplay { get; set; }

        public string GroupId { get; set; }
        //public int[] MemberIds { get; set; }
    }
}
