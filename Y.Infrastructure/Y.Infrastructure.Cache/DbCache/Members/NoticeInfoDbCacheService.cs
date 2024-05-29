using Microsoft.Extensions.Options;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using Y.Infrastructure.ICache.IDbCache.Members;
using Y.Packet.Entities.Members;

namespace Y.Infrastructure.Cache.DbCache.Members
{
    public class NoticeInfoDbCacheService : BaseDbCacheService<NoticeInfo, int>, INoticeInfoDbCacheService
    {
        public NoticeInfoDbCacheService(IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {

        }
    }
}
