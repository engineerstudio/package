////////////////////////////////////////////////////////////////////
//                          _ooOoo_                               //
//                         o8888888o                              //
//                         88" . "88                              //
//                         (| ^_^ |)                              //
//                         O\  =  /O                              //
//                      ____/`---'\____                           //
//                    .'  \\|     |//  `.                         //
//                   /  \\|||  :  |||//  \                        //
//                  /  _||||| -:- |||||-  \                       //
//                  |   | \\\  -  /// |   |                       //
//                  | \_|  ''\---/''  |   |                       //
//                  \  .-\__  `-`  ___/-. /                       //
//                ___`. .'  /--.--\  `. . ___                     //
//              ."" '<  `.___\_<|>_/___.'  >'"".                  //
//            | | :  `- \`.;`\ _ /`;.`/ - ` : | |                 //
//            \  \ `-.   \_ __\ /__ _/   .-` /  /                 //
//      ========`-.____`-.___\_____/___.-`____.-'========         //
//                           `=---='                              //
//      ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^        //
//                   佛祖保佑       永不宕机     永无BUG          //
////////////////////////////////////////////////////////////////////

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：用户基本信息表                                                    
*│　作    者：Aaron                                              
*│　版    本：1.0   模板代码自动生成                                              
*│　创建时间：2020-08-30 15:00:55                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Users.IRepository                                   
*│　接口名称： IUsersRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Repository;
using Y.Packet.Entities.Members;

namespace Y.Packet.Repositories.IMembers
{
    public interface IUsersRepository : IBaseRepository<Users, Int32>
    {
        /// <summary>
        /// 逻辑删除返回影响的行数
        /// </summary>
        /// <param name="ids">需要删除的主键数组</param>
        /// <returns>影响的行数</returns>
        Int32 DeleteLogical(Int32[] ids);
        /// <summary>
        /// 逻辑删除返回影响的行数（异步操作）
        /// </summary>
        /// <param name="ids">需要删除的主键数组</param>
        /// <returns>影响的行数</returns>
        Task<Int32> DeleteLogicalAsync(Int32[] ids);

        /// <summary>
        /// 查询站点会员是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="psw"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<int> ExistMemberAsync(string name, string psw, int merchantId);

        /// <summary>
        /// 判断站点会员名字是否存在
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<int> ExistMemberNameAsync(int merchantId, string name);

        /// <summary>
        /// 获取站点所有代理
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<IEnumerable<Users>> GetAgentsAsync(int merchantId);

        /// <summary>
        /// 根据会员用户名获取会员信息
        /// </summary>
        /// <param name="accountName"></param>
        /// <returns></returns>
        Task<Users> GetByAccountNameAsync(int merchantId, string accountName);


        /// <summary>
        /// 获取商户/代理的字典
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<int, int>> GetAgentIdAndMerchantIdDicAsync();

        Task<Dictionary<int, int>> GetMerchantsAndAgentsDicAsync(int[] merchantIds);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Users> GetAsync(int merchantId, int userId);

        /// <summary>
        /// 获取用户组Id信息
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        Task<List<int>> GetIdsAsync(int merchantId, List<int> groupIds);

        /// <summary>
        /// [VIP生日礼金数据] 指定日期 用户/用户组 键值对
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        Task<Dictionary<int, int>> GetBirthDictionaryAsync(int merchantId, DateTime dt);


        /// <summary>
        /// 获取代理链接 商户/配置字符串合集
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<int, string>> GetAgentsUrlsAsync();

        /// <summary>
        /// 获取商户代理  用户Id/AgentSet字符串
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<Dictionary<int, string>> GetAgentSettingDicAsync(int merchantId);

        /// <summary>
        /// 获取指定时间内新注册用户的数量
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="startTime"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        Task<int> GetRegisterCountAsync(int agentId, DateTime startTime, DateTime dateTime);
        Task<int> GetNewRegisterCountAsync(int merchantId, int agentId, DateTime startTime, DateTime dateTime);
        Task<Users> GetFromCacheAsync(int merchantId, int userId);
        Task<int?> InsertWithCacheAsync(Users users);
        Task<int> UpdateWithCacheAsync(Users user);
        Task<Users> GetFromCacheAsync(int memberId);
        Task MigrateSqlDbToRedisDbAsync();
    }
}