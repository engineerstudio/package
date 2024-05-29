using System;
using System.Collections.Concurrent;
using System.Reflection;
using Y.Infrastructure.Library.Core.Encrypt;
using Y.Infrastructure.Library.Core.Helper;

namespace Y.Infrastructure.Library.Core.CacheFactory.Factory
{
    public class YCacheFactory : IYCacheFactory
    {
        private readonly ConcurrentDictionary<string, IYCache> CacheDic =
            new ConcurrentDictionary<string, IYCache>(StringComparer.OrdinalIgnoreCase);

        public IYCache Create(string name, string config)
        {
            // 这个地方出现的bug： 初始化redis多个库，所以name需不一致
            string cacheName = $"{name}{MD5EncryptHelper.ToMD5(config)}";
            IYCache cache;
            if (CacheDic.TryGetValue(cacheName, out cache)) return cache;
            dynamic type = GetType();
            //  linux下运行考虑路径
            var currentDirectory = Y.Infrastructure.Library.Core.Helper.ServerHelper.GetAssemblyPath();
            //string currentDirectory = System.IO.Path.GetDirectoryName(type.Assembly.Location);
            Assembly thisAssem = null;
            string dllDirectory = string.Empty;
            if (ServerHelper.IsWindowRunTime())
                dllDirectory = $"{currentDirectory}\\Y.Infrastructure.Library.Core.dll";
            else
                dllDirectory = $"{currentDirectory}/Y.Infrastructure.Library.Core.dll";
            Console.WriteLine(dllDirectory);
            thisAssem = Assembly.LoadFrom(dllDirectory);
            Object[] constructParms = new object[] {config, null};
            string typeName = "Y.Infrastructure.Library.Core.CacheFactory.Implementation." + name;
            var o = System.Activator.CreateInstance(thisAssem.GetType(typeName), constructParms);
            return CacheDic[cacheName] = (IYCache) o;
        }


        public void Dispose()
        {
            CacheDic.Clear();
        }
    }
}