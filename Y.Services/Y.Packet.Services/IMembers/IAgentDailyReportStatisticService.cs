using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Entities.RequestModel;
using Y.Packet.Entities.Members;

namespace Y.Packet.Services.IMembers
{
    public interface IAgentDailyReportStatisticService
    {


        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        Task<(IEnumerable<AgentDailyReportStatistic>, int)> GetPageListAsync(AgentDailyReportQuery q);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="agentId"></param>
        /// <param name="startAt"></param>
        /// <param name="endAt"></param>
        /// <returns>盈亏，充值，新增人数</returns>
        Task<(decimal, decimal, int)> GetDatasAsync(int merchantId, int agentId, DateTime startAt, DateTime endAt);
    }
}