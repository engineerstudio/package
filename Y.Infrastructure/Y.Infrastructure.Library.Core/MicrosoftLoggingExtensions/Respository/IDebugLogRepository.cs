using Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions.Model;
using Y.Infrastructure.Library.Core.Repository;

namespace Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions.Repository
{
    interface IDebugLogRepository : IBaseRepository<DebugLog, int>
    {
    }
}