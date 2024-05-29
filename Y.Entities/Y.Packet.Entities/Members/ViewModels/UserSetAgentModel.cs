using System;
using System.Collections.Generic;
using System.Text;
using static Y.Packet.Entities.Members.Users;

namespace Y.Packet.Entities.Members.ViewModels
{
    public class UserSetAgentModel
    {
        public int MerchantId { get; set; }
        public int UserId { get; set; }

        public UserType Type { get; set; }

        public int AgentId { get; set; }

        /// <summary>
        /// 返水活动ID，此应配置在 [活动]
        /// </summary>
        public int commissionId { get; set; }

        public int ContractId { get; set; }

        /// <summary>
        /// 代理返点活动
        /// </summary>
        public int AgentRebateId { get; set; }
        public string Code { get; set; }

    }


    public class UserAgentSetModel
    {
        public int MerchantId { get; set; }
        public int Id { get; set; }
        public string AccountName { get; set; }
        public int ParentAgentId { get; set; }

        /// <summary>
        /// 代理返点活动
        /// </summary>
        public int AgentRebateId { get; set; }
        /// <summary>
        /// 返佣活动ID，此应配置在 [活动]
        /// </summary>
        public int commissionId { get; set; }

        public int ContractId { get; set; }
        public string Url { get; set; }
        public string Template { get; set; }
    }


}
