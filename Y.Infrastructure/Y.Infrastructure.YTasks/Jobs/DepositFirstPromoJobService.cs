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
    /// [首存活动]
    /// </summary>
    public class DepositFirstPromoJobService : IJob
    {
        private readonly IHybridTaskService _hybridTaskService;
        public DepositFirstPromoJobService(IHybridTaskService hybridTaskService)
        {
            _hybridTaskService = hybridTaskService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"[首存活动]-{DateTime.UtcNow.AddHours(8).ToDateTimeString()}");
        }


    }
}
