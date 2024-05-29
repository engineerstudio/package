using System;
using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions.Model;
using Y.Infrastructure.Library.Core.Repository;

namespace Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions.Repository
{
    class DebugLogRepository : BaseRepository<DebugLog, Int32>, IDebugLogRepository
    {
        public DebugLogRepository(DbOption options)
        {
            if (options == null)
            {
//#if DEBUG
//                _dbOption = new DbOption();
//                _dbOption.DbType = "SqlServer";
//                _dbOption.ConnectionString =
//                    "Data Source=.;Initial Catalog=Qing.Logs;User ID=sa;Password=MsSql2019;Persist Security Info=True;Max Pool Size=50;Min Pool Size=0;Connection Lifetime=300;";
//#endif
//#if !DEBUG
//                var ops =
// (IOptionsSnapshot<DbOption>)ProcessHelper.HttpContextInstance.RequestServices.GetService(typeof(IOptionsSnapshot<DbOption>));
//                _dbOption = ops.Get(DefaultString.LogDb);
//#endif
            }
            else
                _dbOption = options;


            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }

            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }
    }
}