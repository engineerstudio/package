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
    /// [VIP自动晋级] 服务
    /// </summary>
    public class VipUpgradePromoJobService : IJob
    {
        private readonly IHybridTaskService _hybridTaskService;
        public VipUpgradePromoJobService(IServiceProvider provider)
        {
            _hybridTaskService = (IHybridTaskService)provider.GetService(typeof(IHybridTaskService));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _hybridTaskService.VipsUpgradeTaskAsync();
            Console.WriteLine($"[VIP自动晋级]-{DateTime.UtcNow.AddHours(8).ToDateTimeString()}");
        }
    }
}
