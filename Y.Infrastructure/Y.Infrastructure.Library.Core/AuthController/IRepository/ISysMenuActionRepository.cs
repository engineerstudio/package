using System;
using Y.Infrastructure.Library.Core.AuthController.Entity;
using Y.Infrastructure.Library.Core.Repository;

namespace Y.Infrastructure.Library.Core.AuthController.IRepository
{
    public interface ISysMenuActionRepository : IBaseRepository<SysMenuAction, Int32>
    {
        /// <summary>
        /// 根据当前主键获取授权的访问地址
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        string[] GetAuthorizedUrl(int[] ids);

        /// <summary>
        /// 获取ActionsType 为 public的数据
        /// </summary>
        /// <returns></returns>
        string[] GetPublicUrl();
    }
}