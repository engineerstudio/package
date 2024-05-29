
using System;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.LogsCoreController.Entity;
using Y.Infrastructure.Library.Core.LogsCoreController.IRepository;
using Y.Infrastructure.Library.Core.LogsCoreController.IService;

namespace Y.Infrastructure.Library.Core.LogsCoreController.Service
{
    public class ReqLogsService : IReqLogsService
    {
        private readonly IReqLogsRepository _repository;

        public ReqLogsService(IReqLogsRepository repository)
        {
            _repository = repository;
        }


        public void Insert(int merchantId, string url, string content)
        {
            if (url.IsNullOrEmpty()) url = "";
            if (content.IsNullOrEmpty()) content = "";

            var log = new ReqLogs()
            {
                MerchantId = merchantId,
                ReqUri = url,
                ReqDetails = content,
                ReqDatetime = DateTime.UtcNow.AddHours(8)
            };

            _repository.Insert(log);
        }
        public async Task InsertAsync(int merchantId, string url, string content)
        {
            if (url.IsNullOrEmpty()) url = "";
            if (content.IsNullOrEmpty()) content = "";

            var log = new ReqLogs()
            {
                MerchantId = merchantId,
                ReqUri = url,
                ReqDetails = content,
                ReqDatetime = DateTime.UtcNow.AddHours(8)
            };

            await _repository.InsertAsync(log);
        }
    }
}