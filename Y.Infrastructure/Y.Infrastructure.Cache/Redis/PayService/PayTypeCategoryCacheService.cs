using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.CacheFactory.Implementation;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.ICache.IRedis.IMerchantsService;
using Y.Infrastructure.ICache.IRedis.IPayService;

namespace Y.Infrastructure.Cache.Redis.PayService
{
    public class PayTypeCategoryCacheService : BaseHandlerCacheService, IPayTypeCategoryCacheService
    {
        private readonly IYCacheFactory _factory;
        public PayTypeCategoryCacheService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory)
        {
            YCacheConfiguration jsonStr = options.Get(DefaultString.LuckyRedis);
            jsonStr.Index = 3;
            _factory = factory;
            RedisDb redisDb = (RedisDb)_factory.Create("RedisDb", jsonStr.ToJson());
            _db = redisDb.GetDb();
            _sut = redisDb.GetRcc();
            _prefix = "Id";
        }


        protected async Task<(bool Exist, string Data)> GetListCacheAsync(int merchantId)
        {
            string key = $"PayTypeCategory_List_{merchantId}";
            if (await base.KeyExistsAsync(key))
                return (true, await base.StringGetAsync(key));
            return (false, null);
        }

        protected async Task SaveGetListCacheAsync(int merchantId, string cacheValue)
        {
            string key = $"PayTypeCategory_List_{merchantId}";
            await base.StringSetAsync(key, cacheValue, TimeSpan.FromHours(1));
        }
        protected async Task DeleteGetListCacheAsync(int merchantId)
        {
            string key = $"PayTypeCategory_List_{merchantId}";
            await base.KeyDeleteAsync(key);
        }


    }
}
