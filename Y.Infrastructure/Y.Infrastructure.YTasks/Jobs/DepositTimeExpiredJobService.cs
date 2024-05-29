using Quartz;
using System;
using System.Threading.Tasks;
using Y.Packet.Services.IPay;

namespace Y.Infrastructure.YTasks.Jobs
{
    public class DepositTimeExpiredJobService : IJob
    {

        private readonly IPayOrderService _payOrderService;
        public DepositTimeExpiredJobService(IServiceProvider provider)
        {
            _payOrderService = (IPayOrderService)provider.GetService(typeof(IPayOrderService));
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _payOrderService.SetPayOrderExpireStatusAsync();
        }
    }
}
