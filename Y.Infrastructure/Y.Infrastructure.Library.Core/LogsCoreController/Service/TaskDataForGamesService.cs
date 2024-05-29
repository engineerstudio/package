using System;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.LogsCoreController.Entity;
using Y.Infrastructure.Library.Core.LogsCoreController.IRepository;
using Y.Infrastructure.Library.Core.LogsCoreController.IService;

namespace Y.Infrastructure.Library.Core.LogsCoreController.Service
{
    public class TaskDataForGamesService : ITaskDataForGamesService
    {
        private readonly ITaskDataForGamesRepository _repository;

        public TaskDataForGamesService(ITaskDataForGamesRepository repository)
        {
            _repository = repository;
        }


        public void Insert(bool success, string gameType, string reqData, string responseData, string reqDate)
        {
            var taskDataForGames = new TaskDataForGames()
            {
                Successed = success,
                Url = "",
                GameType = gameType,
                Requestd = reqData ?? "",
                Responsed = responseData ?? "",
                GameReqTime = reqDate,
                CreateTime = DateTime.UtcNow.AddHours(8)
            };

            _repository.Insert(taskDataForGames);
        }

        public async Task InsertAsync(bool success, string gameType, string reqData, string responseData, string reqDate)
        {
            var taskDataForGames = new TaskDataForGames()
            {
                Successed = success,
                Url = "",
                GameType = gameType,
                Requestd = reqData ?? "",
                Responsed = responseData ?? "",
                GameReqTime = reqDate,
                CreateTime = DateTime.UtcNow.AddHours(8)
            };

            await _repository.InsertAsync(taskDataForGames);
        }
    }
}