using Microsoft.Extensions.Options;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.ICache.IDbCache.Games;
using Y.Packet.Entities.Games;

namespace Y.Infrastructure.Cache.DbCache.Games
{
    public class GameLogsLotteryDbCacheService : BaseDbCacheService<GameLogsLottery, int>, IGameLogsLotteryDbCacheService
    {
        public GameLogsLotteryDbCacheService(IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {
        }


        protected string GameLogsLotteryHash = "GameLogsLotteryHash";


    }
}
