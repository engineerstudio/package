using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.ICache.IRedis.IMerchantsService;
using Y.Packet.Entities.Merchants;
using Y.Packet.Entities.Merchants.ViewModels;

namespace Y.Packet.Services.IMerchants
{
    public interface IHelpAreaService: IHelpAreaCacheService
    {
        /// <summary>
        /// 获取商户底部菜单. 底部菜单显示为类别名称
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<(bool, string)> GetByMerchantIdAsync(int merchantId, int typeId = 0);

        /// <summary>
        /// 根据类型ID获取详细
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        Task<(bool, string)> GetByTypeIdAsync(int merchantId, int typeId);

        Task<(IEnumerable<HelpArea>, int)> GetPageListAsync(HelpAreaListQuery q);

        Task<(bool, string)> InsertOrModify(HelpAreaAOM m);

        Task<(bool, string, HelpArea)> GetAsync(int merchantId, int id);

        Task<(bool, string)> DeleteAsync(int merchantId, int id);

        /// <summary>
        /// 获取页面底部显示项
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<IEnumerable<HelpAreaVMV2>> GetShowIndexItemsAsync(int merchantId);

    }
}