using System.Collections.Generic;
namespace Y.Infrastructure.Library.Core.AuthController.IService
{
    public interface ISysRolePermissionService
    {
        /// <summary>
        /// 通过角色主键获取菜单主键数组
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        int[] GetIdsByRoleId(int RoleId);
        IEnumerable<int> GetActionIdsByRoleId(int RoleId);
    }
}