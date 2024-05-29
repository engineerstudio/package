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
*│　创建时间：2020-09-13 20:08:41                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Games                                  
*│　类    名： GameLogsEsportService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Threading.Tasks;
using Y.Packet.Entities.Games;
using Y.Packet.Repositories.IGames;
using Y.Packet.Services.IGames;

namespace Y.Packet.Services.Games
{
    public class GameLogsEsportService : IGameLogsEsportService
    {
        private readonly IGameLogsEsportRepository _repository;

        public GameLogsEsportService(IGameLogsEsportRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddEsportLog(GameLogsEsport log)
        {
            var dt = DateTime.UtcNow.AddHours(8);
            log.CreateTime = dt;
            log.UpdateTime = dt;
            var rt = await _repository.InsertAsync(log);
            return rt != null && rt.HasValue && rt.Value > 0;
        }

        /// <summary>
        /// 同步已存在订单的结算内容
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public async Task<bool> UpdateEsportLog(GameLogsEsport log)
        {
            var dt = DateTime.UtcNow.AddHours(8);
            var sportLog = await _repository.GetBySourceIdAsync(log.GameTypeStr, log.SourceId);
            sportLog.Results = log.Results;
            sportLog.ValidBet = log.ValidBet;
            sportLog.Money = log.Money;
            sportLog.AwardAmount = log.AwardAmount;
            sportLog.SourceOrderAwardTime = log.SourceOrderAwardTime;
            sportLog.Status = log.Status;
            sportLog.Raw = log.Raw;
            sportLog.SettlementTime = log.SettlementTime;
            var rt = await _repository.UpdateAsync(sportLog);
            return rt > 0;
        }
    }
}