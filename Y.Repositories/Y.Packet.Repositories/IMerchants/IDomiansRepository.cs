using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Packet.Entities.Merchants;

namespace Y.Packet.Repositories.IMerchants
{
    public interface IDomiansRepository : IBaseRepository<Domains, Int32>
    {
        /// <summary>
        /// 获取到已经存在的域名数量
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<int> ExistDomainNoAsync(string url);

        /// <summary>
        /// 获取=的URL数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<IEnumerable<Domains>> GetByUrlAsync(string url);

        /// <summary>
        /// 获取Like的URL数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<IEnumerable<Domains>> GetByLikeUrlAsync(string url);
        Task<int?> InsertWithCacheAsync(Domains d);
        Task<int?> UpdateWithCacheAsync(Domains d);
        Task<int> GetMerchantIdByDomainAsync(string domian);
        Task<string> GetCallBackUrlCacheAsync(int merchantId);
        Task<int> DeleteCacheAsync(int id);

        Task MigrateSqlDbToRedisDbAsync();
    }
}