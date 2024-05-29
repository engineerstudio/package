using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Repository;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Y.Infrastructure.Library.Core.AuthController.Entity;
using Y.Infrastructure.Library.Core.AuthController.IRepository;

namespace Y.Infrastructure.Library.Core.AuthController.Repository
{
    public class SysMenuRepository : BaseRepository<SysMenu, Int32>, ISysMenuRepository
    {
        public SysMenuRepository(IOptionsMonitor<DbOption> options)
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
            string sql = "update SysMenu set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update SysMenu set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        public async Task<Dictionary<int, string>> MenuDic()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            string sql = "select id,name from SysMenu";
            using (var reader = await _dbConnection.ExecuteReaderAsync(sql))
            {
                while (reader.Read())
                {
                    dic.Add(Convert.ToInt32(reader["id"]), reader["name"].ToString());
                }
            }

            return dic;
        }

        public async Task<Int32> ChangeDisplayStatusByIdAsync(int id, bool status)
        {
            string sql = "update SysMenu set IsDisplay=@IsDisplay where  Id=@Id";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                IsDisplay = status ? 1 : 0,
                Id = id,
            });
        }

        public async Task<Boolean> GetDisplayStatusByIdAsync(int id)
        {
            string sql = "select IsDisplay from SysMenu where Id=@Id and IsDelete=0";
            return await _dbConnection.QueryFirstOrDefaultAsync<bool>(sql, new
            {
                Id = id,
            });
        }

        public async Task<Boolean> IsExistsNameAsync(string Name, string sysStr)
        {
            string sql = "select Id from SysMenu where Name=@Name and IsDelete=0 and SysStr!=@sysStr";
            var result = await _dbConnection.QueryAsync<int>(sql, new
            {
                Name = Name,
                SysStr = sysStr
            });
            if (result != null && result.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Boolean> IsExistsNameAsync(string Name, Int32 Id, string sysStr)
        {
            string sql = "select Id from SysMenu where Name=@Name and Id <> @Id and IsDelete=0 and SysStr!=@sysStr";
            var result = await _dbConnection.QueryAsync<int>(sql, new
            {
                Name = Name,
                Id = Id,
                SysStr = sysStr
            });
            if (result != null && result.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}