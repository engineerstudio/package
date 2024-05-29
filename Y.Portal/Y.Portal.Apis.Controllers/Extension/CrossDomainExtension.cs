using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.Portal.Apis.Controllers.Extension
{
    /// <summary>
    /// 跨域扩展
    /// </summary>
    public static class CrossDomainExtension
    {
        public static readonly string AllowSpecificOriginName = "any";
        public static IServiceCollection AddCrossDoaminServices(this IServiceCollection services)
        {
            var corsPolicy = new string[] {
                "localhost",
                "localhost:60001",
                "https://*.yiyingstudio.com",
                "https://devbw-merchant.yiyingstudio.com",
                "https://*.vpnlv.com",
            };

            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOriginName, policy =>
                {
                    policy.WithOrigins(corsPolicy)
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            return services;
        }


        public static IApplicationBuilder UseCrossDoaminConfigure(this IApplicationBuilder app)
        {
            app.UseCors(AllowSpecificOriginName);
            return app;
        }



    }
}
