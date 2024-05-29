using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Entities.Promotions;
using Y.Packet.Repositories.IPromotions;

namespace Y.Packet.Repositories.Promotions
{
    public class ActivityStatisticRepository:BaseRepository<ActivityStatistic,Int32>, IActivityStatisticRepository
    {
        public ActivityStatisticRepository(IOptionsMonitor<DbOption> options)
        {
            _dbOption =options.Get("Ying.Promotions");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

		public int DeleteLogical(int[] ids)
        {
            string sql = "update ActivityStatistic set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update ActivityStatistic set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

    }
}