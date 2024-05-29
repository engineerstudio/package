using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using Y.Packet.Entities.Members.ViewModels;
using Y.Packet.Entities.Members;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Y.Infrastructure.IApplication;
using Y.Infrastructure.Library.Core.YEntity;
using static Y.Packet.Entities.Members.Users;
using Y.Packet.Entities.Games;
using Y.Portal.Apis.Controllers.Helper;
using Y.Packet.Services.IVips;
using Y.Packet.Services.IMembers;
using Y.Packet.Services.IGames;

namespace Y.Portal.Apis.Controllers.MerchantController
{
    [Route(RouteHelper.BaseMerchantRoute)]
    [ApiController]
    public class MemberController : ControllerBase
    {

        private readonly IUsersService _usersService;
        private readonly IVipGroupsService _vipGroupsService;
        private readonly IUsersFundsLogService _usersFundsLogService;
        private readonly Dictionary<string, string> _sysconfig;
        private readonly IBaseHandlerService _baseHandlerService;
        private readonly IGameUsersService _gameUsersService;

        public MemberController(IUsersService usersService, IVipGroupsService vipGroupsService, IUsersFundsLogService usersFundsLogService, IOptionsSnapshot<Dictionary<string, string>> options, IBaseHandlerService baseHandlerService, IGameUsersService gameUsersService)
        {
            _vipGroupsService = vipGroupsService;
            _usersService = usersService;
            _usersFundsLogService = usersFundsLogService;
            _sysconfig = options.Get(DefaultString.Sys_Default);
            _baseHandlerService = baseHandlerService;
            _gameUsersService = gameUsersService;
        }

        #region 用户操作

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("load")]
        public async Task<string> GetMembersAsync([FromForm] UsersListQuery q)
        {
            int merchantId = _baseHandlerService.MerchantId;
            q.MerchantId = merchantId;
            var rt = await _usersService.GetPageListAsync(q);
            var groupDic = await _vipGroupsService.GetGroupIdAndNameDicAsync(merchantId);

            return (new TableDataModel()
            {
                count = rt.Item2,
                data = rt.Item1.Select(t => new
                {
                    t.Id,
                    t.AccountName,
                    t.Type,
                    t.GroupId,
                    t.AgentSetting,
                    t.AgentId,
                    GroupName = groupDic.Item2.GetByKey(t.GroupId)
                })
            }).ToJson();
        }

        /// <summary>
        /// 新增一个用户
        /// </summary>
        /// <param name="rmodel"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<string> AddMember([FromForm] UserRegisterModel rmodel)
        {
            rmodel.MerchantId = _baseHandlerService.MerchantId;
            rmodel.Pasw = DefaultString.DefaultPassword;
            var defalutGroup = await _vipGroupsService.GetDefaultGroupIdAsync(_baseHandlerService.MerchantId);
            if (!defalutGroup.Item1) return (defalutGroup.Item1, defalutGroup.Item2).ToJsonResult();
            rmodel.DefaultGroupId = defalutGroup.Item3;
            return (await _usersService.InsertAsync(rmodel)).ToJsonResult();
        }

        [HttpPost("updateinfo")]
        public async Task<string> UpdateMemberInfo([FromForm] UserModifyModel rq)
        {
            var rt = await _usersService.UpdateMemberInfoAsync(rq);
            return rt.ToJsonResult();
        }

        [HttpPost("resetfpsw")]
        public async Task<string> ResetFpswAsync([FromForm] int userId)
        {
            var rt = await _usersService.ResetUserInfoAsync(userId, "FPasw");
            return rt.ToJsonResult().ToString();
        }


        #endregion

        #region 代理操作

        /// <summary>
        /// 设置用户为代理
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("setagent")]
        public async Task<string> SetAgent([FromForm] UserSetAgentModel req)
        {
            req.MerchantId = _baseHandlerService.MerchantId;
            AgentSetting agentSet = new AgentSetting() { AgentRebateId = req.AgentRebateId, CommissionId = req.commissionId, ContractId = req.ContractId };
            return (await _usersService.UpdateUserTypeStatus(req.MerchantId, req.UserId, req.AgentId, req.Type, agentSet)).ToJsonResult();
        }

        /// <summary>
        /// 获取所有代理用户
        /// </summary>
        /// <returns></returns>
        [HttpPost("getagents")]
        public async Task<string> GetAgents()
        {
            return (await _usersService.GetAllAgentsDicJsonAsync(_baseHandlerService.MerchantId)).ToJsonResult();
        }

        /// <summary>
        /// 获取代理列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("agentload")]
        public async Task<string> GetAgents([FromForm] UsersListQuery q)
        {
            int merchantId = _baseHandlerService.MerchantId;
            q.MerchantId = merchantId;
            var rt = await _usersService.GetAgentPageListAsync(q);
            var groupDic = await _vipGroupsService.GetGroupIdAndNameDicAsync(merchantId);

            // 获取所有的返水方案

            // 获取所有的合约方案

            return (new TableDataModel()
            {
                count = rt.Item2,
                data = rt.Item1.Select(t => new
                {
                    t.Id,
                    t.AccountName,
                    t.AgentSetting,
                    RebatePlan = t.AgentSettingEntity.CommissionId,
                    ContractPlan = t.AgentSettingEntity.ContractId,
                    IsDefault = t.AgentSettingEntity.IsDefault ? "是" : "否",
                    Code = t.AgentSettingEntity.Code,
                    t.AgentId,
                    t.CreateTime
                })
            }).ToJson();
        }

        [HttpPost("setagentrule")]
        public async Task<string> SetAgentRule([FromForm] UserAgentSetModel q)
        {
            q.MerchantId = _baseHandlerService.MerchantId;
            if (q.Id == 0)
            {
                // 1. 添加用户
                UserRegisterModel rmodel = new UserRegisterModel() { AccountName = q.AccountName, MerchantId = _baseHandlerService.MerchantId, Pasw = DefaultString.DefaultPassword };
                var defalutGroup = await _vipGroupsService.GetDefaultGroupIdAsync(_baseHandlerService.MerchantId);
                if (!defalutGroup.Item1) return (defalutGroup.Item1, defalutGroup.Item2).ToJsonResult();
                rmodel.DefaultGroupId = defalutGroup.Item3;
                var rt = (await _usersService.InsertAsync(rmodel));
                if (!rt.Item1) return rt.ToJsonResult();
                // 2. 设置用户的代理属性
                q.Id = rt.Item3; // 新增的用户Id
            }
            // 更新代理规则
            return (await _usersService.UpdateAgentRules(q)).ToJsonResult();
        }

        [HttpPost("setdefaultagent")]
        public async Task<string> SetDefaultAgentAsync([FromForm] int userId)
        {
            return (await _usersService.SetDefaultAgentAsync(_baseHandlerService.MerchantId, userId, true)).ToJsonResult();
        }

        [HttpPost("canneldefaultagent")]
        public async Task<string> CannelDefaultAgentAsync([FromForm] int userId)
        {
            return (await _usersService.SetDefaultAgentAsync(_baseHandlerService.MerchantId, userId, false)).ToJsonResult();
        }



        #endregion

        #region 用户账变


        [HttpPost("getfundslog")]
        public async Task<string> GetMemberFundsLog([FromForm] FundsLogQuery q)
        {
            q.MerchantId = _baseHandlerService.MerchantId;
            if (q.EndAt == null) q.EndAt = DateTime.UtcNow.AddHours(8);
            var rt = (await _usersFundsLogService.GetPageListAsync(q));
            return (new TableDataModel()
            {
                count = rt.Item2,
                data = rt.Item1.Select(t =>
                {
                    var subType = FundsTypeTransUtility.TransToFundLogType(t.SubFundsType);

                    return new
                    {
                        t.Id,
                        t.Amount,
                        t.LockedAmount,
                        t.Balance,
                        FundsType = t.FundsType.ToString(),
                        FundsTypeDesc = t.FundsType.GetDescription(),
                        FundsTo = subType.DescValue,
                        TransTypeDesc = t.TransType.GetDescription(),
                        t.IP,
                        t.Marks,
                        t.CreateTime
                    };
                })
            }).ToJson();
        }




        #endregion

        #region 游戏账户

        [HttpPost("gameuser")]
        public async Task<string> LoadGameUsersAsync([FromForm] int userId)
        {
            var rt = await _gameUsersService.GetAsync(_baseHandlerService.MerchantId, userId);

            return (new TableDataModel()
            {
                count = rt.Count(),
                data = rt.Select(t => new
                {
                    t.Id,
                    t.TypeStr,
                    t.PlayerName,
                    t.Balance,
                    t.IsLock,
                    t.IsTest,
                    t.RegisterTime
                })
            }).ToJson();
        }

        [HttpPost("deletegameuser")]
        public async Task<string> DeleteGameUsersAsync(int gameuserId, GameType type)
        {
            var rt = await _gameUsersService.DeleteGameUserAsync(gameuserId, type);
            return rt.ToJsonResult();
        }

        [HttpPost("asyncgameuserbalance")]
        public async Task<string> AsyncGameUsersBalanceAsync(int gameuserId, GameType type)
        {
            return (true, "方法未实现").ToJsonResult();
        }





        #endregion


    }
}