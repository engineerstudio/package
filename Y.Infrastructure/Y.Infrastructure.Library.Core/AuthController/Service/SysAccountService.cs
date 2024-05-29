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
*│　类    名： SysAccountService                                    
*└──────────────────────────────────────────────────────────────┘
*/

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.AuthController.Entity;
using Y.Infrastructure.Library.Core.AuthController.IRepository;
using Y.Infrastructure.Library.Core.AuthController.IService;
using Y.Infrastructure.Library.Core.Encrypt;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;


namespace Y.Infrastructure.Library.Core.AuthController.Service
{
    public class SysAccountService : ISysAccountService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISysAccountRepository _repository;
        private readonly ISysAccountSessionRepository _accSessionRepository;
        private readonly ISysRoleRepository _roleRepository;
        private readonly ISysRolePermissionRepository _permissionRepository;
        private readonly ISysMenuActionService _sysMenuActionService;
        private readonly ISysRoleService _sysRoleService;

        public SysAccountService(ISysAccountRepository repository, ISysRoleRepository roleRepository,
            ISysAccountSessionRepository accSessionRepository, ISysRolePermissionRepository permissionRepository,
            IHttpContextAccessor httpContextAccessor, ISysMenuActionService sysMenuActionService,
            ISysRoleService sysRoleService) //, IMemoryCacheService memoryCacheService
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _accSessionRepository = accSessionRepository;
            _permissionRepository = permissionRepository;
            _httpContextAccessor = httpContextAccessor;
            _sysMenuActionService = sysMenuActionService;
            _sysRoleService = sysRoleService;
        }

        /// <summary>
        /// 添加用户账户
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<ValueTuple<bool, string>> AddOrModify(SysAccount t)
        {
            if (t.Id == 0)
            {
                if (await _repository.IsExistAccountName(t.Name))
                    return (false, Msg.DuplicateNameMsg);

                t.CreatedTime = DateTime.Now;
                t.IsLock = false;
                t.IsDelete = false;
                if (await _repository.InsertAsync(t) > 0)
                {
                    return (true, Msg.SucessMsg);
                }
            }
            else
            {
            }

            return (false, Msg.FailedMsg);
        }

        /// <summary>
        /// 根据ID删除账户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ValueTuple<bool, string>> DeleteById(int id)
        {
            var account = _repository.Get(id);
            if (account == null) return (false, Msg.ObjectDoesNotExistMsg);
            // 删除对应角色关系
            // 删除对象
            if (await _repository.DeleteAsync(id) > 0) return (true, Msg.SucessMsg);
            return (false, Msg.FailedMsg);
        }

        public async Task<ValueTuple<bool, string>> ResetPassword(string oldPsw, string newPsw, string userId)
        {
            var account = _repository.Get(userId);
            if (account.Password != oldPsw) return (false, Msg.FalsePasswordMsg);
            account.Password = newPsw;
            if (await _repository.UpdateAsync(account) > 0) return (true, Msg.SucessMsg);
            return (false, Msg.FailedMsg);
        }

        /// <summary>
        /// 登录操作，成功则写日志
        /// </summary>
        /// <param name="model">登陆实体</param>
        /// <returns>状态</returns>
        public async Task<SysAccount> SignInAsync(LoginModel model)
        {
            model.Password = AESEncryptHelper.Encode(model.Password.Trim(), DefaultString.AesEncryptKeys);
            model.UserName = model.UserName.Trim();
            string conditions = $"select * from {nameof(SysAccount)} where IsDelete=0 "; //未删除的
            conditions +=
                $"and (Name = @UserName or Mobile =@UserName or Email =@UserName) and Password=@Password and SysStr=@UniqueStr";
            var manager = await _repository.GetAsync(conditions, model);
            if (manager != null)
            {
                manager.LastLoginIp = model.Ip;
                //manager.LoginCount += 1;
                manager.LastLoginTime = DateTime.Now;
                //_repository.Update(manager);
                //await _managerLogRepository.InsertAsync(new ManagerLog()
                //{
                //    ActionType = CzarCmsEnums.ActionEnum.SignIn.ToString(),
                //    AddManageId = manager.Id,
                //    AddManagerNickName = manager.NickName,
                //    AddTime = DateTime.Now,
                //    AddIp = model.Ip,
                //    Remark = "用户登录"
                //});

                // 更新Session
                var session = new SysAccountSession()
                {
                    AccountId = manager.Id,
                    Session = Guid.NewGuid().ToString("N"),
                    UpdateTime = DateTime.Now
                };
                var rt = _accSessionRepository.Insert(session);
                if (rt.HasValue && rt.Value > 0)
                    manager.Session = session.Session;
            }

            return manager;
        }

        public async Task<ValueTuple<bool, string>> AddOrModifyAsync(ManagerAddOrModifyModel model)
        {
            SysAccount account;
            if (model.Id == 0)
            {
                //TODO ADD
                //account = _mapper.Map<Manager>(model);
                account = new SysAccount()
                {
                    Id = model.Id,
                    RoleId = model.RoleId,
                    Name = model.UserName,
                    Avatar = model.Avatar,
                    NickName = model.NickName,
                    Mobile = model.Mobile,
                    Email = model.Email,
                    IsLock = model.IsLock,
                    Remark = model.Remark,
                    SysStr = model.SysStr,
                    MerchantId = model.MerchantId
                };


                account.Password = AESEncryptHelper.Encode(DefaultString.DefaultPassword, DefaultString.AesEncryptKeys);
                //account.LoginCount = 0;
                account.CreatedId = 1;
                account.IsDelete = false;
                account.Avatar = "1";
                account.LastLoginIp = "1";
                account.CreatedTime = DateTime.Now;
                account.LastLoginTime = DateTime.Now;
                if (await _repository.InsertAsync(account) > 0)
                    return (true, Msg.SucessMsg);
            }
            else
            {
                //TODO Modify
                account = await _repository.GetAsync(model.Id);
                if (account != null)
                {
                    //_mapper.Map(item, account);

                    account.Id = model.Id;
                    account.RoleId = model.RoleId;
                    account.Name = model.UserName;
                    account.Avatar = model.Avatar;
                    account.NickName = model.NickName;
                    account.Mobile = model.Mobile;
                    account.Email = model.Email;
                    account.IsLock = model.IsLock;
                    account.Remark = model.Remark;


                    //account.CreatedId = 1;
                    //account.ModifyTime = DateTime.Now;
                    if (_repository.Update(account) > 0)
                        return (true, Msg.SucessMsg);
                }
            }

            return (false, Msg.FailedMsg);
        }

        public async Task<ValueTuple<bool, string>> ChangeLockStatusAsync(ChangeStatusModel model)
        {
            //判断状态是否发生变化，没有则修改，有则返回状态已变化无法更改状态的提示
            var isLock = await _repository.GetLockStatusByIdAsync(model.Id);
            if (isLock == !model.Status)
            {
                var count = await _repository.ChangeLockStatusByIdAsync(model.Id, model.Status);
                return (true, Msg.SucessMsg);
            }

            return (false, Msg.FailedMsg);
        }

        public async Task<ValueTuple<bool, string>> ChangePasswordAsync(ChangePasswordModel model)
        {
            string oldPwd = await _repository.GetPasswordByIdAsync(model.Id); //数据库中的密码
            if (oldPwd == AESEncryptHelper.Encode(model.OldPassword, DefaultString.AesEncryptKeys))
            {
                var count = await _repository.ChangePasswordByIdAsync(model.Id,
                    AESEncryptHelper.Encode(model.NewPassword.Trim(), DefaultString.AesEncryptKeys));
                if (count > 0)
                    return (true, Msg.SucessMsg);
            }
            else
                return (false, Msg.OldPasswordErrorMsg);

            return (false, Msg.FailedMsg);
        }

        public async Task<ValueTuple<bool, string>> UpdateManagerInfoAsync(ChangeInfoModel model)
        {
            var account = await _repository.GetAsync(model.Id);
            if (account != null)
            {
                //_mapper.Map(model, account);
                account.Id = model.Id;
                account.NickName = model.NickName;
                account.Avatar = model.Avatar;
                account.NickName = model.NickName;
                account.Mobile = model.Mobile;
                account.Email = model.Email;
                account.Remark = model.Remark;

                if (await _repository.UpdateAsync(account) > 0)
                    return (true, Msg.SucessMsg);
            }

            return (false, Msg.FailedMsg);
        }

        public async Task<ValueTuple<bool, string>> DeleteIdsAsync(int[] Ids)
        {
            if (Ids.Count() == 0)
                return (false, Msg.FalsePasswordMsg);
            else
            {
                var count = await _repository.DeleteLogicalAsync(Ids);
                if (count > 0)
                    return (true, Msg.SucessMsg);
            }

            return (false, Msg.FailedMsg);
        }

        public async Task<SysAccount> GetManagerByIdAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<SysAccount> GetManagerContainRoleNameByIdAsync(int id)
        {
            return await _repository.GetManagerContainRoleNameByIdAsync(id);
        }

        public async Task<TableDataModel> LoadDataAsync(ManagerRequestModel model)
        {
            string conditions = "where IsDelete=0 AND SysStr=@SysStr"; //未删除的
            if (model.MerchantId != 0)
            {
                conditions += $" AND MerchantId={model.MerchantId}";
            }
            if (!model.Key.IsNullOrWhiteSpace())
            {
                conditions +=
                    $" and (Name like '%@Key%' or NickName like '%@Key%' or Remark like '%@Key%' or Mobile like '%@Key%' or Email like '%@Key%')";
            }

            var qObj = new { SysStr = model.UniqueStr };
            var list = (await _repository.GetListPagedAsync(model.Page, model.Limit, conditions, "Id desc", qObj))
                .ToList();
            var viewList = new List<ManagerListModel>();
            list?.ForEach(x =>
            {
                //var item = _mapper.Map<ManagerListModel>(x);

                var item = new ManagerListModel()
                {
                    Id = x.Id,
                    RoleId = x.RoleId,
                    RoleName = null,
                    UserName = x.Name,
                    Password = x.Password,
                    Avatar = x.Avatar,
                    Email = x.Email,
                    NickName = x.NickName,
                    Mobile = x.Mobile,
                    LoginLastIp = x.LastLoginIp,
                    LoginLastTime = x.LastLoginTime,
                    IsLock = x.IsLock,
                    Remark = x.Remark
                };

                item.RoleName = _roleRepository.GetNameById(x.RoleId);
                viewList.Add(item);
            });
            return new TableDataModel
            {
                count = await _repository.RecordCountAsync(conditions, qObj),
                data = viewList,
            };
        }

        /// <summary>
        /// 根据传入的GUID 查找用户是否有权限
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public bool ExistAuths(string sessionId, int menuId)
        {
            if (_accSessionRepository.GetNumberBySession(sessionId) > 0)
            {
                var accountId = _accSessionRepository.GetAccountIdBySession(sessionId);
                var roleId = _repository.Get(accountId).RoleId;
                var roles = _permissionRepository.GetMenuIdsByRoleId(roleId);
                if (roles.Contains(menuId))
                {
                    // 保存用户信息到 HttpContext
                    var account = _repository.Get(accountId);
                    //_httpContextAccessor.HttpContext.Session.Set("AccountInfo", account);
                    //_memoryCacheService.InsertSlidingExpirationCache("AccountInfo", account, 15);

                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// 判断用户是否已经登陆 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns>true:当前登陆账户ID, false: 0</returns>
        public (bool, int) ExistSession(string sessionId)
        {
            if (_accSessionRepository.GetNumberBySession(sessionId) == 0) return (false, 0);
            return (true, _accSessionRepository.GetAccountIdBySession(sessionId));
        }

        /// <summary>
        /// 判断用户是否有操作权限
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool ExistAuthorized(int accountId, string url)
        {
            if (accountId == 0 || string.IsNullOrEmpty(url)) return false;
            // 获取公开访问的地址
            var publicReq = _sysMenuActionService.GetPublicUrl();
            if (publicReq.Contains(url)) return true;

            // 获取角色的权限
            var roleId = _repository.Get(accountId).RoleId;
            var actions = _permissionRepository.GetActionIdsByRoleId(roleId);
            var authorizedUrls = _sysMenuActionService.GetAuthorizedUrl(actions.ToArray());

            return authorizedUrls.Contains(url);
        }

        /// <summary>
        /// 获取所有权限的的Id,包含菜单Id与操作Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<int> GetRoleAuthIdList(int roleId)
        {
            var ids = _permissionRepository.GetMenuIdsByRoleId(roleId);
            var actions = _permissionRepository.GetActionIdsByRoleId(roleId);
            List<int> authIdList = new List<int>();
            authIdList.AddRange(ids);
            authIdList.AddRange(actions);
            return authIdList;
        }


        /// <summary>
        /// 根据账户ID获取账户名称
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public string GetAccountName(int accountId)
        {
            return _repository.GetAccountName(accountId);
        }

        public async Task CreateMerchantAdminDefaultAccountAsync(int merchantId, string systemStr)
        {
            var m = new ManagerAddOrModifyModel()
            {
                RoleId = 3,
                UserName = $"bw{merchantId}",
                Avatar = "",
                NickName = $"bw{merchantId}",
                Mobile = "",
                Email = "",
                IsLock = false,
                Remark = "",
                SysStr = systemStr,
                MerchantId = merchantId,
            };
            await this.AddOrModifyAsync(m);
        }

    }
}