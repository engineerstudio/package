using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Configuration;
using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Cache;

using Y.Infrastructure.YCache.Entity;
using Y.Infrastructure.YCache.Extensions;
using Y.Infrastructure.YCache.YCacheImplementation;

namespace Y.Infrastructure.YCache.Test
{
    [TestClass]
    public class ProjectInitializationService_Test
    {
        [TestMethod]
        public void TestMethod1()
        {

            var pService = BuildServiceForSqlServer();
            var service = pService.GetRequiredService<ProjectInitializationService>();

            //IProjectInitializationService pis = (ProjectInitializationService)services.GetService<IProjectInitializationService>();
        }

        public static IServiceProvider BuildServiceForSqlServer()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddYCache();

            services.Configure<YCacheConfiguration>(DefaultString.Ying_Redis, GetConfiguration().GetSection("RedisConfig"));
            services.AddScoped<ProjectInitializationService>();
            return services.BuildServiceProvider();

        }

        public static IConfiguration GetConfiguration()
        {
            var configuration = new ConfigurationBuilder();
            configuration.AddJsonFile("appsettings.json");
            return configuration.Build();
        }


    }
}
