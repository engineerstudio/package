using Microsoft.Extensions.Options;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.ICache.IDbCache.Promotions;
using Y.Packet.Entities.Promotions;

namespace Y.Infrastructure.Cache.DbCache.Promotions
{
    public class PromotionsTagDbCacheService : BaseDbCacheService<PromotionsTag, int>, IPromotionsTagDbCacheService
    {
        public PromotionsTagDbCacheService(IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {

        }

        protected string ProTagEntityHash = "ProTagEntityHash";

    }
}
