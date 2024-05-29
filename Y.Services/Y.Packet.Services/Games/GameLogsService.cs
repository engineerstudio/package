using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Packet.Repositories.IGames;
using Y.Packet.Services.IGames;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Games.ViewModels;

namespace Y.Packet.Services.Games
{
    public class GameLogsService : IGameLogsService
    {
        private readonly IGameLogsRepository _repository;

        private readonly IGameLogsEsportRepository _gameLogsEsportRepository;
        private readonly IGameLogsSportRepository _gameLogsSportRepository;
        private readonly IGameLogsSlotRepository _gameLogsSlotRepository;
        private readonly IGameLogsLiveRepository _gameLogsLiveRepository;
        private readonly IGameLogsHuntRepository _gameLogsHuntRepository;
        private readonly IGameLogsLotteryRepository _gameLogsLotteryRepository;
        private readonly IGameLogsChessRepository _gameLogsChessRepository;
        //private readonly IGameLogsCacheService _gameLogsCacheService;

        private readonly IGameUsersDailyReportStatisticRepository _gameUsersDailyReportStatisticRepository;

        public GameLogsService(
            IGameLogsRepository repository,
            IGameLogsEsportRepository gameLogsEsportRepository,
            IGameLogsSportRepository gameLogsSportRepository,
            IGameLogsSlotRepository gameLogsSlotRepository,
            IGameLogsLiveRepository gameLogsLiveRepository,
            IGameLogsHuntRepository gameLogsHuntRepository,
            IGameLogsLotteryRepository gameLogsLotteryRepository,
            IGameLogsChessRepository gameLogsChessRepository,
            IGameUsersDailyReportStatisticRepository gameUsersDailyReportStatisticRepository
             )
        {
            _repository = repository;
            _gameLogsEsportRepository = gameLogsEsportRepository;
            _gameLogsSportRepository = gameLogsSportRepository;
            _gameLogsSlotRepository = gameLogsSlotRepository;
            _gameLogsLiveRepository = gameLogsLiveRepository;
            _gameLogsHuntRepository = gameLogsHuntRepository;
            _gameLogsLotteryRepository = gameLogsLotteryRepository;
            _gameLogsChessRepository = gameLogsChessRepository;

            _gameUsersDailyReportStatisticRepository = gameUsersDailyReportStatisticRepository;
            //_gameLogsCacheService = gameLogsCacheService;
        }

        public async Task<(bool, string, int)> SaveLogAsync(GameLogs logs)
        {
            // 创建日报表 GameUsersDailyReportStatistic
            await _gameUsersDailyReportStatisticRepository.CreateAsync(logs.CreateTimeUtc8.ToDateString(), logs.MemberId, logs.GameTypeStr, logs.MemberId, logs.PlayerName);

            int? finishedId = 0;
            // 判断订单是否已经结束了
            if (logs.Status.IsFinishOrder())
                logs.SettlementTimeUtc8 = DateTime.UtcNow.AddHours(8);
            else
                logs.SettlementTimeUtc8 = DefaultString.DefaultDateTime;

            // 判断是否存在 sourceId 与 游戏类型 ，存在sourceId 则进行更新，否则插入
            var existLogs = await _repository.ExistLogAsync(logs.GameTypeStr, logs.SourceId);

            if (existLogs.Item1) // 存在  更新
            {
                var log = existLogs.Item2;
                log.ValidBet = logs.ValidBet;
                log.Money = logs.Money;
                log.AwardAmount = logs.AwardAmount;
                log.Status = logs.Status;
                log.SourceOrderAwardTime = logs.SourceOrderAwardTime;
                log.OrderAwardTimeUtc8 = logs.OrderAwardTimeUtc8;
                await _repository.UpdateAsync(log);

                // 判断是否完结了，如果完结了 更新 有效投注 盈亏
                if (log.Status.IsFinishOrder())
                {
                    finishedId = log.Id;
                    await _gameUsersDailyReportStatisticRepository.SaveValidbetAndMoney(logs.SettlementTimeUtc8.ToDateString(), logs.MemberId, log.GameTypeStr, log.ValidBet, log.Money);
                    //await FinishOrderCallBackEventAsync(log);
                }
            }
            else // 不存在  插入
            {
                finishedId = await _repository.InsertAsync(logs);
                // 更新用户日报表的投注金额
                await _gameUsersDailyReportStatisticRepository.SaveBetAmountAsync(logs.CreateTimeUtc8.ToDateString(), logs.MemberId, logs.GameTypeStr, logs.BetAmount);
            }

            //  更新redis 缓存列表  gametype,siteId,userId,sourceId, 是否更新游戏日报表，是否更新用户游戏日报表，
            if (!logs.Status.IsFinishOrder())
                finishedId = 0;
            return (true, string.Empty, finishedId.Value);
        }

        /// <summary>
        /// 完结的订单回调事件
        /// </summary>
        /// <returns></returns>
        private async Task FinishOrderCallBackEventAsync(GameLogs log)
        {
            // 处理打码量
            // 缓存塞入List， 然后通过任务调度进行处理
            //await _gameLogsCacheService.ListLeftPushAsync("GAMELOGS_KEY", log.ToJson());
        }

        public async Task<(bool, string)> PushOrderToListAsync(int orderId)
        {
            string msg = "保存成功";
            try
            {
                //var order_result = await GetAsync(orderId);
                //if (!order_result.sucess) return (false, order_result.msg);
                //await FinishOrderCallBackEventAsync(order_result.entity);



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                msg = ex.Message;
            }
            return (true, msg);
        }

        public async Task<(IEnumerable<GameLogs>, int, Dictionary<string, decimal>)> GetPageListAsync(MerchantGameQuery q)
        {
            var parms = new DynamicParameters();
            string conditions = "WHERE 1=1 ";
            //conditions += $" AND MerchantId={q.MerchantId}";
            //if (!string.IsNullOrEmpty(q.GameCategoryStr))
            //{
            //    var gcategoryRt = q.GameCategoryStr.ToEnum<GameCategory>();
            //    if (gcategoryRt != null)
            //        conditions += $" AND GameCategory = {(int)gcategoryRt.Value} ";
            //}

            //if (!string.IsNullOrEmpty(q.GameTypeStr))
            //{
            //    if (q.GameTypeStr.ToEnum<GameType>() != null)
            //        conditions += $" AND GameTypeStr = '{q.GameTypeStr}'";
            //}

            //if (!string.IsNullOrEmpty(q.SourceId))
            //{
            //    conditions += $" AND SourceId = @SourceId";
            //    parms.Add("SourceId", q.SourceId);
            //}

            //if (!string.IsNullOrEmpty(q.GameAccount))
            //{
            //    conditions += $" AND PlayerName = @PlayerName";
            //    parms.Add("PlayerName", q.GameAccount);
            //}


            //if (!string.IsNullOrEmpty(q.AccountName))
            //{
            //    // 获取到该用户的ID
            //}

            //if (q.MemberId != 0)
            //{
            //    conditions += $" AND MemberId = {q.MemberId}";
            //}


            if (q.MerchantId != 0)
            {
                conditions += $" AND MerchantId = {q.MerchantId}";
            }

            if (q.CreateStartTime != null)  // CreateTimeUtc8
            {
                conditions += $" AND CreateTimeUtc8 > '{q.CreateStartTime}'";
                if (q.CreateEndTime != null)
                    conditions += $" AND CreateTimeUtc8 <= '{q.CreateEndTime}'";
                else
                    conditions += $" AND CreateTimeUtc8 <= '{DateTime.UtcNow.AddHours(8)}'";
            }



            if (!string.IsNullOrEmpty(q.OrderStatusStr))
            {
                var status = q.OrderStatusStr.ToEnum<OrderStatusForCustomers>();
                switch (status)
                {
                    case OrderStatusForCustomers.ALL:
                        break;
                    case OrderStatusForCustomers.Settled:
                        conditions += $" AND Status != {(int)OrderStatus.None} ";
                        break;
                    case OrderStatusForCustomers.UnSettled:
                        conditions += $" AND Status = {(int)OrderStatus.None} ";
                        break;
                }
            }
            var dicSum = await _repository.GetAmountSumAsync(conditions, parms);
            var list = await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", parms);
            return (list, _repository.RecordCount(conditions, parms), dicSum);

        }


        public async Task GetDataByDate(DateTime date)
        {


        }


        public async Task<(bool, dynamic)> GetByOrderDetailsId(int orderId, GameCategory cate)
        {
            if (orderId == 0) return (false, "");

            switch (cate)
            {
                case GameCategory.NotSet:
                    throw new Exception("未查询到订单");
                case GameCategory.ESport:
                    var esport_order = await _gameLogsEsportRepository.GetAsync(orderId);
                    return (true, esport_order);
                case GameCategory.Sport:
                    var sport_order = await _gameLogsSportRepository.GetAsync(orderId);
                    return (true, sport_order);
                case GameCategory.Live:
                    var live_order = await _gameLogsLiveRepository.GetAsync(orderId);
                    return (true, live_order);
                case GameCategory.Slot:
                    var slot_order = await _gameLogsSlotRepository.GetAsync(orderId);
                    return (true, slot_order);
                case GameCategory.Lottery:
                    var lottery_order = await _gameLogsLotteryRepository.GetAsync(orderId);
                    return (true, lottery_order);
                case GameCategory.Chess:
                    var chess_order = await _gameLogsChessRepository.GetAsync(orderId);
                    return (true, chess_order);
                case GameCategory.Hunt:
                    var hunt_order = await _gameLogsHuntRepository.GetAsync(orderId);
                    return (true, hunt_order);
                default:
                    break;
            }

            return (false, "");
        }


        public async Task<(bool sucess, string msg, GameLogs entity)> GetAsync(int orderId)
        {
            if (orderId == 0) return (false, "标识为零", null);
            var entity = await _repository.GetAsync(orderId);
            if (entity == null) return (false, "数据不存在", null);
            return (true, string.Empty, entity);
        }


        //Task<decimal> GetTaskLaskWeekValidBet(int merchantId, int userId)

        /// <summary>
        /// [周签到活动] 获取上周的有效投注
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<decimal> GetTaskLaskWeekValidBet(int merchantId, int userId)
        {
            // TODO 定期生成周报表 或者  从用户游戏日报表读取
            return await _repository.GetTaskLaskWeekValidBetAsync(merchantId, userId);
        }





    }
}