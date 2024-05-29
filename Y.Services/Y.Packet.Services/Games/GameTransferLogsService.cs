
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
*│　描    述：游戏转账日志                                                    
*│　作    者：Aaron                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-09-07 16:30:43                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Games                                  
*│　类    名： GameTransferLogsService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Games.ViewModels;
using Y.Packet.Repositories.IGames;
using Y.Packet.Services.IGames;

namespace Y.Packet.Services.Games
{
    public class GameTransferLogsService : IGameTransferLogsService
    {
        private readonly IGameTransferLogsRepository _repository;

        public GameTransferLogsService(IGameTransferLogsRepository repository)
        {
            _repository = repository;
        }


        public async Task<(IEnumerable<GameTransferLogs>, int)> GetPageList(TransferLogQuery q)
        {
            string conditions = "WHERE 1=1 ";
            if (q.MerchantId != 0)
                conditions += $" AND MerchantId={q.MerchantId} ";
            if (q.MemberId != 0)
                conditions += $" AND UserId={q.MemberId} ";
            conditions += $" AND TypeStr='{q.GameType}' ";
            return (await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", null), _repository.RecordCount(conditions));

        }


        public async Task<(bool, GameTransferLogs, string)> AddTransferLogsAsync(GameType gameType, int merchantId, int userId, decimal money, TransType action, string status)
        {
            if (merchantId == 0 || userId == 0 || money == 0) return (false, null, "转入游戏参数错误");
            var transModel = new GameTransferLogs()
            {
                MerchantId = merchantId,
                UserId = userId,
                TypeStr = gameType.ToString(),
                Status = status.ToEnum<TransferStatus>().Value,
                CreateTime = DateTime.UtcNow.AddHours(8),
                Money = money,
                TransType = action,
                Raw = ""
            };
            var rt = await _repository.InsertAsync(transModel);
            if (rt == null || !rt.HasValue) return (false, null, "转入游戏保存失败");
            transModel.Id = rt.Value;
            return (true, transModel, "保存成功");
        }

        public async Task UpdateTransLogsStatusAsync(int logId, TransferStatus status)
        {
            var logs = await _repository.GetAsync(logId);
            logs.Status = status;
            await _repository.UpdateAsync(logs);
        }


        public async Task<GameTransferLogs> GetAsync(int logId)
        {
            return await _repository.GetAsync(logId);
        }

    }
}