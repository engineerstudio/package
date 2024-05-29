using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Pay
{
    /// <summary>
    /// Aaron
    /// 2020-09-30 16:19:19
    /// 
    /// </summary>
    public partial class PayMerchant
    {
        [NotMapped]
        [JsonIgnore]
        public PayValidation Validation
        {
            get
            {
                return JsonConvert.DeserializeObject<PayValidation>(this.ValidationStr);
            }
        }

        public class PayValidation
        {
            /// <summary>
            /// 固定充值范围
            /// </summary>
            public string FixedRange { get; set; }
            /// <summary>
            /// 最大充值
            /// </summary>
            public int? Price_Max { get; set; }
            /// <summary>
            /// 最低充值
            /// </summary>
            public int? Price_Min { get; set; }
        }

    }
}
