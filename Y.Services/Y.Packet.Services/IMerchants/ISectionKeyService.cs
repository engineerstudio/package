using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.ICache.IRedis.MerchantsService;
using Y.Packet.Entities.Merchants;
using Y.Packet.Entities.Merchants.ViewModels;

namespace Y.Packet.Services.IMerchants
{
    public interface ISectionKeyService: ISectionKeyCacheService
    {
        Task<IEnumerable<SectionDetail>> GetByKeyAsync(int merchantId, string key);

        /// <summary>
        /// 根据Id获取到Key string
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> GetAsync(int id);

        /// <summary>
        /// 获取H5首页面显示的数据
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="secIds"></param>
        /// <returns></returns>
        Task<List<SectionH5VM>> GetByMerchantIdForH5Async(int merchantId, string[] secIds);

        /// <summary>
        /// 获取PC页面显示的Section数据
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<List<SectionVM>> GetAllByMerchantIdForPcAsync(int merchantId);
        /// <summary>
        /// 获取PC的菜单
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<List<SectionDVM>> GetByMerchantIdForPcTopMenusAsync(int merchantId);
        Task<List<SectionDVMV2>> GetByMerchantIdForPcTopMenusV2Async(int merchantId);

        Task<(bool, IEnumerable<SectionKey>)> GetByMerchantIdAsync(int merchantId);

        /// <summary>
        /// 插入key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<(bool, string, int)> InsertAsync(SectionKey key);


    }
}