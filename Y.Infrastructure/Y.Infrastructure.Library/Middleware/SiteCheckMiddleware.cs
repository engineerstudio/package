using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.WebInfrastructure.Entity;

namespace Y.Infrastructure.Library.Middleware
{

    /// <summary>
    /// IP黑名单检查 
    /// 站点状态审查
    /// </summary>
    public class SiteCheckMiddleware
    {
        private readonly RequestDelegate _next;
        public SiteCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // TODO
        public async Task Invoke(HttpContext context)
        {
            // # 不必须登陆可访问的地址
            List<string> unAuthUrl = new List<string>() { "/login/signin", "/helparea/get", "/helparea/getbytypeid", "/promo/list", "/promo/get", "/sections/get", "/sections/getall", "/signup/do", "/signup/validcode", "/signup/config", "/sites/config" };
            var req_url = context.Request.Path.Value;
            var member = (MemberInfo)context.Request.HttpContext.Items["Member"];

            // # TODO 代理可以访问的地址
            if (!unAuthUrl.Contains(req_url) && context.Request.HttpContext.Items["islogin"].To<string>() == "false") // 必须登陆才可以访问的地址
            {
                await HandleUnAuthorAsync(context, "请先登录");
            }
            else if (!unAuthUrl.Contains(req_url) && member != null && member.Id == 0)
            {
                await HandleUnAuthorAsync(context, "请先登录");
            }
            else
            {
                // #站点状态

                // #IP黑名单
                // 1. 获取当前访问IP

                // 2. 获取当前站点禁用IP

                // 3. 判断

                await _next.Invoke(context);

            }
        }


        async Task HandleUnAuthorAsync(HttpContext context, string errorMsg)
        {
            string result = (new { code = 0, msg = errorMsg }).ToJson();
            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)200;
            await context.Response.WriteAsync(result);
        }


    }
}
