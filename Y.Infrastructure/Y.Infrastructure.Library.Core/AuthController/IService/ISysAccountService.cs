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
*│　接口名称： ISysAccountRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.AuthController.Entity;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Infrastructure.Library.Core.AuthController.IService
{
    public interface ISysAccountService
    {
        Task<ValueTuple<bool, string>> AddOrModify(SysAccount t);

        Task<ValueTuple<bool, string>> ResetPassword(string oldPsw, string newPasw, string userId);

        Task<ValueTuple<bool, string>> DeleteById(int id);


        /// <summary>
        /// 登录操作，成功则写日志
        /// </summary>
        /// <param name="model">登陆实体</param>
        /// <returns>实体对象</returns>
        Task<SysAccount> SignInAsync(LoginModel model);


        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="model">查询实体</param>
        /// <returns>table数据</returns>
        Task<TableDataModel> LoadDataAsync(ManagerRequestModel model);

        /// <summary>
        /// 新增或者修改服务
        /// </summary>
        /// <param name="item">新增或者修改试图实体</param>
        /// <returns>结果实体</returns>
        Task<ValueTuple<bool, string>> AddOrModifyAsync(ManagerAddOrModifyModel model);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids">主键id数组</param>
        /// <returns>结果实体</returns>
        Task<ValueTuple<bool, string>> DeleteIdsAsync(int[] Ids);

        /// <summary>
        /// 修改锁定状态
        /// </summary>
        /// <param name="model">修改锁定状态实体</param>
        /// <returns>结果</returns>
        Task<ValueTuple<bool, string>> ChangeLockStatusAsync(ChangeStatusModel model);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model">修改密码实体</param>
        /// <returns>结果</returns>
        Task<ValueTuple<bool, string>> ChangePasswordAsync(ChangePasswordModel model);

        Task<SysAccount> GetManagerByIdAsync(int id);

        Task<SysAccount> GetManagerContainRoleNameByIdAsync(int id);

        /// <summary>
        /// 个人资料修改
        /// </summary>
        /// <param name="model">个人资料修改实体</param>
        /// <returns>结果</returns>
        Task<ValueTuple<bool, string>> UpdateManagerInfoAsync(ChangeInfoModel model);

        /// <summary>
        /// 判断账户是否存在此权限
        /// </summary>
        /// <param name="sessionId">用户缓存Id</param>
        /// <param name="menuId">菜单Id</param>
        /// <returns></returns>
        bool ExistAuths(string sessionId, int menuId);


        /// <summary>
        /// 判断用户是否已经登陆 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns>true:当前登陆账户ID, false: 0</returns>
        (bool, int) ExistSession(string sessionId);

        /// <summary>
        /// 判断用户是否存在操作权限
        /// </summary>
        /// <param name="accountId">账户ID</param>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        bool ExistAuthorized(int accountId, string url);

        /// <summary>
        /// 获取所有权限的的Id,包含菜单Id与操作Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<int> GetRoleAuthIdList(int roleId);

        /// <summary>
        /// 获取角色名称
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        string GetAccountName(int accountId);

        Task CreateMerchantAdminDefaultAccountAsync(int merchantId,string systemStr);
    }
}