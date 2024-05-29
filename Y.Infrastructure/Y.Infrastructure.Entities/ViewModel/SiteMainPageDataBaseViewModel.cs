using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.Infrastructure.Entities.ViewModel
{

    public class SiteMainPageDataBaseViewModel
    {
        public IEnumerable<MessageForMainPage> Message { get; set; } = new List<MessageForMainPage>();
        public IEnumerable<PopUpForMainPage> Promo { get; set; } = new List<PopUpForMainPage>();
        public IEnumerable<MessageForMainPage> PopUpAds { get; set; } = new List<MessageForMainPage>();
        public IEnumerable<BannerViewModel> Banner { get; set; } = new List<BannerViewModel>();
    }

    public class SiteMainPageMessageViewModel
    {
        public IEnumerable<MessageForMainPage> Message { get; set; } = new List<MessageForMainPage>();
        public IEnumerable<MessageForMainPage> PopUpAds { get; set; } = new List<MessageForMainPage>();
    }


    // promo activity,pop up ads
    public class PopUpForMainPage
    {
        public string Img { get; set; }
        public string Link { get; set; }
    }

    public class BannerViewModel : PopUpForMainPage
    {
        public string Title { get; set; }
    }

    // message info
    public class MessageForMainPage : PopUpForMainPage
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
