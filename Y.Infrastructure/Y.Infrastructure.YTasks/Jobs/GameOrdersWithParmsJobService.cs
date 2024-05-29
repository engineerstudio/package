using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.LogsCoreController.IService;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Games;
using Y.Packet.Services.IGames;

namespace Y.Infrastructure.YTasks.Jobs
{
    [DisallowConcurrentExecution]
    public class GameOrdersWithParmsJobService : IJob
    {
        private readonly IInfrastructureGamesService _infrastructureGamesService;
        private readonly IGameInfoService _gameInfoService;
        private readonly IExcptLogsService _excptLogsService;
        public GameOrdersWithParmsJobService(IServiceProvider provider)
        {
            _gameInfoService = (IGameInfoService)provider.GetRequiredService(typeof(IGameInfoService));
            _infrastructureGamesService = (IInfrastructureGamesService)provider.GetRequiredService(typeof(IInfrastructureGamesService));
            _excptLogsService = (IExcptLogsService)provider.GetService(typeof(IExcptLogsService));
        }
        public GameOrdersWithParmsJobService()
        {

        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var gameTypeRt = await _gameInfoService.GetListAsync();
                var gameTypelist = gameTypeRt.Item1.Where(t => t.Status == GameInfo.GameStatus.Normal);
                foreach (var l in gameTypelist)
                {
                    await _infrastructureGamesService.ExecGetLogs(l.TypeStr.ToEnum<GameType>().Value, l.Config);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ExtractAllStackTrace());
                await _excptLogsService.InsertAsync(ex, "GameOrdersWithParmsJobService.Job");
            }
            finally { } 
        }
    }
}