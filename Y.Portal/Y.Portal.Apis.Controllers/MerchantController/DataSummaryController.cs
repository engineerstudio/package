using Microsoft.AspNetCore.Mvc;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;
using Y.Infrastructure.Library.Core.YEntity;
using Y.Infrastructure.IApplication;
using Y.Packet.Services.IGames;
using Y.Packet.Services.IMembers;
using Y.Packet.Services.IPay;
using Y.Portal.Apis.Controllers.DtoModel.Merchant;

namespace Y.Portal.Apis.Controllers.MerchantController
{
    [Route("merchant/ds")]
    [ApiController]
    public class DataSummaryController : ControllerBase
    {

        private readonly IBaseHandlerService _baseHandler;
        private readonly IUsersService _usersService;
        private readonly IUserHierarchyService _userHierarchyService;
        private readonly IMemberDataSummaryService _memberDataSummaryService;
        private readonly IGameUsersDailyReportStatisticService _gameUsersDailyReportStatisticService;
        private readonly IGameDailyReportStatisticService _gameDailyReportStatisticService;
        private readonly IReportsHybridService _reportsHybridService;

        public DataSummaryController(IBaseHandlerService baseHandler, IUsersService usersService, IUserHierarchyService userHierarchyService, IMemberDataSummaryService memberDataSummaryService, IGameUsersDailyReportStatisticService gameUsersDailyReportStatisticService, IGameDailyReportStatisticService gameDailyReportStatisticService, IReportsHybridService reportsHybridService)
        {
            _baseHandler = baseHandler;
            _usersService = usersService;
            _userHierarchyService = userHierarchyService;
            _memberDataSummaryService = memberDataSummaryService;
            _gameUsersDailyReportStatisticService = gameUsersDailyReportStatisticService;
            _gameDailyReportStatisticService = gameDailyReportStatisticService;
            _reportsHybridService = reportsHybridService;
        }

        #region 代理统计

        [HttpPost("agentd")]
        public async Task<string> AgentData([FromForm] AgentQuery q)
        {
            int userId = 0;
            if (!q.AgentName.IsNullOrEmpty())
            {
                 userId = await _usersService.GetMemberIdByAccountNameAsync(_baseHandler.MerchantId, q.AgentName);
                if (userId == 0) return (false, "数据不存在").ToJsonResult();
            }
            if (q.StartTime is null) q.StartTime = DateTime.UtcNow.Date;
            if (q.EndTime is null) q.EndTime = DateTime.UtcNow.Date.AddDays(1);

            var list = await _reportsHybridService.LoadAgentsReportsAsync(_baseHandler.MerchantId, userId, q.StartTime.Value, q.EndTime.Value, q.AgentName);

            if (!list.Item1) return list.Item2.ToTableModelError();

            return (new TableDataModel
            {
                count = list.Item3.Count(),
                data = list.Item3
            }).ToJson();
        }



        #endregion


        #region 游戏盈亏

        [HttpPost("gamed")]
        public async Task<string> GameData(GameQuery q)
        {
            if (q.QueryType.IsNullOrEmpty()) return (false, "请选择查询方式").ToJsonResult();
            if (q.QueryType == "Merchant") // 按照包网平台时间
            {
                var tbRt = await _gameDailyReportStatisticService.Get(_baseHandler.MerchantId, q.GameCate.GetEnumValue(), q.GameTypes, q.StartTime, q.EndTime);
                var list = tbRt.Item3;
                return (new TableDataModel
                {
                    count = list.Count(),
                    data = list.Select(t => new GameD()
                    {
                        GameName = t.GameTypeStr.ToEnum<GameType>().Value.GetDescription(),
                        BetOrderCount = t.BetOrderCount,
                        SettlementOrderCount = t.SettlementOrderCount,
                        TotalBet = t.BetAmount,
                        TatalValidBet = t.ValidBet,
                        MerchantMoney = t.Money,
                        GameMoney = t.Money
                    })
                }).ToJson();

            }
            else //Game： 按照游戏平台时间
            {

            }
            return string.Empty;
        }






        #endregion



        #region 会员报表

        [HttpPost("memberd")]
        public async Task<string> MemberData()
        {
            return string.Empty;
        }


        #endregion


        #region 充提统计



        [HttpPost("moneyd")]
        public async Task<string> MoneyData()
        {
            return string.Empty;
        }






        #endregion



    }
}
