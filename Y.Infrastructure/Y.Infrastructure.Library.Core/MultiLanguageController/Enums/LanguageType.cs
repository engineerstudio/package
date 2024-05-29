using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Y.Infrastructure.Library.Core.MultiLanguageController.Enums
{
    public enum LanguageType
    {
        [Description("中文(简体)")]
        ZHCN,
        [Description("中文(繁體)")]
        ZHTW,
        [Description("英文")]
        EN
    }
}
