using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Infrastructure.Library.EventsTriggers
{
    public static class EventsTriggersServiceExtensions
    {
        public static IServiceCollection IniTransientImplementationType(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBus), typeof(Bus));
            services.AddTransient(typeof(ITriggerBus), typeof(TriggerBus));

            return services;
        }
    }
}
