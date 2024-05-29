using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Entities.Pay;
using Y.Packet.Repositories.IPay;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Y.Infrastructure.Library.Core.Extensions;

namespace Y.Packet.Repositories.Pay
{
    public class MemberDataSummaryRepository : BaseRepository<MemberDataSummary, Int32>, IMemberDataSummaryRepository
    {
        public MemberDataSummaryRepository(IOptionsMonitor<DbOption> options)
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
            string sql = "update MemberDataSummary set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update MemberDataSummary set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }


        public async Task<(decimal, decimal)> GetMembersDepositWithdrawalTotalAsync(IEnumerable<int> members)
        {
            string sql = @"CREATE TABLE #MemberIds (id INT)
                            INSERT INTO #MemberIds
                            SELECT value FROM string_split(@memberIds,',')
                            SELECT ISNULL(SUM(PaymentTotal),0) PaymentTotal,ISNULL(SUM(WithdrawalTotal),0) WithdrawalTotal FROM [MemberDataSummary] mds
                            WHERE Date BETWEEN '' AND '' AND  EXISTS (
                                SELECT id FROM  #MemberIds WHERE  #MemberIds.id =mds.MemberId
                            ) GROUP BY mds.MerchantId 
                          drop table #MemberIds
                    ";

            using (var reader = await _dbConnection.ExecuteReaderAsync(sql, new { memberIds = members.ToSplitByComma() }))
            {
                DataTable table = new DataTable();
                table.Load(reader);
                var row0 = table.Rows.Cast<DataRow>().ToList()[0];
                if (row0[0] != System.DBNull.Value)
                    return ((decimal)row0[0], (decimal)row0[1]);
                else
                    return (0, 0);
            }

        }

    }
}