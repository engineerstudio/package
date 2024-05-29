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
*│　创建时间：2021-01-10 19:22:45                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Repositories.Pay                                  
*│　类    名： WithdrawMerchantRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Y.Infrastructure.Library.Core.DbHelper;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Entities.Pay;
using Y.Packet.Repositories.IPay;
using Y.Infrastructure.Cache.DbCache.Pay;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Y.Packet.Repositories.Pay
{
    public class WithdrawMerchantRepository : WithdrawMerchantDbCacheService, IWithdrawMerchantRepository
    {
        public WithdrawMerchantRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {
            _dbOption = options.Get("Ying.Pay");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        public int DeleteLogical(int[] ids)
        {
            string sql = "update WithdrawMerchant set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update WithdrawMerchant set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<WithdrawMerchant> GetByIdAsync(int merchantId, int id)
        {
            string sql = $"SELECT * FROM WithdrawMerchant WHERE MerchantId={merchantId} AND Id={id}";

            return await _dbConnection.QuerySingleOrDefaultAsync<WithdrawMerchant>(sql);

        }

        public async Task<WithdrawMerchant> ExistAsync(int merchantId, string typeStr)
        {
            string sql = $"SELECT * FROM WithdrawMerchant WHERE  MerchantId={merchantId} AND TypeStr='{typeStr}' ";
            return await _dbConnection.QuerySingleOrDefaultAsync<WithdrawMerchant>(sql);
        }

        public async Task<int?> InsertWithCacheAsync(WithdrawMerchant d)
        {
            var id = await _dbConnection.InsertAsync<WithdrawMerchant>(d);
            if (id == null || id.Value < 1) return id;
            d.Id = id.Value;
            await CachAsync(d);
            return id;
        }

        private async Task CachAsync(WithdrawMerchant d)
        {
            //await _db.HashSetAsync(WithdrawMerchantEntityHash, $"{d.MerchantId}{d.Id}", d.ToJson());
            await _db.HashSetAsync(WithdrawMerchantEntityHash, d.Id, d.ToJson());
            await _db.SetAddAsync($"{WithdrawMerchantEntityHash}{d.MerchantId}", d.ToJson());
        }

        public async Task<int> UpdateWithCacheAsync(WithdrawMerchant d)
        {
            var id = await _dbConnection.UpdateAsync<WithdrawMerchant>(d);
            if (id < 1) return id;
            var cache = await _db.HashGetAsync(WithdrawMerchantEntityHash, d.Id);
            await _db.SetRemoveAsync($"{WithdrawMerchantEntityHash}{d.MerchantId}", cache);
            await CachAsync(d);
            return id;
        }

        public async Task<WithdrawMerchant> GetFromCacheAsync(int id)
        {
            string v = await _db.HashGetAsync(WithdrawMerchantEntityHash, id.ToString());
            if (v.IsNullOrEmpty()) return null;
            return JsonHelper.JSONToObject<WithdrawMerchant>(v);
        }

        public async Task<List<WithdrawMerchant>> GetListAsync(int merchantId)
        {
            string key = $"{WithdrawMerchantEntityHash}{merchantId}";
            var listStrs = await _db.SetMembersAsync(key);
            var list = new List<WithdrawMerchant>();
            foreach (var l in listStrs)
                list.Add(JsonHelper.JSONToObject<WithdrawMerchant>(l));
            return list;
        }


        public async Task<WithdrawMerchant> GetFromCacheAsync(int merchantId, int id)
        {
            //string key = $"{merchantId}{id}";
            //string v = await _db.HashGetAsync(WithdrawMerchantEntityHash, key);
            //if (v.IsNullOrEmpty()) return null;
            //return JsonHelper.JSONToObject<WithdrawMerchant>(v);
            return null;
        }
        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<WithdrawMerchant>();
            foreach (var mchId in list.Select(t => t.MerchantId))
                await _db.KeyDeleteAsync($"{WithdrawMerchantEntityHash}{mchId}");
            foreach (var d in list)
                await CachAsync(d);
        }
    }
}