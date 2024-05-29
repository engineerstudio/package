using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Promotions.ViewModels
{
    public class PromoH5VM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PromoH5DetailVM> Dtls { get; set; }
    }

    public class PromoH5DetailVM
    {
        public int Id { get; set; }
        public int CatId { get; set; }
        public string H5Img { get; set; }
    }


    public class PromoH5V1ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PromoH5IndexViewModel> Dtls { get; set; }
    }


    public class PromoH5IndexViewModel
    {
        public int Id { get; set; } 
        public string Img { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }    
    }
}
