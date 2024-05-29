using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.EventsTriggers;

namespace Y.Infrastructure.Entities.Message
{
    [MessageHander]
    public class OrdersMessage : BusMessage,Library.EventsTriggers.IMessage
    {
        public string Trigger => typeof(OrdersMessage).Name;
    }
}
