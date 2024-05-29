
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Y.Infrastructure.Library.Core.AuthController;
using Y.Infrastructure.Library.Core.AuthController.IService;

namespace Y.Portal.Apis.Controllers.MerchantController
{

    [Route("merchant/auth/account")]
    //[ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class SysAccountController : BaseSysController
    {

        private readonly string CaptchaCodeSessionName = "CaptchaCode";
        private readonly string ManagerSignInErrorTimes = "ManagerSignInErrorTimes";
        private readonly int MaxErrorTimes = 3;
        private readonly ISysAccountService _service;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly ICookieService _cookieService;
        //private readonly IMemoryCacheService _memoryCacheService;
        private readonly ISysRoleService _roleService;
        private readonly ISysRolePermissionService _rolePermissionService;
        private readonly ISysMenuService _menuService;

        public SysAccountController(ISysAccountService service, IHttpContextAccessor httpContextAccessor
            //, ICookieService cookieService
            , ISysRoleService roleService, ISysRolePermissionService rolePermissionService, ISysMenuService menuService
            //, IMemoryCacheService memoryCacheService
            , ISysMenuActionService sysMenuActionService
            ) : base(service, httpContextAccessor, roleService, rolePermissionService, menuService, sysMenuActionService, "bwMerchant")
        {
            _service = service;
            _httpContextAccessor = httpContextAccessor;
            //_cookieService = cookieService;
            //_memoryCacheService = memoryCacheService;
            _roleService = roleService;
            _rolePermissionService = rolePermissionService;
            _menuService = menuService;
        }



        //private readonly string CaptchaCodeSessionName = "CaptchaCode";
        //private readonly string ManagerSignInErrorTimes = "ManagerSignInErrorTimes";
        //private readonly int MaxErrorTimes = 3;
        //private readonly ISysAccountService _service;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly ICookieService _cookieService;
        //private readonly IMemoryCacheService _memoryCacheService;
        //private readonly ISysRoleService _roleService;
        //private readonly ISysRolePermissionService _rolePermissionService;
        //private readonly ISysMenuService _menuService;
        //private readonly IBaseHandlerService _baseHandler;
        //private readonly ISysMenuActionService _sysMenuActionService;

        //public SysAccountController(ISysAccountService service, IHttpContextAccessor httpContextAccessor, ICookieService cookieService, ISysRoleService roleService, ISysRolePermissionService rolePermissionService, ISysMenuService menuService, IMemoryCacheService memoryCacheService, IBaseHandlerService baseHandler, ISysMenuActionService sysMenuActionService) //
        //{
        //    _service = service;
        //    _httpContextAccessor = httpContextAccessor;
        //    _cookieService = cookieService;
        //    _memoryCacheService = memoryCacheService;
        //    _roleService = roleService;
        //    _rolePermissionService = rolePermissionService;
        //    _menuService = menuService;
        //    _baseHandler = baseHandler;
        //    _sysMenuActionService = sysMenuActionService;
        //}


        //#region 登录

        //[HttpPost("signin")]
        //public async Task<string> SignInAsync([FromForm] LoginModel model)
        //{
        //    #region 判断验证码
        //    //if (!ValidateCaptchaCode(model.CaptchaCode))
        //    //{
        //    //    result.code = BadMsg.SignInCaptchaError;
        //    //    result.msg = BadMsg.SignInCaptchaErrorMsg;
        //    //    return result.ToJson();
        //    //}
        //    #endregion
        //    #region 判断错误次数
        //    var ErrorTimes = 0;// HttpContext.Session.GetInt32(ManagerSignInErrorTimes);
        //    if (ErrorTimes == null)
        //    {
        //        //HttpContext.Session.SetInt32(ManagerSignInErrorTimes, 1);
        //        ErrorTimes = 1;
        //    }
        //    else
        //    {
        //        //HttpContext.Session.SetInt32(ManagerSignInErrorTimes, ErrorTimes.Value + 1);
        //    }
        //    if (ErrorTimes > MaxErrorTimes)
        //    {
        //        //result.code = BadMsg.SignInErrorTimes;
        //        //result.msg = BadMsg.SignInErrorTimesMsg;
        //        return (false, "登录错误超过限制").ToJsonResult();
        //    }
        //    #endregion
        //    #region 再次属性判断
        //    //LoginModelValidation validation = new LoginModelValidation();
        //    //ValidationResult results = validation.Validate(model);
        //    //if (!results.IsValid)
        //    //{
        //    //    result.Code = BadMsg.CommonModelStateInvalidCode;
        //    //    result.Msg = results.ToString("||");
        //    //}
        //    #endregion

        //    model.Ip = HttpContext.GetClientUserIp();
        //    var account = await _service.SignInAsync(model);
        //    if (account == null)
        //    {
        //        return (false, "用户名/密码错误").ToJsonResult();
        //    }
        //    else if (account.IsLock)
        //    {
        //        return (false, "用户已锁定").ToJsonResult();
        //    }
        //    else
        //    {
        //        //_httpContextAccessor.HttpContext.Session.SetInt32("Id", account.Id);
        //        //_httpContextAccessor.HttpContext.Session.SetInt32("RoleId", account.RoleId);
        //        //_httpContextAccessor.HttpContext.Session.SetString("NickName", account.NickName ?? "匿名");
        //        //_httpContextAccessor.HttpContext.Session.SetString("Email", account.Email ?? "");
        //        //_httpContextAccessor.HttpContext.Session.SetString("Avatar", account.Avatar ?? "/images/userface1.jpg");
        //        //_httpContextAccessor.HttpContext.Session.SetString("Mobile", account.Mobile ?? "");

        //        _cookieService.Set("_guid", account.Session, 10080000);

        //    }
        //    var obj = new { guid = account.Session, msg = "登录成功", name = account.Name };
        //    return (true, obj.ToJson()).ToJsonResult();
        //}

        ///// <summary>
        ///// 获取登录验证码
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("GetCaptchaImage")]
        //public IActionResult GetCaptchaImage()
        //{
        //    string captchaCode = CaptchaHelper.GenerateCaptchaCode();
        //    var result = CaptchaHelper.GetImage(116, 36, captchaCode);
        //    //_httpContextAccessor.HttpContext.Session.SetString(CaptchaCodeSessionName, captchaCode);
        //    return new FileStreamResult(new MemoryStream(result.CaptchaByteData), "image/png");
        //}

        ///// <summary>
        ///// 验证验证码是否正确
        ///// </summary>
        ///// <param name="userInputCaptcha"></param>
        ///// <returns></returns>
        //private bool ValidateCaptchaCode(string userInputCaptcha)
        //{
        //    //var isValid = userInputCaptcha.Equals(HttpContext.Session.GetString(CaptchaCodeSessionName), StringComparison.OrdinalIgnoreCase);
        //    //HttpContext.Session.Remove(CaptchaCodeSessionName);
        //    var isValid = true;
        //    return isValid;
        //}

        //[HttpPost("signout")]
        //public async Task<string> SignOutAsync()
        //{
        //    //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    //return RedirectToAction("Index");
        //    return string.Empty;
        //}
        //#endregion



        //#region Main

        ///// <summary>
        ///// 左侧的菜单树
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost("home/menus")]
        //public string GetMenu()
        //{
        //    //var account = (SysAccount)_memoryCacheService.Get("AccountInfo");
        //    //var roleId = account.RoleId;
        //    var roleId = _baseHandler.Account.RoleId;
        //    var navViewTree = _roleService.GetMenusByRoleId(roleId).GenerateTree(x => x.Id, x => x.ParentId);
        //    return navViewTree.ToJson();
        //}

        ///// <summary>
        ///// 获取所有的隐藏菜单
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost("home/codes")]
        //public string GetUnAuthorizedActionsCode()
        //{
        //    var roleId = _baseHandler.Account.RoleId;

        //    return (true, _roleService.GetUnAuthorizedCodes(roleId)).ToJsonResult();
        //}


        //#endregion




        //#region 账户增删改

        //[HttpPost("manager/addormodify")]
        //public async Task<string> AccountAddOrModify([FromForm] ManagerAddOrModifyModel item)
        //{
        //    var result = await _service.AddOrModifyAsync(item);
        //    return result.ToJsonResult();
        //}

        ///// <summary>
        ///// 修改状态
        ///// </summary>
        ///// <param name="item"></param>
        ///// <returns></returns>
        //[HttpPost("manager/changelockstatus")]
        //public async Task<string> ChangeLockStatusAsync([FromForm] ChangeStatusModel item)
        //{
        //    //ManagerLockStatusModelValidation validationRules = new ManagerLockStatusModelValidation();
        //    //ValidationResult results = validationRules.Validate(item);
        //    //if (results.IsValid)
        //    //{
        //    var result = await _service.ChangeLockStatusAsync(item);
        //    //}
        //    //else
        //    //{
        //    //    result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
        //    //    result.ResultMsg = results.ToString("||");
        //    //}
        //    //return JsonHelper.ObjectToJSON(result);
        //    return result.ToJsonResult();
        //}

        ///// <summary>
        ///// 修改密码
        ///// </summary>
        ///// <param name="item"></param>
        ///// <returns></returns>
        //[HttpPost("manager/changepassword")]
        //public async Task<string> ChangePasswordAsync([FromForm] ChangePasswordModel item)
        //{
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    result.Code = BadMsg.CommonModelStateInvalidCode;
        //    //    result.Msg = ToErrorString(ModelState, "||");
        //    //}
        //    //else
        //    //{
        //    var result = await _service.ChangePasswordAsync(item);
        //    //}
        //    //return JsonHelper.ObjectToJSON(result);
        //    return result.ToJsonResult();
        //}

        ///// <summary>
        ///// 加载账户列表
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost("manager/load")]
        //public async Task<string> GetAccontsAsync([FromQuery] ManagerRequestModel model)
        //{
        //    model.MerchantId = _baseHandler.MerchantId;
        //    var result = await _service.LoadDataAsync(model);
        //    return result.ToJson();
        //}


        //#endregion



        //#region 角色增删改

        //[HttpPost("role/load")]
        //public string GetRolesAsync([FromQuery] ManagerRoleRequestModel model)
        //{
        //    model.MerchantId = _baseHandler.MerchantId;
        //    return _roleService.LoadData(model).ToJson();
        //}

        //[HttpPost("role/addormodify")]
        //public string RoleAddOrModify([FromForm] ManagerRoleAddOrModifyModel item)
        //{
        //    item.MerchantId = _baseHandler.MerchantId;
        //    //ManagerRoleValidation validationRules = new ManagerRoleValidation();
        //    //ValidationResult results = validationRules.Validate(item);
        //    //if (results.IsValid)
        //    //{
        //    var result = _roleService.AddOrModify(item);
        //    //}
        //    //else
        //    //{
        //    //    result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
        //    //    result.ResultMsg = results.ToString("||");
        //    //}
        //    //return JsonHelper.ObjectToJSON(result);

        //    return result.ToJsonResult();
        //}

        //[HttpPost("role/delete")]
        //public string RoleDelete(int[] roleId)
        //{
        //    return _roleService.DeleteIds(roleId).ToJson();
        //}

        //[HttpPost("role/roledic")]
        //public string GetRoleDicAsync()
        //{
        //    return (true, _roleService.GetRoleDic(_baseHandler.MerchantId).ToJson()).ToJsonResult();
        //}


        //#endregion



        //#region 菜单增删改

        //[HttpGet("menu/load")]
        //public string GetMenusAsync([FromQuery] MenuRequestModel model)
        //{
        //    return _menuService.LoadData(model).ToJson();
        //}

        //[HttpPost("menu/addormodify")]
        //public async Task<string> MenuAddOrModify([FromForm] MenuAddOrModifyModel item)
        //{
        //    //MenuValidation validationRules = new MenuValidation();
        //    //ValidationResult results = validationRules.Validate(item);
        //    //if (results.IsValid)
        //    //{
        //    var result = await _menuService.AddOrModifyAsync(item);
        //    //}
        //    //else
        //    //{
        //    //    result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
        //    //    result.ResultMsg = results.ToString("||");
        //    //}
        //    //return JsonHelper.ObjectToJSON(result);

        //    return result.ToJsonResult();
        //}

        //[HttpPost("menu/delte")]
        //public async Task<string> DeleteAsync(int[] menuId)
        //{
        //    return (await _menuService.DeleteIdsAsync(menuId)).ToJsonResult();
        //}

        //[HttpPost("menu/changedisplaystatus")]
        //public async Task<string> ChangeDisplayStatus([FromForm] ChangeStatusModel item)
        //{
        //    //ManagerLockStatusModelValidation validationRules = new ManagerLockStatusModelValidation();
        //    //ValidationResult results = validationRules.Validate(item);
        //    //if (results.IsValid)
        //    //{
        //    //    result = await _service.ChangeDisplayStatusAsync(item);
        //    //}
        //    //else
        //    //{
        //    //    result.ResultCode = ResultCodeAddMsgKeys.CommonModelStateInvalidCode;
        //    //    result.ResultMsg = results.ToString("||");
        //    //}
        //    //return JsonHelper.ObjectToJSON(result);

        //    return (await _menuService.ChangeDisplayStatusAsync(item)).ToJsonResult();
        //}

        //[HttpGet("menu/isexistname")]
        //public async Task<string> IsExistsName([FromQuery] MenuAddOrModifyModel item)
        //{
        //    var result = await _menuService.IsExistsNameAsync(item);
        //    return result.ToJson();
        //}

        //[HttpPost("menu/loaddatawithparentroleid")]
        //public string LoadDataWithParentId([FromForm] int ParentId = -1, [FromForm] int roleId = -1)
        //{
        //    var menus = _menuService.GetChildListByParentId(ParentId);
        //    var actions = _sysMenuActionService.GetList(SysMenuAction.ActionsDataType.None);

        //    var roleSelected = _rolePermissionService.GetIdsByRoleId(roleId);
        //    var actionRoleSelected = _rolePermissionService.GetActionIdsByRoleId(roleId);

        //    var sub = actions.Select(t =>
        //    {
        //        return new MenuTreeView
        //        {
        //            Id = t.Id,
        //            ParentId = t.ParentId,
        //            Name = t.Name,
        //            DisplayName = t.Name,
        //            Checked = actionRoleSelected.Contains(t.Id)
        //        };

        //    }).ToList();
        //    var obj = menus.Select(t =>
        //    {
        //        return new MenuTreeView
        //        {
        //            Id = t.Id,
        //            ParentId = t.ParentId,
        //            Name = t.Name,
        //            DisplayName = t.DisplayName,
        //            Checked = roleSelected.Contains(t.Id),
        //        };
        //    }).ToList();
        //    obj.AddRange(sub);

        //    return obj.ToJson();
        //}

        //#endregion


    }
}
