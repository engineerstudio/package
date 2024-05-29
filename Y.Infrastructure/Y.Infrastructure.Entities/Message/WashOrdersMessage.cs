using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Infrastructure.Entities.Message
{
    public class WashOrdersMessage : Library.EventsTriggers.IMessage
    {
        public string Trigger => typeof(WashOrdersMessage).Name;

        public List<int> Ids { get; set; }
    }
}
