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
    /// [VIP生日礼金]
    /// </summary>
    public class VipBirthPromoJobService : IJob
    {
        private readonly IHybridTaskService _hybridTaskService;
        public VipBirthPromoJobService(IHybridTaskService hybridTaskService)
        {
            _hybridTaskService = hybridTaskService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _hybridTaskService.VipBirthTask();
            Console.WriteLine($"[VIP生日礼金]-{DateTime.UtcNow.AddHours(8).ToDateTimeString()}");
        }
    }
}
