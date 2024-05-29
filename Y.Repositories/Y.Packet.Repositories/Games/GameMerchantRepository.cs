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
*│　描    述：接口实现                                                    
*│　作    者：Aaron                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-09-08 23:59:35                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Repositories.Games                                  
*│　类    名： GameMerchantRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Y.Infrastructure.Library.Core.DbHelper;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

using Y.Packet.Repositories.IGames;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.Cache.DbCache.Games;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using System.Collections.Generic;
using Y.Infrastructure.Library.Core.Helper;
using System.Linq;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Packet.Entities.Games;

namespace Y.Packet.Repositories.Games
{
    public class GameMerchantRepository : GameMerchantDbCacheService, IGameMerchantRepository
    {
        public GameMerchantRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {
            _dbOption = options.Get("Ying.Games");//YSQLServer
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        public int DeleteLogical(int[] ids)
        {
            string sql = "update GameMerchant set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update GameMerchant set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }
        public async Task<GameMerchant> GetAsync(int merchantId, GameType gameType)
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<GameMerchant>($"SELECT * FROM {typeof(GameMerchant).Name} WHERE MerchantId={merchantId} AND TypeStr='{gameType.ToString()}'");
        }

        public async Task<GameMerchant> GetFromCacheAsync(int merchantId, GameType type)
        {
            string cache = await _db.HashGetAsync($"{GameMerchantEntityHash}{merchantId}", type.ToString());
            if (cache.IsNullOrEmpty())
            {
                // 1. 缓存不存在  看站点
                string sql = $"SELECT * FROM GameMerchant WHERE MerchantId = {merchantId} AND Type = {type.GetEnumValue()}";
                var dd = await _dbConnection.QueryFirstOrDefaultAsync<GameMerchant>(sql);
                if (dd == null)
                {
                    // 1. 站点不存在  看游戏
                    sql = $"SELECT * FROM GameInfo WHERE Type = {type.GetEnumValue()}";
                    var gInfo = await _dbConnection.QueryFirstOrDefaultAsync<GameInfo>(sql);
                    if (gInfo == null) throw new NullReferenceException($"站点{merchantId}不存在游戏{type.ToString()}");
                    var gm = new GameMerchant()
                    {
                        MerchantId = merchantId,
                        Type = type,
                        TypeStr = type.ToString(),
                        TypeDesc = type.GetDescription(),
                        Enabled = false,
                        Config = "",
                        Rate = gInfo.DefaultRate,
                        SysEnabled = false,
                    };
                    await InsertWithCacheAsync(gm);
                }
                else
                    await InsertWithCacheAsync(dd);
                cache = await _db.HashGetAsync($"{GameMerchantEntityHash}{merchantId}", type.ToString());
            }
            var d = JsonHelper.JSONToObject<GameMerchant>(cache);
            return d;
        }


        public async Task<bool> IsEnabledGameAsync(int merchantId, GameType gameType)
        {
            //string sql = $"SELECT Count(Id) FROM  GameMerchant WHERE MerchantId ={merchantId} AND  TypeStr = '{gameType.ToString()}' AND Enabled = 1 AND SysEnabled = 1";
            //return (await _dbConnection.ExecuteScalarAsync<int>(sql)) > 0;

            string key = $"{GameMerchantEntityHash}{merchantId}";
            if (!(await _db.HashExistsAsync(key, gameType.ToString()))) return false;

            var cache = await _db.HashGetAsync(key, gameType.ToString());
            var d = JsonHelper.JSONToObject<GameMerchant>(cache);
            return d.Enabled && d.SysEnabled;
        }

        public async Task<IEnumerable<GameMerchant>> GetListAsync(int merchantId, bool? enabled, bool? sysenabled, string? desc)
        {
            string key = $"{GameMerchantEntitySetHash}{merchantId}";
            //Console.WriteLine(key);
            var listStr = await _db.SetMembersAsync(key);
            List<GameMerchant> list = new List<GameMerchant>();
            foreach (var str in listStr)
                list.Add(JsonHelper.JSONToObject<GameMerchant>(str));

            //Console.WriteLine(listStr.ToJson());
            //Console.WriteLine(list.ToJson());

            if (enabled.IsNotEqualNull())
                list = list.Where(t => t.Enabled == enabled.Value).ToList();
            if (sysenabled.IsNotEqualNull())
                list = list.Where(t => t.SysEnabled == sysenabled.Value).ToList();
            if (!desc.IsNullOrEmpty())
                list = list.Where(t => t.TypeDesc.Contains(desc)).ToList();

            return list;
        }


        public async Task<Dictionary<string, string>> GetTypeAndNameDicAsync(int merchantId)
        {
            // GameMerchantTypeAndNameDic
            string key = $"{GameMerchantTypeAndNameDic}{merchantId}";
            var list = await base.HashGetDicAsync(key);
            return list;
        }


        private async Task CacheAsync(GameMerchant d)
        {
            await _db.HashSetAsync($"{GameMerchantEntityHash}{d.MerchantId}", d.TypeStr, d.ToJson(), StackExchange.Redis.When.Always);
            string key = $"{GameMerchantEntitySetHash}{d.MerchantId}";
            await _db.SetAddAsync(key, d.ToJson());
            await _db.HashSetAsync($"{GameMerchantTypeAndNameDic}{d.MerchantId}", d.TypeStr, d.TypeDesc, StackExchange.Redis.When.Always);
        }


        public async Task<int> DeleteGameMerchantAsync(int gameMerchantId)
        {
            string sql = $"SELECT * FROM GameMerchant WHERE Id={gameMerchantId}";
            var d = await _dbConnection.QuerySingleOrDefaultAsync<GameMerchant>(sql);
            d = new GameMerchant() { MerchantId = 1001,TypeStr = "Finance_FNH" };
            string key = $"{GameMerchantEntitySetHash}{d.MerchantId}";
            var dCache = await _db.SetMembersAsync(key);
            await _db.SetRemoveAsync(key, dCache);

            await _db.HashDeleteAsync($"{GameMerchantEntityHash}{d.MerchantId}", d.TypeStr);
            await _db.HashDeleteAsync($"{GameMerchantTypeAndNameDic}{d.MerchantId}", d.TypeStr);

            return await _dbConnection.DeleteAsync(d);
        }

        public async Task<int?> InsertWithCacheAsync(GameMerchant d)
        {
            var id = await _dbConnection.InsertAsync<GameMerchant>(d);
            if (id == null || id < 1) return id;
            d.Id = id.Value;
            await CacheAsync(d);
            return id;
        }

        public async Task<int> UpdateWithCacheAsync(GameMerchant d)
        {
            var id = await _dbConnection.UpdateAsync<GameMerchant>(d);
            if (id < 1) return id;
            var cache = await _db.HashGetAsync($"{GameMerchantEntityHash}{d.MerchantId}", d.TypeStr);
            await _db.SetRemoveAsync($"{GameMerchantEntitySetHash}{d.MerchantId}", cache);
            await CacheAsync(d);
            return id;
        }

        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<GameMerchant>();
            foreach (var mchId in list.Select(t => t.MerchantId))
                await _db.KeyDeleteAsync($"{GameMerchantEntitySetHash}{mchId}");
            foreach (var d in list)
                await CacheAsync(d);
        }
    }
}