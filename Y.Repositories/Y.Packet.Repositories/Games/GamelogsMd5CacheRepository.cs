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
*│　创建时间：2020-09-13 23:23:32                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Repositories.Games                                  
*│　类    名： GamelogsMd5CacheRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Y.Infrastructure.Library.Core.DbHelper;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

using Y.Packet.Repositories.IGames;
using Y.Infrastructure.Cache.DbCache.Games;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Helper;
using Y.Packet.Entities.Games;

namespace Y.Packet.Repositories.Games
{
    public class GamelogsMd5CacheRepository : GamelogsMd5CacheDbCacheService, IGamelogsMd5CacheRepository
    {
        public GamelogsMd5CacheRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
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
            string sql = "update GamelogsMd5Cache set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update GamelogsMd5Cache set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<GamelogsMd5Cache?> GetFromCacheAsync(string typeStr, string sourceId)
        {
            if (!(await _db.HashExistsAsync(GamelogsMd5CacheEntityHash, $"{typeStr}{sourceId}"))) return null;
            var cache = await _db.HashGetAsync(GamelogsMd5CacheEntityHash, $"{typeStr}{sourceId}");
            return JsonHelper.JSONToObject<GamelogsMd5Cache>(cache);
        }

        private async Task CacheAsync(GamelogsMd5Cache d)
        {
            await _db.HashSetAsync(GamelogsMd5CacheEntityHash, $"{d.GameTypeStr}{d.SourceId}", d.ToJson(), StackExchange.Redis.When.Always);
        }

        public async Task<int?> InsertWithCacheAsync(GamelogsMd5Cache d)
        {
            Console.WriteLine(_dbConnection.State.ToString());
            if(_dbConnection.State != System.Data.ConnectionState.Open) _dbConnection.Open();
            var id = await _dbConnection.InsertAsync<GamelogsMd5Cache>(d);
            if (id == null || id < 1) return id;
            d.Id = id.Value;
            await CacheAsync(d);
            return id;
        }

        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<GamelogsMd5Cache>();
            foreach (var d in list)
                await CacheAsync(d);
        }

    }
}