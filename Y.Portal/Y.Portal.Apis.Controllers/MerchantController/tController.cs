using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Y.Portal.Apis.Controllers.Helper;

namespace Y.Portal.Apis.Controllers.MerchantController
{
    [Route(RouteHelper.BaseMerchantRoute)]
    [ApiController]
    public class tController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "t";
        }
    }
}
