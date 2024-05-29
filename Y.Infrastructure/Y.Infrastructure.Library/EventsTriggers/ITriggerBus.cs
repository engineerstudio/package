using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Y.Infrastructure.Library.EventsTriggers
{
    public interface ITriggerBus
    {
        Task PublishAsync(IMessage triggerEvent);
        Task SendTo(string queueName, object message, Dictionary<string, string> headers = null);
    }
    public class TriggerBus : ITriggerBus
    {
        private readonly IBus _bus;
        public TriggerBus(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishAsync(IMessage triggerEvent)
        {
            await _bus.PublishAsync(triggerEvent);
        }

        public Task SendTo(string queueName, object message, Dictionary<string, string> headers = null)
        {
            throw new NotImplementedException();
        }
    }
}
