using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Packet.Services.IGames
{
    public interface IInfrastructureGamesService
    {

        Task<(bool, string)> RegisterAsync(int merchantId, int userId, string userName, GameType gameType, string gameconfig);

        Task<(bool, string)> LoginAsync(int merchantId, int userId, string playerName, GameType gameType, Dictionary<string, string> args, string gameconfig);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <param name="playerName"></param>
        /// <param name="gameType"></param>
        /// <param name="sourceId"></param>
        /// <param name="money"></param>
        /// <param name="action"></param>
        /// <param name="gameconfig"></param>
        /// <returns>是否成功，返沪消息，TransferLogs的Id </returns>
        Task<(bool sucess, string msg, string transId, TransferStatus? status)> TransferAsync(int merchantId, int userId, string playerName, GameType gameType, int sourceId, decimal money, TransType action, string gameconfig);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <param name="playerName"></param>
        /// <param name="gameType"></param>
        /// <param name="sourceId"></param>
        /// <param name="gameconfig"></param>
        /// <returns></returns>
        Task<(bool, TransferStatus?, string)> CheckTransferAsync(int merchantId, int userId, string playerName, GameType gameType, int sourceId, TransType transType, string gameconfig);

        Task SaveGameOrdersAsync(GameType gameType, DateTime startDateTime, long startDateTimeStamps, string gameconfig);

        Task ExecGetLogs(GameType gameType, string gameconfig);

        Task<(bool, string)> AsyncToMainGamesLogsAsync(GameCategory gameCateStr, GameType? gameTypeStr, int? orderId);
    }
}
