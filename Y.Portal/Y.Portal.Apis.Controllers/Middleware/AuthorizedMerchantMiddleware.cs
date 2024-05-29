using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.AuthController.IService;
using Y.Infrastructure.Library.Core.Helper;
using Y.Packet.Services.IMerchants;
using System.Threading.Tasks;

namespace Y.Portal.Apis.Controllers.Middleware
{
    public class AuthorizedMerchantMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthorizedMerchantMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var req_url = context.Request.Path.Value;
            if (!req_url.StartsWith("/merchant"))
                await _next.Invoke(context);
            else
                switch (context.Request.Method.ToUpper())
                {
                    case "POST":
                        await ThisInvoke(context);
                        break;
                    default:
                        await _next.Invoke(context);
                        break;
                }
        }

        //private async Task ReturnNoAuthorized(HttpContext context)
        //{
        //    var response = new
        //    {
        //        Code = "401",
        //        Message = "You are not authorized!"
        //    };
        //    context.Response.StatusCode = 401;
        //    await context.Response.WriteAsync(response.ToJson());
        //}

        private async Task ThisInvoke(HttpContext context)
        {
            // 1. 获取当前请求路径
            var req_url = context.Request.Path.Value;
            // 2. 获取当前请求域名
            var req_domain = context.Request.GetHeadersOriginUrl();
            // 3. 获取当前请求cookie
            var req_key = context.Request.Cookies["_guid"];
            var req_auth = context.Request.Headers["Authorization"];
            if (req_key == null) req_key = req_auth;
            //req_key = "d6e01a051cca4a9cb0215b8cc2d0a567";
            // 4. 判断域名是否存在 merchant.yplatform.com
            var merchantService = (IMerchantService)context.RequestServices.GetService(typeof(IMerchantService));
            var existMerchantUrl = await merchantService.IsValidDoaminNameAsync(req_domain);
            if (!existMerchantUrl.Item1 && req_domain != "localhost") await HandleUnAuthorAsync(context, $"{req_domain}-域名不存在");
            else
            {
                if (req_url == "/merchant/auth/account/signin")
                {
                    context.Items["ExceptionUrl"] = 1;
                    await _next(context);
                }
                else
                {
                    // 5. 判断是否登陆
                    if (req_key == null)
                        await HandleUnAuthorAsync(context, "暂未登陆,请重新登陆");
                    else
                    {
                        // 6. 判断用户是否存在操作权限
                        // 6.1 获取当前登陆的用户
                        var accountService = (ISysAccountService)context.RequestServices.GetService(typeof(ISysAccountService));
                        var rt = accountService.ExistSession(req_key);
                        if (rt.Item1)
                        {
                            // 6.2 根据获取的账户ID 判断用户是否有操作权限
                            // 6.2.1 管理账户不需要访问权限验证
                            string requset_url = context.Request.Path.Value;
                            //if (requset_url.StartsWith("/merchant/auth")) requset_url = requset_url.ToString().Replace("/merchant/auth", "");
                            if (requset_url.EndsWith("/")) requset_url = requset_url.Substring(0, requset_url.LastIndexOf("/"));
                            var existAuths = accountService.ExistAuthorized(rt.Item2, requset_url);

                            // 6.3 传递到下一层初始化赋值使用
                            context.Items["AccountId"] = rt.Item2;
                            await _next(context);

                            //if (accountService.ExistAuthorized(rt.Item2, requset_url) || accountService.GetAccountName(rt.Item2).ToLower() == DefaultString.Sys_Admin)
                            //{
                            //    // 6.3 传递到下一层初始化赋值使用
                            //    context.Items["AccountId"] = rt.Item2;
                            //    await _next(context);
                            //}
                            //else
                            //    await HandleUnAuthorAsync(context, $"不存在的权限2 requset_url:{requset_url} accountId:{rt.Item2}  {existAuths}");
                        }
                        else
                            await HandleUnAuthorAsync(context, "请重新登陆");
                    }
                }
            }
        }


        async Task HandleUnAuthorAsync(HttpContext context, string errorMsg)
        {
            string err = $"{errorMsg} 1 {context.Request.Path.Value.ToString().Replace("/auth", "")}";
            string result = (new { code = 198, msg = err }).ToJson();
            context.Response.ContentType = "text/html";
            context.Response.StatusCode = (int)200;
            await context.Response.WriteAsync(result);
        }


    }
}
