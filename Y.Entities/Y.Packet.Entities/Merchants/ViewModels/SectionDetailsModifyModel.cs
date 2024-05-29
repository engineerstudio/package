using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Merchants.ViewModels
{
    public class SectionDetailsModifyModel
    {
        public int Id { get; set; }
        //public string Name { get; set; }
        public string Alias { get; set; }
        public bool Enabled { get; set; }
        public bool HasSubMenu { get; set; }
        public string SKey { get; set; }
        public int KeyId { get; set; }
        public int MerchantId { get; set; }
        public string PcImgUrl { get; set; }
        public string H5ImgUrl { get; set; }
        public string PageUrl { get; set; }
        public int SortNo { get; set; }
        public string Tcontent { get; set; }

    }
}
