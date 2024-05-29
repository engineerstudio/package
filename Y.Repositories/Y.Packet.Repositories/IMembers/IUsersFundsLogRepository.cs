using System;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Members;

namespace Y.Packet.Repositories.IMembers
{
    public interface IUsersFundsLogRepository : IBaseRepository<UsersFundsLog, Int32>
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
        /// 用户转账类型是否已经存在
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <param name="sourceId"></param>
        /// <param name="transType"></param>
        /// <returns>true(存在)</returns>
        Task<bool> ExistFundsLogSourceIdAsync(int merchantId, int userId, string sourceId, TransType transType);
        Task<bool> ExistFundsLogSourceIdAsync(int merchantId, int userId, string sourceId, FundLogType fundLogType);
        /// <summary>
        /// 插入转账日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="transSucess">API转账是否成功字段，成功操作balance,未成功，则增加锁定金额</param>
        /// <returns></returns>
        Task<bool> InsertByTransAsync(UsersFundsLog log, bool transSucess = true);


        /// <summary>
        /// 获取用户过去三个月的充值总额
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<decimal> GetAccountLastThreeMonthsRechargeMoneyAsync(int merchantId, int userId);

        /// <summary>
        /// 判断日志是否存在
        /// </summary>
        /// <param name="logId"></param>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="gameType"></param>
        /// <returns></returns>
        Task<int> ExistLogAsync(int logId, int merchantId, int memberId, GameType gameType);
    }
}