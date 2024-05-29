using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IMerchants;

using Y.Infrastructure.Cache.DbCache.Merchants;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Helper;
using System.Linq;
using System.Collections.Generic;
using Y.Packet.Entities.Merchants;

namespace Y.Packet.Repositories.Merchants
{
    public class SectionKeyRepository : SectionKeyDbCacheService, ISectionKeyRepository
    {
        public SectionKeyRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {
            _dbOption = options.Get("Ying.Merchants");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        public int DeleteLogical(int[] ids)
        {
            string sql = "update SectionKey set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update SectionKey set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<SectionKey> GetByKeyAsync(int merchantId, string key)
        {
            //string sql = $"SELECT * FROM SectionKey WHERE MerchantId={merchantId} AND SKey=@key";
            //return await _dbConnection.QueryFirstOrDefaultAsync<SectionKey>(sql, new { key = key });

            key = key.Replace("-", "");
            var d = await _db.HashGetAsync($"{SectionKeyEntityHash}{merchantId}Skey", key);
            return JsonHelper.JSONToObject<SectionKey>(d);
        }

        public async Task<SectionKey> GetByMerchantIdAndIdAsync(int merchantId, int id)
        {
            string sql = $"SELECT * FROM SectionKey WHERE MerchantId={merchantId} AND Id={id}";
            return await _dbConnection.QueryFirstOrDefaultAsync<SectionKey>(sql);
        }

        public async Task<int?> InsertWithCacheAsync(SectionKey d)
        {
            var id = await _dbConnection.InsertAsync<SectionKey>(d);
            if (id == null || id.Value == 0) return id;
            d.Id = id.Value;
            await _db.SetAddAsync($"{SectionKeyEntityHash}{d.MerchantId}", d.ToJson());
            await _db.HashSetAsync($"{SectionKeyEntityHash}{d.MerchantId}Skey", d.SKey.Replace("-", ""), d.ToJson());
            return id;
        }

        public async Task<List<SectionKey>> GetListAsync(int merchantId, string[] secIds)
        {
            var list = new List<SectionKey>();
            foreach (var sec in secIds)
            {
                var str = await _db.HashGetAsync($"{SectionKeyEntityHash}{merchantId}Skey", sec.Replace("-", ""));
                list.Add(JsonHelper.JSONToObject<SectionKey>(str));
            };
            return list;
        }

        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<SectionKey>();
            foreach (var mchId in list.Select(t => t.MerchantId))
                await _db.KeyDeleteAsync($"{SectionKeyEntityHash}{mchId}");
            foreach (var d in list)
            {
                await _db.SetAddAsync($"{SectionKeyEntityHash}{d.MerchantId}", d.ToJson());
                string key = d.SKey.Replace("-", "");
                await _db.HashSetAsync($"{SectionKeyEntityHash}{d.MerchantId}Skey", key, d.ToJson());
            }
        }


    }
}