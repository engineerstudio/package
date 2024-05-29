using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Packet.Entities.Promotions;

namespace Y.Packet.Repositories.IPromotions
{
    public interface IPromotionsConfigRepository : IBaseRepository<PromotionsConfig, Int32>
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
        /// 更新配置内容
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        Task<int> UpdateConfigAsync(int id, string config);

        /// <summary>
        /// 根据站点与活动ID获取活动信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="promoId"></param>
        /// <returns></returns>
        Task<PromotionsConfig> GetAsync(int merchantId, int promoId);

        /// <summary>
        /// 获取首页显示的活动
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<Dictionary<int, string>> GetHomeDisplayAsync(int merchantId);
        Task<int?> InsertWithCacheAsync(PromotionsConfig config);
        Task<int?> UpdateWithCacheAsync(PromotionsConfig config);
        Task<IEnumerable<PromotionsConfig>> GetListAsync(int? merchantId, ActivityType? activityType = null, bool? enabled = null, bool? visible = null, DateTime? startTime = null, DateTime? endTime = null);
        Task<PromotionsConfig> GetFromCacheAsync(int merchantId, int promoId);

        Task MigrateSqlDbToRedisDbAsync();
    }
}