using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;


namespace Y.Infrastructure.YCache.Extensions
{
    public static class YCacheServiceCollectionExtensions
    {
        public static IServiceCollection AddYCache(this IServiceCollection services)
        {
            services.TryAdd(ServiceDescriptor.Singleton<IYCacheFactory, YCacheFactory>());
            return services;
        }

    }
}
