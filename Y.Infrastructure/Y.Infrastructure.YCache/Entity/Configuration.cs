using StackExchange.Redis.Extensions.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Infrastructure.YCache.Entity
{
    public class YCacheConfiguration
    {
        public RedisHost[] Host { get; set; }
        public string Password { get; set; }
    }



}
