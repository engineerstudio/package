using Microsoft.Extensions.Options;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.ICache.IDbCache.Merchants;
using Y.Packet.Entities.Merchants;

namespace Y.Infrastructure.Cache.DbCache.Merchants
{
    public class SectionDetailDbCacheService : BaseDbCacheService<SectionDetail, int>, ISectionDetailDbCacheService
    {
        public SectionDetailDbCacheService(IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {

        }


        protected string SectionDetailEntityHash = "SectionDetailEntityHash";




    }
}
