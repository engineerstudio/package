using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Entities.ViewModel;
using Y.Infrastructure.IApplication;
using Y.Packet.Services.IGames;
using Y.Packet.Services.IMembers;
using Y.Packet.Services.IMerchants;
using Y.Packet.Services.IPay;


namespace Y.Infrastructure.Application
{
    public class ReportsHybridService : IReportsHybridService
    {
        private readonly IMerchantService _merchantService;
        private readonly IUsersService _usersService;
        private readonly IPayOrderService _payOrderService;

        private readonly IUserHierarchyService _userHierarchyService;
        private readonly IMemberDataSummaryService _memberDataSummaryService;
        private readonly IGameUsersDailyReportStatisticService _gameUsersDailyReportStatisticService;
        private readonly IGameDailyReportStatisticService _gameDailyReportStatisticService;
        public ReportsHybridService(IMerchantService merchantService, IUsersService usersService, IPayOrderService payOrderService, IUserHierarchyService userHierarchyService, IMemberDataSummaryService memberDataSummaryService, IGameUsersDailyReportStatisticService gameUsersDailyReportStatisticService, IGameDailyReportStatisticService gameDailyReportStatisticService)
        {
            _merchantService = merchantService;
            _usersService = usersService;
            _payOrderService = payOrderService;
            _userHierarchyService = userHierarchyService;
            _memberDataSummaryService = memberDataSummaryService;
            _gameUsersDailyReportStatisticService = gameUsersDailyReportStatisticService;
            _gameDailyReportStatisticService = gameDailyReportStatisticService;
        }

        // 查询当前代理，以及当前代理下的汇总信息
        public async Task<(bool,string , List<AgentsReportsViewModel>)> LoadAgentsReportsAsync(int merchantId, int agentId, DateTime startAt, DateTime endAt,string agentName)
        {
            if (merchantId == 0 ) return (false,"参数错误",null);
            if (startAt == default || endAt == default) return (false, "请输入查询时间区间", null);
            var mch = await _merchantService.GetAsync(merchantId);
            // 0. 代理信息
            var agentUser = await _usersService.GetUserById(merchantId,agentId);
            // 1. 成员新增

            // 2. 成员充值

            // 3. 打码

            // 4. 负盈利

            // 5. 分润点位


            Dictionary<int, string> agentDic = new Dictionary<int, string>();
            if (!agentName.IsNullOrEmpty())
            {
                int userId = await _usersService.GetMemberIdByAccountNameAsync(merchantId, agentName);
                if (userId == 0) return (false, "数据不存在",null);
                agentDic.Add(userId, agentName);
            }
            else
                agentDic = await _usersService.GetAllAgentsDicAsync(merchantId);
            List<AgentsReportsViewModel> list = new List<AgentsReportsViewModel>();
            AgentsReportsViewModel agent;
            foreach (var ag in agentDic)
            {
                agent = new AgentsReportsViewModel();
                agent.UserName = ag.Value;
                var subUserIds = await _userHierarchyService.GetMemberSubHierarchy(agentId, ag.Key);
                agent.SubUserNo = subUserIds.Count();
                agent.NewUserNo =  await _usersService.GetNewRegisterCountAsync(merchantId, ag.Key,startAt,endAt); // 单位时间新增
                var payWith = await _memberDataSummaryService.GetMembersDepositWithdrawalTotalAsync(subUserIds);
                agent.Deposit = payWith.Item1.ToString("F2");
                agent.Withdrawal = payWith.Item2.ToString("F2");
                var game = await _gameUsersDailyReportStatisticService.GetMembersDataSummaryByIds(merchantId, subUserIds, startAt, endAt);
                agent.Bet = game.Item1.ToString("F2");
                agent.ValidBet = game.Item2.ToString("F2");
                agent.Loss = game.Item3.ToString("F2");
                list.Add(agent);
            }

            return (true, "", list);
        }
    }
}
