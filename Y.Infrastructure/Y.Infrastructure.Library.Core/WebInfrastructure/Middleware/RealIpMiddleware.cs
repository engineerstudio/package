using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Y.Infrastructure.Library.Core.WebInfrastructure
{
    public class RealIpMiddleware
    {
        private readonly RequestDelegate _next;

        public RealIpMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            context.Items["IP"] = context.Connection.RemoteIpAddress.MapToIPv4();
            var headers = context.Request.Headers;
            if (headers.ContainsKey("X-Forwarded-For"))
            {
                //[::1]:63712
                if (headers["X-Forwarded-For"].ToString().Contains("[::1]"))
                    context.Connection.RemoteIpAddress = IPAddress.Parse("127.0.0.1");
                else
                {
                    var headersArr = headers["X-Forwarded-For"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries);
                    if (headersArr.Length > 0)
                    {
                        string arr0 = headersArr[0].Split(':')[0];
                        context.Connection.RemoteIpAddress = IPAddress.Parse(arr0);
                    }
                    else
                    {
                        context.Connection.RemoteIpAddress = IPAddress.Parse("127.0.0.10");
                    }
                }

                context.Items["IP"] = context.Connection.RemoteIpAddress.MapToIPv4();
            }

            return _next(context);
        }
    }
}