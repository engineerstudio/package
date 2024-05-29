using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Packet.Entities.Games;


namespace Y.Packet.Repositories.IGames
{
    public interface IGameUsersDailyReportStatisticRepository : IBaseRepository<GameUsersDailyReportStatistic, Int32>
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
        /// 是否存在该日期的记录
        /// </summary>
        /// <param name="date"></param>
        /// <param name="memberId"></param>
        /// <param name="gameTypeStr"></param>
        /// <returns></returns>
        Task<(bool, GameUsersDailyReportStatistic)> ExistDateAsync(string date, int memberId, string gameTypeStr);


        /// <summary>
        /// 判断是否存在日订单数据，不存在则创建日订单数据
        /// </summary>
        /// <param name="date"></param>
        /// <param name="memberid"></param>
        /// <param name="gameTypeStr"></param>
        /// <param name="merchantId"></param>
        /// <param name="playerName"></param>
        /// <returns></returns>
        Task CreateAsync(string date, int memberid, string gameTypeStr, int merchantId, string playerName);

        Task SaveBetAmountAsync(string date, int memberId, string gameTypeStr, decimal betAmount);
        Task SaveValidbetAndMoney(string date, int memberId, string gameTypeStr, decimal validbet, decimal money);


        /// <summary>
        ///  获取会员的游戏信息汇总
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="members"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<Dictionary<string, decimal>> GetMembersDataSummaryByIds(int merchantId, IEnumerable<int> members, DateTime startTime, DateTime endTime);

        Task<(IEnumerable<GameUsersDailyReportStatistic>, int)> GetPageListAsync(int merchantId, IEnumerable<int> members, DateTime startTime, DateTime endTime, int page, int pageSize);

        /// <summary>
        /// 用户Id/有效投注 数据
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<Dictionary<int, decimal>> GetRebateGradeDataAsync(int merchantId, DateTime startTime, DateTime endTime);
    }
}