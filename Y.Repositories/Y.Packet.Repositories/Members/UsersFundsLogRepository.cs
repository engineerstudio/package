using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IMembers;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Members;

namespace Y.Packet.Repositories.Members
{
    public class UsersFundsLogRepository : BaseRepository<UsersFundsLog, Int32>, IUsersFundsLogRepository
    {
        public UsersFundsLogRepository(IOptionsMonitor<DbOption> options)
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
            string sql = "update UsersFundsLog set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update UsersFundsLog set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async  Task<bool> ExistFundsLogSourceIdAsync(int merchantId, int userId, string sourceId, TransType transType)
        {
            string sql = $"SELECT COUNT(1) c FROM UsersFundsLog WHERE MerchantId={merchantId} AND MemberId={userId} AND SourceId='{sourceId}' AND TransType='{transType.GetEnumValue()}' ";
            return await _dbConnection.ExecuteScalarAsync<int>(sql, null) > 0;
        }

        public async Task<bool> ExistFundsLogSourceIdAsync(int merchantId, int userId, string sourceId, FundLogType fundLogType)
        {
            string sql = $"SELECT COUNT(1) c FROM UsersFundsLog WHERE MerchantId={merchantId} AND MemberId={userId} AND SourceId='{sourceId}' AND FundsType='{fundLogType.GetEnumValue()}' ";
            return await _dbConnection.ExecuteScalarAsync<int>(sql, null) > 0;
        }

        /// <summary>
        /// 转账  加减款   游戏转入转出
        /// </summary>
        /// <param name="log"></param>
        /// <param name="transSucess">是否直接转入成功/转账等待</param>
        /// <returns></returns>
        public async Task<bool> InsertByTransAsync(UsersFundsLog log, bool transSucess = true)
        {
            using (var trans = _dbConnection.BeginTransaction(System.Data.IsolationLevel.RepeatableRead))
            {

                // 2. 操作  用户金额表
                var userfunds = await _dbConnection.QueryFirstOrDefaultAsync<UsersFunds>($"SELECT TOP 1* FROM UsersFunds WHERE MemberId={log.MemberId}", null, trans);
                if (userfunds == null) throw new Exception("用户初始化数据失败");

                if (transSucess) // 转账成功
                    userfunds.TotalFunds = userfunds.TotalFunds + log.Amount;
                else
                    userfunds.LockFunds = userfunds.LockFunds + Math.Abs(log.Amount); //  转账失败/转账等待

                int? rt = await _dbConnection.UpdateAsync(userfunds, trans);
                if (rt == null || !rt.HasValue)
                {
                    trans.Rollback();
                    return false;
                }

                // 1. 操作  日志表
                var lastlog = await _dbConnection.QueryFirstOrDefaultAsync<UsersFundsLog>("SELECT TOP 1* FROM UsersFundsLog ORDER BY CreateTime DESC", null, trans);
                if (lastlog == null) lastlog = new UsersFundsLog();
                // 转账成功
                if (transSucess) // 转账成功
                    log.LockedAmount = lastlog.LockedAmount; // 转账成功，锁定金额不变
                else
                    log.LockedAmount = lastlog.LockedAmount + Math.Abs(log.Amount); // 转账失败/转账等待，增加锁定金额

                log.Balance = userfunds.TotalFunds;  // 操作用户余额

                rt = await _dbConnection.InsertAsync(log, trans);
                if (rt == null && !rt.HasValue)
                {
                    trans.Rollback();
                    return false;
                }



                if (rt.Value > 0) trans.Commit();
                return true;
            }
        }




        /// <summary>
        /// 获取过去三个月的用户充值总额
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<decimal> GetAccountLastThreeMonthsRechargeMoneyAsync(int merchantId, int userId)
        {
            string sql = $"SELECT SUM(Amount) FROM UsersFundsLog WHERE  MemberId={userId} AND MerchantId = {merchantId} AND FundsType = {(int)FundLogType.Recharge} AND  CreateTime > '{DateTime.UtcNow.AddHours(8)}'";
            return (await _dbConnection.ExecuteScalarAsync<decimal>(sql));
        }


        /// <summary>
        /// 判断日志是否存在
        /// </summary>
        /// <param name="logId"></param>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <param name="gameType"></param>
        /// <returns></returns>
        public async Task<int> ExistLogAsync(int logId, int merchantId, int memberId, GameType gameType)
        {
            string sql = $"SELECT COUNT(1) FROM {typeof(UsersFundsLog).Name} WHERE MerchantId={merchantId} AND MemberId={merchantId} AND FundsType={FundLogType.Games.GetEnumValue()} AND SubFundsType={gameType.TransToFundLogType().GetEnumValue()} AND SourceId='{logId}'";

            return (await _dbConnection.ExecuteScalarAsync<int>(sql));

        }



    }
}