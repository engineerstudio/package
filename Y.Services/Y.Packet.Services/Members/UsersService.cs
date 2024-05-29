using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Encrypt;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.Mapper;
using Y.Packet.Entities.Members.ViewModels;
using Y.Packet.Repositories.IMembers;
using Y.Packet.Services.IMembers;
using Y.Packet.Entities.Members;
using System.Linq;
using Dapper;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.WebInfrastructure.EnumeratedType;
using Y.Infrastructure.Cache.Redis.VipsService;
using Microsoft.Extensions.Options;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using static Y.Packet.Entities.Members.Users;
using Y.Packet.Entities.Members.ViewModels.UserCenter;
using Y.Packet.Entities.Members.ViewModels.AgentCenter;

namespace Y.Packet.Services.Members
{
    public class UsersService : VipGroupsCacheService, IUsersService
    {
        private readonly IUsersRepository _repository;
        private readonly IUsersSessionService _usersSessionService;
        private readonly IUserHierarchyService _userHierarchyService;
        private readonly IUsersFundsRepository _usersFundsRepository;
        public UsersService(IOptionsMonitor<YCacheConfiguration> options, IYCacheFactory factory, IUsersRepository repository, IUsersSessionService usersSessionService, IUserHierarchyService userHierarchyService, IUsersFundsRepository usersFundsRepository) : base(options, factory)
        {
            _repository = repository;
            _usersSessionService = usersSessionService;
            _userHierarchyService = userHierarchyService;
            _usersFundsRepository = usersFundsRepository;
        }


        public async Task<(IEnumerable<Users>, int)> GetPageListAsync(UsersListQuery q)
        {
            var parms = new DynamicParameters();
            string conditions = $"WHERE 1=1 AND MerchantId={q.MerchantId} ";
            if (q.Id != 0)
                conditions += $" AND Id={q.Id} ";
            if (q.GroupId != 0)
                conditions += $" AND GroupId={q.GroupId} ";
            if (!string.IsNullOrEmpty(q.AccountName))
            {
                conditions += $" AND AccountName like @AccountName ";
                parms.Add("AccountName", $"%{q.AccountName}%");
            }
            if (!string.IsNullOrEmpty(q.MemberType))
            {
                var type = q.MemberType.ToEnum<UserType>();
                conditions += $" AND Type = {(int)type}";
            }
            if (q.AgentId != null)
            {
                conditions += $" AND AgentId={q.AgentId.Value} ";
            }
            if (q.RegisterAt != null && q.RegisterEnd != null)
            {
                conditions += $" AND CreateTime >= N'{q.RegisterAt}' AND CreateTime < N'{q.RegisterEnd}'";
            }

            var list = await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", parms);
            return (list, _repository.RecordCount(conditions, parms));
        }


        public async Task<(bool sucess, string errorMsg, Users entity)> GetUserById(int merchantId, int userId)
        {
            if (merchantId == 0 || userId == 0) return (false, "参数错误", null);

            var user = await _repository.GetFromCacheAsync(merchantId, userId);
            if (user == null) return (false, "用户不存在", null);

            return (true, null, user);
        }

        /// <summary>
        /// 判断用户Id是否属于该商户下面
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns>true:用户存在， false:用户不存在</returns>
        public async Task<bool> ExistUser(int merchantId, int userId)
        {
            var user = await _repository.GetFromCacheAsync(merchantId, userId);
            return user != null;
        }

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="users"></param>
        /// <returns>是否新增成功，信息，新增成功的Id</returns> 
        public async Task<(bool, string, int)> InsertAsync(UserRegisterModel rmodel)
        {
            Users users = ModelToEntity.Mapping<Users, UserRegisterModel>(rmodel);
            int userId = default(int);

            if (users.MerchantId == 0) return (false, "商户信息错误", userId);
            if (string.IsNullOrEmpty(users.AccountName)) return (false, "登录用户名不得为空", userId);
            if (string.IsNullOrEmpty(users.Pasw)) return (false, "登录密码不得为空", userId);
            if (!XValidator.IsStringLengthOnlyStartWithEnglishCharacter(users.AccountName, 5, 18)) return (false, "注册用户名不符合规则(字母开头，允许6-18字节，允许字母数字下划线)", userId);
            if (!XValidator.IsStringLengthOnlyStartWithEnglishCharacter(users.Pasw, 5, 18)) return (false, "注册密码不符合规则(字母开头，允许6-18字节，允许字母数字下划线)", userId);
            if (await _repository.ExistMemberNameAsync(rmodel.MerchantId, rmodel.AccountName) != 0) return (false, "用户名已经存在", userId);


            // 手机条件判断
            if (string.IsNullOrEmpty(users.Mobile))
                users.Mobile = "";
            else if (!XValidator.IsMobilePhoneNumber(users.Mobile)) return (false, "注册手机号码不符合规则", userId);

            // 资金密码判断
            if (rmodel.FPasw.IsNullOrEmpty())
                users.FPasw = "";
            else
            {
                if (rmodel.FPasw.Length != 6) return (false, "请输入六位数字的资金密码", userId);
                if (!XValidator.IsNumber(rmodel.FPasw)) return (false, "资金密码必须为数字", userId);
                users.FPasw = AESEncryptHelper.Encode(users.FPasw.Trim(), DefaultString.AesEncryptKeysForFunds);
            }


            //// 真实姓名判断
            //if(rmodel.IdName.IsNullOrEmpty())
            //    users.AccountName

            // 判断当前域名是否是代理域名，如果不是，那么清空该域名
            var agentId_get = await GetAgentIdByUrlAsync(rmodel.MerchantId, rmodel.RegisterUrl);
            if (agentId_get == 0) rmodel.RegisterUrl = null;

            Users.UserType usr_type = UserType.User;
            // #根据代理Code / 域名找出上级代理
            if (!rmodel.Code.IsNullOrEmpty())
            {
                // 判断code是否粗存在，如不存在则返回错误
                var agentId = await GetAgentIdByCodeAsync(rmodel.MerchantId, rmodel.Code);
                if (agentId == 0) return (false, "推广码错误，请输入代理推广码", userId);
                users.AgentId = agentId;
            }
            else if (!rmodel.RegisterUrl.IsNullOrEmpty()) // 根据Url找出对应的代理
            {
                users.AgentId = await GetAgentIdByUrlAsync(rmodel.MerchantId, rmodel.RegisterUrl);
            }
            else
            {
                if (await ExistMemberAsync(rmodel.MerchantId)) // 极少数注册会员不存在code 与 url的
                {
                    // TODO 这里可以判定如果不是代理进来的话，那么禁止注册 
                    // TODO 找到站点的默认代理，分配给用户
                    var rt_agentId_default = await GetDefaultAgentIdAsync(rmodel.MerchantId);
                    if (!rt_agentId_default.Item1) return (false, "不存在的代理上级", userId);
                    users.AgentId = rt_agentId_default.Item3;
                }
                else // 当前站点不存在会员，站点初始化，设置默认代理 
                {
                    users.AgentId = 0;
                    usr_type = UserType.Agent;
                }
            }

            users.Type = usr_type;
            users.CreateTime = DateTime.UtcNow.AddHours(8);
            users.BirthDate = DefaultString.DefaultDateTime;
            users.Pasw = AESEncryptHelper.Encode(users.Pasw.Trim(), DefaultString.AesEncryptKeys);
            users.IdName = "";
            users.AgentSetting = users.AgentSettingEntity.ToJson();
            users.GroupId = rmodel.DefaultGroupId;

            var rt = await _repository.InsertWithCacheAsync(users);
            if (rt.HasValue)
            {
                // TODO 插入用户金额记录
                await _usersFundsRepository.InsertWithCacheAsync(new UsersFunds() { MerchantId = rmodel.MerchantId, UserId = rt.Value });
                // TODO 插入等级关系
                await _userHierarchyService.SetMemberHierarchy(rmodel.MerchantId, rt.Value, rmodel.AgentId);
            }

            return rt.HasValue ? (true, "保存用户成功", rt.Value) : (false, "保存用户失败", userId);

        }

        /// <summary>
        /// [站点初始化]创建新增用户
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<(bool, string, int)> CreateMerchantAdminDefaultMemberAsync(int merchantId, int groupId)
        {
            var rt_default_agentId = await GetDefaultAgentIdAsync(merchantId);
            UserRegisterModel urm = new UserRegisterModel();
            urm.MerchantId = merchantId;
            urm.AccountName = $"User{merchantId}";
            urm.Pasw = DefaultString.DefaultPassword;
            urm.IdName = "";
            urm.FPasw = "";
            urm.AgentId = rt_default_agentId.Item3;
            urm.Mobile = "13288888888";
            urm.Code = "";
            urm.RegisterUrl = "";
            urm.DefaultGroupId = groupId;
            var rt_user = await InsertAsync(urm);

            var rt_agent_set = await UpdateUserTypeStatus(merchantId, rt_user.Item3, 0, UserType.Agent, new AgentSetting() { IsDefault = true });
            return rt_user;
        }

        /// <summary>
        /// 获取当前站点默认代理Id
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<(bool, string, int)> GetDefaultAgentIdAsync(int merchantId)
        {
            var rt = await _repository.GetAgentsAsync(merchantId);
            if (rt.Count() == 0) return (false, string.Empty, default(int));
            int default_int = default(int);
            foreach (var r in rt)
                if (r.AgentSettingEntity.IsDefault)
                    default_int = r.Id;

            if (default_int == default(int)) return (false, string.Empty, default(int));

            return (true, string.Empty, default_int);
        }

        /// <summary>
        /// [站点初始化]判断站点是否存在会员
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns>true:存在会员，false:不存在会员</returns>
        private async Task<bool> ExistMemberAsync(int merchantId)
        {
            var rt = await _repository.RecordCountAsync($"WHERE MerchantId={merchantId}");
            if (rt == 0) return false;
            return true;
        }


        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        public async Task<(bool, string)> UpdateMemberInfoAsync(UserModifyModel rq)
        {
            var user = await _repository.GetFromCacheAsync(rq.MemberId);
            user.GroupId = rq.GroupId;
            var rt = await _repository.UpdateWithCacheAsync(user);
            if (rt > 0) return (true, "更新成功");
            return (false, "更新失败");
        }

        /// <summary>
        /// 根据用户Id获取账户名称
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public async Task<string> GetAccountNameByMemberIdAsync(int memberId)
        {
            if (memberId == 0) return "";
            var cache = await base.GetAccountNameByMemberIdCacheAsync(memberId);
            if (cache.Exist) return cache.Data;
            var rt = await _repository.GetFromCacheAsync(memberId);
            if (rt == null) return "";
            await base.CreateAccountNameByMemberIdCacheAsync(memberId, rt.AccountName);
            return rt.AccountName;
        }


        public async Task<int> GetMemberIdByAccountNameAsync(int merchantId, string accountName)
        {
            if (string.IsNullOrEmpty(accountName)) return 0;
            var rt = await _repository.GetByAccountNameAsync(merchantId, accountName);
            if (rt == null) return 0;
            return rt.Id;
        }

        /// <summary>
        /// 更新登陆密码
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="oldPsw"></param>
        /// <param name="newPsw"></param>
        /// <returns></returns>
        public async Task<(bool, string)> UpdatePasswordAsync(int memberId, string oldPsw, string newPsw)
        {
            if (oldPsw.IsNullOrEmpty()) return (false, "登录密码不得为空");
            if (newPsw.IsNullOrEmpty()) return (false, "新登录密码不得为空");
            if (!XValidator.IsStringLengthOnlyStartWithEnglishCharacter(newPsw, 6, 18)) return (false, "注册密码不符合规则(字母开头，允许6-18字节，允许字母数字下划线)");
            if (memberId == 0) return (false, "会员不存在");
            var user = await _repository.GetFromCacheAsync(memberId);
            if (user == null) return (false, "会员不存在");
            if (user.Pasw != AESEncryptHelper.Encode(oldPsw.Trim(), DefaultString.AesEncryptKeys)) return (false, "旧密码输入错误");
            user.Pasw = AESEncryptHelper.Encode(newPsw.Trim(), DefaultString.AesEncryptKeys);
            var rt = await _repository.UpdateWithCacheAsync(user);
            if (rt == 0) return (false, "修改密码失败,请联系管理员");
            // TODO 用户T出线
            return (true, "保存成功");
        }

        /// <summary>
        /// 更新资金密码
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="oldFpsw"></param>
        /// <returns></returns>
        public async Task<(bool, string)> UpdateFundsPasswordAsync(int memberId, string oldFpsw, string fpsw)
        {
            if (memberId == 0) return (false, "会员不存在");
            if (oldFpsw.IsNullOrEmpty()) return (false, "旧密码不得为空");
            if (fpsw.IsNullOrEmpty()) return (false, "资金密码不得为空");
            if (!XValidator.IsStringLengthOnlyStartWithEnglishCharacter(oldFpsw, 6, 18)) return (false, "旧密码不符合规则(字母开头，允许6-18字节，允许字母数字下划线)");
            if (!XValidator.IsStringLengthOnlyStartWithEnglishCharacter(fpsw, 6, 18)) return (false, "资金密码不符合规则(字母开头，允许6-18字节，允许字母数字下划线)");
            var user = await _repository.GetFromCacheAsync(memberId);
            if (user == null) return (false, "会员不存在");
            if (user.FPasw != AESEncryptHelper.Encode(oldFpsw.Trim(), DefaultString.AesEncryptKeysForFunds)) return (false, "旧密码输入错误");
            user.FPasw = AESEncryptHelper.Encode(fpsw.Trim(), DefaultString.AesEncryptKeysForFunds);
            var rt = await _repository.UpdateWithCacheAsync(user);
            if (rt == 0) return (false, "修改密码失败,请联系管理员");
            return (true, "保存成功");
        }

        /// <summary>
        /// 验证资金密码
        /// </summary>
        /// <param name="fpasw"></param>
        /// <returns></returns>
        private (bool, string) ValidateFPasw(string fpasw)
        {
            if (fpasw.IsNullOrEmpty()) return (false, "资金密码不得为空");
            if (fpasw.Length != 6) return (false, "请输入六位的资金密码");
            if (!XValidator.IsInteger(fpasw)) return (false, "资金密码不符合规则(只允许六位数字)");
            return (true, string.Empty);
        }


        /// <summary>
        /// 获取代理单位时间内注册的人数
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<int> GetRegisterCount(int agentId, DateTime startTime, DateTime endTime)
        {
            return await _repository.GetRegisterCountAsync(agentId, startTime, endTime);
        }

        /// <summary>
        /// 获取代理单位时间内注册的人数，包含代理下级
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public async Task<int> GetNewRegisterCountAsync(int merchantId, int agentId, DateTime startTime, DateTime endTime)
        {
            return await _repository.GetNewRegisterCountAsync(merchantId, agentId, startTime, endTime);
        }

        /// <summary>
        /// [用户中心]更新用户绑定信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<(bool, string)> UpdateUserInfo(UserBindInfo info)
        {
            if (!info.Email.IsNullOrEmpty())
                if (XValidator.IsEmail(info.Email)) return (false, "请输入符合格式的邮箱地址,该地址后续作为找回账户必选条件");
            if (!info.MobileNo.IsNullOrEmpty())
                if (XValidator.IsMobilePhoneNumber(info.MobileNo.ToString())) return (false, "请输入符合格式的手机号码,该号码后续作为找回账户必选条件");
            if (!info.FPasw.IsNullOrEmpty())
            {
                if (info.FPasw.Length != 6) return (false, "请输入六位数字密码");
                if (!XValidator.IsInteger(info.FPasw)) return (false, "请输入六位数字密码");
            }


            //if (info.Email.IsNullOrEmpty()) return (false, "请输入邮箱地址,该地址后续作为找回账户必选条件");
            //if (info.MobileNo.IsNullOrEmpty()) return (false, "请输入手机地址,该号码后续作为找回账户必选条件");


            if (info.MemberId == 0 || info.MerchantId == 0) return (false, "会员异常,请联系客服");

            var usr = await _repository.GetFromCacheAsync(info.MerchantId, info.MemberId);
            if (usr == null) return (false, "用户不存在");
            usr.Mobile = info.MobileNo;
            usr.FPasw = info.FPasw;
            usr.BirthDate = info.BirthDate;
            // TODO  用户表增加冗余字段，增加birdth,email,gender字段

            var rt = await _repository.UpdateWithCacheAsync(usr);

            if (rt > 0) return (true, "更新成功");
            return (false, "更新失败");
        }


        public async Task<(bool, string)> UpdateUserInfoAsync(int memberId, Dictionary<string, string> info)
        {
            if (memberId == 0) return (false, "无效的用户");
            var usr = await _repository.GetFromCacheAsync(memberId);
            foreach (var kv in info)
            {
                switch (kv.Key)
                {
                    case "IdName":
                        if (kv.Value.IsNullOrEmpty()) return (false, "请输入真实姓名");
                        usr.IdName = kv.Value;
                        break;
                    case "Gender":
                        usr.Gender = kv.Value.ToEnum<Gender>().Value;
                        break;
                    case "BirthDate":

                        usr.BirthDate = kv.Value.To<DateTime>();
                        break;
                    case "MobileNo":
                        if (kv.Value.IsNullOrEmpty()) return (false, "请输入手机号码");
                        //if (XValidator.IsMobilePhoneNumber(kv.Value.ToString())) return (false, "请输入符合格式的手机号码,该号码后续作为找回账户必选条件");
                        usr.Mobile = kv.Value;
                        break;
                    case "FPasw":
                        if (kv.Value.IsNullOrEmpty()) return (false, "请输入取款密码");
                        if (kv.Value.Length != 6) return (false, "请输入六位数字密码");
                        if (!XValidator.IsInteger(kv.Value)) return (false, "请输入六位数字密码");
                        usr.FPasw = kv.Value;
                        break;
                    case "Email":
                        if (kv.Value.IsNullOrEmpty()) return (false, "请输入邮箱");
                        //if (XValidator.IsEmail(kv.Value)) return (false, "请输入符合格式的邮箱地址,该地址后续作为找回账户必选条件");
                        usr.Email = kv.Value;
                        break;
                }
            }
            var rt = await _repository.UpdateWithCacheAsync(usr);

            if (rt > 0) return (true, "更新成功");
            return (false, "更新失败");
        }


        /// <summary>
        /// 判定用户是否是代理
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        public async Task<bool> IsAgentAsync(int merchantId, int memberId)
        {
            // TODO 缓存
            var user = await this.GetUserById(merchantId, memberId);
            return user.Item3.Type == UserType.Agent;
        }

        public async Task<(bool, string)> ResetUserInfoAsync(int memberId, string key)
        {

            if (memberId == 0) return (false, "无效的用户");
            var usr = await _repository.GetFromCacheAsync(memberId);

            switch (key)
            {
                case "IdName":
                    usr.IdName = "";
                    break;
                case "Gender":
                    usr.Gender = Gender.None;
                    break;
                case "BirthDate":
                    usr.BirthDate = DefaultString.DefaultDateTime;
                    break;
                case "MobileNo":
                    usr.Mobile = "";
                    break;
                case "FPasw":
                    usr.FPasw = "";
                    break;
                case "Email":
                    usr.Email = "";
                    break;
            }
            var rt = await _repository.UpdateWithCacheAsync(usr);

            if (rt > 0) return (true, "更新成功");
            return (false, "更新失败");
        }

        /// <summary>
        /// 获取代理的下级用户数据
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<Users>, int)> GetSubsetUsers(AgentSubsetQuery q)
        {
            if (q.MerchantId == 0 || q.MemberId == 0) return (new List<Users>(), default(int));
            // 获取用户下的所有用户Id,根据Id查询出所有的用户
            var userIds = (await _userHierarchyService.GetMemberSubHierarchy(q.MerchantId, q.MemberId))
                .Where(t => t != q.MemberId)
                .OrderByDescending(g => g)
                .Skip(q.Limit * (q.Page - 1))
                .Take(q.Limit);


            List<Users> userList = new List<Users>();
            foreach (var id in userIds)
            {
                if (id == q.MemberId) continue;
                userList.Add(await _repository.GetFromCacheAsync(q.MerchantId, id));
            }

            return (userList, userList.Count - 1);
        }


        //public async Task<int> GetSubUsersCountAsync(int merchantId, int agentId,DateTime startTime,DateTime endTime)
        //{


        //}


        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<(bool, string)> UpdateUserTypeStatus(int merchantId, int userId, int parentAgentId, Users.UserType type, AgentSetting agentSetting)
        {
            if (merchantId == 0 || userId == 0) return (false, "数据异常");
            var usr = await _repository.GetFromCacheAsync(userId);
            if (usr == null) return (false, "用户不存在");
            if (usr.MerchantId != merchantId) return (false, "用户不存在");

            usr.Type = type;
            if (type == Users.UserType.User) // 状态更改为用户的情况
            {
                if (parentAgentId == 0) return (false, "请选择该用户上级");
                var agent = await _repository.GetFromCacheAsync(parentAgentId);
                if (agent.Type != Users.UserType.Agent) return (false, "请选择该用户上级");
                usr.AgentId = parentAgentId;
                usr.AgentSetting = "";
            }
            else if (type == UserType.None)
            { }
            else //type == UserType.Agent [初始化创建，代理数据更新]
            {
                usr.AgentId = parentAgentId;
                if (agentSetting.Code.IsNullOrEmpty()) agentSetting.Code = RandomHelper.GetLowerCase(6);
                usr.AgentSetting = agentSetting.ToJson();
            }

            var rt = await _repository.UpdateWithCacheAsync(usr);
            if (rt > 0)
            {
                // 设置上级后 需要更新级别表
                await _userHierarchyService.SetMemberHierarchy(merchantId, userId, parentAgentId);
                return (true, "保存成功");
            }
            return (false, "保存失败");
        }
        /// <summary>
        /// 代理规则配置更新
        /// </summary>
        /// <param name="merchantId">商户ID</param>
        /// <param name="userId">用户ID等于零是新增</param>
        /// <param name="userAgentSetModel"></param>
        /// <returns></returns>
        public async Task<(bool, string)> UpdateAgentRules(UserAgentSetModel userAgentSetModel)
        {
            if (userAgentSetModel.AccountName.IsNullOrEmpty()) return (false, "代理用户名不能为空");
            // 判断更新代理ID是否是当前站点的
            var existUser = await ExistUser(userAgentSetModel.MerchantId, userAgentSetModel.Id);
            if (!existUser) throw new Exception($"所更新的该代理({userAgentSetModel.Id},{userAgentSetModel.AccountName})不属于当前站点");

            return await (UpdateUserTypeStatus(userAgentSetModel.MerchantId, userAgentSetModel.Id, userAgentSetModel.ParentAgentId, UserType.Agent, new AgentSetting() { AgentRebateId = userAgentSetModel.AgentRebateId, CommissionId = userAgentSetModel.commissionId, ContractId = userAgentSetModel.ContractId, IsDefault = false, Code = RandomHelper.GetLowerCase(6), Url = userAgentSetModel.Url, Template = userAgentSetModel.Template }));
        }


        /// <summary>
        /// 获取商户代理字典
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<(bool, string)> GetAllAgentsDicJsonAsync(int merchantId)
        {
            var dic = await GetAllAgentsDicAsync(merchantId);
            return (true, dic.ToJson());
        }

        public async Task<Dictionary<int, string>> GetAllAgentsDicAsync(int merchantId)
        {
            if (merchantId == 0) return new Dictionary<int, string>();
            IEnumerable<Users> rt = await _repository.GetAgentsAsync(merchantId);
            return rt.ToDictionary(t => t.Id, t => t.AccountName);
        }


        /// <summary>
        /// 获取代理分页
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<Users>, int)> GetAgentPageListAsync(UsersListQuery q)
        {
            var parms = new DynamicParameters();
            string conditions = $"WHERE 1=1 AND MerchantId={q.MerchantId} ";
            if (q.Id != 0)
                conditions += $" AND Id={q.Id} ";
            if (q.GroupId != 0)
                conditions += $" AND GroupId={q.GroupId} ";
            if (!string.IsNullOrEmpty(q.AccountName))
            {
                conditions += $" AND AccountName like @AccountName ";
                parms.Add("AccountName", $"%{q.AccountName}%");
            }

            // 获取代理内容
            conditions += $" AND Type = {UserType.Agent.GetEnumValue()}";

            var list = await _repository.GetListPagedAsync(q.Page, q.Limit, conditions, "Id desc", parms);
            return (list, _repository.RecordCount(conditions, parms));
        }

        /// <summary>
        /// 是否已经存在代理链接
        /// </summary>
        /// <param name="url"></param>
        /// <returns>是否存在链接,错误消息,商户Id</returns>
        public async Task<(bool, string, int)> ExistAgentUrlAsync(string url)
        {
            var llDic = await _repository.GetAgentsUrlsAsync();
            foreach (var ll in llDic)
            {
                List<AgentSetting> l = JsonHelper.JSONToObject<List<AgentSetting>>($"[{ll.Value}]");
                if (l.Where(t => t.Url == url).Count() != 0) return (true, "", ll.Key);
            }
            return (false, "未授权的地址", 0);
            //string json = $"[{listStr.Aggregate((x, y) => x + "," + y)}]";
            //List<AgentSetting> l = JsonHelper.JSONToObject<List<AgentSetting>>(json);
            //bool r = l.Where(t => t.Url == url).FirstOrDefault() != null;
            //return (r, r ? "OK" : "未授权的地址");
        }

        /// <summary>
        /// 获取所有代理的连接地址,商户Id 键值对
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, int>> GetAgentUrlMerchantIdDicsAsync()
        {
            var llDic = await _repository.GetAgentsUrlsAsync();
            Dictionary<string, int> urlMerchantIdDics = new Dictionary<string, int>();
            foreach (var ll in llDic)
            {
                AgentSetting l = JsonHelper.JSONToObject<AgentSetting>($"[{ll.Value}]");
                if (!l.Url.IsNullOrEmpty())
                    urlMerchantIdDics.Add(l.Url, ll.Key);
            }
            return urlMerchantIdDics;
        }



        /// <summary>
        /// 根据推广码获取代理Ids
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<int> GetAgentIdByCodeAsync(int merchantId, string code)
        {
            // TODO 对代理做缓存 code,Id
            var dic = await _repository.GetAgentSettingDicAsync(merchantId);
            foreach (var dc in dic)
                if (dc.Value.JsonStringToEntity<AgentSetting>().Code == code) return dc.Key;
            return default(int);
        }

        public async Task<int> GetAgentIdByUrlAsync(int merchantId, string url)
        {
            // TODO 对代理做缓存 url,Id
            var dic = await _repository.GetAgentSettingDicAsync(merchantId);
            foreach (var dc in dic)
                if (dc.Value.JsonStringToEntity<AgentSetting>().Url == url) return dc.Key;
            return default(int);
        }


        /// <summary>
        /// 获取站点所有代理商户
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<(bool, string, dynamic)> GetAllAgentsAsync(int merchantId)
        {
            if (merchantId == 0) return (false, "商户错误", null);

            var rt = await _repository.GetAgentsAsync(merchantId);

            return (true, "", rt);
        }

        /// <summary>
        /// 登录验证  
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<(bool, string, int)> SignInValidateAsync(UserSignInModel req)
        {
            if (string.IsNullOrEmpty(req.Name) || string.IsNullOrEmpty(req.Password)) return (false, "用户名/密码格式错误", 0);
            if (!XValidator.IsStringLengthOnlyStartWithEnglishCharacter(req.Name, 6, 18)) return (false, "用户名/密码格式错误", 0);
            if (!XValidator.IsStringLengthOnlyStartWithEnglishCharacter(req.Password, 6, 18)) return (false, "用户名/密码格式错误", 0);

            req.Password = AESEncryptHelper.Encode(req.Password.Trim(), DefaultString.AesEncryptKeys);

            var rt = await _repository.ExistMemberAsync(req.Name, req.Password, req.MerchantId);

            if (rt > 0) return (true, "登录成功", rt);

            return (false, "登录失败,用户名/密码错误", 0);
        }

        public async Task<(bool, string, UserLoginRt)> SignInAsync(UserSignInModel req)
        {
            var r = await SignInValidateAsync(req);
            if (!r.Item1) return (r.Item1, r.Item2, null);

            var ent = await _repository.GetFromCacheAsync(r.Item3);
            var session = await _usersSessionService.SaveSessionAsync(r.Item3);
            if (!session.Item1) return (false, "创建Session失败", null);

            var rt = new UserLoginRt()
            {
                UserId = r.Item3,
                UserName = req.Name,
                Session = session.Item2.Session
            };

            return (true, "", rt);
        }

        public async Task<Users> GetUserBySession(string session)
        {
            var usr_rt = await _usersSessionService.GetBySession(session);

            if (!usr_rt.Item1)
                return null;

            return await _repository.GetFromCacheAsync(usr_rt.Item2);
        }

        /// <summary>
        /// 获取用户组里的所有用户Id
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        public async Task<List<int>> GetIds(int merchantId, List<int> groupIds)
        {
            return await _repository.GetIdsAsync(merchantId, groupIds);
        }

        /// <summary>
        /// [VIP生日礼金数据] 指定日期 用户/用户组 键值对
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public async Task<Dictionary<int, int>> GetBirthDictionaryAsync(int merchantId, DateTime dt)
        {
            return await _repository.GetBirthDictionaryAsync(merchantId, dt);
        }


        /// <summary>
        /// 设置代理为默认代理
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<(bool, string)> SetDefaultAgentAsync(int merchantId, int userId, bool isDefault)
        {
            if (merchantId == 0 || userId == 0) return (false, "参数错误");
            var user = await GetUserById(merchantId, userId);
            if (!user.sucess) return (user.sucess, user.errorMsg);
            var user_entity = user.entity;
            var user_entity_agentSetting = user_entity.AgentSettingEntity;
            user_entity_agentSetting.IsDefault = isDefault;
            user_entity.AgentSetting = user_entity_agentSetting.ToJson();
            var rt = await _repository.UpdateWithCacheAsync(user_entity);
            return rt.ToResult("修改成功", "修改失败");
        }

    }
}