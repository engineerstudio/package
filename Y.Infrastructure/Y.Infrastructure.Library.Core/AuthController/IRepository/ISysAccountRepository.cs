using System;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Infrastructure.Library.Core.AuthController.Entity;

namespace Y.Infrastructure.Library.Core.AuthController.IRepository
{
    public interface ISysAccountRepository : IBaseRepository<SysAccount, Int32>
    {
        /// <summary>
        /// 逻辑删除返回影响的行数（异步操作）
        /// </summary>
        /// <param name="ids">需要删除的主键数组</param>
        /// <returns>影响的行数</returns>
        Task<Int32> DeleteLogicalAsync(Int32[] ids);

        Task<bool> IsExistAccountName(string accountName);
        Task<Boolean> GetLockStatusByIdAsync(int id);

        /// <summary>
        /// 修改锁定状态
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="status">更改后的状态</param>
        /// <returns></returns>
        Task<Int32> ChangeLockStatusByIdAsync(Int32 id, bool status);

        /// <summary>
        /// 通过主键获取密码
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        Task<string> GetPasswordByIdAsync(Int32 Id);

        /// <summary>
        /// 通过主键获取密码
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        Task<int> ChangePasswordByIdAsync(Int32 Id, string Password);

        Task<SysAccount> GetManagerContainRoleNameByIdAsync(int id);


        /// <summary>
        /// 根据账户ID获取账户名称
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        string GetAccountName(int accountId);


        /// <summary>
        /// 根据账户名称获取账户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<SysAccount> GetSysAccountByName(string name);
    }
}