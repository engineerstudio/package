using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Packet.Entities.Merchants;
using Y.Packet.Entities.Merchants.ViewModels;

namespace Y.Packet.Services.IMerchants
{
    public interface ISectionDetailService
    {
        Task<(bool, IEnumerable<SectionDetail>)> GetBySectionIdAsync(int merchantId, int sectionId);

        Task<(bool, string)> InsertOrUpdateAsync(SectionDetailsModifyModel m);

        Task<(bool, string)> UpdateContentAsync(int merchantId, int sectionId, int dId, string detailType, string content);

        Task<(bool, string)> UpdateBannerAsync(int merchantId, int sectionId, List<SectionBanner> banner);

        Task<(bool, string)> InsertAsync(List<SectionDetail> l);

        /// <summary>
        /// 获取站点的所有详细信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<IEnumerable<SectionDetail>> GetByMerchantIdAsync(int merchantId);
    }
}