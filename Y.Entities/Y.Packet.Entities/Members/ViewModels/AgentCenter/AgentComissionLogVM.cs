using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Members.ViewModels.AgentCenter
{
    public class AgentComissionLogVM
    {
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Commission { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public decimal Description { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }

    }
}
