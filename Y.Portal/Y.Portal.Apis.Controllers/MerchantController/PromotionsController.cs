using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.IApplication;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Games.ViewModels;
using Y.Packet.Entities.Pay.ViewModel;
using Y.Packet.Entities.Promotions;
using Y.Packet.Entities.Promotions.ViewModels;
using Y.Packet.Services.IGames;
using Y.Packet.Services.IMembers;
using Y.Packet.Services.IPay;
using Y.Packet.Services.IPromotions;
using Y.Packet.Services.IVips;
using Y.Portal.Apis.Controllers.DtoModel.Merchant;
using Y.Portal.Apis.Controllers.Helper;

namespace Y.Portal.Apis.Controllers.MerchantController
{
    [Route(RouteHelper.BaseMerchantRoute)]
    [ApiController]
    public class PromotionsController : ControllerBase
    {

        private readonly IPromotionsConfigService _promotionsConfigService;
        private readonly IActivityOrdersService _activityOrdersService;
        private readonly IActivityOrdersDetailsService _activityOrdersDetailsService;
        private readonly IGameUsersDailyReportStatisticService _gameUsersDailyReportStatisticService;
        private readonly IUsersFundsLogService usersFundsLogService;
        private readonly IUsersService _usersService;
        private readonly IPromotionsTagService _promotionsTagService;
        private readonly IBaseHandlerService _baseHandler;
        public PromotionsController(
            IPromotionsConfigService promotionsConfigService,
            IActivityOrdersService activityOrdersService,
            IActivityOrdersDetailsService activityOrdersDetailsService,
            IGameUsersDailyReportStatisticService gameUsersDailyReportStatisticService,
            IUsersService usersService,
            IPromotionsTagService promotionsTagService,
            IBaseHandlerService baseHandler)
        {
            _promotionsConfigService = promotionsConfigService;
            _activityOrdersService = activityOrdersService;
            _activityOrdersDetailsService = activityOrdersDetailsService;
            _gameUsersDailyReportStatisticService = gameUsersDailyReportStatisticService;
            _usersService = usersService;
            _promotionsTagService = promotionsTagService;
            _baseHandler = baseHandler;
        }

        #region 活动页面操作

        /// <summary>
        /// 优惠活动页面查询数据
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [HttpPost("load")]
        public async Task<string> List([FromForm] PromotionsListPageQuery q)
        {
            q.MerchantId = _baseHandler.MerchantId;

            var result = await _promotionsConfigService.GetPageListAsync(q);
            var tagDic = await _promotionsTagService.GetDicAsync(_baseHandler.MerchantId);
            return (new TableDataModel
            {
                count = result.Item2,
                data = result.Item1.Select(t =>
                {
                    var conf = new
                    {
                        BonusType = t.BaseConfig.BonusType.ToString(),
                        BonusCalType = t.BaseConfig.BonusCalType.ToString(),
                        IPCheckType = t.BaseConfig.IPCheckType.ToString(),
                        Wash = t.BaseConfig.Wash.ToString()
                    };
                    //var configs = 
                    return new
                    {
                        t.Id,
                        t.AType,
                        TypeDesc = t.AType.GetDescription(),
                        TypeStrToLower = t.AType.ToString().ToLower(),
                        TypeStr = t.AType.ToString(),
                        t.Title,
                        t.Config,
                        Conf = conf,
                        t.Enabled,
                        t.Visible,
                        t.StartTime,
                        t.EndTime,
                        t.Description,
                        t.PcCover,
                        t.Deleted,
                        TagId = t.TagId,
                        t.H5Cover,
                        t.IndexPageCover,
                        TagName = tagDic.Item2.GetByKey(t.TagId),
                        t.SortNo
                    };
                })
            }).ToJson();
        }

        /// <summary>
        /// 删除优惠活动
        /// </summary>
        /// <returns></returns>
        [HttpPost("deltepro")]
        public async Task<string> DeletePromotions([FromForm] int id)
        {
            return (await _promotionsConfigService.UpdateDeleteStatusAsync(_baseHandler.MerchantId, id)).ToJsonResult();
        }

        /// <summary>
        /// 保存活动基本的配置内容
        /// </summary>
        /// <returns></returns>
        [HttpPost("savepro")]
        public async Task<string> SavePromotions([FromForm] PromotionsDetailsPage q)
        {
            PromotionsConfig config = new PromotionsConfig();
            if (q.Id != 0)
            {
                var configRt = await _promotionsConfigService.GetAsync(_baseHandler.MerchantId, q.Id);
                if (!configRt.Item1) return (false, configRt.Item2).ToJsonResult();
                config = configRt.Item3;
            }

            config.Id = q.Id;
            config.TagId = q.CategoryId;
            config.AType = q.AType;
            config.Title = q.Title;
            //config.Config = q.Config;
            config.Enabled = q.Enabled;
            config.Visible = q.Visible;
            config.StartTime = q.StartTime;
            config.EndTime = q.EndTime;
            config.CreateTime = DateTime.Now;
            config.Description = q.Content;
            config.TagId = q.TagId;
            config.PcCover = q.Cover;
            config.H5Cover = q.H5Cover;
            config.IndexPageCover = q.IndexPageCover;
            config.SortNo = q.SortNo;
            // TODO 获取全局站点ID
            config.MerchantId = _baseHandler.MerchantId;

            var bConfig = q.Id == 0 ? config.GetConfigInstance : config.BaseConfig; // 初始化配置字符串实例
            // 奖金发放类型
            bConfig.BonusType = q.BonusType;
            if (q.BonusType != PromotionsConfig.BonusType.None)
                bConfig.EnabledBonusType = true;

            bConfig.BonusCalType = q.BonusCalType;
            if (q.BonusCalType != PromotionsConfig.BonusCalType.None)
            {
                bConfig.EnabledBonusCalType = true;
                bConfig.BonusCalTypeValue = q.BonusCalTypeValue;
            }

            bConfig.IPCheckType = q.IPCheckType;
            if (q.IPCheckType != PromotionsConfig.IPCheckType.None)
                bConfig.EnabledIpCcheck = true;

            bConfig.Wash = q.Wash;
            bConfig.WashValue = q.WashValue;

            config.Config = bConfig.ToJson();
            (bool, string) rt = await _promotionsConfigService.InsertOrUpdateAsync(config, false);



            return rt.ToJsonResult();

        }

        /// <summary>
        /// 保存活动设置 公共方法 所有活动均通过这个接口进行保存
        /// </summary>
        /// <param name="atype"></param>
        /// <param name="config"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("savepromoconfig")]
        public async Task<string> SavePromotionsConfig([FromForm] ActivityType atype, [FromForm] string config, [FromForm] int id)
        {
            return (await _promotionsConfigService.UpdateConfigAsync(_baseHandler.MerchantId, id, atype, config)).ToJsonResult();
        }

        #region 优惠标签


        /// <summary>
        /// 优惠活动的类别标签
        /// </summary>
        /// <returns></returns>
        [HttpPost("tagload")]
        public async Task<string> TagList([FromForm] PromotionsTagListPageQuery q)
        {
            q.MerchantId = _baseHandler.MerchantId;
            var result = await _promotionsTagService.GetPageListAsync(q);

            return (new TableDataModel()
            {
                count = result.Item2,
                data = result.Item1.Select(t => new
                {
                    t.Id,
                    t.Name,
                    t.Sort,
                    t.Image,
                    t.CreateTime
                })
            }).ToJson();
        }

        [HttpPost("savetag")]
        public async Task<string> SaveTag([FromBody] PromotionsTagAddOrModifyModel m)
        {
            m.MerchantId = _baseHandler.MerchantId;
            return (await _promotionsTagService.InsertOrModifyAsync(m)).ToJsonResult();
        }

        [HttpPost("deletetag")]
        public async Task<string> DeleteTag([FromForm] int id)
        {
            return (await _promotionsTagService.DeleteAsync(_baseHandler.MerchantId, id)).ToJsonResult();
        }

        [HttpPost("tagdic")]
        public async Task<string> GetTagDic()
        {
            var rt = await _promotionsTagService.GetDicAsync(_baseHandler.MerchantId);
            return (rt.Item1, rt.Item2.ToJson()).ToJsonResult();
        }

        #endregion

        //TODO 这个方法引起的 Swagger 异常。 暂时注销

        ///// <summary>
        ///// 获取订单日汇总
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost("ordersummary")]
        //public async Task<string> GetOrderSummary([FromForm] ActivityOrderSummaryListPageQuery q)
        //{
        //    var rt = await _activityOrdersService.GetOrderSummary(_baseHandler.MerchantId, q.Id);

        //    return (new TableDataModel
        //    {
        //        count = rt.Item2,
        //        data = rt.Item1.Select(
        //            t => new
        //            {
        //                UserName = t.UserId == 0 ? "集体用户" : _usersService.GetAccountNameByMemberId(t.UserId),
        //                t.Reward,
        //                t.CreateDate,
        //                t.RewardDate
        //            })
        //    }).ToJson();
        //}




        #endregion



        #region 活动订单页面操作


        [HttpPost("getorderlist")]
        public async Task<string> Orders([FromForm] ActivityOrdersListPageQuery q)
        {
            q.MerchantId = _baseHandler.MerchantId;
            var rt = await _activityOrdersService.GetListAsync(q);
            // 获取当前站点的活动字典
            var pro_name_dic = await _promotionsConfigService.GetProDicByMerchantIdAsync(q.MerchantId);

            List<object> data = new List<object>();
            foreach (var t in rt.Item1)
            {
                data.Add(new
                {
                    t.Id,
                    t.AType,
                    TypeDesc = t.AType.GetDescription(),
                    TypeStrToLower = t.AType.ToString().ToLower(),
                    TypeStr = t.AType.ToString(),
                    t.MerchantId,
                    t.UserId,
                    UserName = t.UserId == 0 ? "集体用户" : await _usersService.GetAccountNameByMemberIdAsync(t.UserId),
                    t.PromotionId,
                    PromotionName = pro_name_dic[t.PromotionId],
                    t.Reward,
                    t.Status,
                    StatusDesc = t.Status.GetDescription(),
                    t.Ip,
                    t.Description,
                    t.RewardTime,
                    t.CreateTime,
                    t.CreateDate
                });
            }

            return (new TableDataModel
            {
                count = rt.Item2,
                data = data
            }).ToJson();
        }


        [HttpPost("saveorder")]
        public async Task<string> SaveOrder([FromForm] ActivityOrderAddModel m)
        {
            m.MerchantId = _baseHandler.MerchantId;
            var rt = await _activityOrdersService.InsertAsync(m);
            if (!rt.Item1) return rt.ToJsonResult();
            // 写入用户账变

            return rt.ToJson();
        }


        /// <summary>
        /// 手动生成返水
        /// </summary>
        /// <returns></returns>
        [HttpPost("createrebate")]
        public async Task<string> CreateRebate([FromForm] DateTime date, [FromForm] int merchantId)
        {
            DateTime dt = DateTime.UtcNow.AddHours(8);
            string rebate_date = DateTime.UtcNow.AddHours(8).Date.ToString("yyyy-MM-dd");
            // 0. 不能生成当日返水
            if (date.Date == DateTime.UtcNow.AddHours(8).Date) return (false, "不能生成当日返水").ToJsonResult();

            // 1. 该日期返水是否存在
            //var exist = await _activityOrdersService.ExistRebateAsync(date, merchantId);
            //if (!exist.Item1) return exist.ToJsonResult();

            // 2. 查看返水开启的活动
            var proList = await _promotionsConfigService.GetListAsync(ActivityType.Rebate, merchantId);
            if (proList.Item2 == 0) return (false, "请开启/配置返水").ToJsonResult();

            // 3. 获取站点该日期内的所有有效投注 , 从用户游戏日报表独取
            var rt = await _gameUsersDailyReportStatisticService.GetByMerchantAsync(merchantId, date);
            if (rt.Item2 == 0) return (false, "没有需要生成的返水记录").ToJsonResult();

            // 4. 开始准备生成返水
            List<GameUsersDailyReportStatistic.RebateTempModel> gameRebate = new List<GameUsersDailyReportStatistic.RebateTempModel>();
            Dictionary<string, int> dic_config = null;
            foreach (var pro in proList.Item1)
            {
                var pro_config = (PromotionsConfig.RebateConfig)pro.BaseConfig;
                dic_config = JsonConvert.DeserializeObject<Dictionary<string, int>>(pro_config.GameConfig);

                GameType type;
                foreach (var d in dic_config)
                {
                    type = d.Key.ToEnum<GameType>().Value;
                    //var dic_r = rt.Item1.Where(t => t.Type == type).Select(t => new
                    //{
                    //    t.UserId,
                    //    t.Type,
                    //    Reward = t.ValidBet * (d.Value / 100)
                    //}).ToDictionary(t => t.Type, t => t.Reward);
                    var dic_rr = rt.Item1.Where(t => t.GameTypeStr == type.ToString()).Select(t => new GameUsersDailyReportStatistic.RebateTempModel
                    {
                        UserId = t.MemberId,
                        Type = t.GameTypeStr.ToEnum<GameType>().Value,
                        Reward = t.ValidBet * (d.Value / 100),
                        MerchantId = merchantId,
                        PromotionId = pro.Id,
                        SourceId = $"{t.MerchantId}:{t.MemberId}:{t.GameTypeStr.ToString()}:{rebate_date}"
                    });
                    gameRebate.AddRange(dic_rr);
                }
            }
            // 根据返水点位生成对应的用户返水金额，写入优惠活动表，优惠活动详细表,用户账变记录表,用户增加余额

            // 5. 判断是否存在已经生成的返水日记录

            // 6. 写入返水记录
            var activityOrders = new ActivityOrders()
            {
                MerchantId = merchantId,
                UserId = 0,
                PromotionId = 0,
                AType = ActivityType.Rebate,
                Reward = 0,
                Status = ActivityOrders.ActivityOrderStatus.None,
                Ip = "127.0.0.1",
                Description = $"{rebate_date} 返水发放",
                CreateTime = dt,
                RewardTime = DefaultString.DefaultDateTime,
                CreateDate = rebate_date
            };

            var rt_order = await _activityOrdersService.InsertAsync(activityOrders);
            if (!rt_order.Item1) return (rt_order.Item1, rt_order.Item2).ToJsonResult();
            int orderId = rt_order.Item3;

            List<string> error_log = new List<string>();
            foreach (var gr in gameRebate)
            {
                var result = await _activityOrdersDetailsService.InsertAsync(orderId, gr.MerchantId, gr.UserId, gr.PromotionId, ActivityType.Rebate, gr.Reward, gr.SourceId, rebate_date, dt, ActivityOrders.ActivityOrderStatus.None);
                if (!result.Item1) error_log.Add(result.Item2);
            }

            // 7. 写入用户记录 (暂未实现，用户手动审核返水发放)

            // 8. 写入日志表


            return (true, "返水日志已生成").ToJsonResult();
        }


        /// <summary>
        /// 返水详细列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [HttpPost("getrebateorderlist")]
        public async Task<string> RebateOrderist([FromForm] ActivityOrdersListPageQuery q)
        {
            q.MerchantId = _baseHandler.MerchantId;

            {
                var rt = await _activityOrdersDetailsService.GetListAsync(q);


                return (new TableDataModel
                {
                    count = rt.Item2,
                    data = rt.Item1.Select(t => new
                    {
                        t.Id,
                        t.AType,
                        TypeDesc = t.AType.GetDescription(),
                        TypeStrToLower = t.AType.ToString().ToLower(),
                        TypeStr = t.AType.ToString(),
                        t.MerchantId,
                        t.MemberId,
                        UserName = t.MemberId == 0 ? "集体用户" : "",
                        PromotionName = "",
                        t.Reward,
                        t.Status,
                        StatusDesc = t.Status.GetDescription(),
                        t.Description,
                        t.RewardTime,
                        t.CreateTime,
                        t.CreateDate
                    })
                }).ToJson();

            }


        }

        #endregion




        #region 代理返点活动

        /// <summary>
        /// 获取代理相关活动
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        [HttpPost("getagentpromodic")]
        public async Task<string> GetAgentRebates([FromForm] string typeStr)
        {
            ActivityType type = typeStr.ToEnum<ActivityType>().Value;
            var rt = await _promotionsConfigService.GetListAsync(type, _baseHandler.MerchantId);

            if (rt.Item2 == 0) return (false, $"请添加{type.GetDescription()}").ToJsonResult();
            var v = rt.Item1.ToDictionary(t => t.Id, t => t.Title);
            return (true, v.ToJson()).ToJsonResult();
        }


        #endregion




    }
}