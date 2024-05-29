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
*│　创建时间：2021-12-27 14:37:53                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Games                                  
*│　类    名： GameLogsFinanceService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Packet.Entities.Games;
using Y.Packet.Repositories.IGames;
using Y.Packet.Services.IGames;

namespace Y.Packet.Services.Games
{
    public class GameLogsFinanceService : IGameLogsFinanceService
    {
        private readonly IGameLogsFinanceRepository _repository;

        public GameLogsFinanceService(IGameLogsFinanceRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddFinanceLog(GameLogsFinance log)
        {
            var dt = DateTime.UtcNow.AddHours(8);
            log.CreateTime = dt;
            log.UpdateTime = dt;
            var rt = await _repository.InsertAsync(log);
            return rt != null && rt.HasValue && rt.Value > 0;
        }

        public async Task<bool> UpdateFinanceLog(GameLogsFinance log)
        {
            var dt = DateTime.UtcNow.AddHours(8);
            var financeLog = await _repository.GetBySourceIdAsync(log.GameTypeStr, log.SourceId);
            if (financeLog == null) return false;
            financeLog.Results = log.Results;
            financeLog.ValidBet = log.ValidBet;
            financeLog.Money = log.Money;
            financeLog.AwardAmount = log.AwardAmount;
            financeLog.SourceOrderAwardTime = log.SourceOrderAwardTime;
            financeLog.Status = log.Status;
            financeLog.Raw = log.Raw;
            financeLog.SettlementTime = log.SettlementTime;
            var rt = await _repository.UpdateAsync(financeLog);
            return rt > 0;
        }
    }
}