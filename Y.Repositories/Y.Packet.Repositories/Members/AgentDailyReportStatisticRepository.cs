using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IMembers;
using Y.Infrastructure.Library.Core.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Y.Packet.Entities.Members;

namespace Y.Packet.Repositories.Members
{
    public class AgentDailyReportStatisticRepository:BaseRepository<AgentDailyReportStatistic,Int32>, IAgentDailyReportStatisticRepository
    {
        public AgentDailyReportStatisticRepository(IOptionsMonitor<DbOption> options)
        {
            _dbOption =options.Get("Ying.Users");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

		public int DeleteLogical(int[] ids)
        {
            string sql = "update AgentDailyReportStatistic set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update AgentDailyReportStatistic set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        // 游戏盈亏 充值
        public async Task<Dictionary<string, decimal>> GetDatasAsync(int merchantId, int agentId, DateTime startAt, DateTime endAt)
        {
            string sql = @$"with a as(
                            SELECT SubId FROM UserHierarchy WHERE  SiteId = {merchantId} AND UserId = {agentId}
                            ),
                            b as(
                            SELECT * FROM 	AgentDailyReportStatistic c
				                            JOIN  a on c.MemberId = a.SubId  
				                            where  c.Date >= N'{startAt.ToDateString()}' AND c.Date <='{endAt.ToDateString()}'
                            )
                            SELECT ISNULL(SUM(GameLoss),0) GameLoss,ISNULL(SUM(Pay),0) Pay FROM AgentDailyReportStatistic ";

            Dictionary<string, decimal> dic = new Dictionary<string, decimal>()
            {
                { "GameLoss",0},
                { "Pay",0},
            };

            using (var reader = await _dbConnection.ExecuteReaderAsync(sql))
            {
                DataTable table = new DataTable();
                table.Load(reader);
                var row0 = table.Rows.Cast<DataRow>().ToList()[0];
                if (row0[0] != System.DBNull.Value)
                    dic["GameLoss"] = row0[0].To<decimal>();
                if (row0[1] != System.DBNull.Value)
                    dic["Pay"] = row0[0].To<decimal>();
                return dic;
            }
        }


        public async Task<decimal> GetstatisticsAsync(int merchantId,int agentId, DateTime startAt, DateTime endAt)
        {
            string sql =$"SELECT SUM(GameLoss) GameLoss FROM AgentDailyReportStatistic WHERE MemberId = {agentId} AND [Date] >= N'{startAt.ToDateString()}' AND [Date] <= N'{endAt.ToDateString()}'";
            return await _dbConnection.ExecuteScalarAsync<decimal>(sql);
        }


    }
}