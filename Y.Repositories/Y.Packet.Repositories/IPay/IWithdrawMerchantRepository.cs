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
*│　命名空间： Y.Packet.Repositories.IPay                                   
*│　接口名称： IWithdrawMerchantRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Packet.Entities.Pay;

namespace Y.Packet.Repositories.IPay
{
    public interface IWithdrawMerchantRepository : IBaseRepository<WithdrawMerchant, Int32>
    {
        /// <summary>
        /// 逻辑删除返回影响的行数
        /// </summary>
        /// <param name="ids">需要删除的主键数组</param>
        /// <returns>影响的行数</returns>
        Int32 DeleteLogical(Int32[] ids);
        /// <summary>
        /// 逻辑删除返回影响的行数（异步操作）
        /// </summary>
        /// <param name="ids">需要删除的主键数组</param>
        /// <returns>影响的行数</returns>
        Task<Int32> DeleteLogicalAsync(Int32[] ids);

        /// <summary>
        /// 根据商户和渠道ID获取信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<WithdrawMerchant> GetByIdAsync(int merchantId, int id);



        Task<WithdrawMerchant> ExistAsync(int merchantId, string typeStr);
        Task<int?> InsertWithCacheAsync(WithdrawMerchant m);
        Task<int> UpdateWithCacheAsync(WithdrawMerchant m);
        Task<WithdrawMerchant> GetFromCacheAsync(int id);
        Task<WithdrawMerchant> GetFromCacheAsync(int merchantId, int id);
        Task<List<WithdrawMerchant>> GetListAsync(int merchantId);
        Task MigrateSqlDbToRedisDbAsync();
    }
}