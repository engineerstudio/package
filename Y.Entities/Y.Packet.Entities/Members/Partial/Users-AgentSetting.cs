using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Members
{
    public class AgentSetting
    {
        /// <summary>
        /// 返点规则Id
        /// </summary>
        public int AgentRebateId { get; set; }

        /// <summary>
        /// 代理返佣规则ID
        /// </summary>
        public int CommissionId { get; set; }


        /// <summary>
        /// 代理合约Id
        /// </summary>
        public int ContractId { get; set; }


        /// <summary>
        /// 设置为站点默认代理
        /// </summary>
        public bool IsDefault { get; set; }


        /// <summary>
        /// 代理推广码,6位字符串
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 代理站点地址,可以根据这个地址进行注册进来的用户,全部属于该代理名下
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 站点模板 扩展功能
        /// </summary>
        public string Template { get; set; }

    }
}
