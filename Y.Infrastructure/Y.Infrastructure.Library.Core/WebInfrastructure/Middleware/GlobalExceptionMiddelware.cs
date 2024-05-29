using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.LogsCoreController;
using Y.Infrastructure.Library.Core.LogsCoreController.IService;

namespace Y.Infrastructure.Library.Core.WebInfrastructure
{
    public class GlobalExceptionMiddelware
    {
        private readonly RequestDelegate _next;
        //private ILogger _logger;

        public GlobalExceptionMiddelware(RequestDelegate next)
        {
            this._next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }


        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // 返回数据
            string result = ex.StackTrace;
            string title = ex.Message;
            try
            {
                // 写入日志
                IOptionsMonitor<DbOption> options =
                    (IOptionsMonitor<DbOption>)context.RequestServices.GetService(typeof(IOptionsMonitor<DbOption>));
                DbOption _dbOption = options.Get(LogsCoreDefault.SqlConStr);
                if (_dbOption == null)
                    _dbOption = options.Get(DefaultString.LogDb);

                IExcptLogsService excpLogService =
                    (IExcptLogsService)context.RequestServices.GetService(typeof(IExcptLogsService));
                excpLogService.Insert(ex, "GlobalExceptionMiddelware");
                //LoggerFactory loggerFactory = new LoggerFactory();
                //ILoggerFactory fac = loggerFactory.AddSqlServerLogger(_dbOption.ConnectionString);
                //_logger = fac.CreateLogger(typeof(SqlServerLogger));
                //_logger.Log(LogLevel.Error, ex, "SysError");
            }
            catch
            {
                result = ex.StackTrace;
            }
            finally
            {
                // #if DEBUG
                // #endif
                //                 result = (new {code = 199, msg = "系统错误,请联系管理员"}).ToJson();
                // #if RELEASE
                //              result = (new { code = 199, msg = "系统错误,请联系管理员",ex = result  }).ToJson();
                // #endif
                //context.Response.ContentType = "application/json";
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = (int)200;
                await context.Response.WriteAsync($"{title}---{result}", Encoding.UTF8);
            }
        }
    }
}