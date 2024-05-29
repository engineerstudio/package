using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Infrastructure.Library.Middleware
{

    /// <summary>
    /// 判断当前请求是否是手机端
    /// </summary>
    public class DeviceRedirectMiddleware
    {

        private readonly RequestDelegate _next;

        public DeviceRedirectMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // TODO 其实可以懂脑子想一下，可以根据路径处理很多事情的
            // 错误页面，IP禁止页面

            bool isMobile = context.Request.IsMobileBrowser();
            if (isMobile)
            {
                // 手机端访问 重定向其它目录
                context.Request.Path = "/Mobile/Index";
                await _next.Invoke(context);
            }
            else
            {
                context.Request.HttpContext.Items["Device"] = isMobile ? DeviceType.Mobile : DeviceType.WebSite;
                await _next.Invoke(context);
            }
        }

    }
}
