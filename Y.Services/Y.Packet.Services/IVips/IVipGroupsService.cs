using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.ICache.IRedis.IVipsService;
using Y.Packet.Entities.Vips;
using Y.Packet.Entities.Vips.ViewModels;

namespace Y.Packet.Services.IVips
{
    public interface IVipGroupsService : IVipGroupsCacheService
    {

        Task<(bool, string)> AddVipSettingsAsync(VipGroups vipGroups, GroupSetting groupSetting, WithdrawalSetting withdrawalSetting, PaySetting paySetting, PointSetting pointSetting);


        /// <summary>
        /// 获取分组列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        Task<(IEnumerable<VipGroups>, int)> GetPageListAsync(VipListQuery q);

        /// <summary>
        /// 获取所有的分组列表
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<IEnumerable<VipGroups>> GetListAsync(int merchantId);

        /// <summary>
        ///  站点初始化时候，创建站点的默认分组
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<(bool, string, int)> CreateMerchantAdminDefaultVIPGroupsAsync(int merchantId);


        Task<(bool, string)> InsertOrUpdateAsync(VipGroupModel rmodel);

        /// <summary>
        /// 设置分组规则
        /// </summary>
        /// <param name="gs"></param>
        /// <param name="marchantId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task<(bool, string)> SetGroupRuleAsync(GroupSetting gs, int marchantId, int groupId);

        /// <summary>
        /// 设置分组可用的支付
        /// </summary>
        /// <param name="merchatnId"></param>
        /// <param name="id"></param>
        /// <param name="payMerchantIds"></param>
        /// <returns></returns>
        Task<(bool, string)> SetPayRuleAsync(int merchatnId, int id, string payMerchantIds);


        /// <summary>
        /// 获取分组字典  ID/Name
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<(bool, Dictionary<int, string>)> GetGroupIdAndNameDicAsync(int merchantId);

        /// <summary>
        /// 判断用户条件是否可以升级
        /// </summary>
        /// <param name="merchatnId"></param>
        /// <param name="memberGroupId"></param>
        /// <param name="chargeAmount"></param>
        /// <param name="betAmount"></param>
        /// <returns>true:升级，false:不升级</returns>
        Task<(bool, int)> ProcessVipRulesAsync(int merchatnId, int memberGroupId, decimal chargeAmount, decimal betAmount);


        /// <summary>
        /// 获取分组支付配置的分组
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<Dictionary<int, string>> GetPaySettingAsync(int merchantId);

        /// <summary>
        /// 获取站点的默认分组Id
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<(bool, string, int)> GetDefaultGroupIdAsync(int merchantId);

        /// <summary>
        /// 获取分组信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task<VipGroups> GetVipAsync(int merchantId, int groupId);

        Task<VipGroups> GetVipNextLevelAsync(int merchantId, int currGroupId);
    }
}