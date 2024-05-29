﻿////////////////////////////////////////////////////////////////////
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
*│　描    述：游戏转账日志                                                    
*│　作    者：Aaron                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-09-07 16:30:43                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.IGames                                   
*│　接口名称： IGameTransferLogsRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Games.ViewModels;

namespace Y.Packet.Services.IGames
{
    public interface IGameTransferLogsService
    {
       Task<(bool, GameTransferLogs, string)> AddTransferLogsAsync(GameType gameType, int merchantId, int userId, decimal money, TransType action, string status);


        Task<(IEnumerable<GameTransferLogs>, int)> GetPageList(TransferLogQuery q);

        Task UpdateTransLogsStatusAsync(int logId, TransferStatus status);
        
        Task<GameTransferLogs> GetAsync(int logId);

    }
}