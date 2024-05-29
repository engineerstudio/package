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
*│　命名空间： Y.Sys.IRepository                                   
*│　接口名称： ISysRoleRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.AuthController.Entity;
using Y.Infrastructure.Library.Core.Repository;

namespace Y.Infrastructure.Library.Core.AuthController.IRepository
{
    public interface ISysRoleRepository : IBaseRepository<SysRole, Int32>
    {
        /// <summary>
        /// 逻辑删除返回影响的行数
        /// </summary>
        /// <param name="ids">需要删除的主键数组</param>
        /// <returns>影响的行数</returns>
        Int32 DeleteLogical(Int32[] ids);

        /// <summary>
        /// 逻辑删除返回影响的行数（异步操作）
        /// </summary>
        /// <param name="ids">需要删除的主键数组</param>
        /// <returns>影响的行数</returns>
        Task<Int32> DeleteLogicalAsync(Int32[] ids);

        /// 事务新增,并保存关联表数据
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        int? InsertByTrans(SysRole model);

        /// <summary>
        /// 事务修改，并保存关联表数据
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns></returns>
        int UpdateByTrans(SysRole model);


        IEnumerable<SysMenu> GetMenusByRoleId(int roleId, string sysStr);

        /// <summary>
        /// 根据主键获取名称
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>名称</returns>
        string GetNameById(int id);


        /// <summary>
        /// 获取角色未授权编码
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<string> GetUnAuthorizedCodes(int roleId);

        /// <summary>
        /// 获取站点默认管理员的Id
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<int> GetMerchantAdminDefaultRoleIdAsync(int merchantId);
    }
}