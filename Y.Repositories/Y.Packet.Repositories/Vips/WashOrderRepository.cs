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
*│　创建时间：2021-05-05 18:55:02                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Repositories.Vips                                  
*│　类    名： WashOrderRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IVips;
using Y.Packet.Entities.Vips;

namespace Y.Packet.Repositories.Vips
{
    public class WashOrderRepository : BaseRepository<WashOrder, Int32>, IWashOrderRepository
    {
        public WashOrderRepository(IOptionsMonitor<DbOption> options)
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
            string sql = "update WashOrder set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update WashOrder set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<bool> IsFinishedAllWashOrderAsync(int memeberId)
        {
            string sql = $"SELECT COUNT(1) FROM [WashOrder] WHERE MemberId = {memeberId} AND Ended = 0 "; // 获取到未完成打码的数量
            var rt = await _dbConnection.ExecuteScalarAsync<int>(sql);
            return rt > 0;
        }


        /// <summary>
        /// 获取到未完成的打码量
        /// </summary>
        public async Task<decimal> GetUnfinishedAmountAsync(int merchantId, int userId)
        {
            // 获取到所有未完成的打码的余额
            string sql = $"SELECT  ISNULL(SUM(WashAmount),0)  FROM WashOrder WHERE MemberId={userId} AND Ended=0";
            var totalAmount = _dbConnection.ExecuteScalar<decimal>(sql);
            // 获取到已经开始打码的余额
            // 1.1获取到已经开始打码的Id
            string sql_top_order_id = $"SELECT TOP 1 Id,WashAmount FROM [WashOrder] WHERE  MemberId = {userId} AND Ended = 0 ORDER BY ID ASC";
            var order = await _dbConnection.QueryFirstAsync<IdWashAmount>(sql_top_order_id);
            // 1.2获取已打码余额
            string sql_balance = $"SELECT TOP 1 Balance from [dbo].[WashOrderDetail] WHERE  MemberId =  {userId}  AND OrderId = {order.Id} ORDER BY Id DESC";
            var finished_balance =  _dbConnection.ExecuteScalar<decimal>(sql_balance);
            return totalAmount - (order.WashAmount - finished_balance);
        }
        class IdWashAmount
        {
            public int Id { get; set; }
            public Decimal WashAmount { get; set; }
        }

        public async Task<int> UpdateOrderStatusToFinishAsync(int orderId)
        {
            string sql = $"UPDATE WashOrder SET Ended =1 WHERE Id={orderId}";

            return await _dbConnection.ExecuteAsync(sql);
        }

    }
}