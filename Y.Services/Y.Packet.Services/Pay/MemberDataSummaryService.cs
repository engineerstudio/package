using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Packet.Repositories.IPay;
using Y.Packet.Services.IPay;

namespace Y.Packet.Services.Pay
{
    public class MemberDataSummaryService : IMemberDataSummaryService
    {
        private readonly IMemberDataSummaryRepository _repository;

        public MemberDataSummaryService(IMemberDataSummaryRepository repository)
        {
            _repository = repository;
        }


        /// <summary>
        /// 返回总充值、总提现金额
        /// </summary>
        /// <param name="members"></param>
        /// <returns></returns>
        public async Task<(decimal, decimal)> GetMembersDepositWithdrawalTotalAsync(IEnumerable<int> members)
        {
            if (members.Count() == 0) return (0,0);
            return await _repository.GetMembersDepositWithdrawalTotalAsync(members);
        }


    }
}