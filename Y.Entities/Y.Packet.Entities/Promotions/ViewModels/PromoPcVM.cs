using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Promotions.ViewModels
{
    public class PromoPcVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PromoPcDetailVM> ProList { get; set; }
    }

    public class PromoPcDetailVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
    }


    public class PromoPcForV1ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int SortNo { get; set; }
        public List<PromoPcDetailForV1ViewModel> ProList { get; set; }
    }

    public class PromoPcDetailForV1ViewModel
    {
        public int Id { get; set; }
        public string Cover { get; set; }
        public int SortNo { get; set; }
    }



}
