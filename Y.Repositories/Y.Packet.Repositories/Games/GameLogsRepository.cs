////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //
//                         O\  =  /O                              //
//                      ____/`---'\____                           //
//                    .'  \\|     |//  `.                         //
//                   /  \\|||  :  |||//  \                        //
//                  /  _||||| -:- |||||-  \                       //
//                  |   | \\\  -  /// |   |                       //
//                  | \_|  ''\---/''  |   |                       //
//                  \  .-\__  `-`  ___/-. /                       //
//                ___`. .'  /--.--\  `. . ___                     //
//              ."" '<  `.___\_<|>_/___.'  >'"".                  //
//            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
//            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
//      ========`-.____`-.___\_____/___.-`____.-'========         //
//                           `=---='                              //
//      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
//                   佛祖保佑       永不宕机     永无BUG          //
////////////////////////////////////////////////////////////////////

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：投注日志表接口实现                                                    
*│　作    者：Aaron                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-09-07 16:30:43                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Repositories.Games                                  
*│　类    名： GameLogsRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IGames;
using System.Collections.Generic;
using Y.Infrastructure.Library.Core.Extensions;
using System.Data;
using System.Linq;
using System.Text;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Games.ViewModels;

namespace Y.Packet.Repositories.Games
{
    public class GameLogsRepository : BaseRepository<GameLogs, Int32>, IGameLogsRepository
    {
        public GameLogsRepository(IOptionsMonitor<DbOption> options)
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
            string sql = "update GameLogs set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update GameLogs set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }


        public async Task<(bool, GameLogs)> ExistLogAsync(string gameTypeStr, string sourceId)
        {
            string sql = $"SELECT TOP 1* FROM GameLogs WHERE GameTypeStr='{gameTypeStr}' AND SourceId='{sourceId}'";
            var rt = await _dbConnection.QuerySingleOrDefaultAsync<GameLogs>(sql);
            if (rt == null) return (false, null);
            return (true, rt);
        }


        public async Task<Dictionary<string, decimal>> GetAmountSumAsync(string conditions, DynamicParameters parms)
        {
            var sql = $"SELECT SUM(BetAmount) BetAmount,SUM(ValidBet) ValidBet,SUM(AwardAmount) AwardAmount,SUM(Money) Money FROM {typeof(GameLogs).Name} {conditions}";
            var dic = new Dictionary<string, decimal>();
            using (var reader = await _dbConnection.ExecuteReaderAsync(sql, parms))
            {
                DataTable table = new DataTable();
                table.Load(reader);
                // 或者使用自定义类  DataTableHelper  定义个实体进行转换
                //reader.Close();
                var row0 = table.Rows.Cast<DataRow>().ToList()[0];
                if (row0[0] != System.DBNull.Value)
                {
                    dic.Add("BetAmount", (decimal)row0[0]);
                    dic.Add("ValidBet", (decimal)row0[1]);
                    dic.Add("AwardAmount", (decimal)row0[2]);
                    dic.Add("Money", (decimal)row0[3]);
                }

                //var dt = ((Dapper.SqlMapper.DapperRow)reader).table;

                //    while (reader.Read())
                //{
                //    //if (((Dapper.DbWrappedReader)reader).HasRows)
                //    //{

                //    dic.Add("BetAmount", reader.GetDecimal(0)); //reader.GetDecimal(0)
                //    dic.Add("ValidBet", reader.GetDecimal(1));
                //    dic.Add("AwardAmount", reader.GetDecimal(2));
                //    dic.Add("Money", reader.GetDecimal(3));
                //    //}
                //}
            }
            return dic;
        }

        public async Task<IEnumerable<GameDailyModel>> GetDailySumAsync(string date)
        {
            //            ; WITH A AS(
            //             SELECT  CONVERT(varchar(100), CreateTimeUtc8, 23) Date, GameTypeStr, MerchantId, MemberId, SUM(BetAmount) BetAmount FROM GameLogs WHERE CONVERT(varchar(100), CreateTimeUtc8, 23) = '2021-02-13'  GROUP BY  CONVERT(varchar(100), OrderCreateTimeUtc8, 23), MemberId, MerchantId, GameTypeStr
            //             ),B AS(
            //             SELECT CONVERT(varchar(100), SettlementTimeUtc8, 23) Date,GameTypeStr, MerchantId , MemberId, SUM(ValidBet) ValidBet, SUM(Money) Money,SUM(AwardAmount)AwardAmount FROM GameLogs WHERE CONVERT(varchar(100), SettlementTimeUtc8, 23) = '2021-02-13' GROUP BY  CONVERT(varchar(100), SettlementTimeUtc8, 23)  ,MemberId,MerchantId,GameTypeStr
            //)
            //SELECT A.Date,A.GameTypeStr,A.MerchantId,A.MemberId,A.BetAmount,B.ValidBet,B.Money,B.AwardAmount FROM A FULL OUTER JOIN B ON A.date = B.date AND A.MemberId = B.MemberId AND A.GameTypeStr = B.GameTypeStr


            StringBuilder sb = new StringBuilder();
            sb.Append(";WITH A AS(");
            sb.Append($"SELECT  CONVERT(varchar(100), CreateTimeUtc8, 23) Date,GameTypeStr, MerchantId , MemberId, SUM(BetAmount) BetAmount FROM GameLogs WHERE CONVERT(varchar(100), CreateTimeUtc8, 23) ='{date}'  GROUP BY  CONVERT(varchar(100), CreateTimeUtc8, 23) ,MemberId,MerchantId,GameTypeStr");
            sb.Append("),B AS(");
            sb.Append($"SELECT  CONVERT(varchar(100), SettlementTimeUtc8, 23) Date,GameTypeStr, MerchantId , MemberId, SUM(ValidBet) ValidBet, SUM(Money) Money,SUM(AwardAmount)AwardAmount FROM GameLogs WHERE CONVERT(varchar(100), SettlementTimeUtc8, 23) ='{date}' GROUP BY  CONVERT(varchar(100), SettlementTimeUtc8, 23)  ,MemberId,MerchantId,GameTypeStr");
            sb.Append(")");
            sb.Append("SELECT A.Date,A.GameTypeStr,A.MerchantId,A.MemberId,ISNULL(A.BetAmount,0) BetAmount,ISNULL(B.ValidBet,0) ValidBet,ISNULL(B.Money,0) Money,ISNULL(B.AwardAmount,0) AwardAmount FROM A FULL OUTER JOIN B ON A.date = B.date AND A.MemberId = B.MemberId AND A.GameTypeStr = B.GameTypeStr ");
            IEnumerable<dynamic> rt = await _dbConnection.QueryAsync(sb.ToString());
            return FormatDailySum(rt);
        }

        public async Task<IEnumerable<GameDailyModel>> GetDailySumAsync(int merchantId, int memberId, string date)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(";WITH A AS(");
            sb.Append($"SELECT  CONVERT(varchar(100), CreateTimeUtc8, 23) Date,GameTypeStr, MerchantId , MemberId, SUM(BetAmount) BetAmount FROM GameLogs WHERE MerchantId={merchantId} AND MemberId={memberId} AND CONVERT(varchar(100), CreateTimeUtc8, 23) ='{date}'  GROUP BY  CONVERT(varchar(100), CreateTimeUtc8, 23) ,MemberId,MerchantId,GameTypeStr");
            sb.Append("),B AS(");
            sb.Append($"SELECT  CONVERT(varchar(100), SettlementTimeUtc8, 23) Date,GameTypeStr, MerchantId , MemberId, SUM(ValidBet) ValidBet, SUM(Money) Money,SUM(AwardAmount)AwardAmount FROM GameLogs WHERE MerchantId={merchantId} AND MemberId={memberId} AND CONVERT(varchar(100), SettlementTimeUtc8, 23) ='{date}' GROUP BY  CONVERT(varchar(100), SettlementTimeUtc8, 23)  ,MemberId,MerchantId,GameTypeStr");
            sb.Append(")");
            sb.Append("SELECT A.Date,A.GameTypeStr,A.MerchantId,A.MemberId,ISNULL(A.BetAmount,0) BetAmount,ISNULL(B.ValidBet,0) ValidBet,ISNULL(B.Money,0) Money,ISNULL(B.AwardAmount,0) AwardAmount FROM A FULL OUTER JOIN B ON A.date = B.date AND A.MemberId = B.MemberId AND A.GameTypeStr = B.GameTypeStr ");
            IEnumerable<dynamic> rt = await _dbConnection.QueryAsync(sb.ToString());
            return FormatDailySum(rt);
            //var l = new List<GameDailyModel>();
            //if (rt.Count() == 0) return l;

            //var d = new GameDailyModel();
            //foreach (var r in rt)
            //{
            //    d = new GameDailyModel();
            //    var f = r as IDictionary<string, object>;
            //    d.Date = f["Date"].ToString();
            //    d.MemberId = (int)f["MemberId"];
            //    d.MerchantId = (int)f["MerchantId"];
            //    d.GameTypeStr = (string)f["GameTypeStr"];
            //    d.BetAmount = (decimal)f["BetAmount"];
            //    d.ValidBet = (decimal)f["ValidBet"];
            //    d.Money = (decimal)f["Money"];
            //    d.AwardAmount = (decimal)f["AwardAmount"];
            //    l.Add(d);
            //}
            //return l;
        }


        private List<GameDailyModel> FormatDailySum(IEnumerable<dynamic> data)
        {
            var l = new List<GameDailyModel>();
            if (data.Count() == 0) return l;

            var d = new GameDailyModel();
            foreach (var r in data)
            {
                d = new GameDailyModel();
                var f = r as IDictionary<string, object>;
                d.Date = f["Date"].ToString();
                d.MemberId = (int)f["MemberId"];
                d.MerchantId = (int)f["MerchantId"];
                d.GameTypeStr = (string)f["GameTypeStr"];
                d.BetAmount = (decimal)f["BetAmount"];
                d.ValidBet = f["ValidBet"].To<decimal>();
                d.Money = (decimal)f["Money"];
                d.AwardAmount = (decimal)f["AwardAmount"];
                l.Add(d);
            }
            return l;
        }




        /// <summary>
        /// 创建代理的日报表数据
        /// </summary>
        /// <param name="date"></param>
        /// <param name="memberIds"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, object>> CreateDailyAgentDataAsync(int merchantId, string date, IEnumerable<int> memberIds)
        {
            var dic = new Dictionary<string, object>();
            //string sql = $"SELECT Date,MerchantId, ISNULL(SUM(BetAmount),0) BetAmount ,ISNULL(SUM(ValidBet),0) ValidBet,ISNULL(SUM(Money),0) Money FROM GameUsersDailyReportStatistic WHERE Date='{date}' AND MemberId IN({string.Join(",", memberIds)}) GROUP BY Date,MerchantId";
            string sql = $"SELECT ISNULL(SUM(BetAmount),0) BetAmount ,ISNULL(SUM(ValidBet),0) ValidBet,ISNULL(SUM(Money),0) Money FROM GameUsersDailyReportStatistic WHERE Date='{date}' AND MemberId IN({string.Join(",", memberIds)}) ";
            //Console.WriteLine(sql );
            using (var reader = await _dbConnection.ExecuteReaderAsync(sql))
            {
                DataTable table = new DataTable();
                table.Load(reader);
                // 或者使用自定义类  DataTableHelper  定义个实体进行转换
                //reader.Close();
                var row0 = table.Rows.Cast<DataRow>().ToList()[0];
                if (row0[0] != System.DBNull.Value)
                {
                    dic.Add("Date", date);
                    dic.Add("MerchantId", merchantId);
                    dic.Add("BetAmount", row0[0]);
                    dic.Add("ValidBet", row0[1]);
                    dic.Add("Money", row0[2]);
                }
            }
            return dic;
        }



        /// <summary>
        /// [周签到] 获取上周用户总流水
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<decimal> GetTaskLaskWeekValidBetAsync(int merchantId, int userId)
        {
            string sql = "SELECT ISNULL(SUM(ValidBet),0) FROM GameLogs WHERE SettlementTimeUtc8 BETWEEN @LastWeekStartTime AND @LastWeekEndTime ";

            return await _dbConnection.ExecuteScalarAsync<decimal>(sql, new
            {
                LastWeekStartTime = DateTime.Now.GetLastWeekMondayDate(),
                LastWeekEndTime = DateTime.Now.GetThisWeekMondayDate(),
            });

        }

        public async Task<IEnumerable<GameLogsForWashOrdersModel>> GetTheLast2MinutesFinishedLogsAsync(int logId = 0)
        {
            string sql = "SELECT Id,MerchantId,MemberId,ValidBet,GameTypeStr FROM [GameLogs] where Status!=0";
            if (logId != 0)
                sql += $" AND id={logId}";
            else
                sql += $" AND SettlementTimeUtc8 > N'{DateTime.UtcNow.AddHours(8).AddMinutes(-2)}'";
            return await _dbConnection.QueryAsync<GameLogsForWashOrdersModel>(sql);
        }

    }
}