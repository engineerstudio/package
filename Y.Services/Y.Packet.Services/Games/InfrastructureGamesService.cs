using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.LogsCoreController.IService;
using Y.Infrastructure.Library.Core.Mapper;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.Entities.Message;
using Y.Infrastructure.Library;
using Y.Infrastructure.Library.EventsTriggers;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Games.ViewModels;

namespace Y.Packet.Services.IGames
{
    public class InfrastructureGamesService : IInfrastructureGamesService
    {
        //private readonly IGamesFactory _fac;
        private readonly IGamesLibraryFactory _fac;
        private readonly IGameLogsService _gameLogsService;
        private readonly IGameLogsFinanceService _gameLogsFinanceService;
        private readonly IGameLogsLotteryService _gameLogsLotteryService;
        private readonly IGameLogsEsportService _gameLogsEsportService;
        private readonly IGameLogsHuntService _gameLogsHuntService;
        private readonly IGamelogsMd5CacheService _gamelogsMd5CacheService;
        private readonly IGameApiTimestampsService _gameApiTimestampsService;
        private readonly IGameUsersService _gameUsersService;
        private readonly IGameTransferLogsService _gameTransferLogsService;
        private readonly IGameLogsSportService _gameLogsSportService;
        private readonly IGameUsersDailyReportStatisticService _gameUsersDailyReportStatisticService;
        private readonly IGameInfoService _gameInfoService;
        private readonly IGameApiRequestLogService _gameApiRequestLogService;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        ITaskDataForGamesService _taskDataForGamesService;
        IExcptLogsService _excptLogsService;
        private readonly IReqLogsService _reqLogsService;
        private readonly ITriggerBus _triggerBus;


        public InfrastructureGamesService(IGamesLibraryFactory games, IGameLogsService gameLogsService, IGamelogsMd5CacheService gamelogsMd5CacheService
            , IGameLogsFinanceService gameLogsFinanceService
            , IGameLogsEsportService gameLogsEsportService, IGameApiTimestampsService gameApiTimestampsService, IGameUsersService gameUsersService, IGameTransferLogsService gameTransferLogsService, IGameLogsSportService gameLogsSportService, IGameUsersDailyReportStatisticService gameUsersDailyReportStatisticService, IGameInfoService gameInfoService, IGameApiRequestLogService gameApiRequestLogService, IGameLogsHuntService gameLogsHuntService
            //, IHttpContextAccessor httpContextAccessor
            , IServiceProvider provider
, IGameLogsLotteryService gameLogsLotteryService
            //, Y.Infrastructure.Library.Core.LogsCoreController.IService.ITaskDataForGamesService taskDataForGamesService
            //, Y.Infrastructure.Library.Core.LogsCoreController.IService.IExcptLogsService excptLogsService
            )
        {
            _fac = games;
            _gameLogsService = (IGameLogsService)provider.GetService(typeof(IGameLogsService));
            _gamelogsMd5CacheService = (IGamelogsMd5CacheService)provider.GetService(typeof(IGamelogsMd5CacheService));
            _gameLogsFinanceService = (IGameLogsFinanceService)provider.GetService(typeof(IGameLogsFinanceService));
            _gameLogsEsportService = (IGameLogsEsportService)provider.GetService(typeof(IGameLogsEsportService));
            _gameApiTimestampsService = (IGameApiTimestampsService)provider.GetService(typeof(IGameApiTimestampsService));
            _gameUsersService = (IGameUsersService)provider.GetService(typeof(IGameUsersService));
            _gameTransferLogsService = (IGameTransferLogsService)provider.GetService(typeof(IGameTransferLogsService)); ;
            _gameLogsSportService = (IGameLogsSportService)provider.GetService(typeof(IGameLogsSportService)); ;
            _gameUsersDailyReportStatisticService = (IGameUsersDailyReportStatisticService)provider.GetService(typeof(IGameUsersDailyReportStatisticService)); ;
            _gameInfoService = (IGameInfoService)provider.GetService(typeof(IGameInfoService)); ;
            _gameApiRequestLogService = (IGameApiRequestLogService)provider.GetService(typeof(IGameApiRequestLogService)); ;
            _gameLogsHuntService = (IGameLogsHuntService)provider.GetService(typeof(IGameLogsHuntService)); ;
            //_httpContextAccessor = httpContextAccessor;
            //_taskDataForGamesService = taskDataForGamesService;
            //_excptLogsService = excptLogsService;
            _taskDataForGamesService = (Y.Infrastructure.Library.Core.LogsCoreController.IService.ITaskDataForGamesService)provider.GetService(typeof(Y.Infrastructure.Library.Core.LogsCoreController.IService.ITaskDataForGamesService));
            _excptLogsService = (Y.Infrastructure.Library.Core.LogsCoreController.IService.IExcptLogsService)provider.GetService(typeof(Y.Infrastructure.Library.Core.LogsCoreController.IService.IExcptLogsService));
            _reqLogsService = (IReqLogsService)provider.GetService(typeof(IReqLogsService));
            _gameLogsLotteryService = gameLogsLotteryService;
            _triggerBus = (ITriggerBus)provider.GetService(typeof(ITriggerBus));
        }

        public async Task<(bool, string)> LoginAsync(int merchantId, int userId, string playerName, GameType gameType, Dictionary<string, string> args, string gameconfig)
        {

            //args: 登陆设备(device),登陆IP(ip)
            var rt = _fac.Login(gameType.ToString(),playerName, args);

            // 1.记录数据请求日志
            await _gameApiRequestLogService.InsertAsync(merchantId, gameType.ToString(), (rt.Item1, rt.Item2, $"{rt.Item3} # {rt.Item4}"));


            if (!rt.Item1) return (rt.Item1, rt.Item4);

            return (rt.Item1, rt.Item5);
        }

        public async Task<(bool, string)> RegisterAsync(int merchantId, int userId, string userName, GameType gameType, string gameconfig)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();

            var rt = _fac.Register(gameType.ToString(),userName, args);

            // 1.记录数据请求日志
            await _gameApiRequestLogService.InsertAsync(merchantId, gameType.ToString(), (rt.Item1, rt.Item2, rt.Item3));

            // 2.判断注册是否成功并记录用户的游戏账密的相关信息
            if (rt.Item1)
                await _gameUsersService.SaveGameUserAsync(merchantId, userId, gameType.ToString(), rt.Item5, rt.Item6);

            // 3. 返回注册成功/失败信息
            return (rt.Item1, rt.Item3);
        }

        public async Task<(bool sucess, string msg, string transId, TransferStatus? status)> TransferAsync(int merchantId, int userId, string playerName, GameType gameType, int sourceId, decimal money, TransType action, string gameconfig)
        {
            var gamestatus_rt = await _gameInfoService.GetGameStatusAsync(gameType);
            if (gamestatus_rt.Item1) // 游戏开启状态
            {
                Dictionary<string, string> args = new Dictionary<string, string>();

                // 1. 增加转账日志   action.ToEnum<TransType>().Value;  0.等待，1.成功，2.失败  GameTransferLogs
                var logs = await _gameTransferLogsService.AddTransferLogsAsync(gameType, merchantId, userId, money, action, TransferStatus.None.GetEnumValue().ToString());

                // 2. 游戏转账
                var rt = _fac.Transfer(gameType.ToString(), playerName, logs.Item2.Id.ToString(), money, action);

                // 3.记录数据请求日志
                await _gameApiRequestLogService.InsertAsync(merchantId, gameType.ToString(), (rt.Item1, rt.Item2, rt.Item3));

                // 4. 判断是否成功，修改转账状态
                if (rt.Item4 != TransferStatus.None)
                    await _gameTransferLogsService.UpdateTransLogsStatusAsync(logs.Item2.Id, rt.Item4);

                // 5. 增加用户转账日志


                return (rt.Item1, rt.Item5, logs.Item2.Id.ToString(), rt.Item4);
            }
            return (gamestatus_rt.Item1, gamestatus_rt.Item2, null, null);
        }

        public async Task<(bool, TransferStatus?, string)> CheckTransferAsync(int merchantId, int userId, string playerName, GameType gameType, int sourceId, TransType transType, string gameconfig)
        {
            var gamestatus_rt = await _gameInfoService.GetGameStatusAsync(gameType);
            if (gamestatus_rt.Item1) // 游戏开启状态
            {
                Dictionary<string, string> args = new Dictionary<string, string>();
                string player = await _gameUsersService.GetPlayerNameAsync(userId, gameType);
                var rt = _fac.CheckTransferStatus(gameType.ToString(), player, sourceId.ToString(), transType);
                // 3.记录数据请求日志
                await _gameApiRequestLogService.InsertAsync(merchantId, gameType.ToString(), (rt.Item1, rt.Item2, rt.Item3));
                // 4. 判断是否成功，修改转账状态
                if (rt.Item4 != TransferStatus.None)
                    await _gameTransferLogsService.UpdateTransLogsStatusAsync(sourceId, rt.Item4);

                return (rt.Item1, rt.Item4, rt.Item5);
            }

            return (false, null, "游戏未开启");
        }


        /// <summary>
        /// 执行获取日志方法
        /// </summary>
        /// <param name="gameType"></param>
        /// <returns></returns>
        public async Task ExecGetLogs(GameType gameType, string gameconfig)
        {
            try
            {
                // 判断游戏当前状态是否为启用状态
                var gamestatus_rt = await _gameInfoService.GetGameStatusAsync(gameType);

                if (gamestatus_rt.Item1) // 游戏开启状态则抓取日志
                {
                    var time = await _gameApiTimestampsService.GetByTypeStrAsync(gameType.ToString());
                    var rt = await _fac.GetLogsAsync(gameType.ToString(), time.QueryTime, time.Timestamps);
                    if (rt.Item4.Count() == 0) // 如果没有返回订单数据，说明没有抓取到订单， 那么只保存请求数据即可
                    { }
                    else
                    {
                        if (gameType.ExistAttributeOfType<ESportAttribute>())
                            await ProcessESportOrders(rt.Item4);
                        if (gameType.ExistAttributeOfType<SportAttribute>())
                            await ProcessSportOrders(rt.Item4);
                        if (gameType.ExistAttributeOfType<SlotAttribute>())
                            await ProcessSlotOrders(rt.Item4);
                        if (gameType.ExistAttributeOfType<LiveAttribute>())
                            await ProcessLiveOrders(rt.Item4);
                        if (gameType.ExistAttributeOfType<LotteryAttribute>())
                            await ProcessLotteryOrders(rt.Item4);
                        if (gameType.ExistAttributeOfType<ChessAttribute>())
                            await ProcessChessOrders(rt.Item4);
                        if (gameType.ExistAttributeOfType<HuntAttribute>())
                            await ProcessHuntOrders(rt.Item4);
                        //if (gameType.ExistAttributeOfType<FinanceAttribute>())
                        //    await ProcessFinanceOrders(rt.Item4);
                    }
                    //Console.WriteLine($"更新最近更新时间{ rt.Item4}");
                    await SaveGameTimesAsync(gameType, rt.Item5, rt.Item6);
                    await _taskDataForGamesService.InsertAsync(false, gameType.ToString(), rt.Item2, rt.Item3, $"{time.QueryTime} ~ {rt.Item5}");
                }
            }
            catch (Exception ex)
            {
                //Y.Infrastructure.Library.Core.LogsCoreController.IService.IExcptLogsService excpLogs = _httpContextAccessor.HttpContext == null ? _excptLogsService : (Y.Infrastructure.Library.Core.LogsCoreController.IService.IExcptLogsService)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(Y.Infrastructure.Library.Core.LogsCoreController.IService.IExcptLogsService));
                await _excptLogsService.InsertAsync(ex, "InfrastructureGamesService");
            }
        }



        public async Task SaveGameOrdersAsync(GameType gameType, DateTime startDateTime, long startDateTimeStamps, string gameconfig)
        {
            var rt = await _fac.GetLogsAsync(gameType.ToString(), startDateTime, startDateTimeStamps);

            // 保存日志信息
            if (rt.Item2 != null && rt.Item4.Count() != 0)
            {
                if (gameType.ExistAttributeOfType<ESportAttribute>())
                    await ProcessESportOrders(rt.Item4);
                if (gameType.ExistAttributeOfType<SportAttribute>())
                    await ProcessSportOrders(rt.Item4);
                if (gameType.ExistAttributeOfType<SlotAttribute>())
                    await ProcessSlotOrders(rt.Item4);
                if (gameType.ExistAttributeOfType<LiveAttribute>())
                    await ProcessLiveOrders(rt.Item4);
                if (gameType.ExistAttributeOfType<LotteryAttribute>())
                    await ProcessLotteryOrders(rt.Item4);
                if (gameType.ExistAttributeOfType<ChessAttribute>())
                    await ProcessChessOrders(rt.Item4);
                if (gameType.ExistAttributeOfType<HuntAttribute>())
                    await ProcessHuntOrders(rt.Item4);
                //if (gameType.ExistAttributeOfType<FinanceAttribute>())
                //    await ProcessFinanceOrders(rt.Item4);

                // 保存时间戳
                await SaveGameTimesAsync(gameType, rt.Item5, rt.Item6);
            }
        }

        async Task SaveGameTimesAsync(GameType gameType, DateTime startDateTime, long startDateTimeStamps)
        {
            await _gameApiTimestampsService.SaveApiLogQueryTimeAsync(gameType, startDateTime, startDateTimeStamps);
        }

        async Task ProcessSlotOrders(IEnumerable<Dictionary<string, object>> item1)
        {
            throw new NotImplementedException();
        }

        async Task ProcessFinanceOrders(IEnumerable<Dictionary<string, object>> orders)
        {
            foreach (Dictionary<string, object> od in orders)
            {
                // 判断sourceId， md5 是否存在
                var rt = await ExistMd5Cache(od["GameTypeStr"].ToString(), od["SourceId"].ToString(), od["Raw"].ToString());
                // 1. sourceId 不存在 那么更新
                bool result = false;
                if (rt.Item1 && rt.Item2) continue;

                var order = JsonConvert.DeserializeObject<GameLogsFinance>(od.ToJson());

                // 需要找出playername 对应的用户ID和站点ID
                var player = await _gameUsersService.GetByPlayerNameAsync(order.GameTypeStr, order.PlayerName);
                order.MerchantId = player.Item2;
                order.UserId = player.Item3;

                if (!rt.Item1 && !rt.Item2) // 插入数据
                {
                    result = await _gameLogsFinanceService.AddFinanceLog(order);
                }
                else if (rt.Item1 && !rt.Item2) // 2. sourceId 存在， md5不一致  那么更新
                {
                    result = await _gameLogsFinanceService.UpdateFinanceLog(order);
                }

                // 3. 插入Logs表
                var log = ModelToEntity.Mapping<GameLogs, GameLogsFinance>(order);
                log.MemberId = order.UserId;
                log.CreateTimeUtc8 = order.CreateTime;
                await _gameLogsService.SaveLogAsync(log);
            }

        }
        /// <summary>
        /// 体育
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        async Task ProcessSportOrders(IEnumerable<Dictionary<string, object>> orders)
        {
            foreach (var od in orders)
            {
                // 1.判断订单SourceId是否已经存在
                //string sourceId = od["SourceId"].ToString();
                //string md5 = od["Raw"].ToString();
                //var rt = await _gamelogsMd5CacheService.Process(od["GameTypeStr"].ToString(), sourceId, md5); // 判断是否存在，不存在则插入数据
                var rt = await ExistMd5Cache(od["GameTypeStr"].ToString(), od["SourceId"].ToString(), od["Raw"].ToString());
                // 2.判断MD5是否存在变化，MD5变化则判断订单状态
                bool result = false;
                if (rt.Item1 && rt.Item2) continue;  // 如果存在该SourceId 并且 MD5一致  那么继续遍历
                var order = JsonConvert.DeserializeObject<GameLogsSport>(od.ToJson());

                // 需要找出playername 对应的用户ID和站点ID
                var player = await _gameUsersService.GetByPlayerNameAsync(order.GameTypeStr, order.PlayerName);
                order.MerchantId = player.Item2;
                order.UserId = player.Item3;

                if ((!rt.Item1 && !rt.Item2)) // 插入订单
                {
                    result = await _gameLogsSportService.AddSportLog(order);
                }

                if (rt.Item1 && !rt.Item2) // 更新订单
                {
                    result = await _gameLogsSportService.UpdateSportLog(order);
                }

                // 3. 插入Logs表
                var log = ModelToEntity.Mapping<GameLogs, GameLogsSport>(order);
                log.MemberId = order.UserId;
                log.CreateTimeUtc8 = order.CreateTime;
                await _gameLogsService.SaveLogAsync(log);


                // 5.回调 用户游戏规则日报表


                // 6.回调 游戏日报表


                // 7.回调 游戏规则日报表


            }
        }

        async Task ProcessESportOrders(IEnumerable<Dictionary<string, object>> orders)
        {
            foreach (Dictionary<string, object> od in orders)
            {
                // 判断sourceId， md5 是否存在
                var rt = await ExistMd5Cache(od["GameTypeStr"].ToString(), od["SourceId"].ToString(), od["Raw"].ToString());
                // 1. sourceId 不存在 那么更新
                bool result = false;
                if (rt.Item1 && rt.Item2) continue;

                var order = JsonConvert.DeserializeObject<GameLogsEsport>(od.ToJson());

                // 需要找出playername 对应的用户ID和站点ID
                var player = await _gameUsersService.GetByPlayerNameAsync(order.GameTypeStr, order.PlayerName);
                order.MerchantId = player.Item2;
                order.UserId = player.Item3;

                if (!rt.Item1 && !rt.Item2) // 插入数据
                {
                    result = await _gameLogsEsportService.AddEsportLog(order);
                }
                else if (rt.Item1 && !rt.Item2) // 2. sourceId 存在， md5不一致  那么更新
                {
                    result = await _gameLogsEsportService.UpdateEsportLog(order);
                }

                // 3. 插入Logs表
                var log = ModelToEntity.Mapping<GameLogs, GameLogsEsport>(order);
                log.MemberId = order.UserId;
                log.CreateTimeUtc8 = order.CreateTime;
                await _gameLogsService.SaveLogAsync(log);
            }
        }


        async Task ProcessLiveOrders(IEnumerable<Dictionary<string, object>> orders)
        { }

        async Task ProcessLotteryOrders(IEnumerable<Dictionary<string, object>> orders)
        {
            List<int> finishedOrderIds = new List<int>();
            foreach (Dictionary<string, object> od in orders)
            {
                // 判断sourceId， md5 是否存在
                var rt = await ExistMd5Cache(od["GameTypeStr"].ToString(), od["SourceId"].ToString(), od["Raw"].ToString());
                // 1. sourceId 不存在 那么更新
                bool result = false;
                if (rt.Item1 && rt.Item2) continue;

                var order = JsonConvert.DeserializeObject<GameLogsLottery>(od.ToJson());

                // 需要找出playername 对应的用户ID和站点ID
                var player = await _gameUsersService.GetByPlayerNameAsync(order.GameTypeStr, order.PlayerName);
                order.MerchantId = player.Item2;
                order.UserId = player.Item3;

                if (!rt.Item1 && !rt.Item2) // 插入数据
                {
                    result = await _gameLogsLotteryService.AddFinanceLog(order);
                }
                else if (rt.Item1 && !rt.Item2) // 2. sourceId 存在， md5不一致  那么更新
                {
                    result = await _gameLogsLotteryService.UpdateFinanceLog(order);
                }

                if (result)
                {
                    await _gamelogsMd5CacheService.InsertAsync(od["GameTypeStr"].ToString(), od["SourceId"].ToString(), od["Raw"].ToString());
                    // 3. 插入Logs表
                    var log = ModelToEntity.Mapping<GameLogs, GameLogsLottery>(order);
                    log.MemberId = order.UserId;
                    log.CreateTimeUtc8 = order.CreateTime;
                    var rt_logs = await _gameLogsService.SaveLogAsync(log);
                    if (rt_logs.Item3 != 0)
                        finishedOrderIds.Add(rt_logs.Item3);
                }
            }

            Console.WriteLine(finishedOrderIds.ToJson());
            if (finishedOrderIds.Count > 0)
            {
                Console.WriteLine($"pushed orders {finishedOrderIds.ToJson()}");
                await _triggerBus.PublishAsync(new WashOrdersMessage() { Ids = finishedOrderIds });
            }

        }

        async Task ProcessChessOrders(IEnumerable<Dictionary<string, object>> orders)
        { }

        async Task ProcessHuntOrders(IEnumerable<Dictionary<string, object>> orders)
        {
            // 所有电子订单都是已经完结的订单
            foreach (Dictionary<string, object> od in orders)
            {
                var rt = await ExistMd5Cache(od["GameTypeStr"].ToString(), od["SourceId"].ToString(), od["Raw"].ToString());
                if (rt.Item1) continue;  // 只要存在sourceId那么没有其它操作了
                var order = JsonConvert.DeserializeObject<GameLogsHunt>(od.ToJson());
                // 需要找出playername 对应的用户ID和站点ID
                var player = await _gameUsersService.GetByPlayerNameAsync(order.GameTypeStr, order.PlayerName);
                order.MerchantId = player.Item2;
                order.UserId = player.Item3;
                // 1. 有效投注规则设定

                // 2. 保存到电子表
                var result = await _gameLogsHuntService.AddHuntLog(order);
                // 3. 保存到日志表
                if (result)
                {
                    var log = ModelToEntity.Mapping<GameLogs, GameLogsHunt>(order);
                    log.MemberId = order.UserId;
                    log.GameCategory = order.GameTypeStr.ToEnum<GameType>().Value.TransToGameCate();
                    await _gameLogsService.SaveLogAsync(log);
                }

            }
        }


        /// <summary>
        /// 判断该订单MD5是否存在或者是否发生变化
        /// </summary>
        /// <param name="gameTypeStr"></param>
        /// <param name="sourceId"></param>
        /// <param name="md5"></param>
        /// <returns>bool1:是否存在sourceId的数据，bool2:MD5是否一致</returns>
        async Task<(bool, bool)> ExistMd5Cache(string gameTypeStr, string sourceId, string md5)
        {
            return await _gamelogsMd5CacheService.Process(gameTypeStr, sourceId, md5);
        }



        async Task SyncToGamesLogsAsync(List<GameLogs> logs)
        {
            List<int> finishedOrderIds = new List<int>();
            foreach (var log in logs)
            {
                var rt_logs = await _gameLogsService.SaveLogAsync(log);
                if (rt_logs.Item3 != 0)
                    finishedOrderIds.Add(rt_logs.Item3);
            }
            if (finishedOrderIds.Count > 0)
            {
                Console.WriteLine($"manually pushed orders {finishedOrderIds.ToJson()}");
                await _triggerBus.PublishAsync(new WashOrdersMessage() { Ids = finishedOrderIds });
            }
        }


        public async Task<(bool, string)> AsyncToMainGamesLogsAsync(GameCategory cate, GameType? gameTypeStr, int? orderId)
        {
            if (cate == GameCategory.NotSet) return (false, "请选择游戏类别");
            GameLogs log = null;
            if (orderId.HasValue && orderId.Value != 0)
            {
                // 获取到具体信息，然后同步
                switch (cate)
                {
                    case GameCategory.ESport:
                        break;
                    case GameCategory.Sport:
                        break;
                    case GameCategory.Live:
                        break;
                    case GameCategory.Slot:
                        break;
                    case GameCategory.Lottery:
                        var lotterylogs = await _gameLogsLotteryService.GetAsync(orderId.Value);
                        log = ModelToEntity.Mapping<GameLogs, GameLogsLottery>(lotterylogs);
                        log.MemberId = lotterylogs.UserId;
                        log.CreateTimeUtc8 = lotterylogs.CreateTime;
                        await SyncToGamesLogsAsync(new List<GameLogs> { log });
                        break;
                    case GameCategory.Chess:
                        break;
                    case GameCategory.Hunt:
                        break;
                }

            }
            else
            {
                // 获取24小时内的数据，然后进行同步
                DateTime endAt = DateTime.UtcNow.ToUtc8DateTime();
                DateTime startAt = endAt.AddHours(-24);
                switch (cate)
                {
                    case GameCategory.ESport:
                        break;
                    case GameCategory.Sport:
                        break;
                    case GameCategory.Live:
                        break;
                    case GameCategory.Slot:
                        break;
                    case GameCategory.Lottery:
                        var lottery_rt = await _gameLogsLotteryService.GetPageListAsync(new GameLogsQuery() { Limit = 100000, Page = 1, StartAt = startAt, EndAt = endAt });
                        if (lottery_rt.Item2 != 0)
                        {
                            List<GameLogs> gameLogs = new List<GameLogs>();
                            foreach (var lotterylogs in lottery_rt.Item1)
                            {
                                log = null;
                                log = ModelToEntity.Mapping<GameLogs, GameLogsLottery>(lotterylogs);
                                log.MemberId = lotterylogs.UserId;
                                log.CreateTimeUtc8 = lotterylogs.CreateTime;
                                gameLogs.Add(log);
                            }
                            await SyncToGamesLogsAsync(gameLogs);
                        }
                        break;
                    case GameCategory.Chess:
                        break;
                    case GameCategory.Hunt:
                        break;
                }
            }

            return (true, "请稍后查看");
        }



    }
}
