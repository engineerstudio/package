using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Packet.Entities.Pay;


namespace Y.Packet.Repositories.IPay
{
    public interface IMemberDataSummaryRepository : IBaseRepository<MemberDataSummary, Int32>
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
        /// 获取汇总数据
        /// </summary>
        /// <param name="members"></param>
        /// <returns></returns>
        Task<(decimal, decimal)> GetMembersDepositWithdrawalTotalAsync(IEnumerable<int> members);



    }
}