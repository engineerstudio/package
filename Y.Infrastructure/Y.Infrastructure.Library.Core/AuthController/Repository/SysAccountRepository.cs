using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.AuthController.Entity;
using Y.Infrastructure.Library.Core.AuthController.IRepository;

namespace Y.Infrastructure.Library.Core.AuthController.Repository
{
    public class SysAccountRepository : BaseRepository<SysAccount, Int32>, ISysAccountRepository
    {
        public SysAccountRepository(IOptionsMonitor<DbOption> options)
        {
            _dbOption = options.Get(AuthDefault.SqlConStr);
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }

            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }


        public async Task<bool> GetLockStatusByIdAsync(int id)
        {
            string sql = "select IsLock from [dbo].[SysAccount] where Id=@Id and IsDelete=0";
            return await _dbConnection.QueryFirstOrDefaultAsync<bool>(sql, new
            {
                Id = id,
            });
        }

        /// <summary>
        /// 是否存在账户名称
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        public async Task<bool> IsExistAccountName(string accountName)
        {
            //string sql = "SELECT 1 FROM SysAccount WHERE Name = @Name";
            return await _dbConnection.RecordCountAsync<SysAccount>(" WHERE Name = @Name ", new
            {
                Name = accountName
            }) > 0;
        }


        public async Task<int> ChangeLockStatusByIdAsync(int id, bool status)
        {
            string sql = "update [SysAccount] set IsLock=@IsLock where  Id=@Id";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                IsLock = status ? 1 : 0,
                Id = id,
            });
        }

        public async Task<string> GetPasswordByIdAsync(Int32 Id)
        {
            string sql = "select Password from SysAccount where Id=@Id and IsDelete=0";
            return await _dbConnection.QueryFirstOrDefaultAsync<string>(sql, new
            {
                Id = Id,
            });
        }


        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update [SysAccount] set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> ChangePasswordByIdAsync(Int32 Id, string Password)
        {
            string sql = "update SysAccount set Password=@Password where Id = @Id";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Password = Password,
                Id = Id
            });
        }

        public async Task<SysAccount> GetManagerContainRoleNameByIdAsync(int id)
        {
            string sql =
                @"SELECT   mr.RoleName, m.Id, m.RoleId, m.UserName, m.Password, m.Avatar, m.NickName, m.Mobile, m.Email, m.LoginCount, 
                m.LoginLastIp, m.LoginLastTime, m.AddManagerId, m.AddTime, m.ModifyManagerId, m.ModifyTime, m.IsLock, 
                m.IsDelete, m.Remark
FROM      SysAccount AS m INNER JOIN
                SysRole AS mr ON m.RoleId = mr.Id where m.Id=@Id and m.IsDelete=0 ";
            return await _dbConnection.QueryFirstOrDefaultAsync<SysAccount>(sql, new
            {
                Id = id
            });
        }


        /// <summary>
        /// 根据账户ID获取账户名称
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public string GetAccountName(int accountId)
        {
            string sql = $"SELECT Name FROM SysAccount WHERE Id={accountId}";
            return _dbConnection.ExecuteScalar<string>(sql);
        }


        /// <summary>
        /// 根据账户名称获取账户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<SysAccount> GetSysAccountByName(string name)
        {
            string sql = $"SELECT Name FROM SysAccount WHERE Name=@Name";
            return await _dbConnection.QueryFirstOrDefaultAsync<SysAccount>(sql, new {Name = name});
        }
    }
}