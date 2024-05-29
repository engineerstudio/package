using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Packet.Entities.Games;

namespace Y.Packet.Services.IGames
{
    public interface IGameUsersDailyReportStatisticService
    {
        Task<(IEnumerable<GameUsersDailyReportStatistic>, int)> GetByMerchantAsync(int merchantId, DateTime date);
        Task<(IEnumerable<GameUsersDailyReportStatistic>, int)> GetPageListAsync(int merchantId, IEnumerable<int> members, DateTime startTime, DateTime endTime, int currentPage, int pageSize);
        /// <summary>
        /// 生成指定商户用户的日报表
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="gameTypeStr"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<(bool, string)> SaveDataAsync(int merchantId, int memberId, string gameTypeStr, string date);
        Task<(bool, string)> CreateGameUserReportAsync(int merchantId, int memberId, string date);
        /// <summary>
        /// 生成指定日期的所有商户用户的日报表
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<(bool, string)> CreateGameUserReportAsync(string date = null);

        /// <summary>
        /// 获取会员指定日期数据汇总
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="members"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<(decimal, decimal, decimal)> GetMembersDataSummaryByIds(int merchantId, IEnumerable<int> members, DateTime startTime, DateTime endTime);

    }
}