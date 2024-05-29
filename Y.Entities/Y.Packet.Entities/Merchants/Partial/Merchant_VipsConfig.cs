using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Merchants
{
    public class Merchant_VipsConfig
    {
        /// <summary>
        /// 开启 每月俸禄自动发放
        /// </summary>
        public bool EnableMonthSalary { get; set; }

        /// <summary>
        /// 开启 生日礼金自动发放
        /// </summary>
        public bool EnableBirthAmount { get; set; }

        /// <summary>
        /// 开启 晋级礼包自动发放
        /// </summary>
        public bool EnableProAmount { get; set; }

        /// <summary>
        /// 开启会员自动升级
        /// </summary>
        public bool EnabledAutoGroup { get; set; }
    }
}
