using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Infrastructure.YCache.Extensions
{
    internal static class RedisDatabaseExtensions
    {
        public static void FlushDatabase(this IDatabase db)
        {
            foreach (var endpoint in db.Multiplexer.GetEndPoints())
            {
                var server = db.Multiplexer.GetServer(endpoint);

                server.FlushDatabase();
            }
        }
    }
}
