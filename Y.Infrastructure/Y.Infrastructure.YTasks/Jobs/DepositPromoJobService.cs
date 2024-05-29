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
    /// [存款活动]
    /// </summary>
    public class DepositPromoJobService : IJob
    {
        private readonly IHybridTaskService _hybridTaskService;
        public DepositPromoJobService(IHybridTaskService hybridTaskService)
        {
            _hybridTaskService = hybridTaskService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _hybridTaskService.ExecRechargePromo();
            Console.WriteLine($"[存款活动]-{DateTime.UtcNow.AddHours(8).ToDateTimeString()}");
        }
    }
}
