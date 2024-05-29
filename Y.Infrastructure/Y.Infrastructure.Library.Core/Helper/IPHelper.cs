using System.Collections.Generic;

namespace Y.Infrastructure.Library.Core.Helper
{
    public class IPHelper
    {
        /// <summary>
        /// 本机访问调试的IP地址
        /// </summary>
        private static readonly List<string> localIpList = new List<string>()
        {
            "127.0.0.1"
        };

        public static bool IsLocalAddress(string ip)
        {
            return localIpList.Contains(ip);
        }
    }
}