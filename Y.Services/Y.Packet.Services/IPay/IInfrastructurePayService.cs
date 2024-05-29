using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Packet.Services.IPay
{
    public interface IInfrastructurePayService
    {
        /// <summary>
        /// 同步支付接口
        /// </summary>
        /// <returns></returns>
        Task<(bool, string)> AsyncPayMethodAsync();

        /// <summary>
        /// 获取所有支付接口
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetWithdrawalsMerchantFromInterfaceAsync();

        /// <summary>
        /// 获取指定代付需要配置的字符串
        /// </summary>
        /// <param name="merchantClassName"></param>
        /// <param name="isPayment">true:支付，false:代付出款</param>
        /// <returns></returns>
        Task<string> GetMerchantConfigMembersFromInterfaceAsync(string merchantClassName, bool isPayment);


        /// <summary>
        /// 获取接口类的属性信息
        /// </summary>
        /// <param name="merchantClassName"></param>
        /// <returns></returns>
        Dictionary<string, PayDbAttribute> GetMerchantMembersAndAttrFromInterface(string merchantClassName, bool isPayment);


        /// <summary>
        /// 获取代付接口所需要的字符串
        /// </summary>
        /// <param name="merchantConfig">商户配置的字符串</param>
        /// <param name="attrs">接口的字符串</param>
        /// <param name="dicValus">传入的字符串</param>
        /// <returns></returns>
        Dictionary<string, string> ToWithdrawalsConfig(string merchantConfig, Dictionary<string, PayDbAttribute> attrs, Dictionary<string, string> dicValus);



    }
}
