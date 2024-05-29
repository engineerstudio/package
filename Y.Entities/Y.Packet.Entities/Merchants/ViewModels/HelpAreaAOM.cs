using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Merchants.ViewModels
{
    public class HelpAreaAOM
    {

        public int Id { get; set; }
        public int MerchantId { get; set; }
        public int TypeId { get; set; }
        public string Title { get; set; }
        public string Tcontent { get; set; }
        public bool ShowIndexPage { get; set; }
        public string Alias { get; set; }
        public int SortNo { get; set; }
    }
}
