using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.WebInfrastructure.Entity;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.IApplication;

namespace Y.Infrastructure.Application
{
    public class BaseHandlerService : IBaseHandlerService
    {


        private readonly IHttpContextAccessor _httpContextAccessor;
        public BaseHandlerService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int MerchantId
        {
            get
            {
                return Convert.ToInt32(_httpContextAccessor.HttpContext.Items["MerchantId"]);
            }
        }

        public int MemberId => Convert.ToInt32(_httpContextAccessor.HttpContext.Items["MemberId"]);

        public string IP => Convert.ToString(_httpContextAccessor.HttpContext.Items["IP"]);

        public MemberInfo Member => (MemberInfo)_httpContextAccessor.HttpContext.Items["Member"];

        public MerchantInfo Merchant => (MerchantInfo)_httpContextAccessor.HttpContext.Items["MerchantInfo"];

        public DeviceType Device => _httpContextAccessor.HttpContext.Items["Device"].ToString().ToEnum<DeviceType>().Value;

        public AccountInfo Account => (AccountInfo)_httpContextAccessor.HttpContext.Items["AccountInfo"];

        public string Host => Convert.ToString(_httpContextAccessor.HttpContext.Items["Host"]);
    }
}
