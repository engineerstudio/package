using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;

namespace Y.Infrastructure.Library.Core.CacheFactory.Extension
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