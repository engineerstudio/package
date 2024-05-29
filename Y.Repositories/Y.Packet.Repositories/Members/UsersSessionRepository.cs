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
*│　创建时间：2020-10-18 22:53:28                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Users.Repository                                  
*│　类    名： UsersSessionRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Entities.Members;
using Y.Packet.Repositories.IMembers;
using Y.Infrastructure.Cache.DbCache.Members;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Helper;
using StackExchange.Redis;
using Y.Infrastructure.Library.Core.Extensions;

namespace Y.Packet.Repositories.Members
{
    public class UsersSessionRepository : UsersSessionDbCacheService, IUsersSessionRepository
    {
        public UsersSessionRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {
            _dbOption = options.Get("Ying.Users");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        public int DeleteLogical(int[] ids)
        {
            string sql = "update UsersSession set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update UsersSession set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteWithCacheAsync(int sessionId)
        {
            var d = await _dbConnection.GetAsync<UsersSession>(sessionId);
            await _db.KeyDeleteAsync($"{UsersSessionStringSetKey}{d.Session}");
            await _db.KeyDeleteAsync($"{UsersSessionStringSetKey}{d.UserId}");
            return 1;
        }


        public async Task<int> GetSessionIdAsync(int memberId)
        {
            //string sql = $"SELECT Id FROM [UsersSession] WHERE UserId={memberId}";
            //return await _dbConnection.ExecuteScalarAsync<int>(sql);
            string key = $"{UsersSessionStringSetKey}{memberId}";
            if (!(await _db.KeyExistsAsync(key))) return 0;
            return (await _db.StringGetAsync(key)).To<int>();
        }



        public async Task<int> GetUserIdAsync(string session)
        {
            //string sql = $"SELECT UserId FROM [UsersSession] WHERE Session='{session}'";
            //return await _dbConnection.ExecuteScalarAsync<int>(sql);
            string key = $"{UsersSessionStringSetKey}{session}";
            if (!(await _db.KeyExistsAsync(key))) return 0;
            return (await _db.StringGetAsync(key)).To<int>();
        }

        public async Task<int?> InsertWithCacheAsync(UsersSession d)
        {
            var id = await _dbConnection.InsertAsync<UsersSession>(d);
            if (id == null || id < 1) return id;
            d.Id = id.Value;
            await CacheAsync(d);
            return id;
        }

        private async Task CacheAsync(UsersSession d)
        {
            await _db.StringSetAsync($"{UsersSessionStringSetKey}{d.Session}", d.UserId);
            await _db.StringSetAsync($"{UsersSessionStringSetKey}{d.UserId}", d.Id);
        }
        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<UsersSession>();
            foreach (var d in list)
                await CacheAsync(d);
        }
    }
}