using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.MultiLanguageController.Enums;

namespace Y.Infrastructure.Library.Core.MultiLanguageController.ViewModels
{
    public class MultiLanguageUpdateViewModel
    {
        public string MerchantId { get; set; }
        public int Id { get; set; }
        public LanguageType Type { get; set; }
        public string Value { get; set; }
    }
}
