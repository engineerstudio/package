using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Packet.Entities.Promotions;
using Y.Packet.Entities.Promotions.ViewModels;

namespace Y.Packet.Services.IPromotions
{
    public interface IActivityOrdersDetailsService
    {
        Task<(bool sucess, string msg, int activityOrderId)> InsertAsync(int orderId, int merchantId, int userId, int promotionId, ActivityType activityType, decimal reward, string sourceId, string createDate, DateTime createTime, ActivityOrders.ActivityOrderStatus status);

        Task<(IEnumerable<ActivityOrdersDetails>, int)> GetListAsync(ActivityOrdersListPageQuery q);
    }
}