using Microsoft.Extensions.Options;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.ICache.IDbCache.Pay;
using Y.Packet.Entities.Pay;

namespace Y.Infrastructure.Cache.DbCache.Pay
{
    public class PayMerchantDbService : BaseDbCacheService<PayMerchant, int>, IPayMerchantDbService
    {
        public PayMerchantDbService(IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {

        }

        protected string PayMerchantEntityHash = "PayMerchantEntityHash";
        protected string PayMerchantSetEntityHash = "PayMerchantSetEntityHash";

    }
}
