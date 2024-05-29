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
    /// [幸运转盘] 活动
    /// </summary>
    public class TurnTablePromoJobService : IJob
    {
        private readonly IHybridTaskService _hybridTaskService;
        public TurnTablePromoJobService(IHybridTaskService hybridTaskService)
        {
            _hybridTaskService = hybridTaskService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _hybridTaskService.ExecTurnTablePromo();
            Console.WriteLine($"[幸运转盘]-{DateTime.UtcNow.AddHours(8).ToDateTimeString()}");
        }
    }
}
