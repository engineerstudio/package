using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Vips
{
    /// <summary>
    /// 用户组提现条件
    /// </summary>
    public class WithdrawalSetting
    {

        /// <summary>
        /// 是否允许提现，true:允许
        /// </summary>
        public bool Eabled { get; set; }

        /// <summary>
        /// 每日免费提现次数
        /// </summary>
        public int FreeWithdrawCount { get; set; }

        /// <summary>
        /// 每日提现次数
        /// </summary>
        public int WithdrawCount { get; set; }

        /// <summary>
        /// 提现手续费百分比
        /// </summary>
        public decimal FeePercentage { get; set; }

        /// <summary>
        /// 最低提现手续费
        /// </summary>
        public decimal MinWithdrawAmount { get; set; }

    }
}
