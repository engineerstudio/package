using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.CacheFactory.Implementation;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.ICache.IRedis.IGamesService;

namespace Y.Infrastructure.Cache.Redis.GamesService
{
    public class GameMerchantCacheService : BaseHandlerCacheService, IGameMerchantCacheService
    {
        private readonly IYCacheFactory _factory;
        public GameMerchantCacheService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory)
        {
            YCacheConfiguration jsonStr = options.Get(DefaultString.LuckyRedis);
            jsonStr.Index = 3;
            _factory = factory;
            RedisDb redisDb = (RedisDb)_factory.Create("RedisDb", jsonStr.ToJson());
            _db = redisDb.GetDb();
            _sut = redisDb.GetRcc();
            _prefix = "Id";
        }



        protected async Task<(bool Exist, bool Data)> IsEnabledGameCacheAsync(int merchantId, GameType type)
        {
            string key = $"IsEnabled_{merchantId}_{type.ToString()}";
            if (await base.KeyExistsAsync(key))
            {
                var result = await base.StringGetAsync(key);
                if (result.IsNullOrEmpty()) return (false, false);
                return (true,result=="True");
            }
            return (false, false);
        }

        protected async Task SaveIsEnabledGameCacheAsync(int merchantId, GameType type,string cache)
        {
            string key = $"IsEnabled_{merchantId}_{type.ToString()}";
            await base.StringSetAsync(key,cache, TimeSpan.FromHours(10));
        }
    }
}
