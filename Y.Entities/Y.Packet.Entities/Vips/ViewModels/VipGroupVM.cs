using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Vips.ViewModels
{
    public class VipGroupVM
    {
        /// <summary>
        /// 分组Id
        /// </summary>
        public int Id { get; set; }

        public string Name { get; set; }

        public int SortNo { get; set; }

        /// <summary>
        /// 累计充值
        /// </summary>
        public decimal RechargeAmount { get; set; }

        /// <summary>
        /// 累计打码量
        /// </summary>
        public decimal EffectiveAmount { get; set; }


        /// <summary>
        /// 晋级礼包金额
        /// </summary>
        public decimal ProAmount { get; set; }


        /// <summary>
        /// 每月俸禄
        /// </summary>
        public decimal MonthSalary { get; set; }

        /// <summary>
        /// 每周俸禄
        /// </summary>
        public decimal WeeklySalary { get; set; }

        /// <summary>
        /// 提款次数
        /// </summary>
        public int WithdrawalsCount { get; set; }


        /// <summary>
        /// 日提款额度
        /// </summary>
        public decimal WithdrawalTotalAccount { get; set; }


        /// <summary>
        /// 晋级奖金
        /// </summary>
        public decimal VipBonus { get; set; }

        /// <summary>
        /// 晋级奖金流水倍数
        /// </summary>
        public decimal VipWashMultiple { get; set; }
    }
}
