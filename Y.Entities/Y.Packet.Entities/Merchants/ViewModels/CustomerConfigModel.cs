using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Merchants.ViewModels
{
    public class CustomerConfigModel
    {
        public string SiteName { get; set; }

        public string WebLogo { get; set; }

        public string MobileLogo { get; set; }

        public string ServiceLink { get; set; }

        public string QRCode { get; set; }

        public bool EnabledH5 { get; set; }

        public bool EnabledAgentModel { get; set; }

    }
}
