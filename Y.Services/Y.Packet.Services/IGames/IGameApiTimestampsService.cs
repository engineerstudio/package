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
*│　接口名称： IGameApiTimestampsRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Games;

namespace Y.Packet.Services.IGames
{
    public interface IGameApiTimestampsService
    {

        /// <summary>
        /// 根据游戏类型查找日志抓取时间
        /// </summary>
        /// <param name="gameType"></param>
        /// <returns></returns>
        Task<GameApiTimestamps> GameApiTimestampsAsync(GameType gameType);

        /// <summary>
        /// 保存API日志查询时间
        /// </summary>
        /// <param name="gameType"></param>
        /// <param name="dateTime"></param>
        /// <param name="stamps"></param>
        /// <returns></returns>
        Task<(bool, string)> SaveApiLogQueryTimeAsync(GameType gameType, DateTime dateTime, long stamps);

        /// <summary>
        /// 根据游戏字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        Task<GameApiTimestamps> GetByTypeStrAsync(string str);

     

    }
}