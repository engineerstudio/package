using System;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Packet.Entities.Vips;

namespace Y.Packet.Repositories.IVips
{
    public interface IWashOrderRepository : IBaseRepository<WashOrder, Int32>
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
        /// 是否已经全部打码
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns> true 还需打码， false 已全部打码</returns>
        Task<bool> IsFinishedAllWashOrderAsync(int memberId);

        /// <summary>
        /// 获取所有未完成的打码量
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<decimal> GetUnfinishedAmountAsync(int merchantId, int userId);

        /// <summary>
        /// 更改当前订单状态为结束
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<int> UpdateOrderStatusToFinishAsync(int orderId);

    }
}