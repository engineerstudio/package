////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //
//                         O\  =  /O                              //
//                      ____/`---'\____                           //
//                    .'  \\|     |//  `.                         //
//                   /  \\|||  :  |||//  \                        //
//                  /  _||||| -:- |||||-  \                       //
//                  |   | \\\  -  /// |   |                       //
//                  | \_|  ''\---/''  |   |                       //
//                  \  .-\__  `-`  ___/-. /                       //
//                ___`. .'  /--.--\  `. . ___                     //
//              ."" '<  `.___\_<|>_/___.'  >'"".                  //
//            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
//            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
//      ========`-.____`-.___\_____/___.-`____.-'========         //
//                           `=---='                              //
//      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
//                   佛祖保佑       永不宕机     永无BUG          //
////////////////////////////////////////////////////////////////////

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：Aaron                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-09-30 16:19:19                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.IPay                                   
*│　接口名称： IPayMerchantRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.ICache.IRedis.IPayService;
using Y.Packet.Entities.Pay;
using Y.Packet.Entities.Pay.ViewModel;

namespace Y.Packet.Services.IPay
{
    public interface IPayMerchantService: IPayMerchantCacheService
    {
        /// <summary>
        /// 保存支付类型
        /// </summary>
        /// <param name="merchantId">商户Id</param>
        /// <param name="payId">支付类型Id</param>
        /// <returns></returns>
        Task<(bool, string)> SavePayAsync(PayMerchantConfig d);

        Task<(IEnumerable<PayMerchant>, int)> GetPageListAsync(PayListPageQuery q);

        /// <summary>
        /// 更新支付类型
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        Task<(bool, string)> UpdatePayAsync(PayMerchantConfig d);

        /// <summary>
        /// 获取所有开启的支付
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<IEnumerable<PayMerchant>> GetListAsync(int merchantId);

        /// <summary>
        /// 获取用户所属分组的支付渠道
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="payMerchantIds"></param>
        /// <returns></returns>
        Task<IEnumerable<PayMerchant>> GetListAsync(int merchantId, string payMerchantIds);

        /// <summary>
        /// 获取商户配置支付的ID/Name字典
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<(Dictionary<int, string>, Dictionary<int, int>)> GetPayMerchantIdAndNameDicAsync(int merchantId);

        /// <summary>
        /// 获取支付商户渠道详细
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<(bool, string, PayMerchant)> GetAsync(int merchantId, int id);

        /// <summary>
        /// [页面] 获取支付渠道详细信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<(bool, string, dynamic)> GetChannelDetailsAsync(int merchantId, int id);

        /// <summary>
        /// 获取支付名称
        /// </summary>
        /// <param name="payId"></param>
        /// <returns></returns>
        Task<string> GetPayNameByIdAsync(int payId);
        Task<PayMerchant> GetByIdAsync(int payId);
    }
}