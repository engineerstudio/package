using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Y.Infrastructure.Library.Core.AuthController.IService;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.WebInfrastructure.Services;
using Y.Infrastructure.IApplication;
using Y.Infrastructure.ICache;
using Y.Packet.Services.IMerchants;

namespace Y.Portal.Apis.Controllers.MerchantController
{
    [Route("merchant/dcache")]
    [ApiController]
    [EnableCors]
    public class PageDataCacheController : ControllerBase
    {

        private readonly ISysRoleService _sysRoleService;
        private readonly IBaseHandlerService _baseHandlerService;
        private readonly IProjectInitializationService _projectInitializationService;
        public PageDataCacheController(ISysRoleService sysRoleService, IBaseHandlerService baseHandlerService, IProjectInitializationService projectInitializationService)
        {
            _sysRoleService = sysRoleService;
            _baseHandlerService = baseHandlerService;
            _projectInitializationService = projectInitializationService;
        }

        [HttpPost("init")]
        public string Init()
        {
            string[] entityAssembel = new string[] { "Y.Games.Entity", "Y.Merchants.Entity", "Y.Promotions.Entity", "Y.Members.Entity", "Y.Vips.Entity", "Y.Infrastructure.Library.Core" };

            string cache = _projectInitializationService.GetMerchantProjectPageData();
            if (cache.IsNullOrEmpty())
            {
                cache = $"{{\"GameDic\":{{}},\"EnumData\":{WebEnumCacheService.GetEnumStatusJsonString(entityAssembel)},\"Auth\":#auth#}}";
                _projectInitializationService.SetMerchantProjectPageData(cache);
            }
            //用户权限
            string auth = _sysRoleService.GetUnAuthorizedCodes(_baseHandlerService.Account.RoleId).ToJson();
            cache = cache.Replace("#auth#", auth);

            return cache;
        }

    }
}
