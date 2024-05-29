using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Members
{
	/// <summary>
	/// Aaron
	/// 2023-01-09 17:58:29
	/// 
	/// </summary>
	public partial class AgentShareProfitLogs
	{

		public enum AgentPayLogStatus
		{
			[Description("审核中")]
			None,
            [Description("已下发")]
            Issued,
            [Description("已拒绝")]
            Rejected,
        }

    }
}
