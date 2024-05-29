using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IVips;
using Y.Packet.Entities.Vips;

namespace Y.Packet.Repositories.Vips
{
    public class WashOrderDetailRepository:BaseRepository<WashOrderDetail,Int32>, IWashOrderDetailRepository
    {
        public WashOrderDetailRepository(IOptionsMonitor<DbOption> options)
        {
            _dbOption =options.Get("Ying.Vips");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

		public int DeleteLogical(int[] ids)
        {
            string sql = "update WashOrderDetail set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update WashOrderDetail set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public decimal GetExistAmount(int orderId)
        {
            string sql = $"SELECT ISNULL(SUM(Amount),0) FROM WashOrderDetail WHERE OrderId = {orderId}";
            return _dbConnection.ExecuteScalar<decimal>(sql);
        }

        
        public async Task<bool> IsExistSourceIdAsync(int memberId, int washOrderId, string sourceId)
        {
            string sql = $"SELECT COUNT(1) FROM WashOrderDetail WHERE OrderId={washOrderId} AND MemberId = {memberId} AND SourceOrderId='{sourceId}' ";
            return (await _dbConnection.ExecuteScalarAsync<int>(sql)) > 0;
        }

        public async Task<decimal> GetTotalWashAmountAsync(int memberId)
        {
            string sql = $"SELECT SUM(Amount) FROM WashOrderDetail WHERE MemberId={memberId}";
            return await _dbConnection.ExecuteScalarAsync<decimal>(sql);
        }

    }
}