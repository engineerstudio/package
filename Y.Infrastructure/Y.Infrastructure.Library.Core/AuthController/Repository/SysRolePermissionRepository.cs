using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using Y.Infrastructure.Library.Core.AuthController.Entity;
using Y.Infrastructure.Library.Core.AuthController.IRepository;
using System.Collections.Generic;

namespace Y.Infrastructure.Library.Core.AuthController.Repository
{
    public class SysRolePermissionRepository : BaseRepository<SysRolePermission, Int32>, ISysRolePermissionRepository
    {
        public SysRolePermissionRepository(IOptionsMonitor<DbOption> options)
        {
            _dbOption = options.Get(AuthDefault.SqlConStr);
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }

            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }


        /// <summary>
        /// 通过角色主键获取菜单主键数组
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public int[] GetMenuIdsByRoleId(int RoleId)
        {
            string sql = "select distinct(MenuId) from SysRolePermission where RoleId=@RoleId";
            return _dbConnection.Query<int>(sql, new
            {
                RoleId = RoleId
            }).ToArray();
        }

        /// <summary>
        /// 根据角色主键获取可操作权限的主键数组
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public IEnumerable<int> GetActionIdsByRoleId(int RoleId)
        {
            string sql = "SELECT MenuActionId FROM  SysRolePermission where RoleId=@RoleId";
            return _dbConnection.Query<int>(sql, new
            {
                RoleId = RoleId
            });
        }
    }
}