using System;

namespace Y.Infrastructure.YCache
{
    public interface IYCacheFactory : IDisposable
    {
        IYCache Create(string name, string config);


    }
}
