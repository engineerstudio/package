using System;
using System.Threading.Tasks;
using Y.Packet.Services.IGames;
using Y.Packet.Repositories.IGames;
using Y.Packet.Entities.Games;

namespace Y.Packet.Services.Games
{
    public class GameApiRequestLogService : IGameApiRequestLogService
    {
        private readonly IGameApiRequestLogRepository _repository;

        public GameApiRequestLogService(IGameApiRequestLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<(bool, string)> InsertAsync(int merchantId, string typeStr, (bool, string, string) rt)
        {
            var m = new GameApiRequestLog()
            {
                MerchantId = merchantId,
                TypeStr = typeStr,
                RequestData = rt.Item2,
                ResultData = rt.Item3,
                Status = rt.Item1,
                CreateTime = DateTime.UtcNow.AddHours(8)
            };
            var r = await _repository.InsertAsync(m);
            if (r != null && r.Value > 0) return (true, "");
            return (false, "");
        }
    }
}