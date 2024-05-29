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
*│　创建时间：2021-01-10 19:22:45                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.IPay                                   
*│　接口名称： IWithdrawMerchantRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Packet.Entities.Pay;
using Y.Packet.Entities.Pay.ViewModel;

namespace Y.Packet.Services.IPay
{
    public interface IWithdrawMerchantService
    {
        /// <summary>
        /// 获取取款列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        Task<(IEnumerable<WithdrawMerchant>, int)> GetPageListAsync(WithdrawalListPageQuery q);

        /// <summary>
        /// 获取取款商户配置
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<(bool, string, Dictionary<int, string>)> GetDicAsync(int merchantId);

        /// <summary>
        /// 判断代付渠道是否存在，不存在则插入
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="typeStr"></param>
        /// <returns></returns>
        Task<(bool, WithdrawMerchant)> ExistOrInsertAsync(int merchantId, string typeStr);


        Task<(bool, string)> UpdateConfigAsync(WithdrawalsMerchantConfig config);

        Task<(bool, WithdrawMerchant)> GetAsync(int id);

    }
}