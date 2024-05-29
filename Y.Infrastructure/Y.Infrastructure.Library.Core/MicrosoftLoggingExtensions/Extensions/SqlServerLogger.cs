using Microsoft.Extensions.Logging;
using System;
using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions.Model;
using Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions.Repository;

namespace Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions
{
    internal class SqlServerLogger : ILogger
    {
        private string _categoryName;

        private string _dbConnection;

        //public SqlServerLogger(string categoryName)
        //{
        //    this._categoryName = categoryName;
        //}
        public SqlServerLogger(string categoryName, string dbConnection)
        {
            this._categoryName = categoryName;
            _dbConnection = dbConnection;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            //return new Scope(loggers.Select(logger => logger.BeginScope(state)));
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            if (logLevel == LogLevel.Error)
                return true;
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            var logger = formatter?.Invoke(state, exception);


            SaveLog(logLevel, eventId, exception, logger);
        }

        private void SaveLog(LogLevel logLevel, EventId eventId, Exception exception, string logger,
            string machineName = "", string message = "", string callSite = "", string stackTrace = "")
        {
            if (_dbConnection == null) return;

            // 写入数据库
            var options = new DbOption()
            {
                ConnectionString = _dbConnection,
                DbType = "SqlServer"
            };

            // 获取详细错误信息
            string errorMsg = $"{exception?.Message} \n {exception?.StackTrace} \n {exception?.ToString()}";

            //machineName = exception.Message.Length > 20 ? exception.Message.Substring(0, 19);
            if (string.IsNullOrEmpty(machineName)) machineName = "";
            ILogRepository logDb = new LogRepository(options);
            var log = new ExcptLogs()
            {
                MachineName = machineName,
                Logged = DateTime.UtcNow.AddHours(8),
                Level = logLevel.ToString(),
                Logger = logger,
                Message = message,
                CallSite = callSite,
                Exception = errorMsg,
                StackTrace = stackTrace
            };

            logDb.Insert(log);
        }
    }
}