using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.ICache.IRedis;

namespace Y.Infrastructure.Cache.Redis
{
    public class BaseHandlerCacheService : BaseRedisRepository, IBaseHandlerCacheService
    {
        protected string _prefix = string.Empty;
    }
}
