////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //

using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Y.Infrastructure.Library.Core.AuthController.Entity;
using Y.Infrastructure.Library.Core.AuthController.IRepository;
using System.Text;

namespace Y.Infrastructure.Library.Core.AuthController.Repository
{
    public class SysRoleRepository : BaseRepository<SysRole, Int32>, ISysRoleRepository
    {
        public SysRoleRepository(IOptionsMonitor<DbOption> options)
        {
            _dbOption = options.Get(AuthDefault.SqlConStr);
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }

            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }


        public int DeleteLogical(int[] ids)
        {
            string sql = "update SysRole set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update SysRole set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public string GetNameById(int id)
        {
            var item = Get(id);
            return item == null ? "角色不存在" : item.RoleName;
        }


        public IEnumerable<SysMenu> GetMenusByRoleId(int roleId, string sysStr)
        {
            string sql =
                @"SELECT   m.Id, m.ParentId, m.Name, m.DisplayName, m.IconClass, m.LinkUrl, m.Sort, rp.MenuActionId, m.IsDisplay, m.IsSystem, 
                m.CreatedId, m.CreatedTime, m.ModifyId, m.ModifyTime, m.IsDelete
                                                FROM      SysRolePermission AS rp INNER JOIN
                                                                SysMenu AS m ON rp.MenuId = m.Id
                                                WHERE   (rp.RoleId = @RoleId) AND (m.IsDelete = 0) AND (m.IsDisplay = 1) AND m.SysStr=@SysStr";
            return _dbConnection.Query<SysMenu>(sql, new
            {
                RoleId = roleId,
                SysStr = sysStr
            });
        }

        public int? InsertByTrans(SysRole model)
        {
            int? roleId = 0;
            string insertMenuPermissionSql = @"INSERT INTO SysRolePermission (RoleId, MenuId, MenuActionId) VALUES (@RoleId,@MenuId, 0)";
            string insertActionsPermissionSql = @"INSERT INTO SysRolePermission (RoleId, MenuId, MenuActionId) VALUES (@RoleId,0,@ActionId)";
            using (var tran = _dbConnection.BeginTransaction())
            {
                try
                {
                    roleId = _dbConnection.Insert(model, tran);
                    if (roleId > 0 && model.MenuIds?.Count() > 0)
                    {
                        foreach (var item in model.MenuIds)
                        {
                            _dbConnection.Execute(insertMenuPermissionSql, new
                            {
                                RoleId = roleId,
                                MenuId = item,
                            }, tran);
                        }
                    }

                    if (roleId > 0 && model.ActionIds?.Count() > 0)
                    {
                        foreach (var item in model.ActionIds)
                        {
                            _dbConnection.Execute(insertActionsPermissionSql, new
                            {
                                RoleId = roleId,
                                ActionId = item,
                            }, tran);
                        }
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }

            }

            return roleId;

        }

        public int UpdateByTrans(SysRole model)
        {
            int result = 0;
            string insertPermissionSql = @"INSERT INTO SysRolePermission (RoleId, MenuId, MenuActionId) VALUES (@RoleId,@MenuId, 0)";
            string insertActionsPermissionSql = @"INSERT INTO SysRolePermission (RoleId, MenuId, MenuActionId) VALUES (@RoleId,0,@ActionId)";
            string deletePermissionSql = "DELETE FROM SysRolePermission WHERE RoleId = @RoleId";
            using (var tran = _dbConnection.BeginTransaction())
            {
                try
                {
                    result = _dbConnection.Update(model, tran);
                    if (result > 0 && model.MenuIds?.Count() > 0)
                    {
                        _dbConnection.Execute(deletePermissionSql, new
                        {
                            RoleId = model.Id,

                        }, tran);
                        foreach (var item in model.MenuIds)
                        {
                            _dbConnection.Execute(insertPermissionSql, new
                            {
                                RoleId = model.Id,
                                MenuId = item,
                            }, tran);
                        }
                    }

                    if (result > 0 && model.ActionIds?.Count() > 0)
                    {
                        foreach (var item in model.ActionIds)
                        {
                            _dbConnection.Execute(insertActionsPermissionSql, new
                            {
                                RoleId = model.Id,
                                ActionId = item,
                            }, tran);
                        }
                    }



                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }

            }

            return result;
        }


        //public int? InsertByTrans(SysRole model)
        //{
        //    int? roleId = 0;
        //    string insertPermissionSql =
        //        @"INSERT INTO SysRolePermission (RoleId, MenuId, Permission) VALUES (@RoleId,@MenuId, '')";
        //    using (var tran = _dbConnection.BeginTransaction())
        //    {
        //        try
        //        {
        //            roleId = _dbConnection.Insert(model, tran);
        //            if (roleId > 0 && model.MenuIds?.Count() > 0)
        //            {
        //                foreach (var item in model.MenuIds)
        //                {
        //                    _dbConnection.Execute(insertPermissionSql, new
        //                    {
        //                        RoleId = roleId,
        //                        MenuId = item,
        //                    }, tran);
        //                }
        //            }

        //            tran.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            tran.Rollback();
        //            throw ex;
        //        }
        //    }

        //    return roleId;
        //}

        //public int UpdateByTrans(SysRole model)
        //{
        //    int result = 0;
        //    string insertPermissionSql =
        //        @"INSERT INTO SysRolePermission (RoleId, MenuId, Permission) VALUES (@RoleId,@MenuId, '')";
        //    string deletePermissionSql = "DELETE FROM SysRolePermission WHERE RoleId = @RoleId";
        //    using (var tran = _dbConnection.BeginTransaction())
        //    {
        //        try
        //        {
        //            result = _dbConnection.Update(model, tran);
        //            if (result > 0 && model.MenuIds?.Count() > 0)
        //            {
        //                _dbConnection.Execute(deletePermissionSql, new
        //                {
        //                    RoleId = model.Id,
        //                }, tran);
        //                foreach (var item in model.MenuIds)
        //                {
        //                    _dbConnection.Execute(insertPermissionSql, new
        //                    {
        //                        RoleId = model.Id,
        //                        MenuId = item,
        //                    }, tran);
        //                }
        //            }

        //            tran.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            tran.Rollback();
        //            throw ex;
        //        }
        //    }

        //    return result;
        //}

        public List<string> GetUnAuthorizedCodes(int roleId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Code FROM SysMenuAction  WHERE  SysMenuAction.DataType !=0 AND NOT EXISTS(");
            sb.Append(
                $"SELECT MenuActionId from SysRolePermission  WHERE SysRolePermission.RoleId = {roleId} AND SysRolePermission.MenuActionId !=0  AND SysMenuAction.Id = SysRolePermission.MenuActionId");
            sb.Append(")");

            return _dbConnection.Query<string>(sb.ToString()).ToList();
        }


        public async Task<int> GetMerchantAdminDefaultRoleIdAsync(int merchantId)
        {
            string sql = $"SELECT Id FROM SysRole WHERE MerchantId={merchantId} AND RoleType = 1 AND IsSystem = 1";
            return await _dbConnection.ExecuteScalarAsync<int>(sql);
        }
    }
}