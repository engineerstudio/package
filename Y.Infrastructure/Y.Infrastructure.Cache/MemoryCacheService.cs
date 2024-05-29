using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq.Expressions;
using Y.Infrastructure.ICache;

namespace Y.Infrastructure.Cache
{
    public class MemoryCacheService : IMemoryCacheService
    {

        private readonly IMemoryCache _memoryCache;
        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }



        public void InsertSlidingExpirationCache(string key, object value, int minute)
        {
            _memoryCache.Set(key, value, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(minute)));
        }



        public object Get(string key)
        {
            object vl = null;
            _memoryCache.TryGetValue(key, out vl);
            return vl;
        }


        public void Clear(string key)
        {
            _memoryCache.Remove(key);
        }

    }
}
