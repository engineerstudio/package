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
*│　创建时间：2020-09-17 23:49:49                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Repositories.Vips                                  
*│　类    名： VipGroupsRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Y.Infrastructure.Library.Core.DbHelper;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Entities.Vips;
using Y.Packet.Repositories.IVips;
using Y.Infrastructure.Cache.DbCache.Vips;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Helper;
using System.Collections.Generic;
using System.Linq;

namespace Y.Packet.Repositories.Vips
{
    public class VipGroupsRepository : VipGroupsDbCacheService, IVipGroupsRepository
    {
        public VipGroupsRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {
            _dbOption = options.Get("Ying.Vips");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        public int DeleteLogical(int[] ids)
        {
            string sql = "update VipGroups set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update VipGroups set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> ExistGroupNameAsync(int merchantId, string name)
        {
            string sql = $"SELECT COUNT(*) FROM VipGroups WHERE MerchantId={merchantId} AND GroupName=@GroupName";

            return await _dbConnection.ExecuteScalarAsync<int>(sql, new { GroupName = name });
        }

        public async Task<(bool, VipGroups)> ExistDefaultGroupAsync(int merchantId)
        {
            string sql = $"SELECT * FROM VipGroups WHERE MerchantId={merchantId} AND IsDefault=1";
            VipGroups rt = await _dbConnection.QueryFirstOrDefaultAsync<VipGroups>(sql);
            return ((rt != null), rt);
        }

        public async Task<int> GetDefaultGroupIdAsync(int merchantId)
        {
            string sql = $"SELECT ISNULL(Id,0) Id FROM VipGroups WHERE MerchantId={merchantId} AND IsDefault=1";
            //Console.WriteLine(sql);
            return await _dbConnection.ExecuteScalarAsync<int>(sql);
        }

        public async Task<VipGroups> GetAsync(int merchantId, int groupId)
        {
            string sql = $"SELECT * FROM VipGroups WHERE  MerchantId={merchantId} AND Id={groupId}";
            VipGroups rt = await _dbConnection.QueryFirstOrDefaultAsync<VipGroups>(sql);
            return rt;
        }

        public async Task<int?> InsertWithCacheAsync(VipGroups d)
        {
            var id = await _dbConnection.InsertAsync<VipGroups>(d);
            d.Id = id.Value;
            await CacheAsync(d);
            return id;
        }

        private async Task CacheAsync(VipGroups d)
        {
            await _db.HashSetAsync(VipGroupsHashEntity, d.Id, d.ToJson(), StackExchange.Redis.When.Always);
            await _db.SetAddAsync($"{VipGroupsHashEntity}{d.MerchantId}", d.ToJson());
            if (d.Enabled)
                await _db.HashSetAsync($"{VipGroupsIdAndNameDicHashEntity}{d.MerchantId}", d.Id, d.GroupName, StackExchange.Redis.When.Always);
            else
                await _db.HashDeleteAsync($"{VipGroupsIdAndNameDicHashEntity}{d.MerchantId}", d.Id);
        }

        public async Task<int> UpdateWithCacheAsync(VipGroups d)
        {
            var rt = await _dbConnection.UpdateAsync<VipGroups>(d);
            if (rt < 1) return rt;

            var c = await _db.HashGetAsync(VipGroupsHashEntity, d.Id);
            await _db.SetRemoveAsync($"{VipGroupsHashEntity}{d.MerchantId}", c);
            await CacheAsync(d);

            return rt;
        }

        public async Task<VipGroups> GetFromCacheAsync(int id)
        {
            var dstr = await _db.HashGetAsync(VipGroupsHashEntity, id);
            var d = JsonHelper.JSONToObject<VipGroups>(dstr);
            return d;
        }

        public async Task<List<VipGroups>> GetListFromCacheAsync(int merchantId)
        {
            var strList = await _db.SetMembersAsync($"{VipGroupsHashEntity}{merchantId}");
            var list = new List<VipGroups>();
            foreach (var str in strList)
                list.Add(JsonHelper.JSONToObject<VipGroups>(str));
            return list;
        }

        public async Task<Dictionary<int, string>> GetIdAndNameDicAsync(int merchantId) => await base.HashGetDic3Async($"{VipGroupsIdAndNameDicHashEntity}{merchantId}");



        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<VipGroups>();
            foreach (var mchId in list.Select(t => t.MerchantId))
                await _db.KeyDeleteAsync($"{VipGroupsHashEntity}{mchId}");
            foreach (var d in list)
                await CacheAsync(d);
        }
    }
}