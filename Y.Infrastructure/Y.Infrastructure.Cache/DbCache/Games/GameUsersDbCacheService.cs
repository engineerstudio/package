using Microsoft.Extensions.Options;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.ICache.IDbCache.Games;
using Y.Packet.Entities.Games;

namespace Y.Infrastructure.Cache.DbCache.Games
{
    public class GameUsersDbCacheService : BaseDbCacheService<GameUsers, int>, IGameUsersDbCacheService
    {
        public GameUsersDbCacheService(IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {

        }


        protected string GameUsersEntityHash = "GameUsersEntityHash";
        protected string GameUsersPlayerNameHash = "GameUsersPlayerNameHash";
        protected string GameUsersGetMerchantIdAndMemberIdHash = "GameUsersGetMerchantIdAndMemberIdHash";
    }
}
