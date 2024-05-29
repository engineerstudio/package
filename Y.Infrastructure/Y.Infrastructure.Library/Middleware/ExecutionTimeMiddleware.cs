using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Y.Infrastructure.Library.Middleware
{
    public class ExecutionTimeMiddleware
    {
        private readonly RequestDelegate _next;

        Stopwatch stopwatch;
        public ExecutionTimeMiddleware(RequestDelegate next)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();

            context.Response.OnStarting(state =>
            {
                stopwatch.Stop();
                var responseTimeForCompleteRequest = stopwatch.ElapsedMilliseconds;

                var httpContext = (HttpContext)state;
                httpContext.Response.Headers.Add("X-Response-Time-Milliseconds", new[] { responseTimeForCompleteRequest.ToString() });
                
                return Task.CompletedTask;
            }, context);

            await _next.Invoke(context);


            //context.Response.Headers["ExecTime"] = stopwatch.ElapsedMilliseconds.ToString();
        }
    }
    public static class ExecutionTimeMiddlewareExtensions
    {
        public static IApplicationBuilder UseExecutionTime(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<ExecutionTimeMiddleware>();
        }
    }
}
