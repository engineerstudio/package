using Microsoft.Extensions.DependencyInjection;
using Y.Infrastructure.Library.Core.LogsCoreController.IRepository;
using Y.Infrastructure.Library.Core.LogsCoreController.IService;
using Y.Infrastructure.Library.Core.LogsCoreController.Repository;
using Y.Infrastructure.Library.Core.LogsCoreController.Service;

namespace Y.Infrastructure.Library.Core.LogsCoreController
{
    public static class LogsCoreServiceExtensions
    {
        public static IServiceCollection IniTransientImplementationType(this IServiceCollection services)
        {
            services.AddTransient(typeof(IExcptLogsRepository), typeof(ExcptLogsRepository));
            services.AddTransient(typeof(IReqLogsRepository), typeof(ReqLogsRepository));
            services.AddTransient(typeof(ITaskDataForGamesRepository), typeof(TaskDataForGamesRepository));

            services.AddTransient(typeof(IExcptLogsService), typeof(ExcptLogsService));
            services.AddTransient(typeof(IReqLogsService), typeof(ReqLogsService));
            services.AddTransient(typeof(ITaskDataForGamesService), typeof(TaskDataForGamesService));

            return services;
        }
    }
}