using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Entities.Promotions;
using Y.Packet.Repositories.IPromotions;
using Y.Infrastructure.Library.Core.Extensions;
using System.Collections.Generic;
using Y.Packet.Entities.Promotions.ViewModels;

namespace Y.Packet.Repositories.Promotions
{
    public class ActivityOrdersRepository : BaseRepository<ActivityOrders, Int32>, IActivityOrdersRepository
    {
        public ActivityOrdersRepository(IOptionsMonitor<DbOption> options)
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
            string sql = "update ActivityOrders set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update ActivityOrders set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }


        public async Task<bool> ExistOrdersAsync(int merchantId, int userId, ActivityType activityType)
        {
            string sql = $"SELECT COUNT(1) FROM ActivityOrders WHERE MerchantId={merchantId} AND UserId={userId} AND AType={activityType.GetEnumValue()}";
            return (await _dbConnection.ExecuteScalarAsync<int>(sql)) > 0;
        }

        /// <summary>
        /// [生日礼金] 判断是否存在在过去360天内发放过，需要根据备注字段进行查询，生日礼金备注字段是固定的
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <param name="dt"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<bool> ExistBirthOrdersAsync(int merchantId, int userId, DateTime dt, string description)
        {
            string sql = $"SELECT COUNT(1) FROM ActivityOrders WHERE MerchantId={merchantId} AND UserId={userId} AND AType={ActivityType.None.GetEnumValue()} AND Description=@Desc AND CreateTime > @CreateTime";
            return (await _dbConnection.ExecuteScalarAsync<int>(sql, new { Desc = description, CreateTime = dt })) > 0;
        }


        public async Task<bool> ExistSourceIdAsync(string sourceId)
        {
            string sql = $"SELECT COUNT(1) FROM ActivityOrders WHERE sourceId=@sourceId";
            return (await _dbConnection.ExecuteScalarAsync<int>(sql, new { sourceId })) > 0;
        }


        public async Task<(IEnumerable<ActivityOrderSummary>, int)> GetOrderSummaryAsync(int merchantId, int promoId)
        {
            string sql = $"SELECT SUM(Reward) reward,UserId,MerchantId,CreateDate FROM ActivityOrders WHERE MerchantId={merchantId} AND Status = 1 AND PromotionId = {promoId}  GROUP BY  CreateDate,MerchantId,UserId";
            string sqlCount = $"SELECT Count(*) FROM ActivityOrders WHERE MerchantId={merchantId} AND Status = 1 AND PromotionId = {promoId}  GROUP BY  CreateDate,MerchantId,UserId";
            return (await _dbConnection.QueryAsync<ActivityOrderSummary>(sql), await _dbConnection.ExecuteScalarAsync<int>(sqlCount));
        }


    }
}