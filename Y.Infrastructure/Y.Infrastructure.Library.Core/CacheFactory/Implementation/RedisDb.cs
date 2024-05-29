using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Core.Implementations;
using StackExchange.Redis.Extensions.Newtonsoft;
using System;
using System.Runtime.InteropServices;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Extension;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.Library.Core.Helper;

namespace Y.Infrastructure.Library.Core.CacheFactory.Implementation
{
    public class RedisDb : YCacheAbs, IDisposable
    {
        private readonly IRedisCacheClient sut;
        private readonly IDatabase db;
        private readonly ISerializer serializer;
        private readonly RedisConfiguration redisConfiguration;
        private readonly IRedisCacheConnectionPoolManager connectionPoolManager;
        private bool isDisposed;
        private IntPtr nativeResource = Marshal.AllocHGlobal(100);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
                return;

            if (disposing)
            {
                // free managed resources
                db.FlushDatabase();
                db.Multiplexer.GetSubscriber().UnsubscribeAll();
                connectionPoolManager.Dispose();
            }

            // free native resources if there are any.
            if (nativeResource != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(nativeResource);
                nativeResource = IntPtr.Zero;
            }

            isDisposed = true;
        }


        protected IRedisCacheClient Sut => sut;

        public RedisDb()
        {
        }

        internal RedisDb(ISerializer serializer)
        {
            this.serializer = serializer;
        }

        public RedisDb(string configJson, string t)
        {
            var m = JsonHelper.JSONToObject<YCacheConfiguration>(configJson);

            redisConfiguration = new RedisConfiguration()
            {
                AbortOnConnectFail = true,
                KeyPrefix = "",
                Hosts = m.Host,
                AllowAdmin = true,
                ConnectTimeout = 3000,
                Database = m.Index,
                PoolSize = 5,
                ServerEnumerationStrategy = new ServerEnumerationStrategy()
                {
                    Mode = ServerEnumerationStrategy.ModeOptions.All,
                    TargetRole = ServerEnumerationStrategy.TargetRoleOptions.Any,
                    UnreachableServerAction = ServerEnumerationStrategy.UnreachableServerActionOptions.Throw
                }
            };

            this.serializer = new NewtonsoftSerializer();
            connectionPoolManager = new RedisCacheConnectionPoolManager(redisConfiguration);
            sut = new RedisCacheClient(connectionPoolManager, this.serializer, redisConfiguration);
            db = sut.GetDbFromConfiguration().Database;
        }

        //internal RedisDb(ISerializer serializer, RedisHost[] hosts)
        //{
        //    redisConfiguration = new RedisConfiguration()
        //    {
        //        AbortOnConnectFail = true,
        //        KeyPrefix = "MyPrefix__",
        //        Hosts = hosts,
        //        AllowAdmin = true,
        //        ConnectTimeout = 3000,
        //        Database = 0,
        //        PoolSize = 5,
        //        ServerEnumerationStrategy = new ServerEnumerationStrategy()
        //        {
        //            Mode = ServerEnumerationStrategy.ModeOptions.All,
        //            TargetRole = ServerEnumerationStrategy.TargetRoleOptions.Any,
        //            UnreachableServerAction = ServerEnumerationStrategy.UnreachableServerActionOptions.Throw
        //        }
        //    };

        //    this.serializer = serializer;
        //    connectionPoolManager = new RedisCacheConnectionPoolManager(redisConfiguration);
        //    sut = new RedisCacheClient(connectionPoolManager, this.serializer, redisConfiguration);
        //    db = sut.GetDbFromConfiguration().Database;
        //}
        public IRedisCacheClient GetRcc() => sut;
        public IDatabase GetDb() => db;
        public IRedisCacheConnectionPoolManager RedisCacheConnectionPoolManager => connectionPoolManager;
    }
}