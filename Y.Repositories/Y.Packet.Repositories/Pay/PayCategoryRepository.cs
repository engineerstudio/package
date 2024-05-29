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
*│　类    名： PayCategoryRepository                                      
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
using System.Collections.Generic;
using System.Linq;
using Y.Infrastructure.Cache.DbCache.Pay;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Helper;


namespace Y.Packet.Repositories.Pay
{
    public class PayCategoryRepository : PayCategoryDbCacheService, IPayCategoryRepository
    {
        public PayCategoryRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
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
            string sql = "update PayCategory set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update PayCategory set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<IEnumerable<string>> GetAllPayTypeAsync()
        {
            string sql = "SELECT PayType FROM PayCategory";
            return await _dbConnection.QueryAsync<string>(sql);
        }

        public async Task<PayCategory> GetFromCacheAsync(int id)
        {
            var dStr = await _db.HashGetAsync(PayCategoryEntityHash, id);
            return JsonHelper.JSONToObject<PayCategory>(dStr);
        }

        public async Task<int?> InsertWithCacheAsync(PayCategory d)
        {
            var id = await _dbConnection.InsertAsync<PayCategory>(d);
            if (id == null || id.Value < 1) return id;

            d.Id = id.Value;
            await _db.HashSetAsync(PayCategoryEntityHash, d.Id, d.ToJson());
            return id;
        }

        public async Task<int> UpdateWithCacheAsync(PayCategory d)
        {
            var id = await _dbConnection.UpdateAsync<PayCategory>(d);
            if (id < 1) return id;

            await _db.HashSetAsync(PayCategoryEntityHash, d.Id, d.ToJson());
            return id;
        }

        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<PayCategory>();
            foreach (var d in list)
                await _db.HashSetAsync(PayCategoryEntityHash, d.Id, d.ToJson());
        }

    }
}