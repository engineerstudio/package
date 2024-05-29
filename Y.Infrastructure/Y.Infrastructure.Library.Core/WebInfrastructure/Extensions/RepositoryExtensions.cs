using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Y.Infrastructure.Library.Core.WebInfrastructure
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection IniTransientImplementationType(this IServiceCollection services,
            string[] assemblyNames)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            foreach (var name in assemblyNames)
            {
                Assembly assembly = Assembly.Load(name);
                if (assembly == null) throw new ArgumentNullException($"未找到该程序集:{name}");
                foreach (Type asType in assembly.GetTypes().Where(t => !t.IsInterface && t.Name.EndsWith("Repository")))
                {
                    if (asType.GetInterfaces().Length != 0)
                    {
                        var interfaceType = asType.GetInterfaces().Where(t => t.Name.Contains(asType.Name))
                            .SingleOrDefault();
                        //services.AddSingleton(interfaceType, asType);
                        if (interfaceType == null) continue;
                        services.AddTransient(interfaceType, asType);
                    }
                }
            }

            return services;
        }



        public static IServiceCollection InitSingletonImplementationType(this IServiceCollection services,
            string[] assemblyNames)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            foreach (var name in assemblyNames)
            {
                Assembly assembly = Assembly.Load(name);
                if (assembly == null) throw new ArgumentNullException($"未找到该程序集:{name}");
                foreach (Type asType in assembly.GetTypes().Where(t => !t.IsInterface && t.Name.EndsWith("Repository")))
                {
                    if (asType.GetInterfaces().Length != 0)
                    {
                        var interfaceType = asType.GetInterfaces().Where(t => t.Name.Contains(asType.Name))
                            .SingleOrDefault();
                        if (interfaceType == null) continue;
                        services.AddSingleton(interfaceType, asType);
                    }
                }
            }

            return services;
        }

    }
}