using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Infrastructure.Entities.Message
{
    public class BusMessage
    {
        public int MerchantId { get; set; } 
        public int UserId { get; set; }
    }
}
