using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Merchants.ViewModels
{
    /// <summary>
    /// AOM: add or modify
    /// </summary>
    public class HelpAreaTypeAOM
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public string Href { get; set; }
        public bool IsHref { get; set; }
        public bool IsOpen { get; set; }
        public string Title { get; set; }
        public string IconImg { get; set; }
    }
}
