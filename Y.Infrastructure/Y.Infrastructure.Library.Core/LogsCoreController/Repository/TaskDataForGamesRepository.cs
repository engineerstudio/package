using Microsoft.Extensions.Options;
using System;
using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.LogsCoreController.Entity;
using Y.Infrastructure.Library.Core.LogsCoreController.IRepository;
using Y.Infrastructure.Library.Core.Repository;

namespace Y.Infrastructure.Library.Core.LogsCoreController.Repository
{
    public class TaskDataForGamesRepository : BaseRepository<TaskDataForGames, Int32>, ITaskDataForGamesRepository
    {
        public TaskDataForGamesRepository(IOptionsMonitor<DbOption> options)
        {
            _dbOption = options.Get(LogsCoreDefault.SqlConStr);
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }

            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }
    }
}