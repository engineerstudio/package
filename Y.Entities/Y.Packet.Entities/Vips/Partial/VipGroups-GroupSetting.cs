using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Vips
{
    public class GroupSetting
    {

        public int GroupId { get; set; }

        #region 用户分组条件, 累计充值金额达到即可升级，升级后即计算VIP优惠条件

        /// <summary>
        /// 累计充值金额 
        /// </summary>
        public decimal AccumulatedRechargeAmount { get; set; }

        /// <summary>
        /// 累计打码量 用分分组
        /// </summary>
        public decimal AccumulatedEffectiveAmount { get; set; }

        #endregion


        #region VIP保级条件
        /// <summary>
        /// 保级打码量(90天)
        /// </summary>
        public decimal StayGroupEffectiveAmount { get; set; }

        /// <summary>
        /// 保级充值金额(90天)
        /// </summary>
        //public decimal StayGroupRechargeAmount { get; set; }

        #endregion


        #region VIP尊享

        /// <summary>
        /// 自动发放VIP优惠/手动发放
        /// </summary>
        public bool EnabledAutoGift { get; set; }
        /// <summary>
        /// 提款次数
        /// </summary>
        public int WithdrawalsCount { get; set; }
        /// <summary>
        /// 日提款额度
        /// </summary>
        public decimal WithdrawalDailyTotalAmount { get; set; }

        /// <summary>
        /// 晋级礼包金额,升级礼金
        /// </summary>
        public decimal ProAmount { get; set; }

        /// <summary>
        /// 生日礼包
        /// </summary>
        public decimal BirthAmount { get; set; }

        /// <summary>
        /// 每月俸禄
        /// </summary>
        public decimal MonthSalary { get; set; }

        /// <summary>
        /// 每周俸禄
        /// </summary>
        public decimal WeeklySalary { get; set; }

        /// <summary>
        /// 最高返水
        /// </summary>
        public decimal MaxRebate { get; set; }

        #endregion


        #region VIP晋级优惠

        /// <summary>
        /// 晋级奖金
        /// </summary>
        public decimal VipBonus { get; set; }

        /// <summary>
        /// 晋级奖金流水倍数
        /// </summary>
        public decimal VipWashMultiple { get; set; }


        #endregion


        /// <summary>
        /// 是否分组自动升级
        /// </summary>
        public bool EnabledAuto { get; set; }



        public string Description { get; set; }



    }





}
