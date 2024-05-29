using Y.Infrastructure.Library.Core.DbHelper;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IMerchants;
using System.Collections.Generic;
using Y.Infrastructure.Cache.DbCache.Merchants;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using StackExchange.Redis;
using Y.Packet.Entities.Merchants;

namespace Y.Packet.Repositories.Merchants
{
    public class DomiansRepository : DomainsDbCacheService, IDomiansRepository
    {
        public DomiansRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {
            _dbOption = options.Get("Ying.Merchants");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }


        public async Task<int> ExistDomainNoAsync(string url)
        {
            string sql = $"SELECT COUNT(*) FROM Domains WHERE Name=@url";
            return await _dbConnection.ExecuteScalarAsync<int>(sql, new { url });
        }

        public async Task<IEnumerable<Domains>> GetByUrlAsync(string url)
        {
            string sql = $"SELECT * FROM Domains WHERE Name='{url}'";
            return await _dbConnection.QueryAsync<Domains>(sql);
        }

        public async Task<IEnumerable<Domains>> GetByLikeUrlAsync(string url)
        {
            //  '%@Key%' 
            DynamicParameters parms = new DynamicParameters();
            parms.Add("parms", $"%{url}%");
            string sql = $"SELECT * FROM Domains WHERE Name like @parms";
            return await _dbConnection.QueryAsync<Domains>(sql, parms);
        }



        public async Task<int?> InsertWithCacheAsync(Domains d)
        {
            var rt = await _dbConnection.InsertAsync<Domains>(d);
            if (rt == null || rt.Value == 0) return rt;
            d.Id = rt.Value;
            await EditDomainsCacheAsync(d);
            return rt;
        }

        public async Task<int?> UpdateWithCacheAsync(Domains d)
        {
            var rt = await _dbConnection?.UpdateAsync(d);
            await EditDomainsCacheAsync(d);
            return rt;
        }

        private async Task EditDomainsCacheAsync(Domains d)
        {
            // url-> site id
            await _db.HashSetAsync(DomainsHash, d.Name, d.MerchantId, When.Always);
            if (d.DoType == Domains.DomainsType.Callback)
                await _db.HashSetAsync(DomainsCallbackUrlHash, d.MerchantId, d.Name, When.Always);
        }


        public async Task<int> GetMerchantIdByDomainAsync(string domian)
        {
            if (await _db.HashExistsAsync(DomainsCallbackUrlHash, domian))
                return (int)(await _db.HashGetAsync(DomainsCallbackUrlHash, domian));
            return default(int);
        }

        public async Task<string> GetCallBackUrlCacheAsync(int merchantId)
        {
            if (await _db.HashExistsAsync(DomainsCallbackUrlHash, merchantId))
                return await _db.HashGetAsync(DomainsCallbackUrlHash, merchantId);
            return string.Empty;
        }


        public async Task<int> DeleteCacheAsync(int id)
        {
            var data = await _dbConnection.GetAsync<Domains>(id);
            if (data == null) return default(int);
            await _db.HashDeleteAsync(DomainsHash, data.Name);
            if (await _db.HashExistsAsync(DomainsCallbackUrlHash, data.MerchantId))
                await _db.HashDeleteAsync(DomainsCallbackUrlHash, data.MerchantId);

            return await _dbConnection.DeleteAsync<Domains>(id);
        }

        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<Domains>();
            foreach (var d in list)
                await EditDomainsCacheAsync(d);
        }

    }
}