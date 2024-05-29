using Y.Infrastructure.Library.Core.DbHelper;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IGames;
using Y.Infrastructure.Cache.DbCache.Games;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Helper;
using Y.Packet.Entities.Games;

namespace Y.Packet.Repositories.Games
{
    public class GameLogsLotteryRepository : GameLogsLotteryDbCacheService, IGameLogsLotteryRepository
    {
        public GameLogsLotteryRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {
            _dbOption = options.Get("Ying.Games");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        /// <summary>
        /// 根据游戏类型，来源ID获取投注日志
        /// </summary> 
        /// <param name="gameTypeStr"></param>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        public async Task<GameLogsLottery> GetBySourceIdAsync(string gameTypeStr, string sourceId)
        {
            //string sql = $"SELECT * FROM GameLogsLottery WHERE GameTypeStr='{gameTypeStr}' AND SourceId='{sourceId}'";
            //return await _dbConnection.QuerySingleOrDefaultAsync<GameLogsLottery>(sql
            GameLogsLottery? rt = null;
            var key = $"{gameTypeStr}{sourceId}";
            var exist = await _db.HashExistsAsync(GameLogsLotteryHash, key);
            if (exist)
            {
                var cache = await _db.HashGetAsync(GameLogsLotteryHash, key);
                rt = JsonHelper.JSONToObject<GameLogsLottery>(cache);
            }
            return rt;
        }

        public async Task<int?> InsertWithCacheAsync(GameLogsLottery d)
        {
            var key = $"{d.GameTypeStr}{d.SourceId}";
            var exist = await _db.HashExistsAsync(GameLogsLotteryHash, key);
            if (exist) return 0;
            var id = await _dbConnection.InsertAsync<GameLogsLottery>(d);
            if (id == null || id < 1) return id;
            d.Id = id.Value;
            await CacheAsync(d);
            return id;
        }

        private async Task CacheAsync(GameLogsLottery d)
        {
            await _db.HashSetAsync(GameLogsLotteryHash, $"{d.GameTypeStr}{d.SourceId}", d.ToJson(), StackExchange.Redis.When.Always);
        }

        public async Task<int> UpdateWithCacheAsync(GameLogsLottery d)
        {
            var id = await _dbConnection.UpdateAsync<GameLogsLottery>(d);
            if (id < 1) return id;
            await CacheAsync(d);
            return id;
        }

        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<GameLogsLottery>();
            await _db.KeyDeleteAsync(GameLogsLotteryHash);
            foreach (var d in list)
                await CacheAsync(d);
        }



    }
}