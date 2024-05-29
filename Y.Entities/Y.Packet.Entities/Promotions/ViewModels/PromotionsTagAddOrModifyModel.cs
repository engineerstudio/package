using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Promotions.ViewModels
{
    public class PromotionsTagAddOrModifyModel
    {
        public int MerchantId { get; set; }
        public int Id { get; set; }

        public string Name { get; set; }

        public int Sort { get; set; }
        public string Image { get; set; }
    }
}
