using Quartz;
using System.Threading.Tasks;
using Y.Games.IService;
using Y.Packet.Services.IGames;

namespace Y.Infrastructure.YTasks.Jobs
{
    public class GameDatasHourlyJobService : IJob, IGameDatasWithParmsJobService
    {
        private readonly IGameUsersDailyReportStatisticService _gameUsersDailyReportStatisticService;
        public GameDatasHourlyJobService(IGameUsersDailyReportStatisticService gameUsersDailyReportStatisticService)
        {
            _gameUsersDailyReportStatisticService = gameUsersDailyReportStatisticService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            // 每30分钟自动生成用户日报表
            // 每天凌晨5分 自动生成日报表
            await _gameUsersDailyReportStatisticService.CreateGameUserReportAsync();
        }
    }
}
