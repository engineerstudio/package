using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Y.Packet.Entities.Vips
{
    public partial class PointSetting
    {
        /// <summary>
        /// 积分项目
        /// </summary>
        public PointItem PointItemType { get; set; }
        /// <summary>
        /// 积分☞
        /// </summary>
        public int Point { get; set; }
        /// <summary>
        /// 积分计算方式
        /// </summary>
        public Calculate CalculateType { get; set; }
        /// <summary>
        /// 积分次数计算方式
        /// </summary>
        public Effective EffectiveType { get; set; }

    }
    public partial class PointSetting
    {
        /// <summary>
        /// 积分项目
        /// </summary>
        public enum PointItem
        {
            [Description("登录")]
            Login,
            [Description("充值")]
            Recharge,
            [Description("签到")]
            SignIn,
            [Description("投注")]
            Betting,
        }

        /// <summary>
        /// 计算方式
        /// </summary>
        public enum Calculate
        {
            [Description("固定值")]
            Fixed,
            [Description("比例值")]
            Rate
        }

        /// <summary>
        /// 有效计算次数
        /// </summary>
        public enum Effective
        {
            [Description("只算一次")]
            Once,
            [Description("每天一次")]
            OnceADay,
            [Description("每次")]
            Each
        }

    }


}
