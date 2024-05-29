using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Vips.ViewModels
{
    public class VipGroupModel
    {
        public Int32 Id { get; set; }


        public Int32 MerchantId { get; set; }


        public String GroupName { get; set; }


        public Boolean Enabled { get; set; }


        public Boolean IsDefault { get; set; }


        public String Description { get; set; }

        public int SortNo { get; set; }

        public string Img { get; set; }

    }
}
