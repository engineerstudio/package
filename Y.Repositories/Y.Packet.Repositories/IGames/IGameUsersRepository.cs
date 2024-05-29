using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Games;

namespace Y.Packet.Repositories.IGames
{
    public interface IGameUsersRepository : IBaseRepository<GameUsers, Int32>
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

        Task<int> DeleteAsync(int memeberId, GameType gameType);

        Task<(int MerchantId, int MemberId)> GetByPlayerNameAsync(string gameStr, string playerName);
        Task<Int32?> InsertWithCacheAsync(GameUsers d);
        Task<string> GetPlayerNameAsync(int MemberId, GameType gameType);
        Task<(int MerchantId, int MemberId)> GetByMerchantIdAndMemberIdAsync(string gameStr, string playerName);
        Task<IEnumerable<GameUsers>> GetListAsync(int merchantId, int memberId);
        Task MigrateSqlDbToRedisDbAsync();
    }
}