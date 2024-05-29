using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Y.Infrastructure.Library.Core.WebInfrastructure
{
    public static class ServicesExtensions
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
                foreach (Type asType in assembly.GetTypes().Where(t => !t.IsInterface && t.Name.EndsWith("Service")))
                {
                    if (asType.GetInterfaces().Length != 0)
                    //services.AddSingleton(asType.GetInterfaces()[0], asType);
                    //services.AddTransient(asType.GetInterfaces()[0], asType);
                    {
                        var interfaceType = asType.GetInterfaces().Where(t => t.Name.Contains(asType.Name))
                            .SingleOrDefault();
                        //services.AddSingleton(interfaceType, asType);
                        services.AddTransient(interfaceType, asType);
                    }
                }
            }


            // # 网络参考备份
            //var baseType = typeof(IDependency);
            //var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            //var referencedAssemblies = System.IO.Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFrom).ToArray();
            //var types = referencedAssemblies
            //    .SelectMany(a => a.DefinedTypes)
            //    .Select(type => type.AsType())
            //    .Where(x => x != baseType && baseType.IsAssignableFrom(x)).ToArray();
            //var implementTypes = types.Where(x => x.IsClass).ToArray();
            //var interfaceTypes = types.Where(x => x.IsInterface).ToArray();
            //foreach (var implementType in implementTypes)
            //{
            //    var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
            //    if (interfaceType != null)
            //        services.AddScoped(interfaceType, implementType);
            //}


            return services;
        }
    }
}