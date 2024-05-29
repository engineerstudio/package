using System;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Games;


namespace Y.Packet.Repositories.IGames
{
    public interface IGameApiTimestampsRepository : IBaseRepository<GameApiTimestamps, Int32>
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


        //Task<GameApiTimestamps> GetByGameTypeAsync(GameType gameType);

        Task<GameApiTimestamps> GetByTypeStrAsync(string str);
        Task<int?> InsertWithCacheAsync(GameApiTimestamps d);
        Task<int> UpdateWithCacheAsync(GameApiTimestamps d);

        Task MigrateSqlDbToRedisDbAsync();
    }
}