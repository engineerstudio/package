using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Vips;
using Y.Packet.Entities.Vips.ViewModels;

namespace Y.Packet.Services.IVips
{
    public interface IWashOrderService
    {

        Task<(IEnumerable<WashOrder>, int)> GetPageListAsync(WashOrderListQuery q);
        Task<(bool, string)> ClearWashOrderAsync(int memberId);
        Task<(bool, string)> InsertAsync(int memberId, FundLogType fundsType, decimal amount, decimal washAmount, string mark);


    }
}