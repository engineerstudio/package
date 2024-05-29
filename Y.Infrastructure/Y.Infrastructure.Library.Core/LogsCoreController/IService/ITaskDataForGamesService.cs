using System.Threading.Tasks;

namespace Y.Infrastructure.Library.Core.LogsCoreController.IService
{
    public interface ITaskDataForGamesService
    {
        void Insert(bool success, string gameType, string reqData, string responseData, string reqDate);
        Task InsertAsync(bool success, string gameType, string reqData, string responseData, string reqDate);
    }
}