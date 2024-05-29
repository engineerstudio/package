using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using Y.Infrastructure.YCache.Extensions;
using Y.Infrastructure.YCache.YCacheImplementation;

namespace Y.Infrastructure.YCache.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            IServiceProvider services = new ServiceCollection().AddYCache().BuildServiceProvider();

            YCacheFactory f = (YCacheFactory)services.GetService<IYCacheFactory>();

            IYCache c = f.Create("RedisDb", @"{""Password"":"""", ""Host"":[{""Host"":""127.0.0.1"", ""Port"":""6379""}]}");

            var m = (RedisDb)c;

      
            var x = m.GetDb();

            x.SetAdd("a","1");

        }
    }
}
