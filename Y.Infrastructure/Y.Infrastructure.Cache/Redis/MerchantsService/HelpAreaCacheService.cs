using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.CacheFactory.Implementation;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.ICache.IRedis.IMerchantsService;
using Y.Infrastructure.ICache.IRedis.MerchantsService;

namespace Y.Infrastructure.Cache.Redis.MerchantsService
{
    public class HelpAreaCacheService : BaseHandlerCacheService, IHelpAreaCacheService
    {

        private readonly IYCacheFactory _factory;
        public HelpAreaCacheService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory)
        {
            YCacheConfiguration jsonStr = options.Get(DefaultString.LuckyRedis);
            jsonStr.Index = 3;
            _factory = factory;
            RedisDb redisDb = (RedisDb)_factory.Create("RedisDb", jsonStr.ToJson());
            _db = redisDb.GetDb();
            _sut = redisDb.GetRcc();
            _prefix = "Id";
        }

    }
}
