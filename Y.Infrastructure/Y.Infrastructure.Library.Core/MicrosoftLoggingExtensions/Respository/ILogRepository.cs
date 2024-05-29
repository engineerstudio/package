using Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions.Model;
using Y.Infrastructure.Library.Core.Repository;

namespace Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions.Repository
{
    interface ILogRepository : IBaseRepository<ExcptLogs, int>
    {
    }
}