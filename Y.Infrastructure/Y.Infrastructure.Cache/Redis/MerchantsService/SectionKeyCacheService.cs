using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.CacheFactory.Implementation;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.ICache.IRedis.MerchantsService;

namespace Y.Infrastructure.Cache.Redis.MerchantsService
{
    public class SectionKeyCacheService : BaseHandlerCacheService, ISectionKeyCacheService
    {
        private readonly IYCacheFactory _factory;
        public SectionKeyCacheService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory)
        {
            YCacheConfiguration jsonStr = options.Get(DefaultString.LuckyRedis);
            jsonStr.Index = 3;
            _factory = factory;
            RedisDb redisDb = (RedisDb)_factory.Create("RedisDb", jsonStr.ToJson());
            _db = redisDb.GetDb();
            _sut = redisDb.GetRcc();
            _prefix = "Id";
        }


        protected async Task<(bool Exist, string Data)> GetByKeyCacheAsync(int merchantId, string secKey)
        {
            string key = $"SectionDetail_Info_{merchantId}_{secKey}";
            if(await base.KeyExistsAsync(key))
                return (true, await base.StringGetAsync(key));
            return (false, null);
        }

        protected async Task SaveGetByKeyCacheAsync(int merchantId, string secKey,string cacheValue)
        {
            string key = $"SectionDetail_Info_{merchantId}_{secKey}";
            await base.StringSetAsync(key, cacheValue, TimeSpan.FromHours(1));
        }


        protected async Task<(bool Exist, string Data)> GetByMerchantIdForH5CacheAsync(int merchantId,string md5)
        {
            string key = $"GetByMerchantIdForH5Async{merchantId}_{md5}";
            if (await base.KeyExistsAsync(key))
                return (true, await base.StringGetAsync(key));
            return (false, null);
        }
        protected async Task SaveGetByMerchantIdForH5CacheAsyncc(int merchantId, string md5, string cacheValue)
        {
            string key = $"GetByMerchantIdForH5Async{merchantId}_{md5}";
            await base.StringSetAsync(key, cacheValue, TimeSpan.FromHours(1));
        }



    }
}
