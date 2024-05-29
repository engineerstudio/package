using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Y.Packet.Services.IPay
{
    public interface IMemberDataSummaryService
    {

        /// <summary>
        /// 获取存款与取款汇总
        /// </summary>
        /// <param name="members"></param>
        /// <returns></returns>
        Task<(decimal, decimal)> GetMembersDepositWithdrawalTotalAsync(IEnumerable<int> members);

    }
}