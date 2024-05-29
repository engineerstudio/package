using System;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.IApplication;
using Y.Packet.Services.IGames;
using Y.Packet.Services.IMembers;

namespace Y.Infrastructure.Application
{
    public class MemberHyberService : IMemberHyberService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUsersService _usersService;
        private readonly IGameLogsService _gameLogsService;
        private readonly IGameUsersDailyReportStatisticService _gameUsersDailyReportStatisticService;
        public MemberHyberService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _usersService = (IUsersService)_serviceProvider.GetService(typeof(IUsersService));
            _gameLogsService = (IGameLogsService)_serviceProvider.GetService(typeof(IGameLogsService));
            _gameUsersDailyReportStatisticService = (IGameUsersDailyReportStatisticService)_serviceProvider.GetService(typeof(IGameUsersDailyReportStatisticService));
        }

        public async Task CreateUsersDailyStatisticAsync(int merchantId, int userId, DateTime date)
        {
            if (merchantId == 0 || userId == 0)
                await _gameUsersDailyReportStatisticService.CreateGameUserReportAsync(date.ToDateString());
            else
                await _gameUsersDailyReportStatisticService.CreateGameUserReportAsync(merchantId, userId, date.ToDateString());
        }

    }
}
