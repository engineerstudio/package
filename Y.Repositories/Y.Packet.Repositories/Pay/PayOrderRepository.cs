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
*│　创建时间：2020-10-16 23:12:38                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Repositories.Pay                                  
*│　类    名： PayOrderRepository                                      
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
using System.Data;
using System.Linq;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Cache.DbCache.Pay;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;

namespace Y.Packet.Repositories.Pay
{
    public class PayOrderRepository : PayOrderDbService, IPayOrderRepository
    {
        public PayOrderRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
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
            string sql = "update PayOrder set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update PayOrder set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }


        public async Task<Int32?> InsertByTransAsync(PayOrder payOrder)
        {
            using (var trans = _dbConnection.BeginTransaction())
            {
                var rt = await _dbConnection.InsertAsync(payOrder, trans);
                trans.Commit();
                return rt;
            }
        }

        /// <summary>
        /// 获取日统计代理数据
        /// </summary>
        /// <param name="date"></param>
        /// <param name="memberIds"></param>
        /// <returns></returns>
        public async Task<(int, decimal)> CreateDailyAgentDataAsync(string date, IEnumerable<int> memberIds)
        {
            string sql = $"SELECT  COUNT(Id) No,  ISNULL(SUM(DepositAmount),0) Amount  FROM PayOrder WHERE  CONVERT(varchar(100), ConfirmTime, 23)='{date}'  AND IsFinish = 1 AND MemberId IN({string.Join(",", memberIds)})";

            using (var reader = await _dbConnection.ExecuteReaderAsync(sql))
            {
                DataTable table = new DataTable();
                table.Load(reader);
                // 或者使用自定义类  DataTableHelper  定义个实体进行转换
                //reader.Close();
                var row0 = table.Rows.Cast<DataRow>().ToList()[0];
                if (row0[0] != System.DBNull.Value)
                {
                    return ((int)row0[0], (decimal)row0[1]);
                }
                else
                    return (0, 0);
            }
        }


        public async Task<IEnumerable<PayOrder>> GetTaskListAsync()
        {
            string sql = $"SELECT * FROM PayOrder WHERE IsFinish =1 AND ConfirmTime > @confirmTime";
            return await _dbConnection.QueryAsync<PayOrder>(sql, new
            {
                confirmTime = DateTime.UtcNow.AddHours(8).AddMinutes(-2)
            });
        }

        /// <summary>
        /// [周签到活动] 获取上周总充值金额
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<decimal> GetTaskRechargeAmountAsync(int merchantId, int userId)
        {
            string sql = $"SELECT ISNULL(SUM(ReqDepositAmount),0) FROM PayOrder WHERE IsFinish =1 AND MerchantId={merchantId} AND MemberId={userId} AND ConfirmTime BETWEEN @confirmStartTime AND @confirmEndTime";

            return await _dbConnection.ExecuteScalarAsync<decimal>(sql, new
            {
                confirmStartTime = DateTime.Now.GetLastWeekMondayDate(),
                confirmEndTime = DateTime.Now.GetThisWeekMondayDate(),
            });
        }


    }
}