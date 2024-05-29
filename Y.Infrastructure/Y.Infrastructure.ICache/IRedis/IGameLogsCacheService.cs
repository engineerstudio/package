using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;

namespace Y.Infrastructure.ICache
{
    public interface IGameLogsCacheService : IBaseRedisRepository
    {
        Task ListLeftPushAsync(string key, string jsonValue);
        Task<List<T>> ListRightPopAsync<T>(string key);
    }
}
