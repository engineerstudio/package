using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Pay.ViewModel
{


    public class PayInfoViewModel
    {

        public string Name { get; set; }
        public string Url { get; set; }
        public int IsVC { get; set; }
        public List<PayInfoSubMerchant> Merchants { get; set; }

    }

    public class PayInfoSubMerchant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string Rule { get; set; }
        public string Desc { get; set; }
    }


    /// <summary>
    /// 支付提交
    /// </summary>
    public class PaySubmitViewModel
    {
        public int PayId { get; set; }

        public int Amount { get; set; }

    }



}
