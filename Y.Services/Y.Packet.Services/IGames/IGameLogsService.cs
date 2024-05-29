using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Games.ViewModels;

namespace Y.Packet.Services.IGames
{
    public interface IGameLogsService
    {


        Task<(IEnumerable<GameLogs>, int, Dictionary<string, decimal>)> GetPageListAsync(MerchantGameQuery q);

        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
        Task<(bool, string, int)> SaveLogAsync(GameLogs logs);


        Task GetDataByDate(DateTime date);

        /// <summary>
        /// 获取数据，只能内部使用
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<(bool sucess, string msg, GameLogs entity)> GetAsync(int orderId);
        Task<(bool, string)> PushOrderToListAsync(int orderId);

        Task<(bool, dynamic)> GetByOrderDetailsId(int orderId, GameCategory cate);

        /// <summary>
        /// [周签到活动]获取上周有效投注
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<decimal> GetTaskLaskWeekValidBet(int merchantId, int userId);
    }
}