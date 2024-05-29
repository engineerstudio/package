using Microsoft.Extensions.Options;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.ICache.IDbCache.Games;

namespace Y.Infrastructure.Cache.DbCache.Games
{
    public class GameMerchantDbCacheService : BaseDbCacheService<Packet.Entities.Games.GameMerchant, int>, IGameMerchantDbCacheService
    {
        public GameMerchantDbCacheService(IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {

        }
        protected string GameMerchantEntityHash = "GameMerchantEntityHash";
        protected string GameMerchantEntitySetHash = "GameMerchantEntitySetHash";
        protected string GameMerchantTypeAndNameDic = "GameMerchantTypeAndNameDic";
    }
}
