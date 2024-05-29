using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Packet.Entities.Members;
using Y.Packet.Entities.Members.ViewModels;
using Y.Packet.Entities.Members.ViewModels.AgentCenter;
using Y.Packet.Entities.Members.ViewModels.UserCenter;
using static Y.Packet.Entities.Members.Users;

namespace Y.Packet.Services.IMembers
{
    public interface IUsersService
    {

        Task<(bool sucess, string errorMsg, Users entity)> GetUserById(int merchantId, int userId);
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        Task<(IEnumerable<Users>, int)> GetPageListAsync(UsersListQuery q);
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        Task<(bool, string, int)> InsertAsync(UserRegisterModel users);

        /// <summary>
        /// 【站点初始化】创建默认会员
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Task<(bool, string, int)> CreateMerchantAdminDefaultMemberAsync(int merchantId, int groupId);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="rq"></param>
        /// <returns></returns>
        Task<(bool, string)> UpdateMemberInfoAsync(UserModifyModel rq);

        /// <summary>
        /// 获取账户名称
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        Task<string> GetAccountNameByMemberIdAsync(int memberId);

        /// <summary>
        /// 根据用户名获取会员ID
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        Task<int> GetMemberIdByAccountNameAsync(int merchantId, string accountName);

        /// <summary>
        /// 更新登陆密码
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="oldPsw"></param>
        /// <param name="newPsw"></param>
        /// <returns></returns>
        Task<(bool, string)> UpdatePasswordAsync(int memberId, string oldPsw, string newPsw);

        /// <summary>
        /// 更新资金密码
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="psw"></param>
        /// <returns></returns>
        Task<(bool, string)> UpdateFundsPasswordAsync(int memberId, string psw, string fpsw);

        /// <summary>
        /// 获取时间段内代理下属会员注册数量
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="startTime"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        Task<int> GetRegisterCount(int agentId, DateTime startTime, DateTime dateTime);
        Task<int> GetNewRegisterCountAsync(int merchantId, int agentId, DateTime startTime, DateTime endTime);
        /// <summary>
        /// [用户中心]更新用户绑定信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task<(bool, string)> UpdateUserInfo(UserBindInfo info);

        /// <summary>
        /// [用户中心]更新用户信息
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        Task<(bool, string)> UpdateUserInfoAsync(int memberId, Dictionary<string, string> info);
        /// <summary>
        /// 商户后台重置用户信息为默认值
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<(bool, string)> ResetUserInfoAsync(int memberId, string key);


        #region 代理操作

        Task<bool> IsAgentAsync(int merchantId, int memberId);


        /// <summary>
        /// 获取代理的下级用户数据
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<(IEnumerable<Users>, int)> GetSubsetUsers(AgentSubsetQuery q);


        /// <summary>
        /// 更新用户代理
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <param name="rebateId"></param>
        /// <returns></returns>
        Task<(bool, string)> UpdateUserTypeStatus(int merchantId, int userId, int parentAgentId, UserType type, AgentSetting agentSetting);
        /// <summary>
        /// 更新代理的详细配置
        /// </summary>
        /// <param name="userAgentSetModel"></param>
        /// <returns></returns>
        Task<(bool, string)> UpdateAgentRules(UserAgentSetModel userAgentSetModel);
        /// <summary>
        /// 根据商户ID获取下属所有代理字典
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<(bool, string)> GetAllAgentsDicJsonAsync(int merchantId);
        Task<Dictionary<int, string>> GetAllAgentsDicAsync(int merchantId);

        /// <summary>
        /// 根据商户ID获取下属所有代理
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<(bool, string, dynamic)> GetAllAgentsAsync(int merchantId);

        /// <summary>
        /// 获取代理分页
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        Task<(IEnumerable<Users>, int)> GetAgentPageListAsync(UsersListQuery q);

        /// <summary>
        /// 获取代理Url地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<(bool, string, int)> ExistAgentUrlAsync(string url);

        /// <summary>
        /// 获取代理连接,商户Id的键值对
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, int>> GetAgentUrlMerchantIdDicsAsync();

        #endregion

        #region 登陆

        /// <summary>
        /// 验证用户是否登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<(bool, string, int)> SignInValidateAsync(UserSignInModel req);


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<(bool, string, UserLoginRt)> SignInAsync(UserSignInModel req);


        /// <summary>
        /// 根据登录Session获取用户
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        Task<Users> GetUserBySession(string session);

        #endregion


        /// <summary>
        /// 根据会员组获取所有会员的Id
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        Task<List<int>> GetIds(int merchantId, List<int> groupIds);

        /// <summary>
        /// [VIP生日礼金数据] 指定日期 用户/用户组 键值对
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        Task<Dictionary<int, int>> GetBirthDictionaryAsync(int merchantId, DateTime dt);

        /// <summary>
        /// 设置代理为默认代理
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<(bool,string)> SetDefaultAgentAsync(int merchantId, int userId, bool isDefault);

    }
}