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
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-08-24 02:23:05                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Sys.IService                                   
*│　接口名称： ISysRoleRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.AuthController.Entity;
using Y.Infrastructure.Library.Core.ViewModel;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;


namespace Y.Infrastructure.Library.Core.AuthController.IService
{
    public interface ISysRoleService
    {
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<BaseResult> AddOrModify(SysRole role, int[] keys);

        /// <summary>
        /// 新增或者修改服务
        /// </summary>
        /// <param name="item">新增或者修改试图实体</param>
        /// <returns>结果实体</returns>
        ValueTuple<bool, string> AddOrModify(ManagerRoleAddOrModifyModel item);


        List<MenuNavView> GetMenusByRoleId(int id, string sysStr);


        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="model">查询实体</param>
        /// <returns>table数据</returns>
        List<SysRole> GetListByCondition(ManagerRoleRequestModel model);


        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="model">查询实体</param>
        /// <returns>table数据</returns>
        TableDataModel LoadData(ManagerRoleRequestModel model);


        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids">主键id数组</param>
        /// <returns>结果实体</returns>
        ValueTuple<bool, string> DeleteIds(int[] roleId);
        Dictionary<int, string> GetRoleDic(int merchantId);
        List<string> GetUnAuthorizedCodes(int roleId);
    }
}