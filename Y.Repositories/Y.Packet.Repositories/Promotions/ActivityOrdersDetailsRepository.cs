using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Entities.Promotions;
using Y.Packet.Repositories.IPromotions;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Y.Packet.Repositories.Promotions
{
    public class ActivityOrdersDetailsRepository : BaseRepository<ActivityOrdersDetails, Int32>, IActivityOrdersDetailsRepository
    {
        public ActivityOrdersDetailsRepository(IOptionsMonitor<DbOption> options)
        {
            _dbOption = options.Get("Ying.Promotions");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        public int DeleteLogical(int[] ids)
        {
            string sql = "update ActivityOrdersDetails set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update ActivityOrdersDetails set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<bool> ExistOrdersAsync(string sourceId)
        {
            string sql = "SELECT COUNT(1) FROM ActivityOrdersDetails WHERE SourceId=@SourceId";

            return (await _dbConnection.ExecuteScalarAsync<int>(sql, new { SourceId = sourceId })) > 0;
        }


        public async Task<(int, decimal)> CreateDailyAgentDataAsync(string date, IEnumerable<int> memberIds)
        {
            string sql = $"SELECT  COUNT(Id) No, ISNULL(SUM(Reward),0) Amount  FROM {typeof(ActivityOrdersDetails).Name} WHERE  CONVERT(varchar(100), RewardTime, 23)='{date}'  AND Status = 1 AND MemberId IN({string.Join(",", memberIds)})";
            Console.WriteLine(sql);
            using (var reader = await _dbConnection.ExecuteReaderAsync(sql))
            {
                DataTable table = new DataTable();
                table.Load(reader);
                // 或者使用自定义类  DataTableHelper  定义个实体进行转换
                //reader.Close();
                var row0 = table.Rows.Cast<DataRow>().ToList()[0];
                if (row0[0] != System.DBNull.Value)
                {
                    decimal amount = (decimal)row0[1];
                    return ((int)row0[0], amount);
                }
                else
                    return (0, 0);
            }

        }
    }
}