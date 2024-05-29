using Microsoft.Extensions.Logging;

namespace Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions
{
    public static class LoggerFactoryExtensions
    {
        //public static ILoggerFactory AddSqlServerLogger(this ILoggerFactory factory)
        //{
        //    factory.AddProvider(new SqlServerLoggerProvider());
        //    return factory;
        //}

        public static ILoggerFactory AddSqlServerLogger(this ILoggerFactory factory, string connection)
        {
            factory.AddProvider(new SqlServerLoggerProvider(connection));
            return factory;
        }
    }
}