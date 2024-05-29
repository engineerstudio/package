using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IGames;
using System.Collections.Generic;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Packet.Entities.Games;

namespace Y.Packet.Repositories.Games
{
    public class GameDailyReportStatisticRepository : BaseRepository<GameDailyReportStatistic, Int32>, IGameDailyReportStatisticRepository
    {
        public GameDailyReportStatisticRepository(IOptionsMonitor<DbOption> options)
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
            string sql = "update GameDailyReportStatistic set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update GameDailyReportStatistic set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }


        public async Task<IEnumerable<GameDailyReportStatistic>> GetAsync(int merchantId, int category, string typeStr, DateTime startTime, DateTime endTime)
        {
            string sql = string.Empty;
            sql = $"SELECT * FROM GameDailyReportStatistic WHERE MerchantId ={merchantId}";
            if (category != 0)
                sql += $" AND GameCategory = {category} ";
            if (!typeStr.IsNullOrEmpty())
                sql += $" AND GameTypeStr IN ('{typeStr}') ";
            sql += $" AND Date BETWEEN '{startTime}' AND '{endTime}'";

            return await _dbConnection.QueryAsync<GameDailyReportStatistic>(sql);
        }

    }
}