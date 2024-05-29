using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Infrastructure.Library.Core.WebInfrastructure
{
    /// <summary>
    /// 获取访问的设备信息
    /// </summary>
    public class DeviceMiddleware
    {
        private readonly RequestDelegate _next;

        public DeviceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.HttpContext.Items["Device"] =
                context.Request.IsMobileBrowser() ? DeviceType.Mobile : DeviceType.WebSite;
            await _next.Invoke(context);
        }
    }
}