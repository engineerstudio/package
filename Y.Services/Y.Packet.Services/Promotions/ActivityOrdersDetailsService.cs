using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Packet.Entities.Promotions;
using Y.Packet.Entities.Promotions.ViewModels;
using Y.Packet.Repositories.IPromotions;
using Y.Packet.Services.IPromotions;

namespace Y.Packet.Services.Promotions
{
    public class ActivityOrdersDetailsService : IActivityOrdersDetailsService
    {
        private readonly IActivityOrdersDetailsRepository _repository;

        public ActivityOrdersDetailsService(IActivityOrdersDetailsRepository repository)
        {
            _repository = repository;
        }

        public async Task<(bool, string, int)> InsertAsync(int orderId, int merchantId, int userId, int promotionId, ActivityType activityType, decimal reward, string sourceId, string createDate, DateTime createTime, ActivityOrders.ActivityOrderStatus status)
        {
            if (merchantId <= 0 || userId <= 0) return (false, "商户/用户参数不正确", 0);
            if (reward <= 0) return (false, "派奖金额不正确", 0);
            if (string.IsNullOrEmpty(sourceId)) return (false, "来源ID不正确", 0);
            if (await _repository.ExistOrdersAsync(sourceId)) return (false, $"该订单已经存在:{sourceId}", 0);

            var orderDetails = new ActivityOrdersDetails()
            {
                OrderId = orderId,
                MerchantId = merchantId,
                PromotionId = promotionId,
                AType = activityType,
                MemberId = userId,
                Reward = reward,
                Status = status,
                Description = $"用户:{userId},游戏{activityType.GetDescription()},{activityType.GetDescription()},{reward}",
                CreateDate = createDate,
                CreateTime = createTime,
                SourceId = sourceId,
                RewardTime = DefaultString.DefaultDateTime
            };

            var rt = await _repository.InsertAsync(orderDetails);
            if (rt != null && rt.HasValue && rt.Value > 0) return (true, "", rt.Value);
            return (false, $"保存失败 {sourceId}", 0);
        }





        public async Task<(IEnumerable<ActivityOrdersDetails>, int)> GetListAsync(ActivityOrdersListPageQuery q)
        {

            string conditions = "WHERE 1=1 ";

            var list = await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", null);

            return (list, _repository.RecordCount());

        }







    }
}