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
*│　类    名： GameInfoService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Games.ViewModels.Sys;
using Y.Packet.Repositories.IGames;
using Y.Packet.Services.IGames;
using static Y.Packet.Entities.Games.GameInfo;

namespace Y.Packet.Services.Games
{
    public class GameInfoService : IGameInfoService
    {
        private readonly IGameInfoRepository _repository;
        private readonly IGameMerchantRepository _gameMerchantRepository;

        public GameInfoService(IGameInfoRepository repository, IGameMerchantRepository gameMerchantRepository)
        {
            _repository = repository;
            _gameMerchantRepository = gameMerchantRepository;
        }
        public async Task<(bool enabled, string errormsg)> GetGameStatusAsync(GameType type)
        {
            string sql = $"SELECT * FROM {typeof(GameInfo).Name} WHERE TypeStr='{type}'";
            var gInfo = await _repository.GetAsync(sql, null);

            if (gInfo == null) return (false, "游戏不存在，请手动添加游戏");

            switch (gInfo.Status)
            {
                case GameInfo.GameStatus.Maintains:
                    return (false, "游戏在维护中");
                case GameInfo.GameStatus.Normal:
                    return (true, "游戏状态正常");
                case GameInfo.GameStatus.Deleted:
                    return (false, "游戏不存在");
                default:
                    return (false, "游戏不存在");
            }

        }

        public async Task<bool> GetGameStatusAsync(int merchantId, GameType type)
        {
            // 1. 判断站点下是否存在该游戏
            var existMerchantGames = await _gameMerchantRepository.IsEnabledGameAsync(merchantId, type);
            if (!existMerchantGames) return false;
            // 2. 判断该游戏的状态为 maintains normal 即可以显示
            var gameStatusInt = await _repository.GetGameStatusAsync(type);
            List<int> ll = new List<int>();
            ll.Add((int)GameStatus.Maintains);
            ll.Add((int)GameStatus.Normal);
            if (ll.Contains(gameStatusInt)) return true;
            return false;
        }


        public async Task<string> GetGameConfig(GameType type)
        {
            var gInfo = await _repository.GetByGameTypeAsync(type);
            return gInfo.Config;
        }

        public string GetGamesByCategory()
        {
            var esport = DictionaryHelper.GetEnumTypeDicByType<ESport>();
            var sport = DictionaryHelper.GetEnumTypeDicByType<Sport>();
            var slot = DictionaryHelper.GetEnumTypeDicByType<Slot>();
            var chess = DictionaryHelper.GetEnumTypeDicByType<Chess>();
            var live = DictionaryHelper.GetEnumTypeDicByType<Live>();
            var lottery = DictionaryHelper.GetEnumTypeDicByType<Lottery>();
            var hunt = DictionaryHelper.GetEnumTypeDicByType<Hunt>();
            //var finance = DictionaryHelper.GetEnumTypeDicByType<Finance>();

            //return $"{{\"ESport\":{esport.ToJson()},\"Sport\":{sport.ToJson()},\"Slot\":{slot.ToJson()},\"Chess\":{chess.ToJson()},\"Live\":{live.ToJson()},\"Lottery\":{lottery.ToJson()},\"Hunt\":{hunt.ToJson()},\"Finance\":{finance.ToJson()}}}";
            return $"{{\"ESport\":{esport.ToJson()},\"Sport\":{sport.ToJson()},\"Slot\":{slot.ToJson()},\"Chess\":{chess.ToJson()},\"Live\":{live.ToJson()},\"Lottery\":{lottery.ToJson()},\"Hunt\":{hunt.ToJson()}}}";
        }

        public async Task<(IEnumerable<GameInfo>, int)> GetPageList(GameListQuery q)
        {
            string conditions = "WHERE 1=1 ";
            //if (q.MerchantId != 0)
            //    conditions += $" ";

            if (!string.IsNullOrEmpty(q.GameCategory))
            {
                conditions += $" AND CategoryType={q.GameCategory.ToEnum<GameCategory>()?.GetEnumValue()}";
            }
            return (await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", null), await _repository.RecordCountAsync(conditions));
        }

        public async Task<(IEnumerable<GameInfo>, int)> GetListAsync()
        {
            var lit = await _repository.GetListAsync(null);
            return (lit, lit.Count());
        }

        public async Task<(bool, string)> AddGameInfo(GameType gameType)
        {
            GameInfo info = new GameInfo()
            {
                Name = gameType.GetDescription(),
                CategoryType = gameType.TransToGameCate(),
                Type = gameType,
                TypeStr = gameType.ToString(),
                Status = GameInfo.GameStatus.NotSet,
                DefaultRate = 0,
                IsTransferWallet = false,
                ApiTimeZone = 0,
                Config = ""
            };
            var rt = await _repository.InsertWithCacheAsync(info);
            var b = rt != null && rt.HasValue && rt.Value > 0;
            return (b, b ? "成功" : "失败");
        }


        public async Task<(bool, string)> SaveGameConfig(GameType gameType, decimal defaultRate, string config)
        {
            if (defaultRate <= 0 || defaultRate > 30) return (false, "请填入1-30的正整数");
            if (!JsonHelper.IsJson(config)) return (false, "请输入正确游戏配置的JSON格式");

            var gInfo = await _repository.GetByGameTypeAsync(gameType);
            gInfo.DefaultRate = defaultRate;
            gInfo.Config = config;
            var rt = await _repository.UpdateWithCacheAsync(gInfo);
            return (rt > 0, rt > 0 ? "更新成功" : "更新失败");
        }




        public async Task<(bool, string)> UpdateGameStatus(GameType gameType, GameStatus status)
        {
            var gInfo = await _repository.GetByGameTypeAsync(gameType);
            if (gInfo == null) return (false, "游戏不存在");
            gInfo.Status = status;
            var rt = await _repository.UpdateWithCacheAsync(gInfo);
            if (rt > 0) return (true, "保存成功");
            return (false, "保存失败");
        }
    }
}