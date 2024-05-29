using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Merchants.ViewModels
{
    public class SectionBannerAddOrModifyModel
    {
        public int SecId { get; set; }
        public List<SectionBanner> SectionBanners;
    }

    public class SectionBanner
    {
        public string Id { get; set; }
        public string ImgUrl { get; set; }
        public string PageUrl { get; set; }
        public string Tcontent { get; set; }
        public bool Deleted { get; set; }
    }


}
