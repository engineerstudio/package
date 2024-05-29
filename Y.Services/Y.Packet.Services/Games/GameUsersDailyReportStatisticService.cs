using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Games.ViewModels;
using Y.Packet.Repositories.IGames;
using Y.Packet.Services.IGames;

namespace Y.Packet.Services.Games
{
    public class GameUsersDailyReportStatisticService : IGameUsersDailyReportStatisticService
    {
        private readonly IGameUsersDailyReportStatisticRepository _repository;
        private readonly IGameLogsRepository _gameLogsRepository;
        private readonly IGameUsersService _gameUsersService;
        private readonly IGameInfoService _gameInfoService;
        public GameUsersDailyReportStatisticService(IGameUsersDailyReportStatisticRepository repository, IGameLogsRepository gameLogsRepository, IGameUsersService gameUsersService, IGameInfoService gameInfoService)
        {
            _repository = repository;
            _gameLogsRepository = gameLogsRepository;
            _gameUsersService = gameUsersService;
            _gameInfoService = gameInfoService;
        }

        public async Task<(IEnumerable<GameUsersDailyReportStatistic>, int)> GetByMerchantAsync(int merchantId, DateTime date)
        {
            string conditions = $"WHERE MerchantId={merchantId} AND Date=N'{date}'";
            var list = await _repository.GetListAsync(conditions);
            return (list, _repository.RecordCount(conditions));
        }

        public async Task<(IEnumerable<GameUsersDailyReportStatistic>, int)> GetPageListAsync(int merchantId, IEnumerable<int> members, DateTime startTime, DateTime endTime,int currentPage,int pageSize)
        {
            if (members == null || members.Count() == 0) return (new List<GameUsersDailyReportStatistic>(), 0);
           return await _repository.GetPageListAsync(merchantId, members, startTime, endTime, currentPage, pageSize);
        }


        public async Task<(bool, string)> SaveDataAsync(int merchantId, int memberId, string gameTypeStr, string date)
        {
            if (merchantId == 0) return (false, "商户不存在");
            if (memberId == 0) return (false, "用户不存在");
            if (gameTypeStr == null) return (false, "请选择游戏");
            else
                if (gameTypeStr.ToEnum<GameType>() == null) return (false, "不存在的游戏");

            if (date == null) return (false, "请选择日期");

            var data = await _gameLogsRepository.GetDailySumAsync(merchantId, memberId, date);
            await ProcessDailyData(data);
            return (true, null);
        }

        public async Task<(bool, string)> CreateGameUserReportAsync(int merchantId,int memberId,string date)
        {
            var data = await _gameLogsRepository.GetDailySumAsync(merchantId, memberId, date);
            await ProcessDailyData(data);
            return (true, null);
        }


        public async Task<(bool, string)> CreateGameUserReportAsync(string date = null)
        {
            date = date == null ? DateTime.UtcNow.AddHours(8).Date.ToString("yyyy-MM-dd") : date;
            var data = await _gameLogsRepository.GetDailySumAsync(date);
            await ProcessDailyData(data);
            return (true, null);
        }


        private async Task ProcessDailyData(IEnumerable<GameDailyModel> data)
        {
            foreach (var d in data)
            {
                // 日期是否存在，不存在新增，存在那么递增
                var existDate = await _repository.ExistDateAsync(d.Date, d.MemberId, d.GameTypeStr);
                if (existDate.Item1)
                {
                    var rpt = existDate.Item2;
                    rpt.BetAmount = d.BetAmount;
                    rpt.ValidBet = d.ValidBet;
                    rpt.Money = d.Money;
                    await _repository.UpdateAsync(rpt);
                }
                else
                {
                    GameType gameType = d.GameTypeStr.ToEnum<GameType>().Value;
                    string player = await _gameUsersService.GetPlayerNameAsync(d.MemberId, gameType);

                    var rpt = new GameUsersDailyReportStatistic();
                    rpt.Date = Convert.ToDateTime(d.Date);
                    rpt.GameTypeStr = d.GameTypeStr;
                    rpt.GameCategory = gameType.TransToGameCate();
                    rpt.MemberId = d.MemberId;
                    rpt.MerchantId = d.MerchantId;
                    rpt.PlayerName = player;
                    rpt.BetAmount = d.BetAmount;
                    rpt.ValidBet = d.ValidBet;
                    rpt.Money = d.Money;
                    await _repository.InsertAsync(rpt);
                }
            }
        }



        /// <summary>
        /// 获取会员数据汇总
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="members"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns>投注金额，有效投注，盈亏</returns>
        public async Task<(decimal, decimal, decimal)> GetMembersDataSummaryByIds(int merchantId, IEnumerable<int> members, DateTime startTime, DateTime endTime)
        {
            if (members.Count() == 0) return (0, 0, 0);
            var d = await _repository.GetMembersDataSummaryByIds(merchantId, members, startTime, endTime);
            return (d["BetAmount"], d["ValidBet"], d["Money"]);
        }





    }
}