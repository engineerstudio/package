using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.LogsCoreController.IService;
using Y.Infrastructure.IApplication;

namespace Y.Infrastructure.YTasks.Jobs
{
    /// <summary>
    /// [充值优惠] 活动
    /// </summary>
    public class RechargePromoService : IJob
    {
        private readonly IHybridTaskService _hybridTaskService;
        readonly IReqLogsService _reqLogsService;
        private readonly IExcptLogsService _excptLogsService;
        public RechargePromoService(IServiceProvider provider)
        {
            _hybridTaskService = (IHybridTaskService)provider.GetService(typeof(IHybridTaskService));
            _reqLogsService = (IReqLogsService)provider.GetService(typeof(IReqLogsService));
            _excptLogsService = (IExcptLogsService)provider.GetService(typeof(IExcptLogsService));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            string msg = string.Empty;
            try
            {
                msg = await _hybridTaskService.ExecRechargePromo();
            }
            catch (Exception ex)
            {
                msg = $"{ex.Message}#{ex.Source}";
                await _excptLogsService.InsertAsync(ex, "RechargePromoService.Job");
            }
            if (!string.IsNullOrEmpty(msg))
            {
                await _reqLogsService.InsertAsync(999, "RechargePromoService.Job", msg);
            }
        }
    }
}
