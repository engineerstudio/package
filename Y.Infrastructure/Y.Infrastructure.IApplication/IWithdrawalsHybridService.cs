using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Y.Infrastructure.IApplication
{
    /// <summary>
    /// 代付类处理
    /// </summary>
    public interface IWithdrawalsHybridService
    {

        Task<(bool, string)> ConfirmOrderAsync(int merchantId, int memeberId, int orderId, int withdrawalsMerchantId);


        Task<(bool, string)> ProcessingCallbackAsync(int payId, Dictionary<string, string> callbackDic);


        Task<(bool, dynamic)> GetCheckInfoAsync(int merchantId, int memberId);

        /// <summary>
        /// 创建出款订单
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="money"></param>
        /// <param name="bankId"></param>
        /// <param name="spswd"></param>
        /// <param name="memberName"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        Task<(bool, string)> CreateWithdrawalsOrder(int merchantId, int memberId, decimal money, int bankId, string spswd, string memberName, string ip);

    }
}
