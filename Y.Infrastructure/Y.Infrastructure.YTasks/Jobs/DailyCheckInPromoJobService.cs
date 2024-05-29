using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.IApplication;

namespace Y.Infrastructure.YTasks.Jobs
{
    /// <summary>
    /// [每日签到] 活动
    /// </summary>
    public class DailyCheckInPromoJobService : IJob
    {

        private readonly IHybridTaskService _hybridTaskService;
        public DailyCheckInPromoJobService(IHybridTaskService hybridTaskService)
        {
            _hybridTaskService = hybridTaskService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _hybridTaskService.ExecDailyCheckInPromo();
            Console.WriteLine($"[每日签到]-{DateTime.UtcNow.AddHours(8).ToDateTimeString()}");
        }

    }
}
