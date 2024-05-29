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
*│　命名空间： Y.Packet.Services.IGames                                   
*│　接口名称： IGameMerchantRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.ICache.IRedis.IGamesService;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Games.ViewModels;

namespace Y.Packet.Services.IGames
{
    public interface IGameMerchantService: IGameMerchantCacheService
    {
        /// <summary>
        /// 站点是否开启该游戏
        /// </summary>
        /// <param name="merchantId">站点Id</param>
        /// <param name="type">游戏类型</param>
        /// <returns>true(游戏开启); false(游戏关闭)</returns>
        Task<bool> IsEnabledGameAsync(int merchantId, GameType type);
        /// <summary>
        /// 开启/关闭站点游戏
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="typeStr"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        Task<(bool, string)> EnableGame(int merchantId, string typeStr, bool enabled);


        /// <summary>
        /// 系统启用/禁用游戏
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="typeStr"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        Task<(bool, string)> SetSysEnabledGame(int merchantId, string typeStr, bool enabled);


        /// <summary>
        /// 获取商户配置游戏列表
        /// </summary>
        /// <returns></returns>
        Task<(IEnumerable<GameMerchant>, int)> GetListAsync(MerchantGameListQuery rq);

        /// <summary>
        /// 获取商户配置游戏字典  游戏字符串/游戏名称
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<(bool, Dictionary<string, string>)> GetListDicAsync(int merchantId);

        /// <summary>
        /// 获取商户配置的游戏 用于返水/返点等的设置
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<string> GetMerchantGames(int merchantId);

        Task<(bool, string)> DeleteAsync(int merchantId, int id);

    }
}