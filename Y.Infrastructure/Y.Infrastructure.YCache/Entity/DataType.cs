using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Infrastructure.YCache.Entity
{
    /// <summary>
    /// 缓存存储的数据类型
    /// </summary>
    public enum YDataType
    {
        String,
        List,
        Set,
        SortedSet,
        Hash
    }
}
