using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Y.Infrastructure.Library.Core.Encrypt;
using Y.Infrastructure.Library.Core.Helper;

namespace Y.Infrastructure.YCache
{
    public class YCacheFactory : IYCacheFactory
    {

        private readonly ConcurrentDictionary<string, IYCache> CacheDic = new ConcurrentDictionary<string, IYCache>(StringComparer.OrdinalIgnoreCase);
        public IYCache Create(string name, string config)
        {
            //IYCache cache;
            //if (CacheDic.TryGetValue(name, out cache)) return cache;
            //dynamic type = GetType();
            //string currentDirectory = System.IO.Path.GetDirectoryName(type.Assembly.Location);
            //Assembly thisAssem = Assembly.LoadFrom($"{currentDirectory}\\Y.Infrastructure.YCache.dll");
            //Object[] constructParms = new object[] { config };
            //var o = System.Activator.CreateInstance(thisAssem.GetType($"Y.Infrastructure.YCache.YCacheImplementation.{name}"), constructParms);
            //return CacheDic[name] = (IYCache)o;


            // 这个地方出现的bug： 初始化redis多个库，所以name需不一致
            string cacheName = $"{name}{MD5EncryptHelper.ToMD5(config)}";
            IYCache cache;
            if (CacheDic.TryGetValue(cacheName, out cache)) return cache;
            dynamic type = GetType();
            // TODO linux下运行考虑路径
            var currentDirectory = Y.Infrastructure.Library.Core.Helper.ServerHelper.GetAssemblyPath();
            //string currentDirectory = System.IO.Path.GetDirectoryName(type.Assembly.Location);
            Assembly thisAssem = null;
            string dllDirectory = string.Empty;
            if (ServerHelper.IsWindowRunTime())
                dllDirectory = $"{currentDirectory}\\Y.Infrastructure.YCache.dll";
            else
                dllDirectory = $"{currentDirectory}/Y.Infrastructure.YCache.dll";
            thisAssem = Assembly.LoadFrom(dllDirectory);
            Object[] constructParms = new object[] { config };
            string typeName = "Y.Infrastructure.YCache.YCacheImplementation." + name;
            var o = System.Activator.CreateInstance(thisAssem.GetType(typeName), constructParms);
            return CacheDic[cacheName] = (IYCache)o;
        }


        public void Dispose()
        {
            CacheDic.Clear();
        }
    }
}
