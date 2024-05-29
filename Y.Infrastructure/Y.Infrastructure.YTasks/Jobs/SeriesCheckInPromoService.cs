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
    /// [连续签到] 活动
    /// </summary>
    public class SeriesCheckInPromoService : IJob
    {
        private readonly IHybridTaskService _hybridTaskService;
        public SeriesCheckInPromoService(IHybridTaskService hybridTaskService)
        {
            _hybridTaskService = hybridTaskService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _hybridTaskService.ExecSeriesCheckInPromo();
            Console.WriteLine($"[连续签到]-{DateTime.UtcNow.AddHours(8).ToDateTimeString()}");
        }
    }
}
