using System;
using System.Collections.Generic;
using System.Text;
using Y.Packet.Repositories.IMembers;
using Y.Packet.Services.IMembers;

namespace Y.Packet.Services.Members
{
    public class AgentShareProfitLogsService: IAgentShareProfitLogsService
    {
        private readonly IAgentShareProfitLogsRepository _repository;

        public AgentShareProfitLogsService(IAgentShareProfitLogsRepository repository)
        {
            _repository = repository;
        }
    }
}