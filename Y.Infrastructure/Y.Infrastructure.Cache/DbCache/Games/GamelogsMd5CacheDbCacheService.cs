using Microsoft.Extensions.Options;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.ICache.IDbCache.Games;
using Y.Packet.Entities.Games;

namespace Y.Infrastructure.Cache.DbCache.Games
{
    public class GamelogsMd5CacheDbCacheService : BaseDbCacheService<GamelogsMd5Cache, int>, IGamelogsMd5CacheDbCacheService
    {
        public GamelogsMd5CacheDbCacheService(IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {

        }

        protected string GamelogsMd5CacheEntityHash = "GamelogsMd5CacheEntityHash";

    }
}
