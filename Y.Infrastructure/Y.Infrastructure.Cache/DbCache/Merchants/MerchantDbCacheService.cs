using Microsoft.Extensions.Options;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.ICache.IDbCache.Merchants;
using Y.Packet.Entities.Merchants;

namespace Y.Infrastructure.Cache.DbCache.Merchants
{
    public class MerchantDbCacheService : BaseDbCacheService<Merchant, int>, IMerchantDbCacheService
    {
        public MerchantDbCacheService(IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {

        }
        protected string MerchantHashEntity = "MerchantHashEntity";
        protected string MerchantDomainDic = "MerchantDomainDic";
        protected string MerchantIdAndNameDicHash = "MerchantIdAndNameDicHash";


    }
}
