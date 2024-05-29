using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.Portal.Apis.Controllers.DtoModel.Sys
{
    public class CsConfig
    {
        public string Name { get; set; }
        public int MerchantId { get; set; }
        public string PcLogo { get; set; }
        public string H5Logo { get; set; }
        public string ServiceLink { get; set; }
        public string AppDownLoadLink { get; set; }
        public string H5SiteUrl { get; set; }
    }
}
