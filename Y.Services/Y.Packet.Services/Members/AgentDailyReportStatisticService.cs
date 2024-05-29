using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Packet.Entities.Members.ViewModels;
using Y.Packet.Entities.Members;
using Y.Packet.Repositories.IMembers;
using Y.Packet.Services.IMembers;
using Y.Infrastructure.Entities.RequestModel;
using Dapper;

namespace Y.Packet.Services.Members
{
    public class AgentDailyReportStatisticService: IAgentDailyReportStatisticService
    {
        private readonly IAgentDailyReportStatisticRepository _repository;
        private readonly IUsersRepository _usersRepository;
        public AgentDailyReportStatisticService(IAgentDailyReportStatisticRepository repository)
        {
            _repository = repository;
        }


        public async Task<(IEnumerable<AgentDailyReportStatistic>, int)> GetPageListAsync(AgentDailyReportQuery q)
        {
            var parms = new DynamicParameters();
            string conditions = $"WHERE 1=1 AND MerchantId={q.MerchantId} ";
            if (q.StartAt != null && q.EndAt != null)
                conditions += $" AND Date >= N'{q.StartAt}' AND Date <= N'{q.EndAt}'";

            var list = await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", parms);
            return (list, _repository.RecordCount(conditions, parms));
        }



        public async Task<(decimal, decimal, int)> GetDatasAsync(int merchantId, int agentId, DateTime startAt, DateTime endAt)
        {
            var dic = await _repository.GetDatasAsync(merchantId,agentId,startAt,endAt);
            // 区间新增人数
            var newNumber = await  _usersRepository.GetNewRegisterCountAsync(merchantId,agentId,startAt,endAt);
            return (dic["GameLoss"], dic["Pay"] ,newNumber);
        }





    }
}