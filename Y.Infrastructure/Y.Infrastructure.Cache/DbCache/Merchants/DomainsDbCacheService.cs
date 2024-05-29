using Microsoft.Extensions.Options;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.ICache.IDbCache.Merchants;
using Y.Packet.Entities.Merchants;

namespace Y.Infrastructure.Cache.DbCache.Merchants
{
    public class DomainsDbCacheService : BaseDbCacheService<Domains, int>, IDomainsDbCacheService
    {
        public DomainsDbCacheService(IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {

        }


        protected string DomainsHash = "DomainsHash";
        protected string DomainsCallbackUrlHash = "DomainsCallbackUrlHash";

    }
}
