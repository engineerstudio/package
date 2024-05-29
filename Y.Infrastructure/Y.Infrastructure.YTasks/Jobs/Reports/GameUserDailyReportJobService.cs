using Quartz;
using System;
using System.Threading.Tasks;
using Y.Games.IService;
using Y.Packet.Services.IGames;

namespace Y.Infrastructure.YTasks.Jobs
{
    /// <summary>
    /// 生成用户日报表 GameUsersDailyReportStatistic 每天凌晨10分 自动生成昨日用户的游戏报表
    /// </summary>
    public class GameUserDailyReportJobService : IJob, IGameDatasWithParmsJobService
    {
        private readonly IGameUsersDailyReportStatisticService _gameUsersDailyReportStatisticService;
        public GameUserDailyReportJobService(IServiceProvider provider)
        {
            _gameUsersDailyReportStatisticService = (IGameUsersDailyReportStatisticService)provider.GetService(typeof(IGameUsersDailyReportStatisticService));
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _gameUsersDailyReportStatisticService.CreateGameUserReportAsync(DateTime.UtcNow.AddHours(8).AddDays(-1).Date.ToString("yyyy-MM-dd"));
        }
    }
}
