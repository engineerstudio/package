using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Infrastructure.Library.Core.MultiLanguageController.ViewModels
{
    public class MultiLanguageInsertViewModel
    {
        public Int32 Id { get; set; }
        public string MerchantId { get; set; }
        public String Zhcn { get; set; }
        public String Zhtw { get; set; }
        public String En { get; set; }
    }
}
