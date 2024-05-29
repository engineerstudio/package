using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.ICache.IRedis.MerchantsService;
using Y.Packet.Entities.Merchants;
using Y.Packet.Entities.Merchants.ViewModels;

namespace Y.Packet.Services.IMerchants
{
    public interface INoticeAreaService: INoticeAreaCacheService
    {
        (IEnumerable<NoticeArea>, int) GetPageList(NoticeAreaListQuery q);
        Task<(IEnumerable<NoticeArea> value, int count)> GetPageListAsync(NoticeAreaListQuery q);

        Task<(bool, string, int)> SaveAsync(NoticeAreaInsertOrModifyModel m);

        Task<(bool, string, NoticeArea)> GetAsync(int merchantId,int id);
        Task<(bool, string)> UpdateDeletedStatusAsync(int merchantId, int noticeId);

        Task<IEnumerable<NoticeArea>> GetHomePageDisplayAsync(int merchantId);
    }
}