using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Y.Infrastructure.ICache
{
    public interface IBaseRedisRepository
    {

        /// <summary>
        /// 清理指定key的缓存
        /// </summary>
        /// <param name="key"></param>
        void ClearStringSet(string key);

        /// <summary>
        /// 清理指定key的缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task ClearStringSetAsync(string key);

    }
}
