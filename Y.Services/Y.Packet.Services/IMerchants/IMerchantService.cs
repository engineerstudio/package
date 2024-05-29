using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.ICache.IRedis.MerchantsService;
using Y.Packet.Entities.Merchants;
using Y.Packet.Entities.Merchants.ViewModels.Sys;

namespace Y.Packet.Services.IMerchants
{
    public interface IMerchantService : IMerchantCacheService
    {
        /// <summary>
        /// 添加商户
        /// </summary>
        /// <param name="merchName"></param>
        /// <returns></returns>
        Task<(bool, string, int)> AddAsync(string merchName, string pcTemplet, string h5Templet);

        /// <summary>
        /// 获取商户列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        Task<(IEnumerable<Merchant>, int)> GetPageListAsync(MerchantListQuery q);

        /// <summary>
        /// 获取所有的商户
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Merchant>> GetMerchantsAsync();

        /// <summary>
        /// 获取商户ID/名称 字典
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<int, string>> MerchantsDicAsync();

        /// <summary>
        /// 是否是有效的后台域名
        /// </summary>
        /// <param name="domains"></param>
        /// <returns>是否存在，站点ID</returns>
        Task<(bool, int)> IsValidDoaminNameAsync(string domains);

        /// <summary>
        /// 获取到站点
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<(bool, Merchant)> GetAsync(int merchantId);

        Task<(bool, string)> UpdateCreditAsync(int merchantId, decimal credit);

        Task<(bool, string)> UpdatePcLogoAsync(int merchantId, string link);

        Task<(bool, string)> UpdateH5LogoAsync(int merchantId, string link);

        Task<(bool, string)> UpdateServiceLinkAsync(int merchantId, string link);

        Task<(bool, string)> UpdateCustomerConfigAsync(string name, int merchantId, string pclogo, string h5logo, string servicelink, string downloadQr, string siteUrl);


        #region 全站VIP设置

        /// <summary>
        /// 获取站点VIP设置
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<(bool, dynamic)> GetVipsConfigAsync(int merchantId);
        /// <summary>
        /// 保存站点VIP设置
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        Task<(bool, string)> SaveVipsConfigAsync(int merchantId, Merchant_VipsConfig config);


        #endregion

        /// <summary>
        /// 获取站点基础配置
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<(bool, string)> GetCustomerConfigAsync(int merchantId);

        /// <summary>
        /// 保存站点基础配置信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="csConfig"></param>
        /// <returns></returns>
        Task<(bool, string)> SaveCustomerConfigAsync(int merchantId, Merchant_CustomerConfig csConfig);


    }
}