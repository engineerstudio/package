using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.LogsCoreController.IService;
using Y.Infrastructure.IApplication;

namespace Y.Infrastructure.YTasks.Jobs
{
    [DisallowConcurrentExecution]
    public class GameLogsCallBackProcessJobService : IJob
    {
        private readonly IGameLogsCallBackProcessService _gameLogsCallBackProcessService;
        private readonly IExcptLogsService _excptLogsService;
        public GameLogsCallBackProcessJobService(IServiceProvider provider)
        {
            _gameLogsCallBackProcessService = (IGameLogsCallBackProcessService)provider.GetService(typeof(IGameLogsCallBackProcessService));
            _excptLogsService = (IExcptLogsService)provider.GetService(typeof(IExcptLogsService));
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await _gameLogsCallBackProcessService.Process();
            }
            catch (Exception ex)
            {
                await _excptLogsService.InsertAsync(ex, "GameLogsCallBackProcessJobService.Job");
            }
        }
    }
}
