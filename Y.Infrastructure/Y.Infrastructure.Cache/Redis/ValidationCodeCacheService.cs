using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.CacheFactory.Implementation;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.ICache.IRedis;

namespace Y.Infrastructure.Cache.Redis
{
    public class ValidationCodeCacheService : BaseRedisRepository, IValidationCodeCacheService
    {
        private readonly IYCacheFactory _factory;
        private readonly IYCache _yCache;
        //internal IDatabase _db;
        //internal IRedisCacheClient _sut;
        public ValidationCodeCacheService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory)
        {
            YCacheConfiguration jsonStr = options.Get(DefaultString.LuckyRedis);
            _factory = factory;
            RedisDb redisDb = (RedisDb)_factory.Create("RedisDb", jsonStr.ToJson());
            _db = redisDb.GetDb();
            _sut = redisDb.GetRcc();
        }
        public ValidationCodeCacheService() { }
        #region 定义本页面的Key

        private readonly string _validationPrefix = "validation_";

        #endregion
        public void DeleteByKey(string key)
        {
            _db.KeyDelete($"{_validationPrefix}{key}");
        }

        public bool ExistKey(string key)
        {
            return _db.KeyExists($"{_validationPrefix}{key}");
        }

        public string GetByKey(string key)
        {
            return _db.StringGet($"{_validationPrefix}{key}");
        }

        public void SetCodeCache(string key, string value)
        {
            _db.StringSet($"{_validationPrefix}{key}", value, TimeSpan.FromMinutes(3));
        }
    }
}