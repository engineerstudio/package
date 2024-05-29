using Microsoft.Extensions.Options;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.ICache.IDbCache.Pay;
using Y.Packet.Entities.Pay;

namespace Y.Infrastructure.Cache.DbCache.Pay
{
    public class WithdrawMerchantDbCacheService : BaseDbCacheService<WithdrawMerchant, int>, IWithdrawMerchantDbCacheService
    {
        public WithdrawMerchantDbCacheService(IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {

        }

        protected string WithdrawMerchantEntityHash = "WithdrawMerchantEntityHash";


    }
}
