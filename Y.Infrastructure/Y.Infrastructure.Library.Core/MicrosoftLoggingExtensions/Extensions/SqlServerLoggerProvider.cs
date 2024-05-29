using Microsoft.Extensions.Logging;

namespace Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions
{
    internal class SqlServerLoggerProvider : ILoggerProvider
    {
        string _dbConnection;

        public SqlServerLoggerProvider()
        {
        }

        public SqlServerLoggerProvider(string dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new SqlServerLogger(categoryName, _dbConnection);
        }

        public ILogger CreateLogger(string categoryName, string dbConnection)
        {
            return new SqlServerLogger(categoryName, _dbConnection);
        }


        public void Dispose()
        {
            if (_dbConnection != null)
                this.Dispose();
        }
    }
}