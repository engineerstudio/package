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
*│　创建时间：2021-04-02 18:50:04                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Games                                  
*│　类    名： GameLogsLotteryService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Games.ViewModels;
using Y.Packet.Repositories.IGames;
using Y.Packet.Services.IGames;

namespace Y.Packet.Services.Games
{
    public class GameLogsLotteryService : IGameLogsLotteryService
    {
        private readonly IGameLogsLotteryRepository _repository;

        public GameLogsLotteryService(IGameLogsLotteryRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddFinanceLog(GameLogsLottery log)
        {
            var dt = DateTime.UtcNow.AddHours(8);
            log.CreateTime = dt;
            log.UpdateTime = dt;
            var rt = await _repository.InsertWithCacheAsync(log);
            return rt != null && rt.HasValue && rt.Value > 0;
        }

        public async Task<bool> UpdateFinanceLog(GameLogsLottery log)
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
            var rt = await _repository.UpdateWithCacheAsync(financeLog);
            return rt > 0;
        }

        public async Task<GameLogsLottery> GetAsync(int id)
        {
            if (id == 0) return null;
            var d = await _repository.GetAsync(id);
            return d;
        }

        public async Task<(IEnumerable<GameLogsLottery>, int)> GetPageListAsync(GameLogsQuery q)
        {
            string conditions = "WHERE 1=1 ";
            //if (q.MerchantId != 0)
            //    conditions += $" ";

            //if (!string.IsNullOrEmpty(q.GameCategory))
            //    conditions += $" AND CategoryType={q.GameCategory.ToEnum<GameCategory>()?.GetEnumValue()}";
            if (q.StartAt != null)
                conditions += $" AND UpdateTime >= '{q.StartAt.Value.ToDateTimeString2()}' ";
            if (q.EndAt != null)
                conditions += $" AND UpdateTime <= '{q.StartAt.Value.ToDateTimeString2()}' ";

            return (await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", null), await _repository.RecordCountAsync(conditions));
        }

    }
}