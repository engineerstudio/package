using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.AuthController.IService;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.WebInfrastructure.Entity;

namespace Y.Portal.Apis.Controllers.Middleware
{
    public class InitSessionDataMiddleware
    {
        private readonly RequestDelegate _next;
        public InitSessionDataMiddleware(RequestDelegate next)
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
                        await ThisPostInvoke(context);
                        break;
                    default:
                        await _next.Invoke(context);
                        break;
                }
        }
        private async Task ThisPostInvoke(HttpContext context)
        {
            if (context.Items["ExceptionUrl"].To<int>() == 0)
                await LoadData(context);
            await _next.Invoke(context);
        }
        private async Task LoadData(HttpContext context)
        {
            int accountId = (int)context.Items["AccountId"];
            var accountService = (ISysAccountService)context.RequestServices.GetService(typeof(ISysAccountService));
            var acc = await accountService.GetManagerByIdAsync(accountId);
            context.Items["MerchantId"] = acc.MerchantId;
            context.Items["AccountInfo"] = new AccountInfo()
            {
                MerchantId = acc.MerchantId,
                AccountId = accountId,
                AccountName = acc.Name,
                RoleId = acc.RoleId,
                NickName = acc.NickName
            };
        }
    }
}
