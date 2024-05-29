using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Packet.Entities.Promotions;
using Y.Packet.Entities.Promotions.ViewModels;

namespace Y.Packet.Repositories.IPromotions
{
    public interface IActivityOrdersRepository : IBaseRepository<ActivityOrders, Int32>
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
        /// 判断用户是否已经参与过此次活动
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <param name="activityType"></param>
        /// <returns></returns>
        Task<bool> ExistOrdersAsync(int merchantId, int userId, ActivityType activityType);

        /// <summary>
        /// [生日礼金] 判断是否存在在过去360天内发放过，需要根据备注字段进行查询，生日礼金备注字段是固定的
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <param name="dt"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        Task<bool> ExistBirthOrdersAsync(int merchantId, int userId, DateTime dt, string description);
        Task<bool> ExistSourceIdAsync(string sourceId);


        /// <summary>
        /// [活动列表->订单汇总]获取订单汇总
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="promoId"></param>
        /// <returns></returns>
        Task<(IEnumerable<ActivityOrderSummary>, int)> GetOrderSummaryAsync(int merchantId, int promoId);
    }
}