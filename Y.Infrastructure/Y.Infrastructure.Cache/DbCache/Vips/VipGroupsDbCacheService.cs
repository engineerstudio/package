using Microsoft.Extensions.Options;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.ICache.IDbCache.Vips;
using Y.Packet.Entities.Vips;

namespace Y.Infrastructure.Cache.DbCache.Vips
{
    public class VipGroupsDbCacheService : BaseDbCacheService<VipGroups, int>, IVipGroupsDbCacheService
    {
        public VipGroupsDbCacheService(IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {

        }

        protected string VipGroupsHashEntity = "VipGroupsHashEntity";
        protected string VipGroupsTempHashEntity = "VipGroupsTempHashEntity";
        protected string VipGroupsIdAndNameDicHashEntity = "VipGroupsIdAndNameDicHashEntity";
    }
}
