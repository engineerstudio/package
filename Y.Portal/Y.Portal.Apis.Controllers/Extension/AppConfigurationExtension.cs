using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.AuthController;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.LogsCoreController;

namespace Y.Portal.Apis.Controllers.Extension
{
    /// <summary>
    /// appsettings 配置类
    /// </summary>
    public static class AppConfigurationExtension
    {

        public static WebApplicationBuilder AddAppConfigurationServices(this WebApplicationBuilder builder)
        {
            var path = ServerHelper.GetAssemblyPath();
            ConfigurationManager config = new ConfigurationManager();
            config.SetBasePath(path).AddJsonFile("appsettings.json", optional: true);
            builder.Configuration.AddConfiguration(config);
            
            // 全局变量初始化
            builder.Services.Configure<Dictionary<string, string>>(DefaultString.Sys_Default, config.GetSection("UrlConfig"));
            // 数据库字段初始化
            builder.Services.Configure<DbOption>(AuthDefault.SqlConStr, config.GetSection(AuthDefault.SqlConStr));
            builder.Services.Configure<DbOption>(DefaultString.Ying_Merchants, config.GetSection("YSQLServer"));
            builder.Services.Configure<DbOption>(DefaultString.Ying_Members, config.GetSection("YSQLServer"));
            builder.Services.Configure<DbOption>(DefaultString.Ying_Games, config.GetSection("YSQLServer"));
            builder.Services.Configure<DbOption>(DefaultString.Ying_Promotions, config.GetSection("YSQLServer"));
            builder.Services.Configure<DbOption>(DefaultString.Ying_Pay, config.GetSection("YSQLServer"));
            builder.Services.Configure<DbOption>(DefaultString.ying_Vips, config.GetSection("YSQLServer"));
            builder.Services.Configure<DbOption>(LogsCoreDefault.SqlConStr, config.GetSection("YSQLServer"));
            builder.Services.Configure<YCacheConfiguration>(DefaultString.LuckyRedis, config.GetSection(DefaultString.LuckyRedis));
            return builder;
        }
    }
}
