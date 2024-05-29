using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Games;

namespace Y.Packet.Services.IGames
{
    public interface IGameUsersService
    {
        /// <summary>
        /// 是否存在指定游戏的用户名
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="gameType">游戏类别</param>
        /// <returns>存在:true, 不存在:false</returns>
        Task<(bool exist, string gameUserName)> ExistAsync(int MemberId, GameType gameType);

        Task<(bool, string)> DeleteGameUserAsync(int memberId, GameType gameType);

        /// <summary>
        /// 获取用户游戏名称
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="gameType">游戏类别</param>
        /// <returns></returns>
        Task<string> GetPlayerNameAsync(int userId, GameType gameType);

        Task<(bool, string)> SaveGameUserAsync(int merchantId, int memberId, string gameStr, string playerName, string psw);

        /// <summary>
        /// 根据游戏字符串与游戏名称查找用户名字
        /// </summary>
        /// <param name="gameStr"></param>
        /// <param name="playerName"></param>
        /// <returns>是否成功，站点ID,用户ID</returns>
        Task<(bool, int, int)> GetByPlayerNameAsync(string gameStr, string playerName);

        Task<IEnumerable<GameUsers>> GetAsync(int merchantId, int memeberId);
    }
}