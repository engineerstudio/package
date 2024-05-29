using Microsoft.Extensions.DependencyInjection;
using Y.Infrastructure.Library.Core.AuthController;
using Y.Infrastructure.Library.Core.LogsCoreController;
using Y.Infrastructure.Library.Core.WebInfrastructure;
using Y.Infrastructure.Library.Core.CacheFactory.Extension;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Y.Infrastructure.Library;

namespace Y.Portal.Apis.Controllers.Extension
{
    /// <summary>
    /// 控制反转 容器类
    /// </summary>
    public static class InversionOfControlExtension
    {
        public static IServiceCollection AddInversionOfControlServices(this IServiceCollection services)
        {

            services.AddHttpContextAccessor();
            // API项目中必须加上这句, 否则报错
            services.AddMemoryCache();
            services.AddYCache();
            AuthServiceExtensions.IniTransientImplementationType(services);

            string[] repositoryAssemble = new string[]
            {
                "Y.Packet.Repositories"
            };
            RepositoryExtensions.InitSingletonImplementationType(services, repositoryAssemble);

            string[] serviceAssemble = new string[]
            {
                "Y.Packet.Services","Y.Infrastructure.Application"
            };
            ServicesExtensions.IniTransientImplementationType(services, serviceAssemble);

            services.TryAdd(ServiceDescriptor.Singleton<IGamesLibraryFactory, GamesLibraryFactory>());
            services.TryAdd(ServiceDescriptor.Singleton<IPaymentLibraryService, PaymentLibraryService>());
            services.TryAdd(ServiceDescriptor.Singleton<IWithdrawalLibraryService, WithdrawalLibraryService>());
            
            //services.AddGames();
            //services.AddPay();
            //services.AddWithdrawals(); // 代付服务
            LogsCoreServiceExtensions.IniTransientImplementationType(services);

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                //options.KnownProxies.Add(IPAddress.Parse("192.168.56.10"));
                options.ForwardedHeaders =
                     ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            return services;
        }
    }
}
