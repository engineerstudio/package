using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.MultiLanguage.Repositories;
using Y.Infrastructure.Library.Core.MultiLanguage.Services;

namespace Y.Infrastructure.Library.Core.MultiLanguageController
{
    public static class MultiLanguageServiceExtensions
    {
        public static IServiceCollection IniTransientImplementationType(this IServiceCollection services)
        {
            services.AddTransient(typeof(IDocsRepository), typeof(DocsRepository));
            services.AddTransient(typeof(IDocsService), typeof(DocsService));
            return services;
        }
    }
}
