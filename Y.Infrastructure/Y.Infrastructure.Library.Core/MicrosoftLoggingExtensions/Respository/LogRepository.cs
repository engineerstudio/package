using Microsoft.Extensions.Options;
using System;
using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions.Model;
using Y.Infrastructure.Library.Core.Repository;

namespace Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions.Repository
{
    class LogRepository : BaseRepository<ExcptLogs, Int32>, ILogRepository
    {
        public LogRepository(DbOption options)
        {
            _dbOption = options;

            if (_dbOption == null)
                _dbOption =
                    (DbOption) ProcessHelper.HttpContextInstance.RequestServices.GetService(
                        typeof(IOptionsMonitor<DbOption>));

            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }

            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }
    }
}