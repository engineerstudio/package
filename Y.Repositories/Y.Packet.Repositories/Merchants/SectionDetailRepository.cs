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
using System.Collections.Generic;
using System.Linq;
using Y.Packet.Entities.Merchants;

namespace Y.Packet.Repositories.Merchants
{
    public class SectionDetailRepository : SectionDetailDbCacheService, ISectionDetailRepository
    {
        public SectionDetailRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
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
            string sql = "update SectionDetail set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update SectionDetail set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }


        public async Task<int?> InsertWithCacheAsync(SectionDetail d)
        {
            var id = await _dbConnection.InsertAsync<SectionDetail>(d);
            if (id == null || id.Value == 0) return id;
            d.Id = id.Value;
            string cache = d.ToJson();
            await _db.SetAddAsync($"{SectionDetailEntityHash}{d.MerchantId}", cache);
            await _db.HashSetAsync(SectionDetailEntityHash, d.Id, cache);
            return id;
        }

        public async Task<int> UpdateWithCacheAsync(SectionDetail d)
        {
            //var cache = await _db.HashGetAsync(SectionDetailEntityHash, d.Id);
            //await _db.SetRemoveAsync($"{SectionDetailEntityHash}{d.MerchantId}", cache);
            var id = await _dbConnection.UpdateAsync<SectionDetail>(d);
            //await _db.SetAddAsync($"{SectionDetailEntityHash}{d.MerchantId}", cache);
            var dd = await _dbConnection.GetAsync<SectionDetail>(d.Id);
            await CacheAsync(dd);
            return id;
        }


        private async Task CacheAsync(SectionDetail d)
        {
            string cache = await _db.HashGetAsync(SectionDetailEntityHash, d.Id);
            await _db.SetRemoveAsync($"{SectionDetailEntityHash}{d.MerchantId}", cache);

            cache = d.ToJson();
            await _db.HashSetAsync(SectionDetailEntityHash, d.Id, cache, StackExchange.Redis.When.Always);
            await _db.SetAddAsync($"{SectionDetailEntityHash}{d.MerchantId}", cache);
        }



        public async Task<List<SectionDetail>> GetListAsync(int merchantId, bool? enabled, int? secKeyId)
        {
            var list = new List<SectionDetail>();
            var listStrs = await _db.SetMembersAsync($"{SectionDetailEntityHash}{merchantId}");
            foreach (var str in listStrs)
                list.Add(JsonHelper.JSONToObject<SectionDetail>(str));

            if (enabled != null)
                list = list.Where(t => t.Enabled == enabled).ToList();

            if (secKeyId != null)
                list = list.Where(t => t.SectionId == secKeyId).ToList();

            return list;
        }
        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<SectionDetail>();
            foreach (var mchId in list.Select(t => t.MerchantId))
            {
                await _db.KeyDeleteAsync($"{SectionDetailEntityHash}{mchId}");
            }
            foreach (var d in list)
            {
                string cache = d.ToJson();
                await _db.HashSetAsync(SectionDetailEntityHash, d.Id, cache);
                await _db.SetAddAsync($"{SectionDetailEntityHash}{d.MerchantId}", cache);
            }
        }


    }
}