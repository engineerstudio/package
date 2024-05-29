using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.LogsCoreController.IService;
using Y.Infrastructure.Library.Core.LogsCoreController.Service;
using Y.Infrastructure.IApplication;
using Y.Packet.Services.IMerchants;

namespace Y.Infrastructure.YTasks.Jobs
{

    /// <summary>
    /// [返水活动]
    /// </summary>
    public class RebatePromoJobService : IJob
    {
        private readonly IHybridTaskService _hybridTaskService;
        private readonly IMerchantService _merchantService;
        private readonly IExcptLogsService _excptLogsService;
        private readonly IReqLogsService _reqLogsService;
        public RebatePromoJobService(IServiceProvider provider)
        {
            _hybridTaskService = (IHybridTaskService)provider.GetService(typeof(IHybridTaskService));
            _merchantService = (IMerchantService)provider.GetService(typeof(IMerchantService));
            _excptLogsService = (IExcptLogsService)provider.GetService(typeof(IExcptLogsService));
            _reqLogsService = (IReqLogsService)provider.GetService(typeof(IReqLogsService));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            List<string> strings = new List<string>();
            try
            {
                var merchants = await _merchantService.MerchantsDicAsync();
                foreach (KeyValuePair<int, string> it in merchants)
                {
                    var msg = await _hybridTaskService.ExecRebatePromoAsync(DateTime.UtcNow.AddHours(8).AddDays(-1), it.Key);
                    strings.Add(msg);
                }

            }
            catch (Exception ex)
            {
                await _excptLogsService.InsertAsync(ex, "RebatePromoJobService.Job");
            }
            if (strings.Count != 0)
            {
                await _reqLogsService.InsertAsync(999, "RebatePromoJobService.Job", strings.ToJson());
            }
        }
    }
}
