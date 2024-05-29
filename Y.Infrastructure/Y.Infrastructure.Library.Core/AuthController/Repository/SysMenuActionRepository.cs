using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using Y.Infrastructure.Library.Core.AuthController.Entity;
using Y.Infrastructure.Library.Core.AuthController.IRepository;
using Y.Infrastructure.Library.Core.DbHelper;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Repository;
using static Y.Infrastructure.Library.Core.AuthController.Entity.SysMenuAction;

namespace Y.Infrastructure.Library.Core.AuthController.Repository
{
    public class SysMenuActionRepository : BaseRepository<SysMenuAction, Int32>, ISysMenuActionRepository
    {
        public SysMenuActionRepository(IOptionsMonitor<DbOption> options)
        {
            _dbOption = options.Get(AuthDefault.SqlConStr);
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }

            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }


        /// <summary>
        /// 根据当前主键获取授权的访问地址
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public string[] GetAuthorizedUrl(int[] ids)
        {
            string sql = "select url from SysMenuAction where Id in @Ids";
            return _dbConnection.Query<string>(sql, new
            {
                Ids = ids
            }).ToArray();
        }


        public string[] GetPublicUrl()
        {
            string sql = $"select url from SysMenuAction where ActionType={ActionsType.Public.GetEnumValue()}";
            return _dbConnection.Query<string>(sql).ToArray();
        }
    }
}