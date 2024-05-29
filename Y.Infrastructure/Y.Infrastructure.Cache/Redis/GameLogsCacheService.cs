using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.CacheFactory.Implementation;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.ICache;

namespace Y.Infrastructure.Cache
{
    public class GameLogsCacheService : BaseRedisRepository, IGameLogsCacheService
    {

        private readonly IYCacheFactory _factory;
        private readonly IYCache _yCache;
        //internal IDatabase _db;
        //internal IRedisCacheClient _sut;
        public GameLogsCacheService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory)
        {
            YCacheConfiguration jsonStr = options.Get(DefaultString.LuckyRedis);
            _factory = factory;
            RedisDb redisDb = (RedisDb)_factory.Create("RedisDb", jsonStr.ToJson());
            _db = redisDb.GetDb();
            _sut = redisDb.GetRcc();
        }
        public GameLogsCacheService() { }


        #region 定义本页面的Key

        public static readonly string GameLogsKey = "GAMELOGS_KEY";


        #endregion

        /// <summary>
        /// 顶部插入数据
        /// </summary>
        /// <param name="jsonValue"></param>
        /// <returns></returns>
        public async Task ListLeftPushAsync(string key, string jsonValue)
        {
            await  _db.ListLeftPushAsync(key, jsonValue);
        }


        /// <summary>
        /// 底部拿出数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<T>> ListRightPopAsync<T>(string key)
        {
            var list = await _db.ListRangeAsync(key);
            if(list == null) return null;
            List<T> result = new List<T>();
            foreach (var ll in list)
                result.Add(ToObject<T>(ll));
            return result;
        }

        /// <summary>
        /// 反序列化返回object
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private T ToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
