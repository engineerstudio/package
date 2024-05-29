using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.WebInfrastructure.Entity;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Infrastructure.IApplication
{
    public interface IBaseHandlerService
    {

        int MerchantId { get; }

        int MemberId { get; }

        string IP { get; }
        string Host { get; }

        DeviceType Device { get; }

        public MemberInfo Member { get; }

        public MerchantInfo Merchant { get; }

        public AccountInfo Account { get; }
    }
}
