using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IMembers;
using Y.Packet.Entities.Members;

namespace Y.Packet.Repositories.Members
{
    public class AgentShareProfitLogsRepository : BaseRepository<AgentShareProfitLogs, Int32>, IAgentShareProfitLogsRepository
    {
        public AgentShareProfitLogsRepository(IOptionsMonitor<DbOption> options)
        {
            _dbOption = options.Get("Ying.Pay");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        public int DeleteLogical(int[] ids)
        {
            string sql = "update AgentShareProfitLogs set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update AgentShareProfitLogs set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        /// <summary>
        /// 判断是否已经存在日期的发放记录
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="startAt"></param>
        /// <param name="endAt"></param>
        /// <returns></returns>
        public async Task<bool> IsExistLogsAsync(int memberId, DateTime startAt, DateTime endAt)
        {
            string sql = $"SELECT COUNT(1) FROM [AgentShareProfitLogs] WHERE MemberId = {memberId} and StartDate = @startAt AND EndDate=@endAt";
            var rt = await _dbConnection.ExecuteScalarAsync<int>(sql, new { startAt, endAt });
            return rt > 0;
        }


    }
}