using System.Collections.Generic;

namespace Y.Infrastructure.Library.Core.LuckyEntity
{
    public class WebApiRequestLimit
    {
        public static readonly Dictionary<string, int> limits = new Dictionary<string, int>()
        {
            {"api/rat/launch", 1800}, // 每30分钟访问一次
            {"api/rat/orders", 30}, // 每30s访问一次
        };
    }
}