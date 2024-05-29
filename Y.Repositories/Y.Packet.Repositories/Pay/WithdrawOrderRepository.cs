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
*│　创建时间：2020-10-25 10:39:54                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Repositories.Pay                                  
*│　类    名： WithdrawOrderRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IPay;
using Y.Packet.Entities.Pay;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Y.Packet.Repositories.Pay
{
    public class WithdrawOrderRepository : BaseRepository<WithdrawOrder, Int32>, IWithdrawOrderRepository
    {
        public WithdrawOrderRepository(IOptionsMonitor<DbOption> options)
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
            string sql = "update WithdrawOrder set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update WithdrawOrder set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }


        public async Task<WithdrawOrder> GetFirstFindishedOrderAsync(int userId)
        {
            string sql = $"SELECT TOP 1* FROM {typeof(WithdrawOrder).Name}  WHERE MemberId={userId} AND IsFinish=1";

            return await _dbConnection.QueryFirstOrDefaultAsync<WithdrawOrder>(sql);
        }


        public async Task<(int, decimal)> CreateDailyAgentDataAsync(string date, IEnumerable<int> memberIds)
        {
            string sql = $"SELECT  COUNT(Id) No, ISNULL(SUM(WithdrawAmount),0) Amount  FROM {typeof(WithdrawOrder).Name} WHERE  CONVERT(varchar(100), ConfirmTime, 23)='{date}'  AND IsFinish = 1 AND MemberId IN({string.Join(",", memberIds)})";

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


        public async Task<WithdrawOrder> GetAsync(int merchantId, int orderId)
        {
            string sql = $"SELECT * FROM {typeof(WithdrawOrder).Name}  WHERE MerchantId={merchantId} AND Id={orderId}";

            return await _dbConnection.QueryFirstOrDefaultAsync<WithdrawOrder>(sql);
        }


    }
}