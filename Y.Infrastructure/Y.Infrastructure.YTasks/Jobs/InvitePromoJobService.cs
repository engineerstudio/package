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
    /// [邀请有礼] 活动
    /// </summary>
    public class InvitePromoJobService : IJob
    {
        private readonly IHybridTaskService _hybridTaskService;
        public InvitePromoJobService(IHybridTaskService hybridTaskService)
        {
            _hybridTaskService = hybridTaskService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _hybridTaskService.ExecInvitePromo();
            Console.WriteLine($"[邀请有礼]-{DateTime.UtcNow.AddHours(8).ToDateTimeString()}");
        }
    }
}
