using Microsoft.Extensions.Options;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.ICache.IDbCache.Members;
using Y.Packet.Entities.Members;

namespace Y.Infrastructure.Cache.DbCache.Members
{
    public class UsersBankDbCacheService : BaseDbCacheService<UsersBank, int>, IUsersBankDbCacheService
    {
        public UsersBankDbCacheService(IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {

        }

        protected string UsersBankEntityHash = "UsersBankEntityHash";

    }
}
