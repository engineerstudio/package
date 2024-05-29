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
using Y.Infrastructure.Library.Core.Helper;

namespace Y.Packet.Repositories.Members
{

    public class UsersBankRepository : UsersBankDbCacheService, IUsersBankRepository
    {
        public UsersBankRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
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
            string sql = "update UsersBank set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update UsersBank set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<IEnumerable<UsersBank>> GetTaskListAsync()
        {
            string sql = $"SELECT * FROM UsersBank WHERE CreateTime > @createTime";
            return await _dbConnection.QueryAsync<UsersBank>(sql, new
            {
                createTime = DateTime.UtcNow.AddHours(8).AddMinutes(-8)
            });
        }

        public async Task<UsersBank> GetFromCacheAsync(int id)
        {
            if (await _db.HashExistsAsync(UsersBankEntityHash, id))
                return JsonHelper.JSONToObject<UsersBank>(await _db.HashGetAsync(UsersBankEntityHash, id));

            string sql = $"SELECT * FROM UsersBank WHERE Id = {id}";
            var d = await _dbConnection.QuerySingleOrDefaultAsync<UsersBank>(sql);
            await _db.HashSetAsync(UsersBankEntityHash, d.Id, d.ToJson());
            return d;
        }

        private async Task CacheAsync(UsersBank d)
        {
            await _db.SetAddAsync($"{UsersBankEntityHash}{d.MerchantId}{d.UserId}", d.ToJson());
            await _db.HashSetAsync(UsersBankEntityHash, d.Id, d.ToJson());
        }


        public async Task<IEnumerable<UsersBank>> GetUsersBanksAsync(int merchantId, int memberId)
        {
            var list = await _db.SetMembersAsync($"{UsersBankEntityHash}{merchantId}{memberId}");
            var ll = new List<UsersBank>();
            foreach (string l in list)
                ll.Add(JsonHelper.JSONToObject<UsersBank>(l));
            return ll;
        }


        public async Task<int?> InsertWithCacheAsync(UsersBank d)
        {
            var id = await _dbConnection.InsertAsync<UsersBank>(d);
            if (id == null || id < 1) return id;
            d.Id = id.Value;
            await CacheAsync(d);
            return id;
        }

        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<UsersBank>();
            foreach (var d in list)
                await CacheAsync(d);
        }


    }
}