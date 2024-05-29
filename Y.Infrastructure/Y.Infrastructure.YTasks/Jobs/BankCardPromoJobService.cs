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
    /// [绑定银行卡] 活动
    /// </summary>
    public class BankCardPromoJobService : IJob
    {
        private readonly IHybridTaskService _hybridTaskService;
        public BankCardPromoJobService(IHybridTaskService hybridTaskService)
        {
            _hybridTaskService = hybridTaskService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _hybridTaskService.ExecBankCardPromo();
            Console.WriteLine($"[绑定银行卡]-{DateTime.UtcNow.AddHours(8).ToDateTimeString()}");
        }
    }
}
