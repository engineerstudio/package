using System;

namespace Y.Infrastructure.ICache
{
    public interface IMemoryCacheService
    {

        /// <summary>
        /// 新增缓存  滑动过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="minute"></param>
        void InsertSlidingExpirationCache(string key, object value, int minute);


        object Get(string key);


        void Clear(string key);

    }
}
