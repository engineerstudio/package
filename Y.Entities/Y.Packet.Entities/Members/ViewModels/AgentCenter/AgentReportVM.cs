using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Members.ViewModels.AgentCenter
{
    public class AgentReportVM
    {

        /// <summary>
        /// 代理人数
        /// </summary>
        public int AgentNum { get; set; }
        /// <summary>
        /// 存款人数
        /// </summary>
        public int DepositNum { get; set; }


        /// <summary>
        /// 存款金额
        /// </summary>
        public decimal Deposit { get; set; }
        /// <summary>
        /// 取款金额
        /// </summary>
        public decimal Withdrawal { get; set; }


        /// <summary>
        /// 投注人数
        /// </summary>
        public decimal BetMemberNum { get; set; }
        /// <summary>
        /// 投注金额
        /// </summary>
        public decimal BetAmount { get; set; }
        /// <summary>
        /// 代理佣金
        /// </summary>
        public decimal Commission { get; set; }

    }
}
