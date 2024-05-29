using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.AuthController.Entity;
using static Y.Infrastructure.Library.Core.AuthController.Entity.SysMenuAction;

namespace Y.Infrastructure.Library.Core.AuthController.IService
{
    public interface ISysMenuActionService
    {
        /// <summary>
        /// 根据当前主键获取授权的访问地址
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<string> GetAuthorizedUrl(int[] ids);

        /// <summary>
        /// 获取自由访问权限的地址
        /// </summary>
        /// <returns></returns>
        List<string> GetPublicUrl();


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <returns></returns>
        (bool, string) Insert(MenuActionAddOrModifyModel m);

        Task<(bool, string)> InsertAsync();

        /// <summary>
        /// 获取所有列表
        /// </summary>
        /// <returns></returns>
        List<SysMenuAction> GetList();

        /// <summary>
        /// 获取列表不包含 ActionsDataType
        /// </summary>
        /// <param name="dType">不包含</param>
        /// <returns></returns>
        List<SysMenuAction> GetList(ActionsDataType dType);

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        (IEnumerable<SysMenuAction>, int) GetPageList(MenuActionsListQuery q);

        (bool, string) Update(MenuActionAddOrModifyModel m);
    }
}