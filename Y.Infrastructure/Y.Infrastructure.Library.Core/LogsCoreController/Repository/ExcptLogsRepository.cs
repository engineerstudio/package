using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.LogsCoreController.Entity;
using Y.Infrastructure.Library.Core.LogsCoreController.IRepository;

namespace Y.Infrastructure.Library.Core.LogsCoreController.Repository
{
    public class ExcptLogsRepository : BaseRepository<ExcptLogs, Int32>, IExcptLogsRepository
    {
        public ExcptLogsRepository(IOptionsMonitor<DbOption> options)
        {
            _dbOption = options.Get(LogsCoreDefault.SqlConStr);
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }

            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        public int DeleteLogical(int[] ids)
        {
            string sql = "update Logs set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update Logs set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }
    }
}