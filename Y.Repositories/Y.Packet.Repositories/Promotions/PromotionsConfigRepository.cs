using Y.Infrastructure.Library.Core.DbHelper;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Entities.Promotions;
using Y.Packet.Repositories.IPromotions;
using System.Collections.Generic;
using System.Linq;
using Y.Infrastructure.Cache.DbCache.Promotions;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Helper;

namespace Y.Packet.Repositories.Promotions
{
    public class PromotionsConfigRepository : PromotionsConfigDbCacheService, IPromotionsConfigRepository
    {
        public PromotionsConfigRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {
            _dbOption = options.Get("Ying.Promotions");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        public int DeleteLogical(int[] ids)
        {
            string sql = "update PromotionsConfig set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update PromotionsConfig set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> UpdateConfigAsync(int id, string config)
        {
            string sql = $"UPDATE  PromotionsConfig set Config=@config where Id = {id}";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                config = config
            });
        }

        public async Task<PromotionsConfig> GetAsync(int merchantId, int promoId)
        {
            string sql = $" SELECT * FROM PromotionsConfig WHERE MerchantId = {merchantId} AND Id = {promoId}";
            return await _dbConnection.QueryFirstOrDefaultAsync<PromotionsConfig>(sql);
        }


        public async Task<Dictionary<int, string>> GetHomeDisplayAsync(int merchantId)
        {
            string sql = $"SELECT Id,PcCover FROM PromotionsConfig WHERE MerchantId={merchantId} AND HomeDisplay = 1";
            return (await _dbConnection.QueryAsync(sql)).ToDictionary(t => (int)t.Id, t => (string)t.PcCover);
        }

        public async Task<int?> InsertWithCacheAsync(PromotionsConfig config)
        {
            var id = await _dbConnection.InsertAsync<PromotionsConfig>(config);
            if (id == null || id.Value < 1) return id;

            config.Id = id.Value;
            await CacheAsync(config);

            return id;
        }

        private async Task CacheAsync(PromotionsConfig config)
        {
            string cache = config.ToJson();
            await _db.HashSetAsync(ProEntityHash, $"{config.MerchantId}:{config.Id}", cache, StackExchange.Redis.When.Always);
            await _db.SetAddAsync($"{ProListEntityHash}{config.MerchantId}", cache);
            await _db.SetAddAsync(ProListEntityHash, cache);
        }


        public async Task<int?> UpdateWithCacheAsync(PromotionsConfig config)
        {
            var d = await _dbConnection.UpdateAsync<PromotionsConfig>(config);
            if (d < 1) return d;

            var cache = await _db.HashGetAsync(ProEntityHash, $"{config.MerchantId}:{config.Id}");
            await _db.SetRemoveAsync($"{ProListEntityHash}{config.MerchantId}", cache);
            await _db.SetRemoveAsync(ProListEntityHash, cache);

            await CacheAsync(config);

            return d;
        }

        public async Task<PromotionsConfig> GetFromCacheAsync(int merchantId, int promoId)
        {
            string cacheStr = await _db.HashGetAsync(ProEntityHash, $"{merchantId}:{promoId}");
            var d = JsonHelper.JSONToObject<PromotionsConfig>(cacheStr);
            return d;
        }

        public async Task<IEnumerable<PromotionsConfig>> GetListAsync(int? merchantId, ActivityType? activityType = null, bool? enabled = null, bool? visible = null, DateTime? startTime = null, DateTime? endTime = null)
        {
            string key = merchantId == null ? ProListEntityHash : $"{ProListEntityHash}{merchantId}";
            var listJsonStr = await _db.SetMembersAsync(key);

            List<PromotionsConfig> data = new List<PromotionsConfig>();
            foreach (var ll in listJsonStr)
                data.Add(JsonHelper.JSONToObject<PromotionsConfig>(ll));

            if (activityType != null)
                data = data.Where(t => t.AType == activityType).ToList();
            if (enabled != null)
                data = data.Where(t => t.Enabled == enabled).ToList();
            if (visible != null)
                data = data.Where(t => t.Visible == visible).ToList();
            if (startTime != null)
                data = data.Where(t => t.StartTime >= startTime).ToList();
            if (endTime != null)
                data = data.Where(t => t.EndTime <= endTime).ToList();

            return data;
        }


        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<PromotionsConfig>();
            await _db.KeyDeleteAsync(ProListEntityHash);
            foreach (var mchId in list.Select(t => t.MerchantId))
                await _db.KeyDeleteAsync($"{ProListEntityHash}{mchId}");
            foreach (var d in list)
                await CacheAsync(d);
        }
    }
}