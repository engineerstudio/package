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
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-09-07 16:30:43                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Games                                  
*│　类    名： GameUsersDailyReportStatisticByGameRulesService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Threading.Tasks;
using Y.Packet.Entities.Games;
using Y.Packet.Repositories.IGames;
using Y.Packet.Services.IGames;

namespace Y.Packet.Services.Games
{
    public class GameUsersDailyReportStatisticByGameRulesService : IGameUsersDailyReportStatisticByGameRulesService
    {
        private readonly IGameUsersDailyReportStatisticByGameRulesRepository _repository;

        public GameUsersDailyReportStatisticByGameRulesService(IGameUsersDailyReportStatisticByGameRulesRepository repository)
        {
            _repository = repository;
        }

        public async Task SaveData(GameLogs log)
        {
            // 1. 按照投注时间存入投注数据
            DateTime date = DateTime.UtcNow.AddHours(8).Date;







        }
    }
}