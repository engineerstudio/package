using Microsoft.Extensions.Options;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.CacheFactory.Implementation;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.ICache;

namespace Y.Infrastructure.Cache
{
    public class ProjectInitializationService : BaseRedisRepository, IProjectInitializationService
    {

        private readonly IYCacheFactory _factory;
        private readonly IYCache _yCache;
        //internal IDatabase _db;
        //internal IRedisCacheClient _sut;
        public ProjectInitializationService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory)
        {
            YCacheConfiguration jsonStr = options.Get(DefaultString.LuckyRedis);
            _factory = factory;
            RedisDb redisDb = (RedisDb)_factory.Create("RedisDb", jsonStr.ToJson());
            _db = redisDb.GetDb();
            _sut = redisDb.GetRcc();
        }
        public ProjectInitializationService() { }
        #region 定义本页面的Key

        private readonly string _merchantProject = "Project_MerchantPageD";

        private readonly string _sysProject = "Project_SysPageD";


        #endregion

        public string GetMerchantProjectPageData()
        {
            return _db.StringGet(_merchantProject).ToString();
        }

        public void SetMerchantProjectPageData(string data)
        {
            _db.StringSet(_merchantProject, data);
        }

        public void ClearMerchantProjectPageDataCache(string key)
        {
            _db.KeyDelete(key);
        }


        public string GetSysProjectPageData()
        {
            throw new NotImplementedException();
        }


        public void SetSysProjectPageData(string data)
        {
            throw new NotImplementedException();
        }
    }
}
