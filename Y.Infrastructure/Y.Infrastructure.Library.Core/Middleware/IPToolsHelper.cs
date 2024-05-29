//using IPTools.Core;
//using Y.Infrastructure.Library.Core.Helper;

//namespace Y.Infrastructure.Library.Core.Middleware
//{
//    /// <summary>
//    /// IP插件 https://github.com/stulzq/IPTools
//    /// https://github.com/lionsoul2014/ip2region
//    /// </summary>
//    public static class IPToolsHelper
//    {
//        static IPToolsHelper()
//        {
//            IpToolSettings.InternationalDbPath = RuntimeHelper.Path;
//            IpToolSettings.LoadInternationalDbToMemory = true;
//        }


//        public static string GetLocation(string ip)
//        {
//            IpInfo info = IpTool.Search(ip);
//            return $"{info.Country}*{info.Province}*{info.City}";
//        }
//    }
//}