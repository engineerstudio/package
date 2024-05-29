using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Packet.Entities.Members;

namespace Y.Packet.Repositories.IMembers
{
    public interface IAgentDailyReportStatisticRepository : IBaseRepository<AgentDailyReportStatistic, Int32>
    {
	     /// <summary>
        /// 逻辑删除返回影响的行数
        /// </summary>
        /// <param name="ids">需要删除的主键数组</param>
        /// <returns>影响的行数</returns>
        Int32 DeleteLogical(Int32[] ids);
        /// <summary>
        /// 逻辑删除返回影响的行数（异步操作）
        /// </summary>
        /// <param name="ids">需要删除的主键数组</param>
        /// <returns>影响的行数</returns>
        Task<Int32> DeleteLogicalAsync(Int32[] ids);
        /// <summary>
        /// 游戏盈亏 充值 GameLoss Pay
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="agentId"></param>
        /// <param name="startAt"></param>
        /// <param name="endAt"></param>
        /// <returns></returns>
        Task<Dictionary<string, decimal>> GetDatasAsync(int merchantId, int agentId, DateTime startAt, DateTime endAt);

        /// <summary>
        /// 获取代理盈亏值
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="agentId"></param>
        /// <param name="startAt"></param>
        /// <param name="endAt"></param>
        /// <returns></returns>
        Task<decimal> GetstatisticsAsync(int merchantId, int agentId, DateTime startAt, DateTime endAt);
    }
}