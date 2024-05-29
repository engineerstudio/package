using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IGames;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Cache.DbCache.Games;
using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Dapper;
using Y.Infrastructure.Library.Core.Helper;
using Y.Packet.Entities.Games;

namespace Y.Packet.Repositories.Games
{
    public class GameApiTimestampsRepository : GameApiTimestampsDbCacheService, IGameApiTimestampsRepository
    {
        public GameApiTimestampsRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {
            _dbOption = options.Get("Ying.Games");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        public int DeleteLogical(int[] ids)
        {
            string sql = "update GameApiTimestamps set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update GameApiTimestamps set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        //public async Task<GameApiTimestamps> GetByGameTypeAsync(GameType gameType)
        //{
        //    string sql = $"SELECT * FROM GameApiTimestamps WHERE Type={gameType.GetEnumValue()}";

        //    return await _dbConnection.QuerySingleOrDefaultAsync<GameApiTimestamps>(sql);

        //}


        public async Task<GameApiTimestamps> GetByTypeStrAsync(string gameType)
        {
            //string sql = $"SELECT * FROM GameApiTimestamps WHERE TypeStr='{str}'";
            //return await _dbConnection.QuerySingleOrDefaultAsync<GameApiTimestamps>(sql);

            string cache = await _db.HashGetAsync(GameApiTimestampsEntityHash, gameType);
            if (cache == null) return null;
            return JsonHelper.JSONToObject<GameApiTimestamps>(cache);
        }

        private async Task CacheAsync(GameApiTimestamps d)
        {
            await _db.HashSetAsync(GameApiTimestampsEntityHash, d.TypeStr, d.ToJson(), StackExchange.Redis.When.Always);
        }

        public async Task<int?> InsertWithCacheAsync(GameApiTimestamps d)
        {
            var id = await _dbConnection.InsertAsync<GameApiTimestamps>(d);
            if (id == null || id < 1) return id;
            d.Id = id.Value;
            await CacheAsync(d);
            return id;
        }

        public async Task<int> UpdateWithCacheAsync(GameApiTimestamps d)
        {
            var id = await _dbConnection.UpdateAsync<GameApiTimestamps>(d);
            if (id < 1) return id;
            await CacheAsync(d);
            return id;
        }


        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<GameApiTimestamps>();
            foreach (var d in list)
                await CacheAsync(d);
        }
    }
}