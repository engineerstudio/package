using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.ICache.IRedis.IPayService;
using Y.Packet.Entities.Pay;

namespace Y.Packet.Services.IPay
{
    public interface IPayTypeCategoryService: IPayTypeCategoryCacheService
    {
        Task<(IEnumerable<PayTypeCategory>, int)> GetPageListAsync(int merchantId);

        /// <summary>
        /// 获取所有开启的类别
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<IEnumerable<PayTypeCategory>> GetListAsync(int merchantId);

        /// <summary>
        /// 创建站点支付类别接口，创建站点时候调用
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<bool> CreatePayTypeCategoryAsync(int merchantId);


        Task<Dictionary<int, string>> GetPayTypeDicAsync(int merchantId);

        /// <summary>
        /// 更新是否开启状态
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="cId"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        Task<(bool, string)> UpdateStatusAsync(int merchantId, int cId, bool enabled);
        
        /// <summary>
        /// 保存支付类别
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<(bool, string)> SavePayTypeCategoryAsync(int merchantId, int id, string name, string url,bool isVc);

    }
}