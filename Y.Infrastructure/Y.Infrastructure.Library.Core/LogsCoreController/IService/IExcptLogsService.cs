using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.LogsCoreController.Entity;
using Y.Infrastructure.Library.Core.LogsCoreController.Entity.ViewModel;

namespace Y.Infrastructure.Library.Core.LogsCoreController.IService
{
    public interface IExcptLogsService
    {
        void Insert(Exception ex, string machineName);
        Task<(IEnumerable<ExcptLogs>, int)> GetPageList(ExceptListPageQuery q);
        Task InsertAsync(Exception ex, string machineName);
    }
}