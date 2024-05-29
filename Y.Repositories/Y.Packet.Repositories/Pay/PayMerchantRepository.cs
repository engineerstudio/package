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
*│　创建时间：2020-09-30 16:19:19                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Repositories.Pay                                  
*│　类    名： PayMerchantRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
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
using System.Collections.Generic;
using Y.Infrastructure.Library.Core.Extensions;


namespace Y.Packet.Repositories.Pay
{
    public class PayMerchantRepository : PayMerchantDbService, IPayMerchantRepository
    {
        public PayMerchantRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
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
            string sql = "update PayMerchant set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update PayMerchant set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<PayMerchant> GetFromCacheAsync(int payId)
        {
            string str = await _db.HashGetAsync(PayMerchantEntityHash, payId);
            if (str.IsNullOrEmpty()) return null;
            return JsonHelper.JSONToObject<PayMerchant>(str);
        }

        public async Task<int?> InsertWithCacheAsync(PayMerchant pay)
        {
            var id = await _dbConnection.InsertAsync<PayMerchant>(pay);
            if (id == null || id.Value < 1) return id;
            pay.Id = id.Value;
            await CacheAsync(pay);
            return id;
        }

        private async Task CacheAsync(PayMerchant pay)
        {
            await _db.HashSetAsync(PayMerchantEntityHash, pay.Id, pay.ToJson());
            await _db.SetAddAsync($"{PayMerchantSetEntityHash}{pay.MerchantId}", pay.ToJson());
        }

        public async Task<int> UpdateWithCacheAsync(PayMerchant pay)
        {
            var id = await _dbConnection.UpdateAsync<PayMerchant>(pay);
            if (id < 1) return id;
            if (await _db.HashExistsAsync(PayMerchantEntityHash, pay.Id))
            {
                var d = await _db.HashGetAsync(PayMerchantEntityHash, pay.Id);
                await _db.SetRemoveAsync($"{PayMerchantSetEntityHash}{pay.MerchantId}", d);
                //Console.WriteLine("deldete set here");
            }
            await CacheAsync(pay);
            return id;
        }

        public async Task<List<PayMerchant>> GetListAsync(int merchantId)
        {
            var listStr = await _db.SetMembersAsync($"{PayMerchantSetEntityHash}{merchantId}");
            var list = new List<PayMerchant>();
            foreach (var l in listStr)
                list.Add(JsonHelper.JSONToObject<PayMerchant>(l));
            return list;
        }

        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<PayMerchant>();
            foreach (var mchId in list)
                await _db.KeyDeleteAsync($"{PayMerchantSetEntityHash}{mchId}");

            foreach (var d in list)
                await CacheAsync(d);
        }
    }
}