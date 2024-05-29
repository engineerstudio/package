using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.Mapper;
using Y.Packet.Entities.Vips;
using Y.Packet.Entities.Vips.ViewModels;
using Y.Packet.Repositories.IVips;
using Y.Packet.Services.IVips;
using Dapper;
using Y.Infrastructure.Cache.Redis.VipsService;
using Microsoft.Extensions.Options;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Extensions;

namespace Y.Packet.Services.Vips
{
    public class VipGroupsService : VipGroupsCacheService, IVipGroupsService
    {
        private readonly IVipGroupsRepository _repository;

        public VipGroupsService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory, IVipGroupsRepository repository) : base(options, factory)
        {
            _repository = repository;
        }


        public async Task<(bool, string)> AddVipSettingsAsync(VipGroups vipGroups, GroupSetting groupSetting, WithdrawalSetting withdrawalSetting, PaySetting paySetting, PointSetting pointSetting)
        {

            // 用户分组金额验证
            if (groupSetting.AccumulatedRechargeAmount <= 0) return (false, "VIP累计充值金额应不为负数");
            //if (groupSetting.StayGroupRechargeAmount <= 0) return (false, "VIP保级金额应不为负数");

            // 提现验证
            if (withdrawalSetting.FreeWithdrawCount <= 0) return (false, "每日免费提现次数应不为负数");
            if (withdrawalSetting.WithdrawCount <= 0) return (false, "每日提现次数应不为负数");
            if (withdrawalSetting.WithdrawCount < withdrawalSetting.FreeWithdrawCount) return (false, "每日提现次数应大于每日免费提现次数");
            if (withdrawalSetting.MinWithdrawAmount <= 0) return (false, "最低提现手续费应不为负数");

            // 支付渠道验证


            // 积分验证
            if (pointSetting.Point <= 0) return (false, "积分应不为负数");

            // VIP分组基本信息验证
            if (string.IsNullOrEmpty(vipGroups.GroupName)) return (false, "请输入VIP组名称");
            if (string.IsNullOrEmpty(vipGroups.Description)) vipGroups.Description = "";
            vipGroups.CreateTime = DateTime.Now;


            var rt = await _repository.InsertWithCacheAsync(vipGroups);
            bool sucess = rt != null && rt.HasValue && rt.Value > 0;
            return (sucess, sucess ? "保存成功" : "保存失败");

        }

        /// <summary>
        /// 获取VIP分组列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<VipGroups>, int)> GetPageListAsync(VipListQuery q)
        {
            var parms = new DynamicParameters();
            string conditions = "WHERE 1=1 ";
            if (q.MerchantId != 0)
                conditions += $" AND MerchantId={q.MerchantId}";
            if (!string.IsNullOrEmpty(q.GroupName))
            {
                conditions += $" AND GroupName like @GroupName ";
                parms.Add("GroupName", $"%{q.GroupName}%");
            }

            return (await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", parms), await _repository.RecordCountAsync(conditions, parms));
        }

        /// <summary>
        /// 获取所有VIP列表
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VipGroups>> GetListAsync(int merchantId)
        {
            if (merchantId == 0) return null;
            var list = await _repository.GetListFromCacheAsync(merchantId);
            return list;
        }

        /// <summary>
        /// 站点初始化：创建站点默认分组
        /// </summary>
        /// <returns></returns>
        public async Task<(bool, string, int)> CreateMerchantAdminDefaultVIPGroupsAsync(int merchantId)
        {
            var m = new VipGroupModel()
            {
                MerchantId = merchantId,
                GroupName = "VIP0",
                Enabled = true,
                IsDefault = true,
                Description = "默认分组",
                SortNo = 0,
                Img = ""
            };
            await InsertOrUpdateAsync(m);
            var default_rt = await GetDefaultGroupIdAsync(merchantId);
            return default_rt;
        }


        public async Task<(bool, string)> InsertOrUpdateAsync(VipGroupModel rmodel)
        {
            if (rmodel.MerchantId == 0) return (false, "商户不存在");
            if (string.IsNullOrEmpty(rmodel.GroupName)) return (false, "请输入分组名称");

            // 1. 是否用户名相同
            if (rmodel.Id == 0)
            {
                int existGroupName = await _repository.ExistGroupNameAsync(rmodel.MerchantId, rmodel.GroupName);
                if (existGroupName > 0) return (false, "分组名称已存在");
            }
            // 2. 是否已经存在默认分组
            var existDefault = await _repository.ExistDefaultGroupAsync(rmodel.MerchantId);
            if (existDefault.Item1 && existDefault.Item2.Id != rmodel.Id && rmodel.IsDefault) return (false, $"已经存在默认分组{existDefault.Item2.ToJson()}");

            if (rmodel.Id == 0)
            {
                VipGroups m = ModelToEntity.Mapping<VipGroups, VipGroupModel>(rmodel);
                m.PaySetting = "";
                m.WithdrawalSetting = "";
                m.GroupSetting = "";
                m.PointSetting = "";
                m.CreateTime = DateTime.Now;
                m.SortNo = 0;
                if (rmodel.Img.IsNullOrEmpty()) rmodel.Img = "";
                m.Img = rmodel.Img;
                var rt = await _repository.InsertWithCacheAsync(m);
                if (rt != null && rt.Value > 0) return (true, "保存成功");
            }
            else
            {
                var m = await _repository.GetFromCacheAsync(rmodel.Id);
                if (m.MerchantId != rmodel.MerchantId) return (false, "商户不存在");
                m.GroupName = rmodel.GroupName;
                m.Enabled = rmodel.Enabled;
                m.IsDefault = rmodel.IsDefault;
                m.Description = rmodel.Description;
                m.SortNo = rmodel.SortNo;
                if (rmodel.Img.IsNullOrEmpty()) rmodel.Img = "";
                m.Img = rmodel.Img;
                var rt = await _repository.UpdateWithCacheAsync(m);
                if (rt > 0) return (true, "保存成功");
            }

            return (false, "保存失败");
        }

        /// <summary>
        /// 设定分组规则
        /// </summary>
        /// <param name="gs"></param>
        /// <returns></returns>
        public async Task<(bool, string)> SetGroupRuleAsync(GroupSetting gs, int marchantId, int groupId)
        {
            // 传入参数验证
            if (marchantId == 0 || groupId == 0) return (false, "参数不正确");
            var group = await _repository.GetFromCacheAsync(groupId);
            if (group == null || group.MerchantId != marchantId) return (false, "分组不存在");

            // 用户分组金额验证
            if (gs.AccumulatedRechargeAmount < 0) return (false, "分组规则累计充值金额应不为负数");
            if (gs.AccumulatedEffectiveAmount < 0) return (false, "分组规则有效打码量金额应不为负数");
            if (gs.StayGroupEffectiveAmount < 0) return (false, "分组规则保级打码量金额应不为负数");
            if (gs.WithdrawalsCount < 0) return (false, "分组规则提款次数额应不为负数");
            if (gs.WithdrawalDailyTotalAmount < 0) return (false, "分组规则日提款额度应不为负数");
            if (gs.ProAmount < 0) return (false, "分组规则晋级礼包应不为负数");
            if (gs.BirthAmount < 0) return (false, "分组规则生日礼包应不为负数");
            if (gs.MonthSalary < 0) return (false, "分组规则每月俸禄应不为负数");
            if (gs.MaxRebate < 0) return (false, "分组规则最高返水应不为负数");


            // 更新分组规则
            group.GroupSetting = gs.ToJson();

            var rt = await _repository.UpdateWithCacheAsync(group);

            if (rt > 0) return (true, "保存成功");
            return (false, "保存失败");
        }


        /// <summary>
        /// 设置分组的支付商户
        /// </summary>
        /// <param name="merchatnId"></param>
        /// <param name="id"></param>
        /// <param name="payMerchantIds"></param>
        /// <returns></returns>
        public async Task<(bool, string)> SetPayRuleAsync(int merchatnId, int id, string payMerchantIds)
        {
            if (merchatnId == 0 || id == 0) return (false, "参数不正确");
            if (!string.IsNullOrEmpty(payMerchantIds))
            {
                int tp = -1;
                foreach (var d in payMerchantIds.Split(","))
                {
                    bool dd = Int32.TryParse(d, out tp);
                    if (!dd) return (false, "商户参数不正确");
                }
            }

            var vip = await _repository.GetFromCacheAsync(id);
            if (vip == null) return (false, "分组不存在");
            vip.PaySetting = payMerchantIds;

            var rt = await _repository.UpdateWithCacheAsync(vip);
            if (rt == 0) return (false, "保存失败");
            return (true, "保存成功");
        }


        public async Task<(bool, Dictionary<int, string>)> GetGroupIdAndNameDicAsync(int merchantId)
        {
            //var dic = (await _repository.GetListAsync($"WHERE MerchantId={merchantId} AND Enabled = 1 ")).ToDictionary(t => t.Id, t => t.GroupName);
            if (merchantId == 0) return (false, new Dictionary<int, string>());
            var dic = await _repository.GetIdAndNameDicAsync(merchantId);
            if (dic != null && dic.Count != 0)
                return (true, dic);
            return (false, dic);
        }


        public async Task<(bool, int)> ProcessVipRulesAsync(int merchatnId, int memberGroupId, decimal chargeAmount, decimal betAmount)
        {

            // 1. 获取所有的启用的分组
            //string conditions = $" WHERE MerchantId={merchatnId} AND Enabled = 1 ORDER BY SortNo ASC ";
            //var list = await _repository.GetListAsync(conditions);
            var list = await _repository.GetListFromCacheAsync(merchatnId);
            list = list.Where(t => t.Enabled).OrderBy(t => t.SortNo).ToList();

            // 2. 获取排序的数据排序信息
            GroupSetting g;
            int groupId = 0;
            foreach (var l in list)
            {
                //if(l.GroupSettingModel.Description)
                if (!string.IsNullOrEmpty(l.GroupSetting))
                    g = JsonHelper.JSONToObject<GroupSetting>(l.GroupSetting);
                else
                    g = new GroupSetting();

                // 3.判断用户是否升级 
                if (g.AccumulatedRechargeAmount > chargeAmount && g.AccumulatedEffectiveAmount > betAmount)
                {
                    groupId = l.Id;
                    break;
                }
            }

            if (memberGroupId == groupId) return (false, groupId);

            return (true, groupId);
        }


        /// <summary>
        /// 获取商户的
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<Dictionary<int, string>> GetPaySettingAsync(int merchantId)
        {
            //var cache = await base.PaySettingCacheAsync(merchantId);
            //if (cache.Exist) return JsonHelper.JSONToObject<Dictionary<int, string>>(cache.Data);

            //string where = $" WHERE MerchantId={merchantId} ";
            //var entity = (await _repository.GetListAsync(where)).ToDictionary(t => t.Id, t => t.PaySetting);

            //var cacheValue = JsonHelper.ToJson(entity);
            //await base.SavePaySettingCacheAsync(merchantId, cacheValue);

            var list = await _repository.GetListFromCacheAsync(merchantId);
            var entity = list.Where(t => t.Enabled).ToDictionary(t => t.Id, t => t.PaySetting);
            return entity;
        }

        /// <summary>
        /// 获取商户的默认分组Id
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<(bool, string, int)> GetDefaultGroupIdAsync(int merchantId)
        {
            var rt = await _repository.GetDefaultGroupIdAsync(merchantId);
            if (rt == 0) return (false, "未设置默认分组,请管理员设置默认分组", 0);
            return (true, null, rt);
        }



        /// <summary>
        /// 获取分组信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task<VipGroups> GetVipAsync(int merchantId, int groupId)
        {
            return await _repository.GetAsync(merchantId, groupId);
        }

        public async Task<VipGroups> GetVipNextLevelAsync(int merchantId, int currGroupId)
        {
            var list = await _repository.GetListFromCacheAsync(merchantId);
            var curr = list.Where(t => t.Id == currGroupId).Single();
            var next = list.Where(t => t.SortNo == (curr.SortNo + 1)).SingleOrDefault();
            if (next == null) return curr;
            return next;
        }

    }
}