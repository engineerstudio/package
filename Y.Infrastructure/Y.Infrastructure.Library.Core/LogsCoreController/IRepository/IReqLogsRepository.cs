using System;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.LogsCoreController.Entity;
using Y.Infrastructure.Library.Core.Repository;

namespace Y.Infrastructure.Library.Core.LogsCoreController.IRepository
{
    public interface IReqLogsRepository : IBaseRepository<ReqLogs, Int32>
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
    }
}