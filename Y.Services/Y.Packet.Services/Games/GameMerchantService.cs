using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Repositories.IGames;
using Y.Packet.Services.IGames;
using Y.Infrastructure.Cache.Redis.GamesService;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Games.ViewModels;

namespace Y.Packet.Services.Games
{
    public class GameMerchantService : GameMerchantCacheService, IGameMerchantService
    {
        private readonly IGameMerchantRepository _repository;

        public GameMerchantService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory, IGameMerchantRepository repository) : base(options, factory)
        {
            _repository = repository;
        }

        public async Task<bool> IsEnabledGameAsync(int merchantId, GameType type) => await _repository.IsEnabledGameAsync(merchantId, type);
        //{
        //    //var data = await base.IsEnabledGameCacheAsync(merchantId, type);
        //    //if (data.Exist) return data.Data;
        //    string conditions = $"WHERE MerchantId={merchantId} AND TypeStr='{type}'  AND Enabled=1 AND SysEnabled=1 ";
        //    var result = (await _repository.RecordCountAsync(conditions, null)) == 1;
        //    //await base.SaveIsEnabledGameCacheAsync(merchantId, type, result.ToString());
        //    return result;
        //}


        /// <summary>
        /// 开启/关闭站点游戏
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="type"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public async Task<(bool, string)> EnableGame(int merchantId, string typeStr, bool enabled)
        {
            var rt_getEntity = await ExistOrInsert(merchantId, typeStr);
            if (!rt_getEntity.Item1) return (rt_getEntity.Item1, rt_getEntity.Item2);
            GameMerchant gm = rt_getEntity.Item3;
            gm.Enabled = enabled;
            var rt = await _repository.UpdateWithCacheAsync(gm);
            //if (rt == 0) return (false, "更新游戏状态失败");
            ////await base.SaveIsEnabledGameCacheAsync(merchantId, gm.Type, enabled.ToString());
            //return (true, "更新成功");
            return rt.ToResult("更新成功", "更新游戏状态失败");
        }


        private async Task<(bool, string, GameMerchant)> ExistOrInsert(int merchantId, string typeStr)
        {
            GameType type = typeStr.ToEnum<GameType>().Value;
            if (merchantId == 0) return (false, "商户不存在", null);
            GameMerchant gm = await _repository.GetFromCacheAsync(merchantId, type);
            if (gm == null)
            {
                gm = new GameMerchant();
                gm.MerchantId = merchantId;
                gm.Type = type;
                gm.TypeStr = type.ToString();
                gm.TypeDesc = type.GetDescription();
                gm.Enabled = false;
                gm.Config = "";
                gm.Rate = 0;
                gm.SysEnabled = true;
                var n = await _repository.InsertWithCacheAsync(gm);
                gm.Id = n.Value;
            }
            return (true, null, gm);
        }


        /// <summary>
        /// 系统开启/关闭站点游戏
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="typeStr"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public async Task<(bool, string)> SetSysEnabledGame(int merchantId, string typeStr, bool enabled)
        {
            var rt_getEntity = await ExistOrInsert(merchantId, typeStr);
            if (!rt_getEntity.Item1) return (rt_getEntity.Item1, rt_getEntity.Item2);
            GameMerchant gm = rt_getEntity.Item3;
            gm.SysEnabled = enabled;
            var rt = await _repository.UpdateWithCacheAsync(gm);
            return rt.ToResult("更新成功", "更新游戏状态失败");
        }



        public async Task<(IEnumerable<GameMerchant>, int)> GetListAsync(MerchantGameListQuery q)
        {
            var parms = new DynamicParameters();
            if (q.MerchantId == 0) return (null, 0);
            //string conditions = $"WHERE  MerchantId={q.MerchantId}";
            //if (!string.IsNullOrEmpty(q.GameName))
            //{
            //    conditions += " AND TypeDesc like @GameName";
            //    parms.Add("GameName", $"%{q.GameName}%");
            //}
            //if (q.SysEnabled != null)
            //    conditions += $" AND SysEnabled={q.SysEnabled.Value.ToInt()}";
            //if (q.Enabled != null)
            //    conditions += $" AND Enabled={q.Enabled.Value.ToInt()}";

            //IEnumerable<GameMerchant> l = await _repository.GetListAsync(conditions, parms);
            //int record = await _repository.RecordCountAsync(conditions, parms); {"MerchantId":1000,"GameName":null,"Enabled":null,"SysEnabled":null,"Page":1,"Limit":1000,"Key":null}
            Console.WriteLine(q.ToJson());
            var list = await _repository.GetListAsync(q.MerchantId, q.Enabled, q.SysEnabled, q.GameName);
            Console.WriteLine(list.ToJson());
            return (list.Skip((q.Page - 1) * q.Limit).Take(q.Limit), list.Count());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<(bool, Dictionary<string, string>)> GetListDicAsync(int merchantId)
        {
            if (merchantId == 0) return (false, null);
            //string conditions = $"WHERE  MerchantId={merchantId}";
            //var l = (await _repository.GetListAsync(conditions)).ToDictionary(t => t.TypeStr, t => t.TypeDesc);
            var l = (await _repository.GetTypeAndNameDicAsync(merchantId));
            return (true, l);
        }


        /// <summary>
        /// 获取商户配置的游戏
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<string> GetMerchantGames(int merchantId)
        {
            List<string> keys = (await this.GetListDicAsync(merchantId)).Item2.Keys.ToList<string>();

            Dictionary<string, string> sport_dic = new Dictionary<string, string>();
            Dictionary<string, string> esport_dic = new Dictionary<string, string>();
            Dictionary<string, string> slot_dic = new Dictionary<string, string>();
            Dictionary<string, string> live_dic = new Dictionary<string, string>();
            Dictionary<string, string> lottery_dic = new Dictionary<string, string>();
            Dictionary<string, string> chess_dic = new Dictionary<string, string>();
            Dictionary<string, string> hunt_dic = new Dictionary<string, string>();
            Dictionary<string, string> finance_dic = new Dictionary<string, string>();

            foreach (var k in keys)
            {
                GameType gameType = k.ToEnum<GameType>().Value;
                if (gameType.ExistAttributeOfType<ESportAttribute>())
                    esport_dic.Add(gameType.ToString(), gameType.GetDescription());
                if (gameType.ExistAttributeOfType<SportAttribute>())
                    sport_dic.Add(gameType.ToString(), gameType.GetDescription());
                if (gameType.ExistAttributeOfType<SlotAttribute>())
                    slot_dic.Add(gameType.ToString(), gameType.GetDescription());
                if (gameType.ExistAttributeOfType<LiveAttribute>())
                    live_dic.Add(gameType.ToString(), gameType.GetDescription());
                if (gameType.ExistAttributeOfType<LotteryAttribute>())
                    lottery_dic.Add(gameType.ToString(), gameType.GetDescription());
                if (gameType.ExistAttributeOfType<ChessAttribute>())
                    chess_dic.Add(gameType.ToString(), gameType.GetDescription());
                if (gameType.ExistAttributeOfType<HuntAttribute>())
                    hunt_dic.Add(gameType.ToString(), gameType.GetDescription());
                //if (gameType.ExistAttributeOfType<FinanceAttribute>())
                //    finance_dic.Add(gameType.ToString(), gameType.GetDescription());
            }

            //return $"{{\"ESport\":{esport_dic.ToJson()},\"Sport\":{sport_dic.ToJson()},\"Slot\":{slot_dic.ToJson()},\"Live\":{live_dic.ToJson()},\"Lottery\":{lottery_dic.ToJson()},\"Chess\":{chess_dic.ToJson()},\"Hunt\":{hunt_dic.ToJson()},\"Finance\":{finance_dic.ToJson()}}}";

            return $"{{\"ESport\":{esport_dic.ToJson()},\"Sport\":{sport_dic.ToJson()},\"Slot\":{slot_dic.ToJson()},\"Live\":{live_dic.ToJson()},\"Lottery\":{lottery_dic.ToJson()},\"Chess\":{chess_dic.ToJson()},\"Hunt\":{hunt_dic.ToJson()}}}";
        }

        public async Task<(bool, string)> DeleteAsync(int merchantId, int id)
        {
            var rt = await _repository.DeleteGameMerchantAsync(id);
            return rt.ToResult("删除成功", "删除失败");
        }

    }
}