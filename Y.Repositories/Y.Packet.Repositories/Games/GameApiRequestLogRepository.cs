using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IGames;
using Y.Packet.Entities.Games;
using Dapper;

namespace Y.Packet.Repositories.Games
{
    public class GameApiRequestLogRepository:BaseRepository<GameApiRequestLog,Int32>, IGameApiRequestLogRepository
    {
        public GameApiRequestLogRepository(IOptionsMonitor<DbOption> options)
        {
            _dbOption =options.Get("Ying.Games");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

		public int DeleteLogical(int[] ids)
        {
            string sql = "update GameApiRequestLog set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update GameApiRequestLog set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

    }
}