using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Packet.Entities.Vips;

namespace Y.Packet.Repositories.IVips
{
    public interface IVipGroupsRepository : IBaseRepository<VipGroups, Int32>
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
        /// 是否存在分组名称
        /// </summary>
        /// <param name="merchatId"></param>
        /// <param name="name"></param>
        /// <returns>不存在返回结果为0</returns>
        Task<int> ExistGroupNameAsync(int merchatId, string name);

        /// <summary>
        /// 是否存在默认分组
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns>是否存在分组</returns>
        Task<(bool, VipGroups)> ExistDefaultGroupAsync(int merchantId);

        /// <summary>
        /// 获取商户默认分组ID
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<int> GetDefaultGroupIdAsync(int merchantId);


        /// <summary>
        /// 获取用户的VIP分组信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task<VipGroups> GetAsync(int merchantId, int groupId);
        Task<int?> InsertWithCacheAsync(VipGroups vipGroups);
        Task<int> UpdateWithCacheAsync(VipGroups d);
        Task<VipGroups> GetFromCacheAsync(int id);
        Task<List<VipGroups>> GetListFromCacheAsync(int merchantId);
        Task<Dictionary<int, string>> GetIdAndNameDicAsync(int merchantId);
        Task MigrateSqlDbToRedisDbAsync();
    }
}