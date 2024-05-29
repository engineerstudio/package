using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.LogsCoreController.Entity;
using Y.Infrastructure.Library.Core.LogsCoreController.Entity.ViewModel;
using Y.Infrastructure.Library.Core.LogsCoreController.IRepository;
using Y.Infrastructure.Library.Core.LogsCoreController.IService;

namespace Y.Infrastructure.Library.Core.LogsCoreController.Service
{
    public class ExcptLogsService : IExcptLogsService
    {
        private readonly IExcptLogsRepository _repository;

        public ExcptLogsService(IExcptLogsRepository repository)
        {
            _repository = repository;
        }


        public async Task<(IEnumerable<ExcptLogs>, int)> GetPageList(ExceptListPageQuery q)
        {
            var parms = new DynamicParameters();
            string conditions = $" WHERE 1=1 ";
            if (q.MerchantId != 0) conditions = $" AND MerchantId={q.MerchantId} ";
            //if (!string.IsNullOrEmpty(q.Name))
            //{
            //    conditions += $" AND Name like @Name ";
            //    parms.Add("Name", $"%{q.Name}%");
            //}

            var rt = await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", parms);
            return (rt, _repository.RecordCount(conditions, parms));
        }


        public void Insert(Exception ex, string machineName)
        {
            var logs = new ExcptLogs()
            {
                MachineName = machineName,
                Logged = DateTime.UtcNow.AddHours(8),
                Level = "",
                Logger = "",
                Callsite = "",
                Exception = "",
                Message = ex.Message,
                StackTrace = ex.ExtractAllStackTrace()
            };
            _repository.Insert(logs);
        }


        public async Task InsertAsync(Exception ex, string machineName)
        {
            var logs = new ExcptLogs()
            {
                MachineName = machineName,
                Logged = DateTime.UtcNow.AddHours(8),
                Level = "",
                Logger = "",
                Callsite = "",
                Exception = "",
                Message = ex.Message,
                StackTrace = ex.ExtractAllStackTrace()
            };
            await _repository.InsertAsync(logs);
        }

    }
}