using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.IApplication;
using Y.Packet.Entities.Merchants;
using Y.Packet.Entities.Merchants.ViewModels;
using Y.Packet.Services.IMerchants;
using Y.Portal.Apis.Controllers.Helper;

namespace Y.Portal.Apis.Controllers.MerchantController
{
    [Route(RouteHelper.BaseMerchantRoute)]
    [ApiController]
    public class MerchantController : ControllerBase
    {


        private readonly IBaseHandlerService _baseHandlerService;
        private readonly IMerchantService _merchantService;
        string img_url = string.Empty;
        public MerchantController(IBaseHandlerService baseHandlerService, IMerchantService merchantService, IOptionsSnapshot<Dictionary<string, string>> options)
        {
            _baseHandlerService = baseHandlerService;
            _merchantService = merchantService;
            img_url = options.Get(DefaultString.Sys_Default)[DefaultString.UrlConfig_IMG];
        }


        #region VIP全局配置


        [HttpPost("getvipconfig")]
        public async Task<string> GetVipsConfig()
        {
            return (await _merchantService.GetVipsConfigAsync(_baseHandlerService.MerchantId)).ToJsonResult();
        }
        [HttpPost("savevipconfig")]
        public async Task<string> SaveVipsConfig([FromForm] Merchant_VipsConfig config)
        {
            return (await _merchantService.SaveVipsConfigAsync(_baseHandlerService.MerchantId, config)).ToJsonResult();
        }

        #endregion



        #region 站点基础配置

        /// <summary>
        /// 获取站点客户配置
        /// </summary>
        /// <returns></returns>
        [HttpPost("getcsconfig")]
        public async Task<string> GetCustomerConfig()
        {
            var rt = await _merchantService.GetCustomerConfigAsync(_baseHandlerService.MerchantId);

            if (!rt.Item1) return rt.ToJsonResult();
            string default_img = $"{img_url}{DefaultString.DefaultImage}";
            if (rt.Item2.IsNullOrEmpty()) return (rt.Item1, string.Empty, new CustomerConfigModel()
            {
                WebLogo = default_img,
                MobileLogo = default_img,
                ServiceLink = string.Empty,
                QRCode = default_img,
                EnabledH5 = false,
                EnabledAgentModel = false,
            }).ToJsonResult(); ;

            var j = JsonHelper.JSONToObject<Merchant_CustomerConfig>(rt.Item2);

            return (rt.Item1, string.Empty, new CustomerConfigModel()
            {
                WebLogo = j.PcLogo,
                MobileLogo = j.H5Logo,
                ServiceLink = j.ServiceLink,
                QRCode = j.DownloadQRCode,
                EnabledH5 = j.EnabledH5Visit,
                EnabledAgentModel = j.EnabledAgentPattern,
            }).ToJsonResult(); ;
        }


        [HttpPost("savecsconfig")]
        public async Task<string> SaveCustomerConfig([FromBody] CustomerConfigModel m)
        {
            if (m.WebLogo.IsNullOrEmpty()) return (false, "请上传站点Logo").ToJsonResult();
            if (m.MobileLogo.IsNullOrEmpty()) return (false, "请上传移动站点Logo").ToJsonResult();
            if (m.QRCode.IsNullOrEmpty()) return (false, "请上传APP下载二维码").ToJsonResult();

            var config = new Merchant_CustomerConfig()
            {
                PcLogo = m.WebLogo,
                H5Logo = m.MobileLogo,
                ServiceLink = m.ServiceLink,
                DownloadQRCode = m.QRCode,
                EnabledH5Visit = m.EnabledH5,
                EnabledAgentPattern = m.EnabledAgentModel
            };

            return (await _merchantService.SaveCustomerConfigAsync(_baseHandlerService.MerchantId, config)).ToJsonResult();
        }


        #endregion






    }
}
