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
*│　类    名： SysMenuService                                    
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
    public class SysMenuService : ISysMenuService
    {
        private readonly ISysMenuRepository _repository;

        public SysMenuService(ISysMenuRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 添加或者修改菜单
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<BaseResult> AddOrModify(SysMenu t)
        {
            if (string.IsNullOrEmpty(t.Name)) return new BaseResult() { code = Msg.NullName, msg = Msg.NullNameMsg };

            if (t.Id == 0)
            {
                t.CreatedTime = DateTime.Now;
                t.IsDisplay = true;
                t.IsSystem = false;
                t.IsDisplay = false;
                if (await _repository.InsertAsync(t) > 0)
                    return new BaseResult() { code = Msg.Sucess, msg = Msg.SucessMsg };
            }
            else
            {
            }

            return new BaseResult() { code = Msg.Failed, msg = Msg.FailedMsg };
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BaseResult> DeleteById(int id)
        {
            var account = _repository.Get(id);
            if (account == null)
                return new BaseResult() { code = Msg.ObjectDoesNotExist, msg = Msg.ObjectDoesNotExistMsg };
            // 删除对应角色关系
            // 删除对象
            if (await _repository.DeleteAsync(id) > 0) return new BaseResult() { code = Msg.Sucess, msg = Msg.SucessMsg };
            return new BaseResult() { code = Msg.Failed, msg = Msg.FailedMsg };
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public async Task<TableResult> List()
        {
            var list = await _repository.GetListPagedAsync(1, 10, null, null);
            TableResult result = new TableResult() { code = Msg.Sucess, msg = Msg.SucessMsg };
            result.data = list;
            return result;
        }

        /// <summary>
        /// 菜单下拉列表
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetIdAndNameDic()
        {
            return _repository.GetList().Where(t => t.SysStr == "bwMerchant").ToDictionary(t => t.Id, t => t.DisplayName);
        }

        /// <summary>
        /// 获取ID 与 URL的字典
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetIdAndUrlDic()
        {
            return _repository.GetList().ToDictionary(t => t.Id, t => t.LinkUrl.ToLower());
        }

        /// <summary>
        /// 获取ID与对应站点字符的字典
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<int, string>> GetIdAndSysStrDic()
        {
            return (await _repository.GetListAsync()).ToDictionary(t => t.Id, t => t.SysStr.ToLower());
        }


        /// <summary>
        /// 菜单是否包含指定路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool ContainsUrl(string url)
        {
            if (string.IsNullOrEmpty(url)) return false;
            var dic = GetIdAndUrlDic();
            return dic.Values.Contains(url.ToLower());
        }


        public async Task<ValueTuple<bool, string>> AddOrModifyAsync(MenuAddOrModifyModel item)
        {
            //var result = new BaseResult();
            SysMenu model;
            if (item.Id == 0)
            {
                model = new SysMenu()
                {
                    Id = item.Id,
                    ParentId = item.ParentId,
                    Name = item.Name,
                    DisplayName = item.DisplayName,
                    IconClass = item.IconUrl,
                    LinkUrl = item.LinkUrl,
                    Sort = item.Sort.Value,
                    Permission = Guid.NewGuid().ToString("N"),
                    IsDelete = false,
                    IsDisplay = item.IsDisplay,
                    IsSystem = item.IsSystem,
                    ModifyTime = DateTime.Now,
                    CreatedTime = DateTime.Now,
                    SysStr = item.SysStr
                };
                if (await _repository.InsertAsync(model) > 0) return (true, Msg.SucessMsg);
            }
            else
            {
                model = await _repository.GetAsync(item.Id);
                if (model == null) return (false, Msg.NullNameMsg);

                //model.Id = item.Id;
                model.ParentId = item.ParentId;
                model.Name = item.Name;
                model.DisplayName = item.DisplayName;
                model.IconClass = item.IconUrl;
                model.LinkUrl = item.LinkUrl;
                model.Sort = item.Sort.Value;
                model.Permission = item.Permission;
                model.IsDelete = item.IsDisplay;
                model.IsSystem = item.IsSystem;

                if (await _repository.UpdateAsync(model) > 0) return (true, Msg.SucessMsg);
            }

            return (false, Msg.FailedMsg);
        }


        public async Task<ValueTuple<bool, string>> DeleteIdsAsync(int[] Ids)
        {
            //var result = new BaseResult();
            if (Ids.Count() == 0)
                return (false, Msg.NullNameMsg);
            else
            {
                var count = await _repository.DeleteLogicalAsync(Ids);
                if (count > 0)
                    return (true, Msg.SucessMsg);
                else
                    return (false, Msg.FailedMsg);
            }
        }

        public TableDataModel LoadData(MenuRequestModel model)
        {
            string conditions = "where IsDelete=0 AND SysStr = @SysStr "; //未删除的
            if (!model.Key.IsNullOrWhiteSpace())
            {
                conditions += $"and DisplayName like '%@Key%'";
            }

            var qObj = new
            {
                Key = model.Key,
                SysStr = model.SysStr
            };
            return new TableDataModel
            {
                count = _repository.RecordCount(conditions, qObj),
                data = _repository.GetListPaged(model.Page, model.Limit, conditions, "Id desc", qObj).ToList(),
            };
        }

        public async Task<ValueTuple<bool, string>> ChangeDisplayStatusAsync(ChangeStatusModel model)
        {
            //判断状态是否发生变化，没有则修改，有则返回状态已变化无法更改状态的提示
            var isLock = await _repository.GetDisplayStatusByIdAsync(model.Id);
            if (isLock == !model.Status)
            {
                var count = await _repository.ChangeDisplayStatusByIdAsync(model.Id, model.Status);
                if (count > 0)
                    return (true, Msg.SucessMsg);
                else
                    return (false, Msg.FailedMsg);
            }
            else
            {
                return (true, Msg.SucessMsg);
            }
            //return result;
        }

        /// <summary>
        /// 判断是否存在名为Name的菜单
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public async Task<bool> IsExistsNameAsync(MenuAddOrModifyModel item)
        {
            bool data = false;
            if (item.Id > 0)
            {
                data = await _repository.IsExistsNameAsync(item.Name, item.Id, item.SysStr);
            }
            else
            {
                data = await _repository.IsExistsNameAsync(item.Name, item.SysStr);
            }

            return data;
        }

        public List<SysMenu> GetChildListByParentId(int ParentId, string sysStr)
        {
            string conditions = $"where IsDelete=0 AND SysStr='{sysStr}'"; //未删除的
            if (ParentId >= 0)
            {
                conditions += " and ParentId =@ParentId";
            }

            return _repository.GetList(conditions, new
            {
                ParentId = ParentId
            }).ToList();
        }
    }
}