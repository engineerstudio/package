using Microsoft.Extensions.DependencyInjection;
using Y.Infrastructure.Library.Core.AuthController.IRepository;
using Y.Infrastructure.Library.Core.AuthController.IService;
using Y.Infrastructure.Library.Core.AuthController.Repository;
using Y.Infrastructure.Library.Core.AuthController.Service;

namespace Y.Infrastructure.Library.Core.AuthController
{
    public static class AuthServiceExtensions
    {
        public static IServiceCollection IniTransientImplementationType(this IServiceCollection services)
        {
            services.AddSingleton(typeof(ISysAccountRepository), typeof(SysAccountRepository));
            services.AddSingleton(typeof(ISysAccountSessionRepository), typeof(SysAccountSessionRepository));
            services.AddSingleton(typeof(ISysMenuActionRepository), typeof(SysMenuActionRepository));
            services.AddSingleton(typeof(ISysMenuRepository), typeof(SysMenuRepository));
            services.AddSingleton(typeof(ISysRolePermissionRepository), typeof(SysRolePermissionRepository));
            services.AddSingleton(typeof(ISysRoleRepository), typeof(SysRoleRepository));


            services.AddTransient(typeof(ISysAccountService), typeof(SysAccountService));
            services.AddTransient(typeof(ISysAccountSessionService), typeof(SysAccountSessionService));
            services.AddTransient(typeof(ISysMenuActionService), typeof(SysMenuActionService));
            services.AddTransient(typeof(ISysMenuService), typeof(SysMenuService));
            services.AddTransient(typeof(ISysRolePermissionService), typeof(SysRolePermissionService));
            services.AddTransient(typeof(ISysRoleService), typeof(SysRoleService));


            return services;
        }
    }
}