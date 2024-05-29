using Microsoft.AspNetCore.Mvc;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.IApplication;
using Y.Packet.Entities.Games;
using Y.Packet.Entities.Games.ViewModels;
using Y.Packet.Services.IGames;
using Y.Packet.Services.IMembers;

namespace Y.Portal.Apis.Controllers.MerchantController
{
    [Route("merchant/game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IInfrastructureGamesService _infrastructureGamesService;
        private readonly IGameInfoService _gameInfoService;
        private readonly IGameMerchantService _gameMerchantService;
        private readonly IGameLogsService _gameLogsService;
        private readonly IGameTransferLogsService _gameTransferLogsService;
        private readonly IUsersFundsService _usersFundsService;
        private readonly IBaseHandlerService _baseHandler;
        private readonly IUsersFundsLogService _usersFundsLogService;
        private readonly IGameLogsCallBackProcessService _gameLogsCallBackProcessService;
        public GameController(IInfrastructureGamesService infrastructureGamesService, IGameInfoService gameInfoService, IGameMerchantService gameMerchantService, IGameLogsService gameLogsService, IGameTransferLogsService gameTransferLogsService, IGameLogsEsportService gameLogsEsportService, IGameLogsSportService gameLogsSportService, IUsersFundsService usersFundsService, IBaseHandlerService baseHandler, IUsersFundsLogService usersFundsLogService, IGameLogsCallBackProcessService gameLogsCallBackProcessService)
        {
            _infrastructureGamesService = infrastructureGamesService;
            _gameInfoService = gameInfoService;
            _gameMerchantService = gameMerchantService;
            _gameLogsService = gameLogsService;
            _gameTransferLogsService = gameTransferLogsService;
            _usersFundsService = usersFundsService;
            _baseHandler = baseHandler;
            _usersFundsLogService = usersFundsLogService;
            _gameLogsCallBackProcessService = gameLogsCallBackProcessService;
        }

        [HttpPost("transfer")]
        public async Task<string> Transfer()
        {
            // TODO 这个必做  游戏转账
            string gameConfig = string.Empty;
            var rt = await _infrastructureGamesService.TransferAsync(_baseHandler.MerchantId, _baseHandler.MemberId, "", GameType.ESport_AVIA, 1000, 1000, TransType.In, gameConfig);

            return (rt.Item1, rt.Item2).ToJsonResult();

        }

        [HttpGet("category")]
        public string GetGamesByCategory()
        {
            return _gameInfoService.GetGamesByCategory();
        }

        /// <summary>
        /// 获取商户配置的游戏JSON数据 
        /// </summary>
        /// <returns></returns>
        [HttpPost("configgames")]
        public async Task<string> GetMerchantGamesByCategoryAsync()
        {
            if (_baseHandler.MerchantId == 0) return (false, "商户错误").ToJsonResult();
            return (await _gameMerchantService.GetMerchantGames(_baseHandler.MerchantId));
        }



        #region 游戏列表

        [HttpPost("merchantsconfig")]
        public async Task<string> GetMerchantGames([FromForm] MerchantGameListQuery q)
        {
            int merchantId = _baseHandler.MerchantId;
            q.MerchantId = merchantId;
            var rt = await _gameMerchantService.GetListAsync(q);

            return (new TableDataModel
            {
                count = rt.Item2,
                data = rt.Item1.Select(t => new
                {
                    t.Id,
                    t.TypeStr,
                    t.TypeDesc,
                    t.Enabled,
                    t.Config,
                    t.Rate
                })
            }).ToJson();

        }


        /// <summary>
        /// 查询Gamelogs表格订单
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [HttpPost("gameorders")]
        public async Task<string> GetMerchantGameOrders([FromForm] MerchantGameQuery q)
        {
            q.MerchantId = _baseHandler.MerchantId;
            var rt = await _gameLogsService.GetPageListAsync(q);
            // 获取到查询用的ID

            // TODO 订单列表

            return (new TableDataModel
            {
                count = rt.Item2,
                data = rt.Item1.Select(t => new
                {
                    t.Id,
                    t.SourceId,
                    t.GameCategory,
                    GameCategoryDes = t.GameCategory.GetDescription(),
                    t.GameTypeStr,
                    GameTypeDes = t.GameTypeStr.ToEnum<GameType>().Value.GetDescription(),
                    t.MemberId,
                    t.PlayerName,
                    t.Status,
                    StatusDes = t.Status.GetDescription(),
                    t.BetAmount,
                    t.ValidBet,
                    t.Money,
                    t.AwardAmount,
                    t.OrderCreateTimeUtc8,
                    t.OrderAwardTimeUtc8
                }),
                totalRow = rt.Item3
            }).ToJson();

        }


        /// <summary>
        /// 获取订单详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("gameordersdetails")]
        public async Task<string> GetGameOrderDetails([FromForm] int orderId, [FromForm] GameCategory cate)
        {

            switch (cate)
            {
                case GameCategory.NotSet:
                    break;
                case GameCategory.ESport:
                    return (await _gameLogsService.GetByOrderDetailsId(orderId, cate)).ToJsonResult();
                case GameCategory.Sport:
                    var sportRt = (await _gameLogsService.GetByOrderDetailsId(orderId, cate));
                    var sport = (GameLogsSport)sportRt.Item2;
                    var sport_detail = new
                    {
                        GameCateStr = sport.GameCategory.GetDescription(),
                        GameType = sport.GameTypeStr.ToEnum<GameType>().Value.GetDescription(),
                        GameName = sport.Event,
                        Player = sport.PlayerName,
                        SourceId = sport.SourceId,
                        OrderStatus = sport.Status.GetDescription(),
                        MatchName = sport.MatchName,
                        BetContent = sport.BetContent,
                        Odds = $"{sport.SourceOdds} / {sport.SourceOddsType.GetDescription()}",
                        BetAmount = sport.BetAmount,
                        AwardAmount = sport.AwardAmount,
                        Money = sport.Money,
                        BetCreateTime = sport.SourceOrderCreateTime,
                        BetAwardTime = sport.SourceOrderAwardTime,
                        UpdateTime = sport.UpdateTime,
                        Results = sport.Results,
                        ReSettlement = "否",
                        //IP = sport.IP
                    };


                    return (sportRt.Item1, sport_detail).ToJsonResult();
                case GameCategory.Live:
                    break;
                case GameCategory.Slot:
                    break;
                case GameCategory.Lottery:
                    break;
                case GameCategory.Chess:
                    break;
                case GameCategory.Hunt:
                    break;
                default:
                    break;
            }
            return (false, "").ToJsonResult();
        }


        /// <summary>
        /// 游戏转账日志
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [HttpPost("transferlogs")]
        public async Task<string> GetGameTransferLogs([FromForm] TransferLogQuery q)
        {
            q.MerchantId = _baseHandler.MerchantId;
            var rt = await _gameTransferLogsService.GetPageList(q);

            return (new TableDataModel
            {
                count = rt.Item2,
                data = rt.Item1.Select(t => new
                {
                    t.Id,
                    t.MerchantId,
                    t.UserId,
                    t.TypeStr,
                    StatusStr = t.Status.GetDescription(),
                    t.Status,
                    t.CreateTime,
                    t.Money,
                    TransTypeStr = t.TransType.GetDescription()
                })
            }).ToJson();
        }



        [HttpPost("unlocktranslog")]
        public async Task<string> UnlockGameTransLog([FromForm] int id)
        {

            // 1. 判断转账日志是否存在
            var logs = await _gameTransferLogsService.GetAsync(id);
            if (logs == null) return (false, "游戏转账信息不存在").ToJsonResult();
            GameType gType = logs.TypeStr.ToEnum<GameType>().Value;
            // 2. 判断锁定金额是否正常
            var lockAmount = await _usersFundsService.GetUserLockFundsAsync(_baseHandler.MerchantId, _baseHandler.MemberId);
            if (logs.Money > lockAmount) return (false, "账户异常,请立即联系管理员").ToJsonResult();
            var usrlog = await _usersFundsLogService.ExistLogAsync(logs.Id, logs.MerchantId, logs.UserId, gType);
            if (!usrlog) return (false, "转账日志不存在").ToJsonResult();
            // 3. 判断第三方转账是否成功
            var gameConfig = await _gameInfoService.GetGameConfig(gType);
            var rt = await _infrastructureGamesService.CheckTransferAsync(logs.MerchantId, logs.UserId, null, gType, id, logs.TransType, gameConfig);
            return (rt.Item1, rt.Item2).ToJsonResult();
        }


        [HttpPost("merchantgametype")]
        public async Task<string> GetMerchantGame([FromForm] string gameCategory)
        {
            int merchantId = _baseHandler.MerchantId;
            var rt = await _gameMerchantService.GetListDicAsync(merchantId);
            return (rt.Item1, rt.Item2.ToJson()).ToJsonResult();
        }


        [HttpPost("updatemerchantgamestatus")]
        public async Task<string> UpdateMerchantGameStatus([FromForm] string gameTypeStr, [FromForm] bool enabled)
        {
            int merchantId = _baseHandler.MerchantId;

            var rt = await _gameMerchantService.EnableGame(merchantId, gameTypeStr, enabled);
            return rt.ToJsonResult();
        }


        [HttpPost("pushordertowash")]
        public async Task<string> PushOrderToWashOrderDetails([FromForm] int orderId)
        {
            await _gameLogsCallBackProcessService.Process(orderId);
            //return (await _gameLogsService.PushOrderToListAsync(orderId)).ToJsonResult();
            return (true, "").ToJsonResult();
        }


        [HttpPost("deletemerchantgame")]
        public async Task<string> DeleteAsync(int id)
        {
            var rt = await _gameMerchantService.DeleteAsync(_baseHandler.MerchantId, id);
            return rt.ToJsonResult();
        }


        [HttpPost("asynctogamelogs")]
        public async Task<string> AsyncToMainGamesLogsAsync([FromForm] GameCategory gameCateStr)
        {
            var rt = await _infrastructureGamesService.AsyncToMainGamesLogsAsync(gameCateStr, null, null);
            return rt.ToJsonResult();
        }


        #endregion




        #region 订单列表









        #endregion






    }
}