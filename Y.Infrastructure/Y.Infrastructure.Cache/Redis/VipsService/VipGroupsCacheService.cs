using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.CacheFactory.Implementation;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.ICache.IRedis.IVipsService;

namespace Y.Infrastructure.Cache.Redis.VipsService
{
    public class VipGroupsCacheService : BaseHandlerCacheService, IVipGroupsCacheService
    {
        private readonly IYCacheFactory _factory;
        public VipGroupsCacheService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory)
        {
            YCacheConfiguration jsonStr = options.Get(DefaultString.LuckyRedis);
            jsonStr.Index = 3;
            _factory = factory;
            RedisDb redisDb = (RedisDb)_factory.Create("RedisDb", jsonStr.ToJson());
            _db = redisDb.GetDb();
            _sut = redisDb.GetRcc();
            _prefix = "Id";
        }

        protected static readonly string IdNameCacheKey = "IdNameCacheKey";


        protected async Task<(bool Exist, string Data)> GetAccountNameByMemberIdCacheAsync(int memberId)
        {
            string key = $"{IdNameCacheKey}_{memberId}";
            if (await base.KeyExistsAsync(key))
                return (true, await base.StringGetAsync(key));
            return (false, string.Empty);
        }

        protected async Task CreateAccountNameByMemberIdCacheAsync(int memberId,string accountName)
        {
            string key = $"{IdNameCacheKey}_{memberId}";
            await base.StringSetAsync(key, accountName);
        }

        protected async Task DeleteAccountNameByMemberIdCacheAsync(int memberId)
        {
            string key = $"{IdNameCacheKey}_{memberId}";
            await base.KeyDeleteAsync(key);
        }




        protected async Task<(bool Exist,string Data)> PaySettingCacheAsync(int merchantId)
        {
            string key = $"PaySetting_Dic_{merchantId}";
            if(await base.KeyExistsAsync(key))
                return (true, await base.StringGetAsync(key));
            return (false, string.Empty);
        }

        protected async Task SavePaySettingCacheAsync(int merchantId, string cacheData)
        {
            string key = $"PaySetting_Dic_{merchantId}";
            await base.StringSetAsync(key, cacheData, TimeSpan.FromHours(1));
        }

        protected async Task DeletePaySettingCacheAsync(int merchantId)
        {
            string key = $"PaySetting_Dic_{merchantId}";
            await base.KeyDeleteAsync(key);
        }




    }
}
