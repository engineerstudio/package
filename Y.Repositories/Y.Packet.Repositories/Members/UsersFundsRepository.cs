using Y.Infrastructure.Library.Core.DbHelper;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Entities.Members;
using Y.Packet.Repositories.IMembers;
using Y.Infrastructure.Cache.DbCache.Members;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.CacheFactory.Utils;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Packet.Entities.Members.RedisDb;

namespace Y.Packet.Repositories.Members
{
    public class UsersFundsRepository : UsersFundsDbCacheService, IUsersFundsRepository
    {
        public UsersFundsRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {
            _dbOption = options.Get("Ying.Users");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        public async Task<decimal> GetUserAvailableFundsAsync(int merchantId, int userId)
        {
            //string sql = $"SELECT (TotalFunds - LockFunds) Balance FROM UsersFunds WHERE UserId = {userId}";
            //return await _dbConnection.ExecuteScalarAsync<decimal>(sql);

            var d1 = await _db.HashGetAsync($"{UsersFundsEntityHash}{merchantId}{userId}", "TotalFunds");
            var d2 = await _db.HashGetAsync($"{UsersFundsEntityHash}{merchantId}{userId}", "LockFunds");

            return (d1.To<long>() - d2.To<long>()) / 10000;
        }

        public async Task<decimal> GetUserFundsAsync(int merchantId, int userId)
        {
            //string sql = $"SELECT TotalFunds Balance FROM UsersFunds WHERE UserId = {userId}";
            //return await _dbConnection.ExecuteScalarAsync<decimal>(sql, null);

            var d = await _db.HashGetAsync($"{UsersFundsEntityHash}{merchantId}{userId}", "TotalFunds");
            return d.To<long>() / 10000;
        }

        public async Task<decimal> GetUserLockFundsAsync(int merchantId, int userId)
        {
            //string sql = $"SELECT LockFunds FROM UsersFunds WHERE UserId = {userId}";
            //return await _dbConnection.ExecuteScalarAsync<decimal>(sql);
            var d = await _db.HashGetAsync($"{UsersFundsEntityHash}{merchantId}{userId}", "LockFunds");
            return d.To<long>() / 10000;
        }

        public async Task<bool> UpdateUserFundsAsync(UsersFunds uFunds, UsersFundsLog log)
        {
            using (var trans = _dbConnection.BeginTransaction())
            {
                int? rt = await _dbConnection.UpdateAsync(uFunds, trans);
                if (rt == 0)
                {
                    trans.Rollback();
                    return false;
                }

                rt = await _dbConnection.InsertAsync(log, trans);
                if (rt == null)
                {
                    trans.Rollback();
                    return false;
                }

                await CacheAsync(uFunds);

                trans.Commit();
                return true;
            }
        }

        public async Task<UsersFunds> GetAsync(int merchantId, int memberId)
        {
            //string sql = $"SELECT * FROM UsersFunds WHERE MerchantId={merchantId} AND UserId = {memberId}";
            //var m = (await _dbConnection.QuerySingleOrDefaultAsync<UsersFunds>(sql));
            var mStr = await _db.HashGetAllAsync($"{UsersFundsEntityHash}{merchantId}{memberId}");
            if (mStr == null)
            {
                UsersFunds m = new UsersFunds() { MerchantId = merchantId, UserId = memberId };
                memberId = (await InsertWithCacheAsync(m)).Value;
                //var rt = await _dbConnection.InsertAsync(m);
                //m.Id = rt.Value;
            }

            var d = await base.HashGetObjAsync<UsersFundsDto>($"{UsersFundsEntityHash}{merchantId}{memberId}");
            return d.ToEntity();
        }

        public async Task<int?> InsertWithCacheAsync(UsersFunds d)
        {
            //UsersFundsEntityHash
            var id = await _dbConnection.InsertAsync<UsersFunds>(d);
            if (id == null || id < 1) return id;
            d.Id = id.Value;
            await CacheAsync(d);
            return id;
        }

        private async Task CacheAsync(UsersFunds d)
        {
            //Console.WriteLine(d.ToJson());
            var dto = new UsersFundsDto()
            {
                Id = d.Id,
                MerchantId = d.MerchantId,
                UserId = d.UserId,
                TotalFunds = (d.TotalFunds * 10000).To<long>(),
                LockFunds = (d.LockFunds * 10000).To<long>(),
                TotalRechargedFunds = (d.TotalRechargedFunds * 10000).To<long>(),
                TotalRechargedFundsCount = d.TotalRechargedFundsCount,
                TotalWithdrawalFunds = (d.TotalWithdrawalFunds * 10000).To<long>(),
                TotalWithdrawalCount = d.TotalWithdrawalCount,
                TotalBetFunds = (d.TotalBetFunds * 10000).To<long>(),
                TotalProfitAndLoss = (d.TotalProfitAndLoss * 10000).To<long>(),
                OtherFunds = (d.OtherFunds * 10000).To<long>()
            };
            await _db.HashSetAsync($"{UsersFundsEntityHash}{d.MerchantId}{d.UserId}", dto.ToHashEntries());//HashGetObjAsync

        }

        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<UsersFunds>();
            foreach (var d in list)
                await CacheAsync(d);
        }
    }
}