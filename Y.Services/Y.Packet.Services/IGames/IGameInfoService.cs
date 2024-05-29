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
*│　创建时间：2020-09-07 16:30:43                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.IGames                                   
*│　接口名称： IGameInfoRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Games.ViewModels.Sys;
using static Y.Packet.Entities.Games.GameInfo;

namespace Y.Packet.Services.IGames
{
    public interface IGameInfoService
    {
        /// <summary>
        /// 查询游戏的状态，维护/正常
        /// </summary>
        /// <param name="type">游戏类型</param>
        /// <returns></returns>
        Task<(bool enabled, string errormsg)> GetGameStatusAsync(GameType type);

        /// <summary>
        /// 获取站点的游戏配置字符串
        /// </summary>
        /// <param name="type">游戏类型</param>
        /// <returns></returns>
        Task<string> GetGameConfig(GameType type);

        /// <summary>
        /// 获取游戏分类的json字符串,用户页面展示使用
        /// </summary>
        /// <returns></returns>
        string GetGamesByCategory();

        /// <summary>
        /// 获取游戏列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        Task<(IEnumerable<GameInfo>, int)> GetPageList(GameListQuery q);

        /// <summary>
        /// 获取所有游戏
        /// </summary>
        /// <returns></returns>
        Task<(IEnumerable<GameInfo>, int)> GetListAsync();

        /// <summary>
        /// 添加游戏
        /// </summary>
        /// <returns></returns>
        Task<(bool, string)> AddGameInfo(GameType gameType);

        /// <summary>
        /// 保存游戏API配置
        /// </summary>
        /// <param name="gameType"></param>
        /// <param name="defaultRate"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        Task<(bool, string)> SaveGameConfig(GameType gameType, decimal defaultRate, string config);

        /// <summary>
        /// 修改游戏状态
        /// </summary>
        /// <param name="gameType"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<(bool, string)> UpdateGameStatus(GameType gameType, GameStatus status);

        /// <summary>
        /// 获取当前站点的游戏状态,当游戏状态为 维护/开启 则返回true
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<bool> GetGameStatusAsync(int merchantId, GameType type);

    }
}