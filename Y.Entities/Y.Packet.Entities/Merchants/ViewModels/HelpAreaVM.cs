using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Merchants.ViewModels
{
    public class HelpAreaTypeVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsHref { get; set; }
        public string Href { get; set; }
        public List<HelpAreaVM> Sub { get; set; } = new List<HelpAreaVM>();

    }




    public class HelpAreaVM
    {
        public string Title { get; set; }
        public string Tcontent { get; set; }

    }


    public class HelpAreaTypeVM2
    {
        public string Title { get; set; }
        public int SortDesc { get; set; }
        public List<HelpAreaVMV2> Sub
        { get; set; } = new List<HelpAreaVMV2>();

    }

    public class HelpAreaTypeVM3
    {
        public string Title { get; set; }
        public int SortDesc { get; set; }
        public string Icon { get; set; }
        public List<HelpAreaVMV2> Sub
        { get; set; } = new List<HelpAreaVMV2>();
    }

    public class HelpAreaVMV2
    {

        //public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public int SortDesc { get; set; }
    }


}
