using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.ApiVersions;

namespace Y.Portal.Apis.Controllers.Extension
{
    public static class AddSwaggerExtension
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {

                foreach (FieldInfo fileld in typeof(ApiVersionInfo).GetFields())
                {
                    options.SwaggerDoc(fileld.Name, new OpenApiInfo
                    {
                        Version = fileld.Name,
                        Title = "API标题",
                        Description = $"API描述,{fileld.Name}版本"
                    });
                }
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerConfigure(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (FieldInfo field in typeof(ApiVersionInfo).GetFields())
                {
                    c.SwaggerEndpoint($"/swagger/{field.Name}/swagger.json", $"{field.Name}");
                }
            });
            return app;
        }


    }
}
