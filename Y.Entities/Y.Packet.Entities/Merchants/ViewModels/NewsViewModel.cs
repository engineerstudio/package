using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Merchants.ViewModels
{
    public class NewsViewModel: NewsDetailsViewModel
    {
        public bool IsRead { get; set; }
    }

    public class NewsDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Date { get; set; }
    }
}
