using Dapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.Cache.Redis.PromotionsService;
using Y.Packet.Entities.Promotions;
using Y.Packet.Entities.Promotions.ViewModels;
using Y.Packet.Repositories.IPromotions;
using Y.Packet.Services.IPromotions;
using static Org.BouncyCastle.Math.EC.ECCurve;
using static Y.Packet.Entities.Promotions.PromotionsConfig;

namespace Y.Packet.Services.Promotions
{
    public class PromotionsConfigService : PromotionsConfigCacheService, IPromotionsConfigService
    {
        private readonly IPromotionsConfigRepository _repository;
        private readonly IPromotionsTagRepository _promotionsTagRepository;

        public PromotionsConfigService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory, IPromotionsConfigRepository repository, IPromotionsTagRepository promotionsTagRepository) : base(options, factory)
        {
            _repository = repository;
            _promotionsTagRepository = promotionsTagRepository;
        }

        #region 获取活动

        public async Task<(IEnumerable<PromotionsConfig>, int)> GetPageListAsync(PromotionsListPageQuery q)
        {
            var parms = new DynamicParameters();
            string conditions = $"WHERE 1=1 ";
            if (q.MerchantId != -1)  // -1 就是搜索全部站点的
                conditions += $" AND MerchantId={q.MerchantId} ";
            if (!string.IsNullOrEmpty(q.AName))
            {
                conditions += " AND Title like @Title";
                parms.Add("Title", $"%{q.AName}%");
            }
            if (!string.IsNullOrEmpty(q.AType))
            {
                var atype = q.AType.ToEnum<ActivityType>();
                if (atype != null)
                    conditions += $" AND AType = {(int)atype.Value}";
            }
            if (q.ShowDeletedPro)
                conditions += " AND Deleted != 1  ";
            else
                conditions += " AND Deleted = 1  ";

            return (await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", parms), await _repository.RecordCountAsync(conditions, parms));
        }

        public async Task<(IEnumerable<PromotionsConfig> promotions, int count)> GetListAsync(ActivityType activityType, int merchantId)
        {
            //string conditions = $"WHERE MerchantId={merchantId} AND Enabled = 1 AND AType={activityType.GetEnumValue()}";
            //var rebateProList = await _repository.GetListAsync(conditions);
            var rebateProList = await _repository.GetListAsync(merchantId, activityType);
            return (rebateProList, rebateProList.Count());
        }

        /// <summary>
        /// 获取商户的所有开启活动
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PromotionsConfig>> GetListAsync(int merchantId)
        {
            if (merchantId == 0) return new List<PromotionsConfig>();
            //string conditions = $"WHERE Enabled=1 AND MerchantId={merchantId} AND Visible=1";
            //return await _repository.GetListAsync(conditions);
            return await _repository.GetListAsync(merchantId, visible: true);
        }

        /// <summary>
        /// [自动执行任务]获取指定活动开启的所有商户/活动信息
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PromotionsConfig>> GetTaskListAsync(ActivityType activityType)
        {
            //string conditions = $" WHERE Enabled=1 AND AType={activityType.GetEnumValue()}";
            //return await _repository.GetListAsync(conditions);
            return await _repository.GetListAsync(null, activityType, enabled: true);
        }


        public async Task<(bool sucess, string msg, PromotionsConfig entity)> GetAsync(int merchantId, int promoId)
        {
            if (merchantId == 0 || promoId == 0) return (false, "活动不存在", null);
            if (await base.KeyExistsAsync($"GetAsync{merchantId}_{promoId}"))
            {
                var str = await base.StringGetAsync($"GetAsync{merchantId}_{promoId}");
                return (true, null, JsonHelper.JSONToObject<PromotionsConfig>(str));
            }
            //string conditions = $"SELECT * FROM PromotionsConfig WHERE MerchantId={merchantId} AND ID={promoId}";
            //var rt = await _repository.GetAsync(conditions);
            var rt = await _repository.GetFromCacheAsync(merchantId, promoId);
            if (rt == null) return (false, "活动不存在", null);
            if (!rt.Enabled) return (false, "活动已经停止", null);

            var cacheValue = JsonHelper.ToJson(rt);
            await base.StringSetAsync($"GetAsync{merchantId}_{promoId}", cacheValue, TimeSpan.FromHours(2));
            return (true, "", rt);
        }

        //public async Task<(bool, string, PromotionsConfig)> GetAsync(int merchantId, int promoId)
        //{
        //    if (merchantId == 0 || promoId == 0) return (false, "活动不存在", null);
        //    string conditions = $"SELECT * FROM PromotionsConfig WHERE MerchantId={merchantId} AND ID={promoId}";
        //    var rt = await _repository.GetAsync(conditions);
        //    if (rt == null) return (false, "活动不存在", null);
        //    if (!rt.Enabled) return (false, "活动已经停止", null);
        //    return (true, "", rt);
        //}


        /// <summary>
        /// [H5版本优惠活动] 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<(bool, string, dynamic)> GetH5ProsListAsync(int merchantId)
        {
            if (merchantId == 0) return (false, "商户不存在", null);
            if (await base.KeyExistsAsync($"GetH5ProsListAsync{merchantId}"))
            {
                var str = await base.StringGetAsync($"GetH5ProsListAsync{merchantId}");
                return (true, null, JsonHelper.JSONToObject<List<PromoH5VM>>(str));
            }
            var dt = DateTime.UtcNow.AddHours(8);
            //string conditions = $" WHERE Enabled=1 AND Visible=1 AND MerchantId={merchantId} AND StartTime <=@StartTime AND EndTime>=@EndTime"; // 获取开启的与显示的活动
            //var listPros = await _repository.GetListAsync(conditions, new { StartTime = dt, EndTime = dt });
            var listPros = await _repository.GetListAsync(merchantId, null, true, true);
            var listTags = await _promotionsTagRepository.GetListAsync($"WHERE MerchantId={merchantId}");
            List<PromoH5VM> list = new List<PromoH5VM>();
            foreach (var tag in listTags)
            {
                PromoH5VM t = new PromoH5VM()
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    Dtls = listPros.Where(t => t.TagId == tag.Id).Select(t => new PromoH5DetailVM()
                    {
                        Id = t.Id,
                        CatId = tag.Id,
                        H5Img = t.H5Cover
                    }).ToList()
                };
                list.Add(t);
            }

            var cacheValue = JsonHelper.ToJson(list);
            await base.StringSetAsync($"GetH5ProsListAsync{merchantId}", cacheValue, TimeSpan.FromHours(2));

            return (true, null, list);
        }

        /// <summary>
        /// [H5版本优惠活动] 获取排序前4条的数据,首页显示使用
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<(bool, string, dynamic)> GetH5ProsIndex4DatasAsync(int merchantId)
        {
            if (merchantId == 0) return (false, "商户不存在", null);
            if (await base.KeyExistsAsync($"GetH5ProsIndex4DatasAsync{merchantId}"))
            {
                var str = await base.StringGetAsync($"GetH5ProsIndex4DatasAsync{merchantId}");
                return (true, null, JsonHelper.JSONToObject<PromoH5IndexViewModel>(str));
            }
            var dt = DateTime.UtcNow.AddHours(8);
            //string conditions = $" WHERE Enabled=1 AND Visible=1 AND MerchantId={merchantId} AND StartTime <=@StartTime AND EndTime>=@EndTime ORDER BY SortNo DESC"; // 获取开启的与显示的活动
            //var listPros = await _repository.GetListAsync(conditions, new { StartTime = dt, EndTime = dt });

            var listPros = (await _repository.GetListAsync(merchantId, null, true, true)).OrderByDescending(t => t.SortNo);

            var list = listPros.Take(4).Select(t =>
            new PromoH5IndexViewModel()
            {
                Id = t.Id,
                Img = t.H5Cover,
                Name = t.Title,
                Desc = ""
            });

            var cacheValue = JsonHelper.ToJson(list);
            await base.StringSetAsync($"GetH5ProsIndex4DatasAsync{merchantId}", cacheValue, TimeSpan.FromHours(2));

            return (true, null, list);
        }


        public async Task<(bool, string, dynamic)> GetH5ProsListV2Async(int merchantId)
        {
            if (merchantId == 0) return (false, "商户不存在", null);
            if (await base.KeyExistsAsync($"GetH5ProsListV2Async{merchantId}"))
            {
                var str = await base.StringGetAsync($"GetH5ProsListV2Async{merchantId}");
                return (true, null, JsonHelper.JSONToObject<List<PromoH5V1ViewModel>>(str));
            }

            var dt = DateTime.UtcNow.AddHours(8);
            //string conditions = $" WHERE Enabled=1 AND Visible=1 AND MerchantId={merchantId} AND StartTime <=@StartTime AND EndTime>=@EndTime ORDER BY SortNo DESC"; // 获取开启的与显示的活动
            //var listPros = await _repository.GetListAsync(conditions, new { StartTime = dt, EndTime = dt });
            var listPros = (await _repository.GetListAsync(merchantId, null, true, true)).OrderByDescending(t => t.SortNo);
            var listTags = await _promotionsTagRepository.GetListAsync($"WHERE MerchantId={merchantId}");
            List<PromoH5V1ViewModel> list = new List<PromoH5V1ViewModel>();
            foreach (var tag in listTags)
            {
                PromoH5V1ViewModel t = new PromoH5V1ViewModel()
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    Dtls = listPros.Where(t => t.TagId == tag.Id).Select(t => new PromoH5IndexViewModel()
                    {
                        Id = t.Id,
                        Img = t.H5Cover,
                        Name = t.Title,
                        Desc = t.Title
                    }).ToList()
                };
                list.Add(t);
            }
            var cacheValue = JsonHelper.ToJson(list);
            await base.StringSetAsync($"GetH5ProsListV2Async{merchantId}", cacheValue, TimeSpan.FromHours(2));
            return (true, null, list);
        }

        private async Task RemoveCacheAsync(int merchantId)
        {
            if (await base.KeyExistsAsync($"GetHomePageDisplayActivityAsync{merchantId}"))
                await base.KeyDeleteAsync($"GetHomePageDisplayActivityAsync{merchantId}");
            if (await base.KeyExistsAsync($"GetH5ProsListV2Async{merchantId}"))
                await base.KeyDeleteAsync($"GetH5ProsListV2Async{merchantId}");
            if (await base.KeyExistsAsync($"GetH5ProsListAsync{merchantId}"))
                await base.KeyDeleteAsync($"GetH5ProsListAsync{merchantId}");
        }

        #endregion



        #region 活动增删改

        public async Task<(bool, string)> InsertOrUpdateAsync(PromotionsConfig config, bool validation)
        {
            if (config == null) return (false, "空配置, 保存失败");
            if (string.IsNullOrEmpty(config.Title)) return (false, "请输入活动名称");
            if (config.Description.IsNullOrEmpty()) return (false, "请输入活动内容");
            if (config.PcCover.IsNullOrEmpty()) config.PcCover = "";
            if (config.H5Cover.IsNullOrEmpty()) config.H5Cover = "";
            if (config.IndexPageCover.IsNullOrEmpty()) config.IndexPageCover = "";

            if (validation)
            {
                var validate = ConfigValidation(config);
                if (!validate.Item1) return validate;
            }
            int? rt = null;
            if (config.Id == 0)
                rt = await _repository.InsertWithCacheAsync(config);
            else
            {
                if (config.Deleted) return (false, "已删除活动禁止编辑");
                rt = await _repository.UpdateWithCacheAsync(config);
            }

            if (rt == null || rt.Value < 1) return (false, "保存失败");

            await RemoveCacheAsync(config.MerchantId);

            return (true, "保存成功");
        }



        public (bool, string) ConfigValidation(PromotionsConfig config)
        {

            // 是否开启奖金发放类型
            if (config.BaseConfig.EnabledBonusType)
            {

            }
            // 是否开启奖金计算方式
            if (config.BaseConfig.EnabledBonusCalType)
                if (config.BaseConfig.BonusCalTypeValue <= 0) return (false, "奖金不可以小于等于零");
                else if (config.BaseConfig.BonusCalType == PromotionsConfig.BonusCalType.CashRate && config.BaseConfig.BonusCalTypeValue > 100)
                    return (false, "奖金百分比不可以大于100");

            //  是否开启IP检查
            if (config.BaseConfig.EnabledIpCcheck)
            {

            }

            if (config.BaseConfig.Wash != PromotionsConfig.WashType.None)
                if (config.BaseConfig.WashValue <= 0) return (false, $"{config.BaseConfig.Wash.GetDescription()}金额不小于零");


            switch (config.AType)
            {
                case ActivityType.None:
                    break;
                case ActivityType.Register:
                    RegisterActivityValidation();
                    break;
                case ActivityType.DailyCheckIn:
                    DailyCheckInActivityValidation();
                    break;
                case ActivityType.WeeklyCheckIn:
                    WeeklyCheckInActivityValidation((PromotionsConfig.WeeklyCheckInConfig)config.BaseConfig);
                    break;
                case ActivityType.BankCard:
                    BankCarkActivityValidation();
                    break;
                case ActivityType.Recharge:
                    RechargeActivityValidation((PromotionsConfig.RechargeConfig)config.BaseConfig);
                    break;
                case ActivityType.TurnTable:
                    TurnTableActivityValidation();
                    break;
                case ActivityType.Invite:
                    InviteActivityValidation();
                    break;
                case ActivityType.Rebate:
                    RebateValidation((PromotionsConfig.RebateConfig)config.BaseConfig);
                    break;
                case ActivityType.AgentRebate: // 代理返点
                    break;
                default:
                    break;
            }
            return (true, null);
        }

        /// <summary>
        /// 检查反水规则配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private (bool, string) RebateValidation(PromotionsConfig.RebateConfig config)
        {
            if (config == null) return (false, "未配置返水规则");
            try
            {
                var dic = JsonConvert.DeserializeObject<Dictionary<string, int>>(config.GameConfig);
                GameType? type = null;
                foreach (var v in dic)
                {
                    type = v.Key.ToEnum<GameType>();
                    if (type == null) return (false, $"请检查游戏是否存在{v.Key}");
                    if (v.Value < 0) return (false, $"游戏返水值应为正数:{type.Value.GetDescription()}");
                    type = null;
                }
                return (true, null);
            }
            catch
            {
                return (false, "字符串格式不正确");
            }

        }

        public async Task<(bool, string)> UpdateConfigAsync(int merchantId, int promotionId, ActivityType activityType, string configStr)
        {
            await RemoveCacheAsync(merchantId);
            if (promotionId == 0) return (false, "优惠活动不存在");
            var pro = await _repository.GetFromCacheAsync(merchantId, promotionId);
            //PromotionsConfig.BConfig config;
            switch (activityType)
            {
                case ActivityType.None:
                    break;
                case ActivityType.Commission:
                    PromotionsConfig.CommissionConfig conf_commission = (PromotionsConfig.CommissionConfig)pro.BaseConfig;
                    JObject jCommission = JObject.Parse(configStr);
                    conf_commission.GameConfig = jCommission["GameConfig"].ToString();
                    conf_commission.RebateType = jCommission["RebateType"].ToString().ToEnum<RebateType>().Value;
                    pro.Config = conf_commission.ToJson();
                    break;
                case ActivityType.Contract:
                    PromotionsConfig.ContractConfig conf_contract = (PromotionsConfig.ContractConfig)pro.BaseConfig;
                    JObject j_contractConfig = JObject.Parse(configStr);
                    conf_contract.RuleConfig = j_contractConfig["RuleConfig"].ToString();
                    pro.Config = conf_contract.ToJson();
                    break;
                case ActivityType.Rebate:
                    PromotionsConfig.RebateConfig config_pro = (PromotionsConfig.RebateConfig)pro.BaseConfig;
                    JObject j = JObject.Parse(configStr);
                    config_pro.EnabledConfigStr = j["EnabledConfigStr"].ToString();
                    config_pro.GameConfig = j["GameConfig"].ToString();
                    config_pro.GroupConfig = j["GroupConfig"].ToString();
                    config_pro.RuleConfig = j["RuleConfig"].ToString();
                    pro.Config = config_pro.ToJson();
                    break;
                case ActivityType.Register:
                    break;
                case ActivityType.DailyCheckIn:
                    break;
                case ActivityType.WeeklyCheckIn:
                    PromotionsConfig.WeeklyCheckInConfig config_weeklyCheckin = (PromotionsConfig.WeeklyCheckInConfig)pro.BaseConfig;
                    JObject j_weeklyCheckInConfig = JObject.Parse(configStr);
                    config_weeklyCheckin.RuleConfig = j_weeklyCheckInConfig["RuleConfig"].ToString();
                    //config_weeklyCheckin.Wash = 
                    pro.Config = config_weeklyCheckin.ToJson();
                    break;
                case ActivityType.BankCard:
                    break;
                case ActivityType.Recharge:
                    PromotionsConfig.RechargeConfig confi_recharge = (PromotionsConfig.RechargeConfig)pro.BaseConfig;
                    JObject j_recharge = JObject.Parse(configStr);
                    confi_recharge.GroupConfig = j_recharge["GroupConfig"].ToString();
                    confi_recharge.RechargeRule = j_recharge["RechargeRule"].ToString();
                    pro.Config = confi_recharge.ToJson();
                    break;
                case ActivityType.TurnTable:
                    break;
                case ActivityType.Invite:
                    break;
                case ActivityType.AgentRebate:
                    PromotionsConfig.AgentRebateConfig config_agent_rebate_pro = (PromotionsConfig.AgentRebateConfig)pro.BaseConfig;
                    config_agent_rebate_pro.GameConfig = configStr;
                    pro.Config = config_agent_rebate_pro.ToJson();
                    break;
                default:
                    break;
            }

            var rt = ConfigValidation(pro);

            if (!rt.Item1) return rt;

            //var r = await _repository.UpdateConfigAsync(promotionId, pro.Config);
            var r = await _repository.UpdateWithCacheAsync(pro);

            if (r > 0) return (true, "保存成功");
            return (false, "保存失败");
        }

        public async Task<Dictionary<int, string>> GetProDicByMerchantIdAsync(int merchantId)
        {
            if (merchantId == 0) return new Dictionary<int, string>();
            //string conditions = $" WHERE MerchantId={merchantId} ";
            var listPros = await _repository.GetListAsync(merchantId);
            return listPros.ToDictionary(t => t.Id, t => t.Title);
        }


        #region 保存活动规则验证

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
        private (bool, string) WeeklyCheckInActivityValidation(PromotionsConfig.WeeklyCheckInConfig config)
        {

            if (config.RuleConfig == null) return (false, "请配置每周签到的规则");
            List<PromotionsConfig.RuleConfigModel> ls = JsonHelper.JSONToObject<List<PromotionsConfig.RuleConfigModel>>(config.RuleConfig);
            foreach (var l in ls)
            {
                if (l.DayCount <= 0 || l.DayCount > 7) return (false, "签到日期配置错误");
                if (l.RechargeAmount < 0 || l.Reward < 0 || l.WashAmount < 0) return (false, "金额只能填写正数");
            }

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
        private (bool, string) RechargeActivityValidation(PromotionsConfig.RechargeConfig config)
        {
            if (config.BonusCalTypeValue <= 0) return (false, "奖金不能小于等于零");
            if (config.BonusCalType == PromotionsConfig.BonusCalType.CashRate)
                if (config.BonusCalTypeValue > 100) return (false, "奖金百分比不能大于100");

            if (config.WashValue <= 0) return (false, "奖金流水值不能小于等于零");

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
        /// 设置活动为删除，伪删除
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="promoId"></param>
        /// <returns></returns>
        public async Task<(bool, string)> UpdateDeleteStatusAsync(int merchantId, int promoId)
        {
            await RemoveCacheAsync(merchantId);
            if (merchantId == 0 || promoId == 0) return (false, "该站点活动不存在");
            var promo = await _repository.GetFromCacheAsync(merchantId, promoId);
            if (promo == null) return (false, "该活动不存在");
            promo.Deleted = true;
            promo.Enabled = false;
            promo.Visible = false;
            var rt = await _repository.UpdateWithCacheAsync(promo);
            if (rt > 0) return (true, "删除状态成功");
            return (false, "删除失败");
        }


        #endregion


        public async Task<Dictionary<int, string>> GetHomePageDisplayActivityAsync(int merchantId)
        {
            if (await base.KeyExistsAsync($"GetHomePageDisplayActivityAsync{merchantId}"))
            {
                var str = await base.StringGetAsync($"GetHomePageDisplayActivityAsync{merchantId}");
                return (JsonHelper.JSONToObject<Dictionary<int, string>>(str));
            }
            var result = await _repository.GetHomeDisplayAsync(merchantId);
            var cacheValue = JsonHelper.ToJson(result);
            await base.StringSetAsync($"GetHomePageDisplayActivityAsync{merchantId}", cacheValue, TimeSpan.FromHours(2));
            return result;
        }


    }
}