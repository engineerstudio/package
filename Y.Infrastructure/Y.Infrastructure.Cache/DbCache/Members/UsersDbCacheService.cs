using Microsoft.Extensions.Options;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.ICache.IDbCache.Members;
using Y.Packet.Entities.Members;

namespace Y.Infrastructure.Cache.DbCache.Members
{
    public class UsersDbCacheService : BaseDbCacheService<Users, int>, IUsersDbCacheService
    {
        public UsersDbCacheService(IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {

        }

        protected string UsersEntityHash = "UsersEntityHash";
        protected string MerchantAgentSet = "MerchantAgentSet";
        protected string MerchantAgentConfigSet = "MerchantAgentConfigSet";

    }
}
