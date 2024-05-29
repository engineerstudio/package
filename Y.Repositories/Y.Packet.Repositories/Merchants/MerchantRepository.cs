using Y.Infrastructure.Library.Core.DbHelper;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

using Y.Packet.Repositories.IMerchants;
using System.Collections.Generic;
using Y.Infrastructure.Library.Core.Extensions;
using System.Linq;
using Y.Infrastructure.Cache.DbCache.Merchants;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.CacheFactory.Extension;
using StackExchange.Redis;
using Y.Packet.Entities.Merchants;
using Y.Packet.Entities.Merchants.RedisHashDto;

namespace Y.Packet.Repositories.Merchants
{
    public class MerchantRepository : MerchantDbCacheService, IMerchantRepository
    {
        public MerchantRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
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
            string sql = "update Merchant set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update Merchant set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int?> InsertWithCacheAsync(Merchant mch)
        {
            var d = await _dbConnection.InsertAsync<Merchant>(mch);
            if (d == null || d.Value == 0) return d;
            mch.Id = d.Value;
            await CacheAsync(mch);
            return d;
        }

        private async Task CacheAsync(Merchant d)
        {
            await _db.HashSetAsync($"{MerchantHashEntity}{d.Id}", d.ToHashEntriesFromEntity());
            var dd = new Dictionary<int, string>();
            dd.Add(d.Id, d.Name);
            await _db.HashSetAsync(MerchantIdAndNameDicHash, dd.ToHashEntriesFromDic());
        }

        public async Task<int> UpdateCrediWithCacheAsync(int merchantId, decimal credit)
        {
            string key = $"{MerchantHashEntity}{merchantId}";
            await _db.HashIncrementAsync(key, "GameCredit", (long)(credit * 1000));
            return await UpdateCreditAsync(merchantId, credit);
        }
        private async Task<int> UpdateCreditAsync(int merchantId, decimal credit)
        {
            string sql = $"UPDATE Merchant Set GameCredit +={credit} WHERE Id={merchantId}";
            return await _dbConnection.ExecuteAsync(sql);
        }

        public async Task<decimal> GetCreditFromCacheAsync(int merchantId)
        {
            var d = await _db.HashGetAsync($"{MerchantHashEntity}{merchantId}", "GameCredit");
            return (d.ToString().ToInt64().Value) / 1000;
        }

        public async Task<int> UpdateWithCacheAsync(Merchant mch)
        {
            await CacheAsync(mch);
            await base.KeyDeleteAsync(MerchantDomainDic);
            return await _dbConnection.UpdateAsync<Merchant>(mch);
        }

        public async Task<Dictionary<int, string>> GetMerchantIdAndNameDicAsync() => await base.HashGetDic3Async(MerchantIdAndNameDicHash);

        public async Task<Merchant> GetFromCacheAsync(int merchantId)
        {
            string key = $"{MerchantHashEntity}{merchantId}";
            if (await base.KeyExistsAsync(key))
            {
                var dto = await base.HashGetObjAsync<MerchantDto>(key);
                return dto.ToEntity();
            }

            var mch = await _dbConnection.GetAsync<Merchant>(merchantId);
            await _db.HashSetAsync($"{MerchantHashEntity}{mch.Id}", mch.ToHashEntriesFromEntity());
            return mch;
        }

        /// <summary>
        /// 获取所有域名
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, int>> GetAllDomainsAsync()
        {
            //if (await base.KeyExistsAsync(MerchantDomainDic)) return await base.HashGetDic2Async(MerchantDomainDic);

            Dictionary<string, int> rows = (await _dbConnection.QueryAsync("SELECT Domains,Id FROM Merchant")).ToDictionary(t => (string)t.Domains, t => (int)t.Id);
            Dictionary<string, int> data = new Dictionary<string, int>();
            if (rows.Count > 0)
            {
                foreach (var row in rows)
                {
                    var domains = row.Key.Split(",");
                    var merchantId = row.Value;
                    if (domains.Length > 1)
                    {
                        foreach (var d in domains)
                        {
                            if (!data.Keys.Contains(d))
                                data.Add(d, merchantId);
                        }
                    }
                    else
                        data.Add(row.Key, merchantId);
                }
            }
            await base.HashSetAsync(MerchantDomainDic, data.ToHashEntriesFromDic());
            return data;
        }


        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<Merchant>();
            foreach (var d in list)
                await CacheAsync(d);
        }

    }
}