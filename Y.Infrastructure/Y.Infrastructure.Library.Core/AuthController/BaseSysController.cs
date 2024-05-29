using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.AuthController.Entity;
using Y.Infrastructure.Library.Core.AuthController.IService;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;

namespace Y.Infrastructure.Library.Core.AuthController
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class BaseSysController : ControllerBase
    {
        private readonly string CaptchaCodeSessionName = "CaptchaCode";
        private readonly string ManagerSignInErrorTimes = "ManagerSignInErrorTimes";
        private readonly int MaxErrorTimes = 3;
        private readonly ISysAccountService _service;

        private readonly IHttpContextAccessor _httpContextAccessor;

        //private readonly IMemoryCacheService _memoryCacheService;
        private readonly ISysRoleService _roleService;
        private readonly ISysRolePermissionService _rolePermissionService;
        private readonly ISysMenuService _menuService;
        private readonly ISysMenuActionService _sysMenuActionService;
        private readonly string UniqueStr;

        public BaseSysController(ISysAccountService service, IHttpContextAccessor httpContextAccessor
            //, ICookieService cookieService
            , ISysRoleService roleService, ISysRolePermissionService rolePermissionService, ISysMenuService menuService
            //, IMemoryCacheService memoryCacheService
            , ISysMenuActionService sysMenuActionService
            , string uniqueStr) //
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            //_cookieService = cookieService;
            //_memoryCacheService = memoryCacheService;
            _roleService = roleService;
            _rolePermissionService = rolePermissionService;
            _menuService = menuService;
            UniqueStr = uniqueStr;
            _sysMenuActionService = sysMenuActionService;
        }


        #region 登录

        [HttpPost("signin")]
        public async Task<string> SignInAsync([FromForm] LoginModel model)
        {
            #region 判断验证码

            //if (!ValidateCaptchaCode(model.CaptchaCode))
            //{
            //    result.code = BadMsg.SignInCaptchaError;
            //    result.msg = BadMsg.SignInCaptchaErrorMsg;
            //    return result.ToJson();
            //}

            #endregion

            #region 判断错误次数

            var ErrorTimes = 0; // HttpContext.Session.GetInt32(ManagerSignInErrorTimes);
            if (ErrorTimes == 0)
            {
                //HttpContext.Session.SetInt32(ManagerSignInErrorTimes, 1);
                ErrorTimes = 1;
            }
            else
            {
                //HttpContext.Session.SetInt32(ManagerSignInErrorTimes, ErrorTimes.Value + 1);
            }

            if (ErrorTimes > MaxErrorTimes)
            {
                //result.code = BadMsg.SignInErrorTimes;
                //result.msg = BadMsg.SignInErrorTimesMsg;
                return (false, "登录错误超过限制").ToJsonResult();
            }

            #endregion

            #region 再次属性判断

            //LoginModelValidation validation = new LoginModelValidation();
            //ValidationResult results = validation.Validate(model);
            //if (!results.IsValid)
            //{
            //    result.Code = BadMsg.CommonModelStateInvalidCode;
            //    result.Msg = results.ToString("||");
            //}

            #endregion

            model.Ip = HttpContext.GetClientUserIp();
            model.UniqueStr = UniqueStr;
            var account = await _service.SignInAsync(model);
            if (account == null)
            {
                return (false, "用户名/密码错误").ToJsonResult();
            }
            else if (account.IsLock)
            {
                return (false, "用户已锁定").ToJsonResult();
            }
            else
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Append("_guid", account.Session,
                    new CookieOptions() { Expires = DateTime.Now.AddMinutes(10080) });
            }
            var obj = new { guid = account.Session, msg = "登录成功", name = account.Name };
            return (true, obj).ToJsonResult();
        }

        /// <summary>
        /// 获取登录验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCaptchaImage")]
        public IActionResult GetCaptchaImage()
        {
            string captchaCode = CaptchaHelper.GenerateCaptchaCode();
            var result = CaptchaHelper.GetImage(116, 36, captchaCode);
            //_httpContextAccessor.HttpContext.Session.SetString(CaptchaCodeSessionName, captchaCode);
            return new FileStreamResult(new MemoryStream(result.CaptchaByteData), "image/png");
        }

        /// <summary>
        /// 验证验证码是否正确
        /// </summary>
        /// <param name="userInputCaptcha"></param>
        /// <returns></returns>
        private bool ValidateCaptchaCode(string userInputCaptcha)
        {
            //var isValid = userInputCaptcha.Equals(HttpContext.Session.GetString(CaptchaCodeSessionName), StringComparison.OrdinalIgnoreCase);
            //HttpContext.Session.Remove(CaptchaCodeSessionName);
            var isValid = true;
            return isValid;
        }

        [HttpPost("signout")]
        public async Task<string> SignOutAsync()
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //return RedirectToAction("Index");
            return string.Empty;
        }

        #endregion


        #region Main

        [HttpPost("home/menus")]
        public async Task<string> GetMenu()
        {
            //var account = (SysAccount)_memoryCacheService.Get("AccountInfo");
            //var roleId = account.RoleId;
            var accountId = _httpContextAccessor.HttpContext.Items["AccountId"].To<int>();
            if (accountId == 0) return "[]";
            SysAccount account = await _service.GetManagerByIdAsync(accountId);
            var roleId = account.RoleId;
            var roleList = _roleService.GetMenusByRoleId(roleId, UniqueStr);
            if (roleList == null) return "[]";
            var navViewTree = _roleService.GetMenusByRoleId(roleId, UniqueStr).GenerateTree(x => x.Id, x => x.ParentId);
            return navViewTree.ToJson();
        }

        #endregion


        #region 账户增删改

        [HttpPost("manager/addormodify")]
        public async Task<string> AccountAddOrModify([FromForm] ManagerAddOrModifyModel item)
        {
            var result = await _service.AddOrModifyAsync(item);
            return result.ToJsonResult();
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("manager/changelockstatus")]
        public async Task<string> ChangeLockStatusAsync([FromForm] ChangeStatusModel item)
        {
            //ManagerLockStatusModelValidation validationRules = new ManagerLockStatusModelValidation();
            //ValidationResult results = validationRules.Validate(item);
            //if (results.IsValid)
            //{
            var result = await _service.ChangeLockStatusAsync(item);
            //}
            //else
            //{
            //    result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
            //    result.ResultMsg = results.ToString("||");
            //}
            //return JsonHelper.ObjectToJSON(result);
            return result.ToJsonResult();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost("manager/changepassword")]
        public async Task<string> ChangePasswordAsync([FromForm] ChangePasswordModel item)
        {
            //if (!ModelState.IsValid)
            //{
            //    result.Code = BadMsg.CommonModelStateInvalidCode;
            //    result.Msg = ToErrorString(ModelState, "||");
            //}
            //else
            //{
            var result = await _service.ChangePasswordAsync(item);
            //}
            //return JsonHelper.ObjectToJSON(result);
            return result.ToJsonResult();
        }

        /// <summary>
        /// 加载账户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("manager/load")]
        public async Task<string> GetAccontsAsync([FromQuery] ManagerRequestModel model)
        {
            model.MerchantId = Convert.ToInt32(_httpContextAccessor.HttpContext.Items["MerchantId"]);
            model.UniqueStr = UniqueStr;
            var result = await _service.LoadDataAsync(model);
            return result.ToJson();
        }

        #endregion


        #region 角色增删改

        [HttpPost("role/load")]
        public string GetRolesAsync([FromQuery] ManagerRoleRequestModel model)
        {
            model.UniqueStr = UniqueStr;
            model.MerchantId = Convert.ToInt32(_httpContextAccessor.HttpContext.Items["MerchantId"]);
            return _roleService.LoadData(model).ToJson();
        }

        [HttpPost("role/addormodify")]
        public string RoleAddOrModify([FromForm] ManagerRoleAddOrModifyModel item)
        {
            item.MerchantId = Convert.ToInt32(_httpContextAccessor.HttpContext.Items["MerchantId"]);
            //ManagerRoleValidation validationRules = new ManagerRoleValidation();
            //ValidationResult results = validationRules.Validate(item);
            //if (results.IsValid)
            //{
            item.SysStr = UniqueStr;
            var result = _roleService.AddOrModify(item);
            //}
            //else
            //{
            //    result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
            //    result.ResultMsg = results.ToString("||");
            //}
            //return JsonHelper.ObjectToJSON(result);

            return result.ToJsonResult();
        }

        [HttpPost("role/roledic")]
        public string GetRoleDicAsync()
        {
            return (true, _roleService.GetRoleDic(Convert.ToInt32(_httpContextAccessor.HttpContext.Items["MerchantId"])).ToJson()).ToJsonResult();
        }


        [HttpPost("role/delete")]
        public string RoleDelete(int[] roleId)
        {
            return _roleService.DeleteIds(roleId).ToJson();
        }

        #endregion


        #region 菜单增删改

        [HttpGet("menu/load")]
        public string GetMenusAsync([FromQuery] MenuRequestModel model)
        {
            model.SysStr = UniqueStr;
            return _menuService.LoadData(model).ToJson();
        }

        [HttpPost("menu/addormodify")]
        public async Task<string> MenuAddOrModify([FromForm] MenuAddOrModifyModel item)
        {
            //MenuValidation validationRules = new MenuValidation();
            //ValidationResult results = validationRules.Validate(item);
            //if (results.IsValid)
            //{
            item.SysStr = UniqueStr;
            var result = await _menuService.AddOrModifyAsync(item);
            //}
            //else
            //{
            //    result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
            //    result.ResultMsg = results.ToString("||");
            //}
            //return JsonHelper.ObjectToJSON(result);

            return result.ToJsonResult();
        }

        [HttpPost("menu/delte")]
        public async Task<string> DeleteAsync(int[] menuId)
        {
            return (await _menuService.DeleteIdsAsync(menuId)).ToJsonResult();
        }

        [HttpPost("menu/changedisplaystatus")]
        public async Task<string> ChangeDisplayStatus([FromForm] ChangeStatusModel item)
        {
            //ManagerLockStatusModelValidation validationRules = new ManagerLockStatusModelValidation();
            //ValidationResult results = validationRules.Validate(item);
            //if (results.IsValid)
            //{
            //    result = await _service.ChangeDisplayStatusAsync(item);
            //}
            //else
            //{
            //    result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
            //    result.ResultMsg = results.ToString("||");
            //}
            //return JsonHelper.ObjectToJSON(result);

            return (await _menuService.ChangeDisplayStatusAsync(item)).ToJsonResult();
        }

        [HttpGet("menu/isexistname")]
        public async Task<string> IsExistsName([FromQuery] MenuAddOrModifyModel item)
        {
            item.SysStr = UniqueStr;
            var result = await _menuService.IsExistsNameAsync(item);
            return result.ToJson();
        }

        [HttpPost("menu/loaddatawithparentroleid")]
        public string LoadDataWithParentId([FromBody] RoleDatas da)
        {
            var menus = _menuService.GetChildListByParentId(da.ParentId, UniqueStr);
            var actions = _sysMenuActionService.GetList(SysMenuAction.ActionsDataType.None);

            var roleSelected = _rolePermissionService.GetIdsByRoleId(da.roleId);
            var actionRoleSelected = _rolePermissionService.GetActionIdsByRoleId(da.roleId);

            var sub = actions.Select(t =>
            {
                return new MenuTreeView
                {
                    Id = t.Id,
                    ParentId = t.ParentId,
                    Name = t.Name,
                    DisplayName = t.Name,
                    Checked = actionRoleSelected.Contains(t.Id)
                };

            }).ToList();
            var obj = menus.Select(t =>
            {
                bool exist = roleSelected.Contains(t.Id);
                return new MenuTreeView
                {
                    Id = t.Id,
                    ParentId = t.ParentId,
                    Name = t.Name,
                    DisplayName = t.DisplayName,
                    Checked = exist,
                };
            }).ToList();
            obj.AddRange(sub);
            obj.Add(new MenuTreeView()
            {
                Id = 100000,
                ParentId = 10000001,
                Name = roleSelected.ToJson(),
                DisplayName = $"{actionRoleSelected.ToJson()}{da.ToJson()}"
            });
            return obj.ToJson();
        }

        public class MenuTreeView
        {
            public int Id { get; set; }
            public int ParentId { get; set; }
            public string Name { get; set; }
            public string DisplayName { get; set; }
            public bool Checked { get; set; }

        }
        public class RoleDatas
        {
            public int ParentId { get; set; } = -1;
            public int roleId { get; set; }
        }

        #endregion
    }
}