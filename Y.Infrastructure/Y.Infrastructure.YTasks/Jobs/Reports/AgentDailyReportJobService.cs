using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.IApplication;

namespace Y.Infrastructure.YTasks.Jobs
{
    /// <summary>
    /// 创建代理日报表 . 每天凌晨1点更新
    /// </summary>
    public class AgentDailyReportJobService : IJob
    {
        private readonly IHybridTaskService _hybridTaskService;
        public AgentDailyReportJobService()
        {
        }
        public AgentDailyReportJobService(IServiceProvider provider)
        {
            _hybridTaskService = (IHybridTaskService)provider.GetService(typeof(IHybridTaskService));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _hybridTaskService.CreateAgentDailyStatisticAsync(DateTime.UtcNow.AddHours(8).AddDays(-1));
        }
    }
}
