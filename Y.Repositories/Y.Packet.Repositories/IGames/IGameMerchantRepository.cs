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
*│　创建时间：2020-09-08 23:59:35                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Repositories.IGames                                   
*│　接口名称： IGameMerchantRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Games;


namespace Y.Packet.Repositories.IGames
{
    public interface IGameMerchantRepository : IBaseRepository<GameMerchant, Int32>
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
        /// 获取商户的游戏
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="gameType"></param>
        /// <returns></returns>
        Task<GameMerchant> GetAsync(int merchantId, GameType gameType);
        Task<bool> IsEnabledGameAsync(int merchantId, GameType gameType);
        Task<int?> InsertWithCacheAsync(GameMerchant d);
        Task<int> UpdateWithCacheAsync(GameMerchant d);

        Task<IEnumerable<GameMerchant>> GetListAsync(int merchantId, bool? enabled, bool? sysenabled, string? desc);
        Task<GameMerchant> GetFromCacheAsync(int merchantId, GameType type);
        Task<Dictionary<string, string>> GetTypeAndNameDicAsync(int merchantId);
        Task MigrateSqlDbToRedisDbAsync();
        Task<int> DeleteGameMerchantAsync(int gameMerchantId);
    }
}