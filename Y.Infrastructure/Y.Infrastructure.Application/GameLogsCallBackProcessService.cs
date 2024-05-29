using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.LogsCoreController.IService;
using Y.Infrastructure.IApplication;
using Y.Packet.Entities.Games.ViewModels;
using Y.Packet.Repositories.IGames;
using Y.Packet.Services.IGames;
using Y.Packet.Services.IVips;

namespace Y.Infrastructure.Application
{
    public class GameLogsCallBackProcessService : IGameLogsCallBackProcessService
    {
        private readonly IWashOrderDetailService _washOrderDetailService;
        private readonly IReqLogsService _reqLogsService;
        private readonly IExcptLogsService _excptLogsService;
        private readonly IGameLogsService _gameLogsService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IGameLogsRepository _gameLogsRepository;

        public GameLogsCallBackProcessService(IGameLogsRepository gameLogsRepository, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _reqLogsService = (IReqLogsService)_serviceProvider.GetService(typeof(IReqLogsService));
            _excptLogsService = (IExcptLogsService)_serviceProvider.GetService(typeof(IExcptLogsService));
            _gameLogsService = (IGameLogsService)_serviceProvider.GetService(typeof(IGameLogsService)); ;
            _gameLogsRepository = gameLogsRepository;
            _washOrderDetailService = (IWashOrderDetailService)_serviceProvider.GetService(typeof(IWashOrderDetailService));
        }


        public async Task Process(int orderId = 0)
        {
            try
            {
                // 1. 处理流水问题
                IEnumerable<GameLogsForWashOrdersModel> list = new List<GameLogsForWashOrdersModel>() ;
                if (orderId != 0)
                {
                    list = await _gameLogsRepository.GetTheLast2MinutesFinishedLogsAsync(orderId);
                }
                else
                {
                    // 获取过去2分钟内的完成的订单数据
                    list = await _gameLogsRepository.GetTheLast2MinutesFinishedLogsAsync();
                }
                if (list == null || list.Count() == 0)
                {
                    return;
                }
                foreach (GameLogsForWashOrdersModel gameLog in list)
                {
                    var rt = await _washOrderDetailService.InsertAsync(gameLog.MemberId, gameLog.ValidBet, gameLog.GameTypeStr, gameLog.Id.ToString());
                }
            }
            catch (Exception ex)
            {
                await _excptLogsService.InsertAsync(ex, "GameLogsCallBackProcessService.Process");
            }
        }
    }
}
