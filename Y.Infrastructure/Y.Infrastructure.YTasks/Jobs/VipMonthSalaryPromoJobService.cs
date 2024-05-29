﻿using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.IApplication;

namespace Y.Infrastructure.YTasks.Jobs
{
    /// <summary>
    /// [VIP每月俸禄]
    /// </summary>
    public class VipMonthSalaryPromoJobService : IJob
    {
        private readonly IHybridTaskService _hybridTaskService;
        public VipMonthSalaryPromoJobService(IHybridTaskService hybridTaskService)
        {
            _hybridTaskService = hybridTaskService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _hybridTaskService.VipsUpgradeTaskAsync();
            Console.WriteLine($"[VIP每月俸禄]-{DateTime.UtcNow.AddHours(8).ToDateTimeString()}");
        }
    }
}
