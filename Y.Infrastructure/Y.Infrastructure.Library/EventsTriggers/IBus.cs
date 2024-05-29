using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.Helper;

namespace Y.Infrastructure.Library.EventsTriggers
{
    public interface IBus
    {
        Task PublishAsync(IMessage triggerEvent);

    }

    public class Bus : IBus
    {
        ISubscriber _sub;
        public Bus(IOptionsSnapshot<YCacheConfiguration> options)
        {
            YCacheConfiguration jsonStr = options.Get(DefaultString.LuckyRedis);
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect($"{jsonStr.Host[0].Host}:{jsonStr.Host[0].Port}");
            _sub = redis.GetSubscriber();
        }

        public async Task PublishAsync(IMessage triggerEvent)
        {
            var msg = new MessageEventArgs(triggerEvent.Trigger, triggerEvent.ToJson());
            await _sub.PublishAsync("SubscriptionMessageEventArgs", msg.ToJson());
        }
    }
}