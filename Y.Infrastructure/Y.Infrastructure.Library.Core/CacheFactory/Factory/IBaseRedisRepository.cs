using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Y.Infrastructure.Library.Core.CacheFactory.Factory
{
    public interface IBaseRedisRepository
    {
        #region 同步方法

        #region String

        /// <summary>
        /// 保存单个key vlaue
        /// </summary>
        bool StringSet(string key, string value, TimeSpan? expiry = null);

        /// <summary>
        /// 保存多个key value
        /// </summary>
        bool StringSet(List<KeyValuePair<RedisKey, RedisValue>> keyValues);

        /// <summary>
        /// 保存对象到redis
        /// </summary>
        bool StringSet<T>(string key, T obj, TimeSpan? expiry = null);

        /// <summary>
        /// 根据key获取value的值
        /// </summary>
        string StringGet(string key);

        /// <summary>
        /// 获取多个key的值
        /// </summary>
        RedisValue[] StringGet<T>(List<string> keys);

        /// <summary>
        /// 根据key获取对象
        /// </summary>
        T StringGet<T>(string key);

        /// <summary>
        /// 为数字增长val
        /// </summary>
        double StringIncrement(string key, double val);

        /// <summary>
        /// 为数字减少val
        /// </summary>
        double StringDecrement(string key, double val);

        #endregion

        #region 分布式锁
        /// <summary>
        /// 对指定的key加锁 key表示的是redis数据库中该锁的名称，不可重复。 value用来标识谁拥有该锁并用来释放锁 可以用Guid()。TimeSpan表示该锁的有效时间。
        /// </summary>
        bool LockTake(string key, string value, TimeSpan expiry);
        #endregion

        #region Hash
        /// <summary>
        /// 判断某个数据是否在此hash表中
        /// </summary>
        bool HashExists(string key, string hashField);

        /// <summary>
        /// 存储字符串到hash表
        /// </summary>
        bool HashSet(string key, string hashField, string value);
        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        bool HashSet<T>(string key, string hashField, T t);

        /// <summary>
        /// 将HashEntry[]存储数据到hash表
        /// </summary>
        void HashSet(string key, HashEntry[] hashEntries);
        /// <summary>
        /// 将Dictionary存储数据到hash表
        /// </summary>
        void HashSet(string key, Dictionary<string, string> dic);
        /// <summary>
        /// 移除hash表中的某个值
        /// </summary>
        bool HashDelete(string key, string hashField);

        /// <summary>
        /// 移除hash表中的多个值
        /// </summary>
        long HashDelete(string key, List<RedisValue> hashFields);

        /// <summary>
        /// 从hash表中获取指定的hashField值
        /// </summary>
        string HashGet(string key, string hashField);

        /// <summary>
        ///  从hash表中获取指定的hashField的对象 
        /// </summary>
        T HashGet<T>(string key, string hashField);

        /// <summary>
        /// 从hash表中获取list
        /// </summary>
        List<T> HashGetList<T>(string key);


        /// <summary>
        /// 从hash表中取出所有键值对 返回dic
        /// </summary>
        Dictionary<string, string> HashGetDic(string key);

        /// <summary>
        /// 从hash表中取出所有键值对 返回T类型
        /// </summary>
        T HashGetObj<T>(string key);

        /// <summary>
        /// 为数字增长val
        /// </summary>
        double HashIncrement(string key, string dataKey, double val = 1);

        /// <summary>
        /// 为数字减少val
        /// </summary>
        double HashDecrement(string key, string dataKey, double val = 1);

        /// <summary>
        /// 获取hashkey所有Redis key(hashField)
        /// </summary>
        List<T> HashKeys<T>(string key);
        #endregion

        #region List
        /// <summary>
        /// 移除指定key的list的内部的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void ListRemove<T>(string key, T value);
        /// <summary>
        /// 获取指定key的List内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        List<T> ListRange<T>(string key);

        /// <summary>
        /// 入队列
        /// </summary> 
        void ListRightPush<T>(string key, T value);

        /// <summary>
        /// 出队列
        /// </summary> 
        T ListRightPop<T>(string key);
        /// <summary>
        /// 入队列
        /// </summary> 
        void ListLeftPush<T>(string key, T value);

        /// <summary>
        /// 出队列
        /// </summary> 
        T ListLeftPop<T>(string key);

        /// <summary>
        /// 获取list的数量
        /// </summary> 
        long ListLength(string key);

        #endregion

        #region SortedSet 有序集合
        /// <summary>
        /// 添加到有序集合中
        /// </summary>
        bool SortedSetAdd<T>(string key, T value, double score);

        /// <summary>
        /// 从有序集合中删除指定的value
        /// </summary>
        bool SortedSetRemove<T>(string key, T value);
        /// <summary>
        /// 获取集合的所有数据返回list
        /// </summary>
        List<T> SortedSetRangeByRank<T>(string key);

        /// <summary>
        /// 获取指定集合的数量
        /// </summary>
        long SortedSetLength(string key);
        #endregion


        #region key
        /// <summary>
        /// 删除指定的key
        /// </summary> 
        bool KeyDelete(string key);

        /// <summary>
        /// 删除多个指定的keys
        /// </summary> 
        long KeyDelete(List<string> keys);

        /// <summary>
        /// 判断指定的key是否存在
        /// </summary> 
        bool KeyExists(string key);

        /// <summary>
        /// 对指定的key进行重新命名为newKey
        /// </summary> 
        bool KeyRename(string key, string newKey);

        /// <summary>
        /// 设置key的过期时间
        /// </summary> 
        bool KeyExpire(string key, TimeSpan? expiry);

        #endregion


        #region 其他
        /// <summary>
        /// 开始使用批量提交打包
        /// </summary>
        IBatch CreateBatch();
        #endregion



        #endregion


        #region 异步方法

        #region String

        /// <summary>
        /// 保存单个key vlaue
        /// </summary>
        Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry = null);

        /// <summary>
        /// 保存多个key value
        /// </summary>
        Task<bool> StringSetAsync(List<KeyValuePair<RedisKey, RedisValue>> keyValues);

        /// <summary>
        /// 保存对象到redis
        /// </summary>
        Task<bool> StringSetAsync<T>(string key, T obj, TimeSpan? expiry = null);

        /// <summary>
        /// 根据key获取value的值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        Task<string> StringGetAsync(string key);

        /// <summary>
        /// 获取多个key的值
        /// </summary>
        Task<RedisValue[]> StringGetAsync<T>(List<string> keys);

        /// <summary>
        /// 根据key获取对象
        /// </summary>
        Task<T> StringGetAsync<T>(string key);

        /// <summary>
        /// 为数字增长val
        /// </summary>
        Task<double> StringIncrementAsync(string key, double val);

        /// <summary>
        /// 为数字减少val
        /// </summary>
        Task<double> StringDecrementAsync(string key, double val);

        #endregion

        #region 分布式锁

        /// <summary>
        /// 对指定的key加锁 key表示的是redis数据库中该锁的名称，不可重复。 value用来标识谁拥有该锁并用来释放锁 可以用Guid()。TimeSpan表示该锁的有效时间。
        /// </summary>
        Task<bool> LockTakeAsync(string key, string value, TimeSpan expiry);

        #endregion

        #region Hash

        /// <summary>
        /// 判断某个数据是否在此hash表中
        /// </summary>
        Task<bool> HashExistsAsync(string key, string hashField);

        /// <summary>
        /// 存储字符串到hash表
        /// </summary>
        Task<bool> HashSetAsync(string key, string hashField, string value);

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        Task<bool> HashSetAsync<T>(string key, string hashField, T t);

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashEntries"></param>
        Task HashSetAsync(string key, HashEntry[] hashEntries);

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        Task HashSetAsync(string key, Dictionary<string, string> dic);

        /// <summary>
        /// 移除hash表中的某个值
        /// </summary>
        Task<bool> HashDeleteAsync(string key, string hashField);

        /// <summary>
        /// 移除hash表中的多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        Task<long> HashDeleteAsync(string key, List<RedisValue> hashFields);

        /// <summary>
        /// 从hash表中获取指定的hashField值
        /// </summary>
        Task<string> HashGetAsync(string key, string hashField);

        /// <summary>
        ///  从hash表中获取指定的hashField的对象 
        /// </summary>
        Task<T> HashGetAsync<T>(string key, string hashField);

        /// <summary>
        /// 从hash表中获取list
        /// </summary>
        Task<List<T>> HashGetListAsync<T>(string key);

        /// <summary>
        /// 从hash表中取出所有键值对 返回dic
        /// </summary>
        Task<Dictionary<string, string>> HashGetDicAsync(string key);

        /// <summary>
        /// 从hash表中取出所有键值对 返回T类型
        /// </summary>
        Task<T> HashGetObjAsync<T>(string key);

        /// <summary>
        /// 为数字增长val
        /// </summary>
        Task<double> HashIncrementAsync(string key, string dataKey, double val = 1);

        /// <summary>
        /// 为数字减少val
        /// </summary>
        Task<double> HashDecrementAsync(string key, string dataKey, double val = 1);

        /// <summary>
        /// 获取hashkey所有Redis key(hashField)
        /// </summary>
        Task<List<T>> HashKeysAsync<T>(string key);

        #endregion

        #region List

        /// <summary>
        /// 移除指定key的list的内部的值
        /// </summary>
        Task<long> ListRemoveAsync<T>(string key, T value);

        /// <summary>
        /// 获取指定key的List内容
        /// </summary> 
        Task<List<T>> ListRangeAsync<T>(string key);

        /// <summary>
        /// 入队列
        /// </summary>
        Task<long> ListRightPushAsync<T>(string key, T value);

        /// <summary>
        /// 出队列
        /// </summary>
        Task<T> ListRightPopAsync<T>(string key);
        /// <summary>
        /// 入队列
        /// </summary>
        Task<long> ListLeftPushAsync<T>(string key, T value);

        /// <summary>
        /// 出队列
        /// </summary>
        Task<T> ListLeftPopAsync<T>(string key);

        /// <summary>
        /// 获取list的数量
        /// </summary> 
        Task<long> ListLengthAsync(string key);

        #endregion

        #region SortedSet 有序集合

        /// <summary>
        /// 添加到有序集合中
        /// </summary>
        Task<bool> SortedSetAddAsync<T>(string key, T value, double score);

        /// <summary>
        /// 从有序集合中删除指定的value
        /// </summary>
        Task<bool> SortedSetRemoveAsync<T>(string key, T value);

        /// <summary>
        /// 获取集合的所有数据返回list
        /// </summary>
        Task<List<T>> SortedSetRangeByRankAsync<T>(string key);

        /// <summary>
        /// 获取指定集合的数量
        /// </summary>
        Task<long> SortedSetLengthAsync(string key);

        #endregion

        #region key

        /// <summary>
        /// 删除指定的key
        /// </summary>
        Task<bool> KeyDeleteAsync(string key);

        /// <summary>
        /// 删除多个指定的keys
        /// </summary>
        Task<long> KeyDeleteAsync(List<string> keys);

        /// <summary>
        /// 判断指定的key是否存在
        /// </summary> 
        Task<bool> KeyExistsAsync(string key);

        /// <summary>
        /// 对指定的key进行重新命名为newKey
        /// </summary> 
        Task<bool> KeyRenameAsync(string key, string newKey);

        /// <summary>
        /// 设置key的过期时间
        /// </summary> 
        Task<bool> KeyExpireAsync(string key, TimeSpan? expiry);
        #endregion

        #endregion
    }
}