////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //
//                         O\  =  /O                              //
//                      ____/`---'\____                           //
//                    .'  \\|     |//  `.                         //
//                   /  \\|||  :  |||//  \                        //
//                  /  _||||| -:- |||||-  \                       //
//                  |   | \\\  -  /// |   |                       //
//                  | \_|  ''\---/''  |   |                       //
//                  \  .-\__  `-`  ___/-. /                       //
//                ___`. .'  /--.--\  `. . ___                     //
//              ."" '<  `.___\_<|>_/___.'  >'"".                  //
//            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
//            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
//      ========`-.____`-.___\_____/___.-`____.-'========         //
//                           `=---='                              //
//      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
//                   佛祖保佑       永不宕机     永无BUG          //
////////////////////////////////////////////////////////////////////

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：Aaron                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-07-26 00:31:01                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Promotions                                  
*│　类    名： ActivityRecordsService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Packet.Entities.Promotions;
using Y.Packet.Entities.Promotions.ViewModels;
using Y.Packet.Repositories.IPromotions;
using Y.Packet.Services.IPromotions;
using static Y.Packet.Entities.Promotions.ActivityOrders;

namespace Y.Packet.Services.Promotions
{
    public class ActivityOrdersService : IActivityOrdersService
    {
        private readonly IActivityOrdersRepository _repository;
        private readonly IPromotionsConfigRepository _promotionsConfigRepository;

        public ActivityOrdersService(IActivityOrdersRepository repository, IPromotionsConfigRepository promotionsConfigRepository)
        {
            _repository = repository;
            _promotionsConfigRepository = promotionsConfigRepository;
        }

        public (bool, string) CheckInsertConditions(int siteId, int userId, ActivityType type, int promotionConfigId, string ip, decimal effectiveMoney, decimal chargeMoney)
        {

            // 1. 获取活动配置 站点是否开启活动 是否在有效期内

            // 2. 判断是否同开启同IP、重复领奖  

            // 3. 针对游戏特殊配置进行验证
            switch (type)
            {
                case ActivityType.None:
                    break;
                case ActivityType.Register:
                    break;
                case ActivityType.DailyCheckIn:
                    break;
                case ActivityType.WeeklyCheckIn:
                    break;
                case ActivityType.BankCard:
                    break;
                case ActivityType.Recharge:
                    break;
                case ActivityType.TurnTable:
                    break;
                case ActivityType.Invite:
                    break;
                default:
                    break;
            }

            throw new NotImplementedException();
        }

        public async Task<(IEnumerable<ActivityOrders>, int)> GetListAsync(ActivityOrdersListPageQuery q)
        {
            string conditions = "WHERE 1=1 ";

            if (q.MerchantId != -1 && q.MerchantId != 0)
                conditions += $" AND MerchantId={q.MerchantId} ";

            // 活动类别
            if (!string.IsNullOrEmpty(q.AType))
            {
                var atype = q.AType.ToEnum<ActivityType>();
                if (atype != null)
                    conditions += $" AND AType={(int)atype.Value}";
            }
            // 会员名称
            if (q.MemberId != 0)
                conditions += $" AND UserId={q.MemberId} ";

            // 订单状态
            if (!string.IsNullOrEmpty(q.Status))
            {
                var status = q.Status.ToEnum<ActivityOrderStatus>();
                if (status != null)
                    conditions += $" AND Status = {status.Value.GetEnumValue()}";
            }
            // 活动ID
            if (q.PromotionId != 0)
                conditions += $" AND PromotionId={q.PromotionId} ";

            var list = await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", null);

            return (list, _repository.RecordCount(conditions));

        }

        public async Task<(bool, string, int)> InsertAsync(ActivityOrders records)
        {
            if (records.MerchantId == 0 || records.UserId == 0 || records.PromotionId == 0) return (false, "参数不正确", 0);
            // 这里一定要判断是否存在重复发放的状况

            var rt = await _repository.InsertAsync(records);
            if (rt.HasValue && rt.Value > 0) return (true, "保存成功", rt.Value);
            return (false, "保存失败", 0);
        }

        public async Task<(bool, string, int)> InsertAsync(ActivityOrderAddModel model)
        {
            if (model.MerchantId == 0) return (false, "商户错误", 0);
            if (model.MemberId == 0) return (false, "会员错误", 0);
            var atype = model.AType.ToEnum<ActivityType>();
            if (atype == null) return (false, "活动参数错误", 0);
            DateTime dt = DateTime.UtcNow.AddHours(8);
            var activityOrders = new ActivityOrders()
            {
                MerchantId = model.MerchantId,
                UserId = model.MemberId,
                PromotionId = model.ProId,
                AType = atype.Value,
                Reward = model.Reward,
                Status = ActivityOrders.ActivityOrderStatus.Ok,
                Ip = "127.0.0.1",
                Description = model.Description,
                CreateTime = dt,
                RewardTime = dt,
                CreateDate = dt.Date.ToString("yyyy-MM-dd")
            };

            var rt = await this.InsertAsync(activityOrders);
            return rt;
        }

        /// <summary>
        /// 是否存在指定活动下面的订单
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <param name="activityType"></param>
        /// <returns></returns>
        public async Task<bool> ExistOrdersAsync(int merchantId, int userId, ActivityType activityType)
        {
            return await _repository.ExistOrdersAsync(merchantId, userId, activityType);
        }


        /// <summary>
        /// [生日礼金] 判断是否存在在过去360天内发放过，需要根据备注字段进行查询，生日礼金备注字段是固定的
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <param name="activityType"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public async Task<bool> ExistBirthOrdersAsync(int merchantId, int userId, DateTime dt, string description)
        {
            return await _repository.ExistBirthOrdersAsync(merchantId, userId, dt, description);
        }


        public async Task<bool> ExistSourceIdAsync(string sourceId)
        {
            return await _repository.ExistSourceIdAsync(sourceId);
        }



        #region 活动详细规则验证

        /// <summary>
        /// 注册活动验证
        /// </summary>
        /// <returns></returns>
        private (bool, string) RegisterActivityValidation()
        {

            return (true, null);
        }

        /// <summary>
        /// 每日签到
        /// </summary>
        /// <returns></returns>
        private (bool, string) DailyCheckInActivityValidation()
        {

            return (true, null);
        }

        /// <summary>
        /// 每周签到
        /// </summary>
        /// <returns></returns>
        private (bool, string) WeeklyCheckInActivityValidation()
        {

            return (true, null);
        }

        /// <summary>
        /// 绑定银行卡活动
        /// </summary>
        /// <returns></returns>
        private (bool, string) BankCarkActivityValidation()
        {

            return (true, null);
        }

        /// <summary>
        /// 充值活动
        /// </summary>
        /// <returns></returns>
        private (bool, string) RechargeActivityValidation()
        {

            return (true, null);
        }

        /// <summary>
        /// 转盘活动
        /// </summary>
        /// <returns></returns>
        private (bool, string) TurnTableActivityValidation()
        {

            return (true, null);
        }

        /// <summary>
        /// 邀请注册活动
        /// </summary>
        /// <returns></returns>
        private (bool, string) InviteActivityValidation()
        {

            return (true, null);
        }

        #endregion




        /// <summary>
        /// [活动列表->订单汇总]获取订单汇总
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="promoId"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<ActivityOrderSummary>, int)> GetOrderSummary(int merchantId, int promoId)
        {
            var info = await _promotionsConfigRepository.GetFromCacheAsync(merchantId, promoId);
            if (info == null) return (new List<ActivityOrderSummary>(), default(int));
            return await _repository.GetOrderSummaryAsync(merchantId, promoId);
        }

    }
}