using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Packet.Entities.Members;

namespace Y.Packet.Repositories.IMembers
{
    public interface IUserHierarchyRepository : IBaseRepository<UserHierarchy, Int32>
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


        Task<int> ExistAsync(int siteId, int agentId, int userId);

        /// <summary>
        /// 获取下级所有的Id的List
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="agentId"></param>
        /// <returns></returns>
        //Task<IEnumerable<int>> GetAsync(int siteId, int agentId);
        Task<IEnumerable<int>> GetSubMemberIdsFromCacheAsync(int merchantId, int agentId);
        Task<int?> InsertWithCacheAsync(UserHierarchy d);
        Task<IEnumerable<UserHierarchy>> GetAgentMemberIdsFromCacheAsync(int merchantId, int subId);
        Task MigrateSqlDbToRedisDbAsync();
    }
}