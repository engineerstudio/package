using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Infrastructure.Library
{
    public interface IGamesLibraryFactory
    {
        (bool, string, string, string, string, string) Register(string gameType, string userName, Dictionary<string, string> args);
        (bool, string, string, string, string) Login(string gameType, string gamePlayerName, Dictionary<string, string> args);
        (bool, string, string, TransferStatus, string) Transfer(string gameType, string gamePlayerName, string orderId, decimal money, TransType action);
        (bool, string, string, TransferStatus, string) CheckTransferStatus(string gameType, string gamePlayerName, string orderId, TransType action);
        Task<(bool, string, string, IEnumerable<Dictionary<string, object>>, DateTime, long)> GetLogsAsync(string gameType, DateTime dateTime, long startDateTimeTimestamp);
        (bool, string, string, decimal, string) GetBalance(string gameType, string gamePlayerName);
    }


    public class GamesLibraryFactory : IGamesLibraryFactory
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <returns>是否注册成功,注册申请数据, 注册返回消息，错误信息，游戏用户名称，用户密码</returns>
        public (bool, string, string, string, string, string) Register(string gameType, string userName, Dictionary<string, string> args)
        {
            return (false, null, null, null, null, null);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="gamePlayerName">游戏用户名称</param>
        /// <param name="args">参数</param>
        /// <returns>是否登录成功，请求登陆字符串，登录返回的Json消息，错误消息，输出到前端的登录字符串</returns>
        public (bool, string, string, string, string) Login(string gameType, string gamePlayerName, Dictionary<string, string> args)
        {
            return (false, null, null, null, null);
        }

        /// <summary>
        /// 转账
        /// </summary>
        /// <param name="gamePlayerName">游戏用户名称</param>
        /// <param name="money">转账金额</param>
        /// <param name="action">转账方式，转入In,转出Out</param>
        /// <returns>是否成功,请求数据，请求返回数据信息，状态，失败信息</returns>
        public (bool, string, string, TransferStatus, string) Transfer(string gameType, string gamePlayerName, string orderId, decimal money, TransType action)
        {
            return (false, null, null, TransferStatus.Failed, null);
        }

        /// <summary>
        /// 检查转账
        /// </summary>
        /// <param name="gamePlayerName">游戏用户名称</param>
        /// <param name="orderId">转账金额</param>
        /// <param name="action">转账方式，转入In,转出Out</param>
        /// <returns>是否解锁成功,请求数据,请求返回数据信息,返回状态(1.等待，2.成功，3.失败), 返回消息</returns>
        public (bool, string, string, TransferStatus, string) CheckTransferStatus(string gameType, string gamePlayerName, string orderId, TransType action)
        {
            return (false, null, null, TransferStatus.Failed, null);
        }

        /// <summary>
        /// 获取注单日志
        /// </summary>
        /// <param name="dateTime">注单日志查询开始时间</param>
        /// <param name="startDateTimeTimestamp">注单日志查询时间戳</param>
        /// <returns>请求查询的数据，请求返回的数，处理后的数据，最后查询时间，最后查询时间的时间戳</returns>
        public async Task<(bool, string, string, IEnumerable<Dictionary<string, object>>, DateTime, long)> GetLogsAsync(string gameType, DateTime dateTime, long startDateTimeTimestamp)
        {
            return (false, null, null, new List<Dictionary<string, object>>(), dateTime, startDateTimeTimestamp);
        }


        /// <summary>
        /// 获取游戏余额
        /// </summary>
        /// <param name="gamePlayerName">游戏用户名称</param>
        /// <returns>是否成功,请求内容,返回内容, 余额,消息</returns>
        public (bool, string, string, decimal, string) GetBalance(string gameType, string gamePlayerName)
        {
            return (false, null, null, default, null);
        }
    }
}
