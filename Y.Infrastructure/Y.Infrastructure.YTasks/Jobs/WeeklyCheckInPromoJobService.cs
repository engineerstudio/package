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
    /// [每周签到] 活动
    /// </summary>
    public class WeeklyCheckInPromoJobService : IJob
    {
        private readonly IHybridTaskService _hybridTaskService;
        public WeeklyCheckInPromoJobService(IHybridTaskService hybridTaskService)
        {
            _hybridTaskService = hybridTaskService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            // 每周一凌晨30分执行
            await _hybridTaskService.ExecWeeklyCheckInPromo();
        }
    }
}
