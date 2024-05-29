using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Y.Infrastructure.Library.Core.WebInfrastructure.EnumeratedType
{
    enum UrlAccessAction
    {
        Allow,
        Deny
    }



    public enum Gender
    {

        [Description("未知")]
        None = 0,
        [Description("男")]
        Male = 1,
        [Description("女")]
        Female = 2
    }


}
