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
			[Description("�����")]
			None,
            [Description("���·�")]
            Issued,
            [Description("�Ѿܾ�")]
            Rejected,
        }

    }
}
