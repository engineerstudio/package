using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Members;
using Y.Packet.Entities.Members.ViewModels;

namespace Y.Packet.Services.IMembers
{
    public interface IUsersFundsLogService
    {
        /// <summary>
        /// 添加用户资金账变记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="merchantId"></param>
        /// <param name="moeny"></param>
        /// <param name="sourceId"></param>
        /// <param name="fundsType"></param>
        /// <param name="subType"></param>
        /// <param name="transType"></param>
        /// <param name="desc"></param>
        /// <param name="ip"></param>
        /// <param name="transSucess">API是否转账成功, 如转账失败，则增加锁定金额</param>
        /// <returns></returns>
        Task<(bool, UsersFundsLog, string)> AddFundsLogAsync(int userId, int merchantId, decimal moeny, string sourceId, FundLogType fundsType, byte subType, TransType transType, string desc, string ip, bool transSucess = true);


        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        Task<(IEnumerable<UsersFundsLog>, int)> GetPageListAsync(FundsLogQuery q);

        /// <summary>
        /// 判断是否存在锁定日志记录
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        Task<bool> ExistLogAsync(int logId, int merchantId, int memberId, GameType gameType);



    }
}