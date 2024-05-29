using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IMerchants;

using System.Collections.Generic;
using Y.Packet.Entities.Merchants;

namespace Y.Packet.Repositories.Merchants
{
    public class HelpAreaRepository : BaseRepository<HelpArea, Int32>, IHelpAreaRepository
    {
        public HelpAreaRepository(IOptionsMonitor<DbOption> options)
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
            string sql = "update HelpArea set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update HelpArea set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<IEnumerable<int>> GetTypeIdsAsync(int merchantId)
        {
            string sql = $"SELECT TypeId FROM HelpArea WHERE MerchantId={merchantId} GROUP BY TypeId";
            return await _dbConnection.QueryAsync<int>(sql);
        }

        public async Task<int> GetDataCountAsync(int merchantId, int id)
        {
            string sql = $"SELECT COUNT(*) FROM HelpArea WHERE MerchantId={merchantId} AND ID={id}";
            return await _dbConnection.ExecuteScalarAsync<int>(sql);
        }


        public async Task<IEnumerable<HelpArea>> GetShowHelpAreaAsync(int merchantId)
        {
            string sql = $"SELECT * FROM HelpArea WHERE  MerchantId={merchantId} AND ShowIndexPage = 1";
            return await _dbConnection.QueryAsync<HelpArea>(sql);
        }

    }
}