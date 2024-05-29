using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IGames;
using Y.Packet.Entities.Games;

namespace Y.Packet.Repositories.Games
{
    public class GameLogsFinanceRepository : BaseRepository<GameLogsFinance, Int32>, IGameLogsFinanceRepository
    {
        public GameLogsFinanceRepository(IOptionsMonitor<DbOption> options)
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
            string sql = "update GameLogsFinance set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update GameLogsFinance set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        /// <summary>
        /// 根据游戏类型，来源ID获取投注日志
        /// </summary>
        /// <param name="gameTypeStr"></param>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        public async Task<GameLogsFinance> GetBySourceIdAsync(string gameTypeStr, string sourceId)
        {
            string sql = $"SELECT * FROM GameLogsFinance WHERE GameTypeStr='{gameTypeStr}' AND SourceId='{sourceId}'";
            return await _dbConnection.QuerySingleOrDefaultAsync<GameLogsFinance>(sql);
        }

    }
}