using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IPromotions;
using Y.Packet.Entities.Promotions;
using Y.Infrastructure.Library.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Y.Packet.Repositories.Promotions
{
    public class SignInLogRepository : BaseRepository<SignInLog, Int32>, ISignInLogRepository
    {
        public SignInLogRepository(IOptionsMonitor<DbOption> options)
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
            string sql = "update SignInLog set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update SignInLog set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public SignInLog GetLastMemberSigninLog(int merchantId, int memberId)
        {
            string sql = $"SELECT TOP 1* FROM SignInLog WHERE MerchantId={merchantId} AND MemberId={memberId} ORDER BY ID DESC";
            return _dbConnection.QuerySingle<SignInLog>(sql);
        }



        public async Task<Dictionary<int, int>> GetTasksLastWeekDicAsync(int merchantId)
        {
            string sql = $"SELECT MemberId, COUNT(Id) C from SignInLog WHERE MerchantId={merchantId} AND CreateTime BETWEEN '{DateTime.UtcNow.AddHours(8).GetLastWeekMondayDate()}' AND '{DateTime.UtcNow.AddHours(8).GetThisWeekMondayDate()}' GROUP BY MemberId";
            return (await _dbConnection.QueryAsync(sql)).ToDictionary(t => (int)t.MemberId, t => (int)t.C);
        }

    }
}