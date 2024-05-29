using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Infrastructure.YCache.Entity
{
    public class RedisDto
    {
        /// <summary>
        /// 传送数据类型
        /// </summary>
        public YDataType DType;
        /// <summary>
        /// 传送数据
        /// </summary>
        public object Dto { get; set; }

        /// <summary>
        /// 数据库索引
        /// </summary>
        public int DbIndex { get; set; }
    }
}
