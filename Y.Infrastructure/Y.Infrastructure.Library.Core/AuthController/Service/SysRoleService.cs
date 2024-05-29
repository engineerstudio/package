////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //
//                         O\  =  /O                              //
//                      ____/`---'\____                           //
//                    .'  \\|     |//  `.                         //
//                   /  \\|||  :  |||//  \                        //
//                  /  _||||| -:- |||||-  \                       //
//                  |   | \\\  -  /// |   |                       //
//                  | \_|  ''\---/''  |   |                       //
//                  \  .-\__  `-`  ___/-. /                       //
//                ___`. .'  /--.--\  `. . ___                     //
//              ."" '<  `.___\_<|>_/___.'  >'"".                  //
//            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
//            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
//      ========`-.____`-.___\_____/___.-`____.-'========         //
//                           `=---='                              //
//      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
//                   佛祖保佑       永不宕机     永无BUG          //
////////////////////////////////////////////////////////////////////

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：Aaron                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-08-24 02:23:05                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Sys.Service                                  
*│　类    名： SysRoleService                                    
*└──────────────────────────────────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.AuthController.Entity;
using Y.Infrastructure.Library.Core.AuthController.IRepository;
using Y.Infrastructure.Library.Core.AuthController.IService;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.ViewModel;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;


namespace Y.Infrastructure.Library.Core.AuthController.Service
{
    public class SysRoleService : ISysRoleService
    {
        private readonly ISysRoleRepository _repository;
        private readonly ISysMenuRepository _sysMenuRepository;
        private readonly ISysMenuActionRepository _sysMenuActionRepository;

        public SysRoleService(ISysRoleRepository repository, ISysMenuRepository sysMenuRepository,
            ISysMenuActionRepository sysMenuActionRepository)
        {
            _repository = repository;
            _sysMenuRepository = sysMenuRepository;
            _sysMenuActionRepository = sysMenuActionRepository;
        }

        public async Task<BaseResult> AddOrModify(SysRole role, int[] keys)
        {
            if (role.Id == 0)
            {
                role.MenuIds = keys;
                int? addResult = await _repository.InsertAsync(role);
                if (addResult.HasValue && addResult.Value > 0)
                    return new BaseResult() { code = 1, msg = "ok" };
            }
            else
            {
                role = _repository.Get(role.Id);
                if (role != null)
                {
                    role.MenuIds = keys;
                    if (_repository.UpdateByTrans(role) > 0) return new BaseResult() { code = 1, msg = "失败" };
                }
            }

            return new BaseResult() { code = 1, msg = "失败" };
        }

        /// <summary>
        /// 新增或者修改服务
        /// </summary>
        /// <param name="item">新增或者修改试图实体</param>
        /// <returns>结果实体</returns>
        public ValueTuple<bool, string> AddOrModify(ManagerRoleAddOrModifyModel item)
        {
            if (item.MerchantId == 0) return (false, "商户错误");
            int[] menuIds =
                item.MenuIds == null ? new int[0] : item.MenuIds.Where(t => t < 10000).ToArray(); // 菜单Id小于10000
            int[] actionIds =
                item.MenuIds == null ? new int[0] : item.MenuIds.Where(t => t > 10000).ToArray(); // 动作Id大于10000

            SysRole managerRole;
            if (item.Id == 0)
            {
                managerRole = new SysRole()
                {
                    MerchantId = item.MerchantId,
                    RoleName = item.RoleName,
                    RoleType = item.RoleType,
                    IsSystem = item.IsSystem,
                    Remark = item.Remark,
                    MenuIds = menuIds,
                    ActionIds = actionIds,
                    CreatedTime = DateTime.UtcNow.AddHours(8),
                    SysStr = item.SysStr
                };

                if (_repository.InsertByTrans(managerRole) > 0)
                    return (true, Msg.SucessMsg);
            }
            else
            {
                //TODO Modify
                managerRole = _repository.Get(item.Id);
                if (managerRole != null)
                {
                    managerRole.RoleName = item.RoleName;
                    managerRole.RoleType = item.RoleType;
                    managerRole.IsSystem = item.IsSystem;
                    managerRole.Remark = item.Remark;
                    managerRole.MenuIds = menuIds;
                    managerRole.ActionIds = actionIds;

                    if (_repository.UpdateByTrans(managerRole) > 0)
                        return (true, Msg.SucessMsg);
                }
            }

            return (false, Msg.FailedMsg);
        }

        /// <summary>
        /// 创建站点默认的系统角色，每次创建站点时候进行调用
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<(bool, string, int)> CreateMerchantAdminDefaultRoleAsync(int merchantId)
        {
            // 1. 获取到所有的菜单目录
            List<int> menuIds = _sysMenuRepository.GetList().Select(t => t.Id).ToList();

            // 2. 获取到所有的操作权限
            List<int> actionIds = _sysMenuActionRepository.GetList().Select(t => t.Id).ToList();


            // 3. 实体类进行创建
            List<int> listIds = new List<int>();
            listIds.AddRange(menuIds);
            listIds.AddRange(actionIds);

            ManagerRoleAddOrModifyModel d = new ManagerRoleAddOrModifyModel();
            d.RoleName = "超级管理员";
            d.RoleType = 1;
            d.IsSystem = true;
            d.Remark = "超管";
            d.MenuIds = listIds.ToArray();
            d.MerchantId = merchantId;

            var rt = AddOrModify(d);

            // 4. 获取当前新增的对象

            var id = await _repository.GetMerchantAdminDefaultRoleIdAsync(merchantId);

            return (true, d.RoleName, id);
        }

        public List<MenuNavView> GetMenusByRoleId(int id, string sysStr)
        {
            var menuList = _repository.GetMenusByRoleId(id, sysStr);
            if (menuList?.Count() == 0)
            {
                return null;
            }

            var menuNavViewList = new List<MenuNavView>();
            MenuNavView navView;
            foreach (var x in menuList)
            {
                navView = new MenuNavView()
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    Name = x.Name,
                    DisplayName = x.DisplayName,
                    IconUrl = x.IconClass,
                    LinkUrl = x.LinkUrl,
                    Spread = false,
                    //Target = false
                };
                menuNavViewList.Add(navView);
            }

            //menuList.ForEach(x =>
            //{
            //    var navView = _mapper.Map<MenuNavView>(x);
            //    menuNavViewList.Add(navView);
            //});
            return menuNavViewList;
        }


        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="model">查询实体</param>
        /// <returns></returns>
        public TableDataModel LoadData(ManagerRoleRequestModel model)
        {
            string conditions = "where IsDelete=0 AND SysStr = @SysStr"; //未删除的
            if (model.MerchantId != 0)
                conditions += $" AND MerchantId={model.MerchantId} ";
            if (!model.Key.IsNullOrWhiteSpace())
                conditions += $"and RoleName like '%@Key%'";

            return new TableDataModel
            {
                count = _repository.RecordCount(conditions, new
                {
                    Key = model.Key,
                    SysStr = model.UniqueStr
                }),
                data = _repository.GetListPaged(model.Page, model.Limit, conditions, "Id desc", new
                {
                    Key = model.Key,
                    SysStr = model.UniqueStr
                })
            };
        }


        public List<SysRole> GetListByCondition(ManagerRoleRequestModel model)
        {
            string conditions = "where IsDelete=0 "; //未删除的
            if (!model.Key.IsNullOrWhiteSpace())
            {
                conditions += $"and RoleName like '%@Key%'";
            }

            return _repository.GetList(conditions, new
            {
                Key = model.Key,
            }).ToList();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="roleId">角色主键id</param>
        /// <returns>结果实体</returns>
        public ValueTuple<bool, string> DeleteIds(int[] roleId)
        {
            if (roleId.Count() != 0)
            {
                var count = _repository.DeleteLogical(roleId);
                if (count > 0)
                    return (true, Msg.SucessMsg);
            }

            return (false, Msg.FailedMsg);
        }

        /// <summary>
        /// 获取角色字典
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public Dictionary<int, string> GetRoleDic(int merchantId)
        {
            if (merchantId == 0) return new Dictionary<int, string>();
            string conditions = $"WHERE MerchantId = {merchantId}";
            var list = _repository.GetList(conditions);
            return list.ToDictionary(t => t.Id, t => t.RoleName);
        }

        /// <summary>
        /// 获取未授权的编码，用户页面隐藏
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<string> GetUnAuthorizedCodes(int roleId)
        {
            return _repository.GetUnAuthorizedCodes(roleId);
        }
    }
}