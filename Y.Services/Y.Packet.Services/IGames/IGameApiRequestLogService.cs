using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Y.Packet.Services.IGames
{
    public interface IGameApiRequestLogService
    {

        Task<(bool, string)> InsertAsync(int merchantId, string typeStr, (bool, string, string) rt);

    }
}