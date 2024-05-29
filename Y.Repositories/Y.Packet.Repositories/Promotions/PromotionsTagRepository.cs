using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Entities.Promotions;
using Y.Packet.Repositories.IPromotions;
using Y.Infrastructure.Cache.DbCache.Promotions;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Helper;
using System.Collections.Generic;
using System.Linq;

namespace Y.Packet.Repositories.Promotions
{
    public class PromotionsTagRepository : PromotionsTagDbCacheService, IPromotionsTagRepository
    {
        public PromotionsTagRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
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
            string sql = "update PromotionsTag set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update PromotionsTag set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteAsync(int merchantId, int id)
        {
            string sql = $"delete from PromotionsTag where MerchantId={merchantId} AND Id={id}";

            await UpdateSetCacheAsync(merchantId, id);
            await _db.HashDeleteAsync(ProTagEntityHash, $"{merchantId}{id}");

            return await _dbConnection.ExecuteAsync(sql);
        }

        public async Task<int?> InsertWithCacheAsync(PromotionsTag d)
        {
            var id = await _dbConnection.InsertAsync<PromotionsTag>(d);
            if (id == null || id < 1) return id;

            d.Id = id.Value;
            await CacheAsync(d);

            return id;
        }


        public async Task CacheAsync(PromotionsTag d)
        {
            await _db.HashSetAsync(ProTagEntityHash, $"{d.MerchantId}{d.Id}", d.ToJson());
            await _db.SetAddAsync($"{ProTagEntityHash}{d.MerchantId}", d.ToJson());
        }

        public async Task UpdateSetCacheAsync(int merchantId, int id)
        {
            var cache = await _db.HashGetAsync(ProTagEntityHash, $"{merchantId}{id}");
            await _db.SetRemoveAsync($"{ProTagEntityHash}{merchantId}", cache);

        }

        public async Task<PromotionsTag> GetFromCacheAsync(int merchantId, int id)
        {
            var str = await _db.HashGetAsync(ProTagEntityHash, $"{merchantId}{id}");
            return JsonHelper.JSONToObject<PromotionsTag>(str);
        }

        public async Task<int> UpdateWithCacheAsync(PromotionsTag d)
        {
            var id = await _dbConnection.UpdateAsync<PromotionsTag>(d);
            if (id < 1) return id;

            await UpdateSetCacheAsync(d.MerchantId, d.Id);
            await CacheAsync(d);

            return id;
        }

        public async Task<IEnumerable<PromotionsTag>> GetListAsync(int merchantId)
        {
            var strStr = await _db.SetMembersAsync($"{ProTagEntityHash}{merchantId}");
            var list = new List<PromotionsTag>();
            foreach (var str in strStr)
                list.Add(JsonHelper.JSONToObject<PromotionsTag>(str));
            return list;
        }

        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<PromotionsTag>();
            foreach (var mchId in list.Select(t => t.MerchantId))
                await _db.KeyDeleteAsync($"{ProTagEntityHash}{mchId}");
            foreach (var d in list)
                await CacheAsync(d);
        }
    }
}