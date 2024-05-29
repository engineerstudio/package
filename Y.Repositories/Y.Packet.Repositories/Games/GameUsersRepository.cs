using Y.Infrastructure.Library.Core.DbHelper;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IGames;
using System.Linq;
using System.Collections.Generic;
using Y.Infrastructure.Cache.DbCache.Games;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Packet.Entities.Games;

namespace Y.Packet.Repositories.Games
{
    public class GameUsersRepository : GameUsersDbCacheService, IGameUsersRepository
    {
        public GameUsersRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
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
            string sql = "update GameUsers set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update GameUsers set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteAsync(int memeberId, GameType gameType)
        {
            string sql = $"SELECT * FROM GameUsers WHERE MemeberId={memeberId} AND TypeStr = '{gameType.ToString()}'";
            var d = await _dbConnection.QueryFirstOrDefaultAsync<GameUsers>(sql);
            if (d == null) return 0;
            var cacheMember = await _db.SetMembersAsync($"{GameUsersEntityHash}{d.MerchantId}{d.MemberId}");
            await _db.SetRemoveAsync($"{GameUsersEntityHash}{d.MerchantId}{d.MemberId}", cacheMember);

            await _db.HashDeleteAsync(GameUsersPlayerNameHash, $"{d.MemberId}{d.TypeStr}");
            await _db.HashDeleteAsync(GameUsersGetMerchantIdAndMemberIdHash, $"{d.TypeStr}{d.PlayerName}");
            return d.Id;
        }

        public async Task<(int MerchantId, int MemberId)> GetByPlayerNameAsync(string gameStr, string playerName)
        {
            string sql = $"SELECT MerchantId,MemberId FROM GameUsers WHERE TypeStr = '{gameStr}' AND PlayerName = '{playerName}'";
            Dictionary<int, int> dic = (await _dbConnection.QueryAsync(sql)).ToDictionary(t => (int)t.MerchantId, t => (int)t.MemberId);
            if (dic == null) return (0, 0);
            KeyValuePair<int, int> kv = dic.FirstOrDefault();
            return (kv.Key, kv.Value);
        }

        private async Task CacheAsync(GameUsers d)
        {
            await _db.SetAddAsync($"{GameUsersEntityHash}{d.MerchantId}{d.MemberId}", d.ToJson());
            await _db.HashSetAsync(GameUsersPlayerNameHash, $"{d.MemberId}{d.TypeStr}", d.PlayerName);
            await _db.HashSetAsync(GameUsersGetMerchantIdAndMemberIdHash, $"{d.TypeStr}{d.PlayerName}", $"{d.MerchantId},{d.MemberId}");

        }
        public async Task<int?> InsertWithCacheAsync(GameUsers d)
        {
            var id = await _dbConnection.InsertAsync<GameUsers>(d);
            if (id == null || id < 1) return id;
            d.Id = id.Value;
            await CacheAsync(d);
            return id;
        }


        public async Task<string> GetPlayerNameAsync(int MemberId, GameType gameType)
        {
            var t = await _db.HashExistsAsync(GameUsersPlayerNameHash, $"{MemberId}{gameType.ToString()}");
            if (!t) return null;

            return await _db.HashGetAsync(GameUsersPlayerNameHash, $"{MemberId}{gameType.ToString()}");
        }

        public async Task<(int MerchantId, int MemberId)> GetByMerchantIdAndMemberIdAsync(string gameStr, string playerName)
        {
            string str = await _db.HashGetAsync(GameUsersGetMerchantIdAndMemberIdHash, $"{gameStr}{playerName}");
            var arr = str.Split(',');
            return (arr[0].ToInt32().Value, arr[1].ToInt32().Value);
        }

        public async Task<IEnumerable<GameUsers>> GetListAsync(int merchantId, int memberId)
        {
            var listStr = await _db.SetMembersAsync($"{GameUsersEntityHash}{merchantId}{memberId}");
            var d = new List<GameUsers>();
            foreach (var ll in listStr)
                d.Add(JsonHelper.JSONToObject<GameUsers>(ll));
            return d;
        }

        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<GameUsers>();
            foreach (var d in list)
                await _db.KeyDeleteAsync($"{GameUsersEntityHash}{d.MerchantId}{d.MemberId}");
            foreach (var d in list)
                await CacheAsync(d);
        }

    }
}