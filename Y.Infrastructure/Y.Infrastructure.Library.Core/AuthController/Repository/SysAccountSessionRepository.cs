using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using Y.Infrastructure.Library.Core.AuthController.Entity;
using Y.Infrastructure.Library.Core.AuthController.IRepository;

namespace Y.Infrastructure.Library.Core.AuthController.Repository
{
    public class SysAccountSessionRepository : BaseRepository<SysAccountSession, Int32>, ISysAccountSessionRepository
    {
        public SysAccountSessionRepository(IOptionsMonitor<DbOption> options)
        {
            _dbOption = options.Get(AuthDefault.SqlConStr);
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }

            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        public int GetNumberBySession(string session)
        {
            string sql = $"SELECT Count(id) FROM {nameof(SysAccountSession)} WHERE Session=@Session";
            return _dbConnection.ExecuteScalar<int>(sql, new {Session = session});
        }


        public int GetAccountIdBySession(string session)
        {
            string sql = $"SELECT AccountId FROM {nameof(SysAccountSession)} WHERE Session=@Session";
            return _dbConnection.ExecuteScalar<int>(sql, new {Session = session});
        }
    }
}