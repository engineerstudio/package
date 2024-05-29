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
*│　描    述：用户层级关系接口实现                                                    
*│　作    者：Aaron                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-08-30 15:00:55                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Repositories.Members                                  
*│　类    名： UserHierarchyRepository                                      
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
using System.Collections.Generic;
using Y.Infrastructure.Cache.DbCache.Members;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;

using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Microsoft.Extensions.Logging;

namespace Y.Packet.Repositories.Members
{
    public class UserHierarchyRepository : UserHierarchyDbCacheService, IUserHierarchyRepository
    {
        public UserHierarchyRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
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
            string sql = "update UserHierarchy set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update UserHierarchy set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }


        public async Task<int> ExistAsync(int siteId, int agentId, int userId)
        {
            string sql = $"SELECT COUNT(*)  FROM UserHierarchy WHERE SiteId = {siteId} AND UserId = {agentId}  AND SubId = {userId} ";
            return await _dbConnection.ExecuteScalarAsync<int>(sql);

        }

        //public async Task<IEnumerable<int>> GetAsync(int siteId, int agentId)
        //{
        //    string sql = $"SELECT SubId  FROM UserHierarchy WHERE SiteId = {siteId} AND UserId = {agentId} ";
        //    return await _dbConnection.QueryAsync<int>(sql);
        //}

        public async Task<IEnumerable<int>> GetSubMemberIdsFromCacheAsync(int merchantId, int agentId)
        {
            var list = await _db.SetMembersAsync($"{UserHierarchySubsHash}{merchantId}{agentId}");
            var ll = new List<int>();
            foreach (string l in list)
                ll.Add(l.ToInt32().Value);
            return ll;
        }

        public async Task<IEnumerable<UserHierarchy>> GetAgentMemberIdsFromCacheAsync(int merchantId, int subId)
        {
            var list = await _db.SetMembersAsync($"{UserHierarchySubsHash}{merchantId}{subId}");
            var ll = new List<UserHierarchy>();
            foreach (string l in list)
                ll.Add(JsonHelper.JSONToObject<UserHierarchy>(l));
            return ll;
        }

        public async Task<int?> InsertWithCacheAsync(UserHierarchy d)
        {
            var id = await _dbConnection.InsertAsync<UserHierarchy>(d);
            if (id == null || id < 1) return id;
            d.Id = id.Value;
            await CacheAsync(d);
            return id;
        }

        private async Task CacheAsync(UserHierarchy d)
        {
            await _db.SetAddAsync($"{UserHierarchySubsHash}{d.SiteId}{d.UserId}", d.SubId);
            await _db.SetAddAsync($"{UserHierarchySubsHash}{d.SiteId}{d.SubId}", d.ToJson());
        }


        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<UserHierarchy>();
            foreach (var d in list)
                await CacheAsync(d);
        }
    }
}