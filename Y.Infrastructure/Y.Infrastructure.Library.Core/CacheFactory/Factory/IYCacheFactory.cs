using System;

namespace Y.Infrastructure.Library.Core.CacheFactory.Factory
{
    public interface IYCacheFactory : IDisposable
    {
        IYCache Create(string name, string config);
    }
}