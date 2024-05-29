using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.ICache.IRedis.IPromotionsService;
using Y.Packet.Entities.Promotions;
using Y.Packet.Entities.Promotions.ViewModels;

namespace Y.Packet.Services.IPromotions
{
    public interface IPromotionsConfigService: IPromotionsConfigCacheService
    {
        Task<(IEnumerable<PromotionsConfig>, int)> GetPageListAsync(PromotionsListPageQuery q);
        /// <summary>
        /// 查看商户开启的指定活动
        /// </summary>
        /// <param name="activityType"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<(IEnumerable<PromotionsConfig> promotions, int count)> GetListAsync(ActivityType activityType, int merchantId);
        /// <summary>
        /// 获取商户所有开启的活动 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<IEnumerable<PromotionsConfig>> GetListAsync(int merchantId);

        /// <summary>
        /// 获取一条活动详细
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="promoId"></param>
        /// <returns></returns>
        Task<(bool sucess, string msg, PromotionsConfig entity)> GetAsync(int merchantId, int promoId);

        /// <summary>
        /// [H5版优惠活动列表]
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<(bool, string, dynamic)> GetH5ProsListAsync(int merchantId);
        /// <summary>
        /// [H5版优惠活动列表]获取首页显示的5条数据
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<(bool, string, dynamic)> GetH5ProsIndex4DatasAsync(int merchantId);
        Task<(bool, string, dynamic)> GetH5ProsListV2Async(int merchantId);


        /// <summary>
        /// 新增活动
        /// </summary>
        /// <param name="config"></param>
        /// <param name="validation">是否验证配置, 增加活动时候不验证</param>
        /// <returns></returns>
        Task<(bool, string)> InsertOrUpdateAsync(PromotionsConfig config, bool validation);


        (bool, string) ConfigValidation(PromotionsConfig config);


        Task<(bool, string)> UpdateConfigAsync(int merchantId, int promotionId, ActivityType activityType, string config);

        /// <summary>
        /// 根据商户Id获取商户配置的活动字典
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<Dictionary<int, string>> GetProDicByMerchantIdAsync(int merchantId);

        /// <summary>
        ///  [自动执行任务]获取指定活动开启的所有商户/活动信息
        /// </summary>
        /// <param name="activityType"></param>
        /// <returns></returns>
        Task<IEnumerable<PromotionsConfig>> GetTaskListAsync(ActivityType activityType);

        /// <summary>
        /// 更改该信息为删除状态
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="promoId"></param>
        /// <returns></returns>
        Task<(bool, string)> UpdateDeleteStatusAsync(int merchantId, int promoId);

        /// <summary>
        /// 获取首页开启的活动
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<Dictionary<int, string>> GetHomePageDisplayActivityAsync(int merchantId);

    }
}