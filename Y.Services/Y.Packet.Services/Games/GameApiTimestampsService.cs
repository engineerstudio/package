using System;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Services.IGames;
using Y.Packet.Repositories.IGames;
using Y.Packet.Entities.Games;

namespace Y.Packet.Services.Games
{
    public class GameApiTimestampsService : IGameApiTimestampsService
    {
        private readonly IGameApiTimestampsRepository _repository;

        public GameApiTimestampsService(IGameApiTimestampsRepository repository)
        {
            _repository = repository;
        }


        public async Task<GameApiTimestamps> GameApiTimestampsAsync(GameType gameType)
        {
            return await _repository.GetByTypeStrAsync(gameType.ToString());
        }

        public async Task<(bool, string)> SaveApiLogQueryTimeAsync(GameType gameType, DateTime dateTime, long stamps)
        {
            if (dateTime < DateTime.Now.AddDays(-35)) return (false, "时间不能设置在35天前");
            if (stamps < 0) return (false, "时间戳不能设置为负数");
            GameApiTimestamps entity = null;
            entity = await GameApiTimestampsAsync(gameType);
            int? rt;
            if (entity == null)
            {
                //Console.WriteLine($"新增时间戳{gameType}");
                entity = new GameApiTimestamps()
                {
                    Type = gameType,
                    QueryTime = dateTime,
                    Timestamps = stamps,
                    Mark = "",
                    TypeStr = gameType.ToString()
                };
                rt = await _repository.InsertWithCacheAsync(entity);
            }
            else
            {
                entity.QueryTime = dateTime;
                entity.Timestamps = stamps;
                entity.Mark = "";
                rt = await _repository.UpdateWithCacheAsync(entity);
            }

            if (rt != null && rt.HasValue && rt.Value > 0) return (true, "保存成功");
            return (false, "保存失败");
        }


        public async Task<GameApiTimestamps> GetByTypeStrAsync(string typeStr)
        {
            var rt = await _repository.GetByTypeStrAsync(typeStr);
            if (rt == null)
            {
                rt = new GameApiTimestamps();
                rt.QueryTime = DateTime.UtcNow.AddHours(8);
                rt.Timestamps = 0;
                rt.TypeStr = typeStr;
                rt.Mark = "";
            }
            return rt;
        }



    }
}