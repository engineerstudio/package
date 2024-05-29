using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IGames;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.YEntity;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Y.Packet.Entities.Games;

namespace Y.Packet.Repositories.Games
{
    public class GameUsersDailyReportStatisticRepository : BaseRepository<GameUsersDailyReportStatistic, Int32>, IGameUsersDailyReportStatisticRepository
    {
        public GameUsersDailyReportStatisticRepository(IOptionsMonitor<DbOption> options)
        {
            _dbOption = options.Get("Ying.Games");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        public int DeleteLogical(int[] ids)
        {
            string sql = "update GameUsersDailyReportStatistic set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update GameUsersDailyReportStatistic set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<(bool, GameUsersDailyReportStatistic)> ExistDateAsync(string date, int memberId, string gameTypeStr)
        {
            string sql = $"SELECT top 1* FROM {typeof(GameUsersDailyReportStatistic).Name} WHERE DATE='{date}' AND MemberId={memberId} AND GameTypeStr='{gameTypeStr}'";
            var rt = await _dbConnection.QueryFirstOrDefaultAsync<GameUsersDailyReportStatistic>(sql);
            return (rt != null, rt);
        }



        public async Task CreateAsync(string date, int memberid, string gameTypeStr, int merchantId, string playerName)
        {
            var rt = await this.ExistDateAsync(date, memberid, gameTypeStr);
            if (!rt.Item1)
            {
                var m = new GameUsersDailyReportStatistic();
                m.Date = Convert.ToDateTime(date);
                m.MemberId = memberid;
                m.GameTypeStr = gameTypeStr;
                m.MerchantId = merchantId;
                m.PlayerName = playerName;
                m.GameCategory = gameTypeStr.ToEnum<GameType>().Value.TransToGameCate();
                await _dbConnection.InsertAsync(m);
            }
        }


        public async Task SaveBetAmountAsync(string date, int memberId, string gameTypeStr, decimal betAmount)
        {
            string sql = $"UPDATE GameUsersDailyReportStatistic SET BetAmount += {betAmount} WHERE  DATE='{date}' AND MemberId={memberId} AND GameTypeStr='{gameTypeStr}' ";
            await _dbConnection.ExecuteAsync(sql);
        }


        public async Task SaveValidbetAndMoney(string date, int memberId, string gameTypeStr, decimal validbet, decimal money)
        {
            string sql = $"UPDATE GameUsersDailyReportStatistic SET ValidBet += {validbet},Money += {money} WHERE  DATE='{date}' AND MemberId={memberId} AND GameTypeStr='{gameTypeStr}' ";
            await _dbConnection.ExecuteAsync(sql);
        }


        public async Task<Dictionary<string, decimal>> GetMembersDataSummaryByIds(int merchantId, IEnumerable<int> members, DateTime startTime, DateTime endTime)
        {
            string sql = $@"CREATE TABLE #MemberIds (id INT)
                            INSERT INTO #MemberIds
                            SELECT value FROM string_split(@memberIds,',')
                            SELECT   ISNULL(SUM(BetAmount),0) BetAmount,ISNULL(SUM(ValidBet),0)ValidBet,ISNULL(SUM(Money),0)Money from GameUsersDailyReportStatistic mds
                            WHERE MerchantId={merchantId} AND Date BETWEEN N'{startTime}' AND N'{endTime}' AND  EXISTS (
                                SELECT id FROM  #MemberIds WHERE  #MemberIds.id =mds.MemberId
                            ) GROUP BY mds.MerchantId 
                          drop table #MemberIds
                    ";
            //Console.WriteLine(sql);
            Dictionary<string, decimal> dic = new Dictionary<string, decimal>()
            {
                { "BetAmount",0},
                { "ValidBet",0},
                { "Money",0},
            };
            string str = string.Empty;
            foreach (var m in members)
                str += $"{m},";

            //Console.WriteLine(str);
            using (var reader = await _dbConnection.ExecuteReaderAsync(sql, new { memberIds = str.RemoveLastChar() }))
            {
                DataTable table = new DataTable();
                table.Load(reader);
                var row0 = table.Rows.Cast<DataRow>().ToList()[0];
                if (row0[0] != System.DBNull.Value)
                    dic["BetAmount"] = row0[0].To<Decimal>();
                if (row0[1] != System.DBNull.Value)
                    dic["ValidBet"] = row0[0].To<Decimal>();
                if (row0[2] != System.DBNull.Value)
                    dic["Money"] = row0[0].To<Decimal>();
                return dic;
            }
        }


        public async Task<(IEnumerable<GameUsersDailyReportStatistic>,int)> GetPageListAsync(int merchantId, IEnumerable<int> members, DateTime startTime, DateTime endTime, int page, int pageSize)
        {
            string sql = $@"CREATE TABLE #MemberIds (id INT)
                            INSERT INTO #MemberIds
                            SELECT value FROM string_split(@memberIds,',')
							SELECT TOP {pageSize} *   
								FROM   
									(  
										SELECT ROW_NUMBER() OVER (ORDER BY id) AS RowNumber,* FROM GameUsersDailyReportStatistic  
									)   as A    
                            WHERE MerchantId={merchantId} AND Date BETWEEN N'{startTime}' AND N'{endTime}' AND  EXISTS (
                                SELECT id FROM  #MemberIds WHERE  #MemberIds.id =mds.MemberId
                            ) AND  RowNumber > {pageSize}*({page}-1) 
                          drop table #MemberIds";

            string sql_count = $@"CREATE TABLE #MemberIds (id INT)
                            INSERT INTO #MemberIds
                            SELECT value FROM string_split(@memberIds,',')
							SELECT  COUNT(Id)No FROM GameUsersDailyReportStatistic  A  
                            WHERE MerchantId={merchantId} AND Date BETWEEN N'{startTime}' AND N'{endTime}' AND  EXISTS (
                                SELECT id FROM  #MemberIds WHERE  #MemberIds.id =A.MemberId
                            ) 
                          drop table #MemberIds";


            string str = string.Empty;
            foreach (var m in members)
                str += $"{m},";

            var d = await _dbConnection.QueryAsync<GameUsersDailyReportStatistic>(sql, new { memberIds = str.RemoveLastChar() });
            var c = await _dbConnection.ExecuteScalarAsync<int>(sql_count, new { memberIds = str.RemoveLastChar() });
            return (d, c);
        }


        /// <summary>
        /// 返水等级数据，用户/有效投注集合
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<Dictionary<int, decimal>> GetRebateGradeDataAsync(int merchantId, DateTime startTime, DateTime endTime)
        {
            string sql = $"SELECT MemberId, SUM(ValidBet)ValidBet  FROM GameUsersDailyReportStatistic WHERE MerchantId = {merchantId} AND Date>=N'{startTime}' AND Date<=N'{endTime}' GROUP BY MemberId";
            return (await _dbConnection.QueryAsync(sql)).ToDictionary(t => (int)t.MemberId, t => (decimal)t.ValidBet);
        }

    }
}