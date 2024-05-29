using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Y.Infrastructure.Library.Core.Encrypt;

//using Y.Infrastructure.Library.Core.Encrypt;
using Y.Infrastructure.Library.Core.Helper;

namespace Y.Infrastructure.Library.EventsTriggers
{
    public class EventsInstanceHelper
    {
        private readonly object _locker = new object();
        private static volatile EventsInstanceHelper _instance;
        public static EventsInstanceHelper Instance()
        {
            if (_instance == null)
                _instance = new EventsInstanceHelper();
            return _instance;
        }
        private EventsInstanceHelper()
        { }



        private readonly ConcurrentDictionary<string, IMessageHandler> CacheDic = new ConcurrentDictionary<string, IMessageHandler>(StringComparer.OrdinalIgnoreCase);


        public IMessageHandler GetHandler(string key, string value)
        {
            // 这个地方出现的bug： 初始化redis多个库，所以name需不一致
            string cacheName = $"{key}{MD5EncryptHelper.ToMD5(key)}";
            IMessageHandler cache;
            if (CacheDic.TryGetValue(cacheName, out cache)) return cache;
            dynamic type = GetType();
            // TODO linux下运行考虑路径
            var currentDirectory = Y.Infrastructure.Library.Core.Helper.ServerHelper.GetAssemblyPath();
            //string currentDirectory = System.IO.Path.GetDirectoryName(type.Assembly.Location);
            Assembly thisAssem = null;
            string dllDirectory = string.Empty;
            if (ServerHelper.IsWindowRunTime())
                dllDirectory = $"{currentDirectory}\\Y.Application.BusMessageHandler.dll";
            else
                dllDirectory = $"{currentDirectory}/Y.Application.BusMessageHandler.dll";
            //Console.WriteLine(dllDirectory);
            thisAssem = Assembly.LoadFrom(dllDirectory);
            Object[] constructParms = new object[] { value };
            string typeName = "Y.Application.BusMessageHandler.MessageHandler." + key;
            var o = System.Activator.CreateInstance(thisAssem.GetType(typeName), constructParms);
            return CacheDic[cacheName] = (IMessageHandler)o;
        }
    }
}
