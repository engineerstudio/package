using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Members.ViewModels.AgentCenter
{
    public class AgentIndexVM
    {
        /// <summary>
        /// 邀请码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 昨日佣金
        /// </summary>
        public decimal Yestoday { get; set; }
        /// <summary>
        /// 本月佣金
        /// </summary>
        public decimal ThisMonth { get; set; }
        /// <summary>
        /// 今日新增
        /// </summary>
        public int NewUser { get; set; }
        /// <summary>
        /// 总计下级人数
        /// </summary>
        public int TotalSubUser { get; set; }

    }
}
