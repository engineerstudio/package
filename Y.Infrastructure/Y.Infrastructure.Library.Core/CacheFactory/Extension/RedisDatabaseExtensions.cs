using StackExchange.Redis;

namespace Y.Infrastructure.Library.Core.CacheFactory.Extension
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