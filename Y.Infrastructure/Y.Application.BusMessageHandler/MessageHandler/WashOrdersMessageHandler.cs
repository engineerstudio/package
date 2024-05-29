using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNetCore.Infrastructure.Extensions;
using XNetCore.Infrastructure.Helper;
using Y.Games.IRepository;
using Y.Infrastructure.Entities.Message;
using Y.Infrastructure.IApplication;
using Y.Infrastructure.Library.EventsTriggers;
using Y.Vips.IServices;

namespace Y.Application.BusMessageHandler.MessageHandler
{
    public class WashOrdersMessageHandler : IMessageHandler
    {
        public string Data { get; set; }
        public WashOrdersMessageHandler(string data)
        {
            Data = data;
        }

        public void Process(IServiceProvider provider)
        {
        }

        public async Task ProcessAsync(IServiceProvider provider)
        {
            Console.WriteLine(Data);
            IGameLogsCallBackProcessService _washOrderDetailService = (IGameLogsCallBackProcessService)provider.GetService(typeof(IGameLogsCallBackProcessService));
            var washOrders = JsonHelper.JSONToObject<WashOrdersMessage>(Data);
            var arrIds = washOrders.Ids;  // GameLogs
            foreach (var id in arrIds)
                await _washOrderDetailService.Process(id);
        }
    }
}
