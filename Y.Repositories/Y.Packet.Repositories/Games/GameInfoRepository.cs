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
*│　创建时间：2020-09-07 16:30:43                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Repositories.Games                                  
*│　类    名： GameInfoRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Y.Infrastructure.Library.Core.DbHelper;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

using Y.Packet.Repositories.IGames;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.Cache.DbCache.Games;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Helper;
using System.Collections.Generic;
using Y.Packet.Entities.Games;

namespace Y.Packet.Repositories.Games
{
    public class GameInfoRepository : GameInfoDbCacheService, IGameInfoRepository
    {
        public GameInfoRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {
            _dbOption = options.Get("Ying.Games");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        public int DeleteLogical(int[] ids)
        {
            string sql = "update GameInfo set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update GameInfo set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }


        public async Task<GameInfo> GetByGameTypeAsync(GameType type)
        {
            //string sql = $"SELECT * FROM {typeof(GameInfo).Name} WHERE TypeStr='{type}'";
            //return await _dbConnection.QuerySingleAsync<GameInfo>(sql);
            var cache = await _db.HashGetAsync(GameInfoEntityHash, type.ToString());
            return JsonHelper.JSONToObject<GameInfo>(cache);
        }

        public async Task<int> GetGameStatusAsync(GameType type)
        {
            //string sql = $"SELECT Status FROM {typeof(GameInfo).Name} WHERE TypeStr='{type}'";
            //return await _dbConnection.ExecuteScalarAsync<int>(sql);

            var cache = await _db.HashGetAsync(GameInfoEntityHash, type.ToString());
            var d = JsonHelper.JSONToObject<GameInfo>(cache);
            return d.Status.GetEnumValue();
        }

        private async Task CacheAsync(GameInfo d)
        {
            await _db.HashSetAsync(GameInfoEntityHash, d.TypeStr, d.ToJson(), StackExchange.Redis.When.Always);
            await _db.SetAddAsync(GameInfoEntitySetHash, d.ToJson());
        }

        public async Task<int?> InsertWithCacheAsync(GameInfo d)
        {
            var id = await _dbConnection.InsertAsync<GameInfo>(d);
            if (id == null || id < 1) return id;
            d.Id = id.Value;
            await CacheAsync(d);
            return id;
        }

        public async Task<int> UpdateWithCacheAsync(GameInfo d)
        {
            var id = await _dbConnection.UpdateAsync<GameInfo>(d);
            if (id < 1) return id;
            var cache = await _db.HashGetAsync(GameInfoEntityHash, d.TypeStr);
            await _db.SetRemoveAsync(GameInfoEntitySetHash, cache);
            await CacheAsync(d);
            return id;
        }

        public async Task<IEnumerable<GameInfo>> GetListAsync(GameCategory? g)
        {
            var listStr = await _db.SetMembersAsync(GameInfoEntitySetHash);
            var list = new List<GameInfo>();
            foreach (var ll in listStr)
                list.Add(JsonHelper.JSONToObject<GameInfo>(ll));
            return list;
        }

        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<GameInfo>();
            await _db.KeyDeleteAsync(GameInfoEntitySetHash);
            foreach (var d in list)
                await CacheAsync(d);
        }

    }
}