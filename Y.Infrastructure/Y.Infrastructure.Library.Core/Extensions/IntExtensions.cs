using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Infrastructure.Library.Core.Extensions
{
    public static class IntExtensions
    {

        public static (bool, string) ToResult(this int? d, string sucessMsg, string faildMsg)
        {
            if (d == null || d.Value < 1) return (false, faildMsg);
            return (true, sucessMsg);
        }
        public static (bool, string) ToResult(this int d, string sucessMsg, string faildMsg)
        {
            if (d < 1) return (false, faildMsg);
            return (true, sucessMsg);
        }
    }
}
