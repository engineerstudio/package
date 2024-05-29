using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNetCore.Infrastructure.Helper;
using Y.Infrastructure.Entities.Message;
using Y.Infrastructure.Library.EventsTriggers;

namespace Y.Application.BusMessageHandler.MessageHandler
{
    public class OrdersMessageHandler : IMessageHandler
    {
        public string Data { get; set; }
        public OrdersMessageHandler(string data)
        {
            Data = data;
        }
        public void Process(IServiceProvider provider)
        {
            var order = JsonHelper.JSONToObject<BusMessage>(Data);
            //var oo = new Orders()
            //{
            //    Id = 0,
            //    MerchantId = 0,
            //    CateId = 1,
            //    UserId = 1,
            //    GameId = 1,
            //    GameBetId = 1,
            //    GameBetItemId = 1,
            //    Status = XNetCore.Infrastructure.YEntity.OrderStatus.Win,
            //    BetTime = DateTime.UtcNow,
            //    RewardTime = DateTime.UtcNow,
            //    SettlementTime = DateTime.UtcNow,
            //    Ip = "",
            //    Amount = 1,
            //    Bonus = 1,
            //    Award = 1,
            //    ModifyTime = DateTime.UtcNow,
            //    Device = XNetCore.Infrastructure.YEntity.DeviceType.WebSite,
            //    Odds = 1,
            //    ResultId = 1,
            //    Stage = XNetCore.Infrastructure.LuckyEntity.StockSchedule.Match

            //};
        }

        public Task ProcessAsync(IServiceProvider provider)
        {
            return null;
        }
    }
}
