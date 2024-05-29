using Microsoft.Extensions.Options;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.ICache.IDbCache.Promotions;
using Y.Packet.Entities.Promotions;

namespace Y.Infrastructure.Cache.DbCache.Promotions
{
    public class PromotionsConfigDbCacheService : BaseDbCacheService<PromotionsConfig, int>, IPromotionsConfigDbCacheService
    {
        public PromotionsConfigDbCacheService(IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {

        }

        protected string ProEntityHash = "ProEntityHash";
        protected string ProListEntityHash = "ProListEntityHash";


    }
}
