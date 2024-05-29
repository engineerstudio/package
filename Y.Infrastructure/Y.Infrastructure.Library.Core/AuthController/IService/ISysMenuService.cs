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
*│　接口名称： ISysMenuRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.AuthController.Entity;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;


namespace Y.Infrastructure.Library.Core.AuthController.IService
{
    public interface ISysMenuService
    {
        //Task<BaseResult> AddOrModify(SysMenu t);

        //Task<TableResult> List();

        //Task<BaseResult> DeleteById(int id);

        Dictionary<int, string> GetIdAndNameDic();

        Dictionary<int, string> GetIdAndUrlDic();

        Task<Dictionary<int, string>> GetIdAndSysStrDic();

        /// <summary>
        /// 是否包含指定的Url
        /// </summary>
        /// <param name="url">小写url</param>
        /// <returns></returns>
        bool ContainsUrl(string url);

        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="model">查询实体</param>
        /// <returns>table数据</returns>
        TableDataModel LoadData(MenuRequestModel model);

        /// <summary>
        /// 新增或者修改服务
        /// </summary>
        /// <param name="item">新增或者修改试图实体</param>
        /// <returns>结果实体</returns>
        Task<ValueTuple<bool, string>> AddOrModifyAsync(MenuAddOrModifyModel model);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Ids">主键id数组</param>
        /// <returns>结果实体</returns>
        Task<ValueTuple<bool, string>> DeleteIdsAsync(int[] Ids);

        /// <summary>
        /// 更改显示的状态
        /// </summary>
        /// <param name="item">改变状态实体</param>
        /// <returns></returns>
        Task<ValueTuple<bool, string>> ChangeDisplayStatusAsync(ChangeStatusModel item);

        /// <summary>
        /// 判断是否存在名为Name的菜单
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        Task<bool> IsExistsNameAsync(MenuAddOrModifyModel item);

        /// <summary>
        /// 根据父节点返回列表
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        List<SysMenu> GetChildListByParentId(int ParentId, string sysStr);
    }
}