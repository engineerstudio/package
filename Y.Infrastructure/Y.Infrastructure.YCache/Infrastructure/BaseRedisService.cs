using Microsoft.Extensions.Options;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.ICache;
using Y.Infrastructure.YCache;
using Y.Infrastructure.YCache.YCacheImplementation;

namespace Y.Infrastructure.Cache
{
    public class BaseRedisRepository : IBaseRedisRepository
    {
        private readonly IYCacheFactory _factory;
        private readonly IYCache _yCache;
        protected IDatabase _db;
        protected IRedisCacheClient _sut;
        //public BaseRedisService(IOptionsMonitor<object> options, IYCacheFactory factory)
        //{
        //    object jsonStr = options.Get(DefaultString.Ying_Redis);
        //    //    //, YCacheFactory factory
        //    _factory = factory;
        //    RedisDb redisDb = (RedisDb)_factory.Create("RedisDb", jsonStr.ToJson());
        //    _db = redisDb.GetDb();
        //}

        public BaseRedisRepository() { }


        public void ClearStringSet(string key)
        {
            if (_db.KeyExists(key))
                _db.KeyDelete(key);
        }

        public async Task ClearStringSetAsync(string key)
        {
            if (await _db.KeyExistsAsync(key))
                await _db.KeyDeleteAsync(key);
        }



    }
}
