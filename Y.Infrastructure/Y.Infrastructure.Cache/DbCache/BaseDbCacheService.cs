using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Extension;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.CacheFactory.Implementation;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.Repository;

namespace Y.Infrastructure.Cache.DbCache
{
    public class BaseDbCacheService<T, Int32> : BaseRepository<T, Int32> where T : class
    {
        private readonly IYCacheFactory _factory;
        protected IDatabase _db;
        protected IRedisCacheClient _sut;
        private IYCacheFactory factory;
        private IBaseRedisRepository baseRedisRepository;
        private readonly IBaseRedisRepository _baseRedisRepository;
        protected int Index { get; set; }
        public BaseDbCacheService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory)
        {
            YCacheConfiguration jsonStr = options.Get(DefaultString.LuckyRedis);
            jsonStr.Index = Index;
            _factory = factory;
            RedisDb redisDb = (RedisDb)_factory.Create("RedisDb", jsonStr.ToJson());
            _db = redisDb.GetDb();
            _sut = redisDb.GetRcc();
        }

        #region String
        /// <summary>
        /// 保存单个key vlaue
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool StringSet(string key, string value, TimeSpan? expiry = null)
        {
            return _db.StringSet(key, value, expiry);
        }

        /// <summary>
        /// 保存多个key value
        /// </summary>
        /// <param name="keyValues">KeyValuePair的list</param>
        /// <returns></returns>
        public bool StringSet(List<KeyValuePair<RedisKey, RedisValue>> keyValues)
        {
            return _db.StringSet(keyValues.ToArray());
        }

        /// <summary>
        /// 保存对象到redis
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="obj">对象</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public bool StringSet<T>(string key, T obj, TimeSpan? expiry = null)
        {
            string json = ToJson(obj);
            return _db.StringSet(key, json, expiry);
        }

        /// <summary>
        /// 根据key获取value的值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public string StringGet(string key)
        {
            if (!_db.KeyExists(key)) { return null; }
            return _db.StringGet(key);
        }

        /// <summary>
        /// 获取多个key的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">key的集合</param>
        /// <returns></returns>
        public RedisValue[] StringGet<T>(List<string> keys)
        {
            return _db.StringGet(ConvertRedisKey(keys));
        }

        /// <summary>
        /// 根据key获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T StringGet<T>(string key)
        {
            if (!_db.KeyExists(key)) { return default(T); }
            var json = _db.StringGet(key);
            return ToObject<T>(json);
        }

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="val">增长的数字可以为负</param>
        /// <returns></returns>
        public double StringIncrement(string key, double val)
        {
            return _db.StringIncrement(key, val);
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="val">减少的数字可以为负</param>
        /// <returns></returns>
        public double StringDecrement(string key, double val)
        {
            return _db.StringDecrement(key, val);
        }

        #endregion

        #region 分布式锁
        /// <summary>
        /// 对指定的key加锁 key表示的是redis数据库中该锁的名称，不可重复。 value用来标识谁拥有该锁并用来释放锁 可以用Guid()。TimeSpan表示该锁的有效时间。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool LockTake(string key, string value, TimeSpan expiry)
        {
            return _db.LockTake(key, value, expiry);
        }
        #endregion

        #region Hash
        /// <summary>
        /// 判断某个数据是否在此hash表中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public bool HashExists(string key, string hashField)
        {
            return _db.HashExists(key, hashField);
        }
        /// <summary>
        /// 存储字符串到hash表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool HashSet(string key, string hashField, string value)
        {
            return _db.HashSet(key, hashField, value);
        }
        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool HashSet<T>(string key, string hashField, T t)
        {
            string json = ToJson<T>(t);
            return _db.HashSet(key, hashField, json);
        }
        /// <summary>
        /// 将HashEntry[]存储数据到hash表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashEntries"></param>
        /// <param name="expiry"></param>
        public void HashSet(string key, HashEntry[] hashEntries)
        {
            _db.HashSet(key, hashEntries);
        }
        /// <summary>
        /// 将Dictionary存储数据到hash表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dic"></param>
        /// <param name="expiry"></param>
        public void HashSet(string key, Dictionary<string, string> dic)
        {
            _db.HashSet(key, dic.ToHashEntriesFromDic());
        }
        /// <summary>
        /// 移除hash表中的某个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public bool HashDelete(string key, string hashField)
        {
            return _db.HashDelete(key, hashField);
        }
        /// <summary>
        /// 移除hash表中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        public long HashDelete(string key, List<RedisValue> hashFields)
        {
            return _db.HashDelete(key, hashFields.ToArray());
        }
        /// <summary>
        /// 从hash表中获取指定的hashField值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public string HashGet(string key, string hashField)
        {
            var value = _db.HashGet(key, hashField);
            return value;
        }
        /// <summary>
        ///  从hash表中获取指定的hashField的对象 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public T HashGet<T>(string key, string hashField)
        {
            var value = _db.HashGet(key, hashField);
            return ToObject<T>(value);
        }
        /// <summary>
        /// 从hash表中获取list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> HashGetList<T>(string key)
        {
            var values = _db.HashValues(key);
            List<T> list = new List<T>();
            foreach (var item in values)
            {
                list.Add(ToObject<T>(item));
            }
            return list;
        }
        /// <summary>
        /// 从hash表中取出所有键值对 返回dic
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, string> HashGetDic(string key)
        {
            var hashEntries = _db.HashGetAll(key);
            var dic = hashEntries.ConvertFromRedisToDic();
            return dic;
        }
        /// <summary>
        /// 从hash表中取出所有键值对 返回T类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T HashGetObj<T>(string key)
        {
            var hashEntries = _db.HashGetAll(key);
            T t = hashEntries.ConvertFromRedisToEntity<T>();
            return t;
        }
        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public double HashIncrement(string key, string dataKey, double val = 1)
        {
            return _db.HashIncrement(key, dataKey, val);
        }
        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public double HashDecrement(string key, string dataKey, double val = 1)
        {
            return _db.HashDecrement(key, dataKey, val);
        }
        /// <summary>
        /// 获取hashkey所有Redis key(hashField)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> HashKeys<T>(string key)
        {
            var values = _db.HashKeys(key);
            List<T> list = new List<T>();
            foreach (var item in values)
            {
                list.Add(ToObject<T>(item));
            }
            return list;
        }

        #endregion

        #region List
        /// <summary>
        /// 移除指定key的list的内部的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void ListRemove<T>(string key, T value)
        {
            _db.ListRemove(key, ToJson(value));
        }

        /// <summary>
        /// 获取指定key的List内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> ListRange<T>(string key)
        {
            var values = _db.ListRange(key);
            List<T> list = new List<T>();
            foreach (var item in values)
            {
                list.Add(ToObject<T>(item));
            }
            return list;
        }

        /// <summary>
        /// 入队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void ListRightPush<T>(string key, T value)
        {
            _db.ListRightPush(key, ToJson(value));
        }

        /// <summary>
        /// 出队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListRightPop<T>(string key)
        {
            var value = _db.ListRightPop(key);
            return ToObject<T>(value);
        }

        /// <summary>
        /// 入队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void ListLeftPush<T>(string key, T value)
        {
            _db.ListLeftPush(key, ToJson(value));
        }

        /// <summary>
        /// 出队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T ListLeftPop<T>(string key)
        {
            var value = _db.ListLeftPop(key);
            return ToObject<T>(value);
        }

        /// <summary>
        /// 获取list的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long ListLength(string key)
        {
            return _db.ListLength(key);
        }
        #endregion

        #region SortedSet 有序集合
        /// <summary>
        /// 添加到有序集合中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="score">分数</param>
        /// <returns></returns>
        public bool SortedSetAdd<T>(string key, T value, double score)
        {
            return _db.SortedSetAdd(key, ToJson(value), score);
        }

        /// <summary>
        /// 从有序集合中删除指定的value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SortedSetRemove<T>(string key, T value)
        {
            return _db.SortedSetRemove(key, ToJson(value));
        }

        /// <summary>
        /// 获取集合的所有数据返回list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> SortedSetRangeByRank<T>(string key)
        {
            var values = _db.SortedSetRangeByRank(key);
            List<T> list = new List<T>();
            foreach (var item in values)
            {
                list.Add(ToObject<T>(item));
            }
            return list;
        }

        /// <summary>
        /// 获取指定集合的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long SortedSetLength(string key)
        {
            return _db.SortedSetLength(key);
        }
        #endregion

        #region key
        /// <summary>
        /// 删除指定的key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyDelete(string key)
        {
            return _db.KeyDelete(key);
        }

        /// <summary>
        /// 删除多个指定的keys
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public long KeyDelete(List<string> keys)
        {
            return _db.KeyDelete(ConvertRedisKey(keys));
        }

        /// <summary>
        /// 判断指定的key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            return _db.KeyExists(key);
        }

        /// <summary>
        /// 对指定的key进行重新命名为newKey
        /// </summary>
        /// <param name="key">原key</param>
        /// <param name="newKey">新的key</param>
        /// <returns></returns>
        public bool KeyRename(string key, string newKey)
        {
            return _db.KeyRename(key, newKey);
        }

        /// <summary>
        /// 设置key的过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool KeyExpire(string key, TimeSpan? expiry)
        {
            return _db.KeyExpire(key, expiry);
        }
        #endregion

        #region 其他
        /// <summary>
        /// 开始使用批量提交打包
        /// </summary>
        public IBatch CreateBatch()
        {
            return _db.CreateBatch();
        }
        #endregion

        #region 异步方法
        #region String
        /// <summary>
        /// 保存单个key vlaue
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry = null)
        {
            return await _db.StringSetAsync(key, value, expiry);
        }

        /// <summary>
        /// 保存多个key value
        /// </summary>
        /// <param name="keyValues">KeyValuePair的list</param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(List<KeyValuePair<RedisKey, RedisValue>> keyValues)
        {
            return await _db.StringSetAsync(keyValues.ToArray());
        }

        /// <summary>
        /// 保存对象到redis
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="obj">对象</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync<T>(string key, T obj, TimeSpan? expiry = null)
        {
            string json = ToJson(obj);
            return await _db.StringSetAsync(key, json, expiry);
        }

        /// <summary>
        /// 根据key获取value的值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public async Task<string> StringGetAsync(string key)
        {
            if (!_db.KeyExists(key)) { return null; }
            return await _db.StringGetAsync(key);
        }

        /// <summary>
        /// 获取多个key的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys">key的集合</param>
        /// <returns></returns>
        public async Task<RedisValue[]> StringGetAsync<T>(List<string> keys)
        {
            return await _db.StringGetAsync(ConvertRedisKey(keys));
        }

        /// <summary>
        /// 根据key获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> StringGetAsync<T>(string key)
        {
            if (!_db.KeyExists(key)) { return default(T); }
            var json = await _db.StringGetAsync(key);
            return ToObject<T>(json);
        }

        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="val">增长的数字可以为负</param>
        /// <returns></returns>
        public async Task<double> StringIncrementAsync(string key, double val)
        {
            return await _db.StringIncrementAsync(key, val);
        }

        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="val">减少的数字可以为负</param>
        /// <returns></returns>
        public async Task<double> StringDecrementAsync(string key, double val)
        {
            return await _db.StringDecrementAsync(key, val);
        }

        #endregion

        #region 分布式锁
        /// <summary>
        /// 对指定的key加锁 key表示的是redis数据库中该锁的名称，不可重复。 value用来标识谁拥有该锁并用来释放锁 可以用Guid()。TimeSpan表示该锁的有效时间。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> LockTakeAsync(string key, string value, TimeSpan expiry)
        {
            return await _db.LockTakeAsync(key, value, expiry);
        }
        #endregion

        #region Hash
        /// <summary>
        /// 判断某个数据是否在此hash表中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<bool> HashExistsAsync(string key, string hashField)
        {
            return await _db.HashExistsAsync(key, hashField);
        }
        /// <summary>
        /// 存储字符串到hash表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync(string key, string hashField, string value)
        {
            return await _db.HashSetAsync(key, hashField, value);
        }
        public async Task<bool> HashSetAsync(string key, string hashField, string value, When when)
        {
            return await _db.HashSetAsync(key, hashField, value, when);
        }
        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync<T>(string key, string hashField, T t)
        {
            string json = ToJson<T>(t);
            return await _db.HashSetAsync(key, hashField, json);
        }
        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashEntries"></param>
        public async Task HashSetAsync(string key, HashEntry[] hashEntries)
        {
            await _db.HashSetAsync(key, hashEntries);
        }
        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public async Task HashSetAsync(string key, Dictionary<string, string> dic)
        {
            await _db.HashSetAsync(key, dic.ToHashEntriesFromDic());
        }
        /// <summary>
        /// 移除hash表中的某个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string key, string hashField)
        {
            return await _db.HashDeleteAsync(key, hashField);
        }
        /// <summary>
        /// 移除hash表中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        public async Task<long> HashDeleteAsync(string key, List<RedisValue> hashFields)
        {
            return await _db.HashDeleteAsync(key, hashFields.ToArray());
        }
        /// <summary>
        /// 从hash表中获取指定的hashField值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<string> HashGetAsync(string key, string hashField)
        {
            var value = await _db.HashGetAsync(key, hashField);
            return value;
        }
        /// <summary>
        ///  从hash表中获取指定的hashField的对象 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<T> HashGetAsync<T>(string key, string hashField)
        {
            var value = await _db.HashGetAsync(key, hashField);
            return ToObject<T>(value);
        }
        /// <summary>
        /// 从hash表中获取list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<T>> HashGetListAsync<T>(string key)
        {
            var values = await _db.HashValuesAsync(key);
            List<T> list = new List<T>();
            foreach (var item in values)
            {
                list.Add(ToObject<T>(item));
            }
            return list;
        }
        /// <summary>
        /// 从hash表中取出所有键值对 返回dic
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> HashGetDicAsync(string key)
        {
            var hashEntries = await _db.HashGetAllAsync(key);
            var dic = hashEntries.ConvertFromRedisToDic();
            return dic;
        }
        public async Task<Dictionary<string, int>> HashGetDic2Async(string key)
        {
            var hashEntries = await _db.HashGetAllAsync(key);
            var dic = hashEntries.ConvertFromRedisToDic2();
            return dic;
        }
        public async Task<Dictionary<int, string>> HashGetDic3Async(string key)
        {
            var hashEntries = await _db.HashGetAllAsync(key);
            var dic = hashEntries.ConvertFromRedisToDic3();
            return dic;
        }
        /// <summary>
        /// 从hash表中取出所有键值对 返回T类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> HashGetObjAsync<T>(string key)
        {
            var hashEntries = await _db.HashGetAllAsync(key);
            T t = hashEntries.ConvertFromRedisToEntity<T>();
            return t;
        }
        /// <summary>
        /// 为数字增长val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>增长后的值</returns>
        public async Task<double> HashIncrementAsync(string key, string dataKey, double val = 1)
        {
            return await _db.HashIncrementAsync(key, dataKey, val);
        }
        /// <summary>
        /// 为数字减少val
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <param name="val">可以为负</param>
        /// <returns>减少后的值</returns>
        public async Task<double> HashDecrementAsync(string key, string dataKey, double val = 1)
        {
            return await _db.HashDecrementAsync(key, dataKey, val);
        }
        /// <summary>
        /// 获取hashkey所有Redis key(hashField)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<T>> HashKeysAsync<T>(string key)
        {
            var values = await _db.HashKeysAsync(key);
            List<T> list = new List<T>();
            foreach (var item in values)
            {
                list.Add(ToObject<T>(item));
            }
            return list;
        }
        #endregion

        #region List
        /// <summary>
        /// 移除指定key的list的内部的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async Task<long> ListRemoveAsync<T>(string key, T value)
        {
            return await _db.ListRemoveAsync(key, ToJson(value));
        }

        /// <summary>
        /// 获取指定key的List内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<T>> ListRangeAsync<T>(string key)
        {
            var values = await _db.ListRangeAsync(key);
            List<T> list = new List<T>();
            foreach (var item in values)
            {
                list.Add(ToObject<T>(item));
            }
            return list;
        }

        /// <summary>
        /// 入队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async Task<long> ListRightPushAsync<T>(string key, T value)
        {
            return await _db.ListRightPushAsync(key, ToJson(value));
        }

        /// <summary>
        /// 出队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> ListRightPopAsync<T>(string key)
        {
            var value = await _db.ListRightPopAsync(key);
            return ToObject<T>(value);
        }

        /// <summary>
        /// 入队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async Task<long> ListLeftPushAsync<T>(string key, T value)
        {
            return await _db.ListLeftPushAsync(key, ToJson(value));
        }

        /// <summary>
        /// 出队列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> ListLeftPopAsync<T>(string key)
        {
            var value = await _db.ListLeftPopAsync(key);
            return ToObject<T>(value);
        }

        /// <summary>
        /// 获取list的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> ListLengthAsync(string key)
        {
            return await _db.ListLengthAsync(key);
        }
        #endregion

        #region SortedSet 有序集合
        /// <summary>
        /// 添加到有序集合中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <param name="score">分数</param>
        /// <returns></returns>
        public async Task<bool> SortedSetAddAsync<T>(string key, T value, double score)
        {
            return await _db.SortedSetAddAsync(key, ToJson(value), score);
        }

        /// <summary>
        /// 从有序集合中删除指定的value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetRemoveAsync<T>(string key, T value)
        {
            return await _db.SortedSetRemoveAsync(key, ToJson(value));
        }

        /// <summary>
        /// 获取集合的所有数据返回list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<List<T>> SortedSetRangeByRankAsync<T>(string key)
        {
            var values = await _db.SortedSetRangeByRankAsync(key);
            List<T> list = new List<T>();
            foreach (var item in values)
            {
                list.Add(ToObject<T>(item));
            }
            return list;
        }

        /// <summary>
        /// 获取指定集合的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> SortedSetLengthAsync(string key)
        {
            return await _db.SortedSetLengthAsync(key);
        }
        #endregion

        #region key
        /// <summary>
        /// 删除指定的key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> KeyDeleteAsync(string key)
        {
            return await _db.KeyDeleteAsync(key);
        }

        /// <summary>
        /// 删除多个指定的keys
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<long> KeyDeleteAsync(List<string> keys)
        {
            return await _db.KeyDeleteAsync(ConvertRedisKey(keys));
        }

        /// <summary>
        /// 判断指定的key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> KeyExistsAsync(string key)
        {
            return await _db.KeyExistsAsync(key);
        }

        /// <summary>
        /// 对指定的key进行重新命名为newKey
        /// </summary>
        /// <param name="key">原key</param>
        /// <param name="newKey">新的key</param>
        /// <returns></returns>
        public async Task<bool> KeyRenameAsync(string key, string newKey)
        {
            return await _db.KeyRenameAsync(key, newKey);
        }

        /// <summary>
        /// 设置key的过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> KeyExpireAsync(string key, TimeSpan? expiry)
        {
            return await _db.KeyExpireAsync(key, expiry);
        }
        #endregion
        #endregion

        #region 辅助方法
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        private string ToJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
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

        /// <summary>
        /// 将集合转化为RedisKey数组
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        RedisKey[] ConvertRedisKey(List<string> keys)
        {
            return keys.Select(b => (RedisKey)b).ToArray();
        }
        #endregion

    }
}
