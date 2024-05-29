using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Infrastructure.ICache.IRedis
{
    public interface IValidationCodeCacheService
    {
        void SetCodeCache(string key, string value);
        bool ExistKey(string key);
        void DeleteByKey(string key);
        string GetByKey(string key);
    }
}
