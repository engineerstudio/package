using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.AuthController.IService;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;


namespace Y.Portal.Apis.Controllers.Middleware
{
    public class AuthorizedSysMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthorizedSysMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var req_url = context.Request.Path.Value;
            if (!req_url.StartsWith("/sys"))
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

            if (context.Request.Host.Host == "localhost")
            {
                if (req_url == "/sys/auth/account/signin")
                {
                    context.Items["ExceptionUrl"] = 1;
                    await _next.Invoke(context);
                }
                else
                {
                    if (req_key == null)
                        await HandleUnAuthorAsync(context, $"暂未登陆,请重新登陆{req_url}");
                    else
                    {
                        var accountService =
                            (ISysAccountService)context.RequestServices.GetService(typeof(ISysAccountService));
                        var rt = accountService.ExistSession(req_key);
                        if (rt.Item1)
                        {
                            var acc = await accountService.GetManagerByIdAsync(rt.Item2);
                            context.Items["AccountId"] = rt.Item2;
                            context.Items["MerchantId"] = acc.MerchantId;
                            await _next.Invoke(context);
                        }
                    }
                }
            }
            else
            {
                // 4. 判断域名是否存在
                //var merchantService = (IMerchantService)context.RequestServices.GetService(typeof(IMerchantService));
                //var existMerchantUrl = merchantService.IsValidDoaminName(req_domain);
                //if (!existMerchantUrl.Item1) await HandleUnAuthorAsync(context, "域名不存在");
                //else
                //{
                if (req_url == "/sys/auth/account/signin")
                {
                    context.Items["ExceptionUrl"] = 1;
                    await _next(context);
                }
                else
                {
                    // 5. 判断是否登陆
                    if (req_key == null)
                        await HandleUnAuthorAsync(context, $"暂未登陆,请重新登陆{req_url}");
                    else
                    {
                        // 6. 判断用户是否存在操作权限
                        // 6.1 获取当前登陆的用户
                        var accountService =
                            (ISysAccountService)context.RequestServices.GetService(typeof(ISysAccountService));
                        var sessionResult = accountService.ExistSession(req_key);
                        if (sessionResult.Item1)
                        {
                            // 6.2 根据获取的账户ID 判断用户是否有操作权限
                            // 6.2.1 管理账户不需要访问权限验证
                            //if (accountService.ExistAuthorized(rt.Item2, req_url) || accountService.GetAccountName(rt.Item2).ToLower() == DefaultString.Sys_Admin)
                            //{
                            //    // 6.3 传递到下一层初始化赋值使用
                            //    context.Items["AccountId"] = rt.Item2;
                            //    await _next(context);
                            //}
                            //else
                            //    await HandleUnAuthorAsync(context, "不存在的权限");
                            // 6.3 传递到下一层初始化赋值使用
                            var account = await accountService.GetManagerByIdAsync(sessionResult.Item2);

                            context.Items["AccountId"] = sessionResult.Item2;
                            context.Items["MerchantId"] = account.MerchantId;
                            await _next(context);
                        }
                        else
                            await HandleUnAuthorAsync(context, "请重新登陆");
                    }
                    //context.Items["AccountId"] = 1;
                    //await _next(context);
                }
                //}
            }
        }


        async Task HandleUnAuthorAsync(HttpContext context, string errorMsg)
        {
            var err = $"{errorMsg} auth";
            string result = (new { code = 198, msg = err }).ToJson();
            context.Response.ContentType = "text/html";
            context.Response.StatusCode = (int)200;
            await context.Response.WriteAsync(result);
        }


    }
}
