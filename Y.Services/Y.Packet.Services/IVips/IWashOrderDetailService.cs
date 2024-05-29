using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Packet.Entities.Vips.ViewModels;
using Y.Packet.Entities.Vips;

namespace Y.Packet.Services.IVips
{
    public interface IWashOrderDetailService
    {
        Task<(IEnumerable<WashOrderDetail>, int)> GetPageListAsync(WashOrderDetailListQuery q);
        Task<(bool, string)> InsertAsync(int userId, decimal amount, string mark, string sourceId);
        Task<decimal> GetTotalWashAmountAsync(int memberId);
    }
}