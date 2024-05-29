using StackExchange.Redis.Extensions.Core.Configuration;

namespace Y.Infrastructure.Library.Core.CacheFactory.Entity
{
    public class YCacheConfiguration
    {
        public RedisHost[] Host { get; set; }
        public string Password { get; set; }
        public int Index { get; set; }
    }
}