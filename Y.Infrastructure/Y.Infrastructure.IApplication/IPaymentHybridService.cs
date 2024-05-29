using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Y.Infrastructure.IApplication
{
    public interface IPaymentHybridService
    {


        /// <summary>
        /// 处理回调信息
        /// </summary>
        /// <param name="payId"></param>
        /// <param name="callbackDic"></param>
        /// <returns></returns>
        Task<(bool, string)> ProcessingCallbackAsync(int payId, Dictionary<string, string> callbackDic);


        Task<(bool Sucess, string Error,string Url)> CreateOrderAsync(int payId, int merchantId, int memberId, decimal depositAmount, string userName, string ip);


        /// <summary>
        /// [页面] 获取商户的支付渠道
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="vipId"></param>
        /// <returns></returns>
        Task<(bool, string, dynamic)> GetPayListAsync(int merchantId, int vipId);
    }
}
