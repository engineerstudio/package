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
*│　描    述：用户基本信息表接口实现                                                    
*│　作    者：Aaron                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-08-30 15:00:55                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Users.Repository                                  
*│　类    名： UsersRepository                                      
*└──────────────────────────────────────────────────────────────┘
*/
using Y.Infrastructure.Library.Core.DbHelper;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Y.Packet.Repositories.IMembers;
using System.Collections.Generic;
using Y.Packet.Entities.Members;
using System.Linq;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Cache.DbCache.Members;
using Y.Infrastructure.Library.Core.CacheFactory.Entity;
using Y.Infrastructure.Library.Core.CacheFactory.Factory;
using static Y.Packet.Entities.Members.Users;


namespace Y.Packet.Repositories.Members
{
    public class UsersRepository : UsersDbCacheService, IUsersRepository
    {
        public UsersRepository(IOptionsMonitor<DbOption> options, IOptionsMonitor<YCacheConfiguration> cfg, IYCacheFactory factory) : base(cfg, factory)
        {
            _dbOption = options.Get("Ying.Users");
            if (_dbOption == null)
            {
                throw new ArgumentNullException(nameof(DbOption));
            }
            _dbConnection = ConnectionFactory.CreateConnection(_dbOption.DbType, _dbOption.ConnectionString);
        }

        public int DeleteLogical(int[] ids)
        {
            string sql = "update Users set IsDelete=1 where Id in @Ids";
            return _dbConnection.Execute(sql, new
            {
                Ids = ids
            });
        }

        public async Task<int> DeleteLogicalAsync(int[] ids)
        {
            string sql = "update Users set IsDelete=1 where Id in @Ids";
            return await _dbConnection.ExecuteAsync(sql, new
            {
                Ids = ids
            });
        }

        /// <summary>
        /// 查询会员是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="psw"></param>
        /// <param name="merchantId"></param>
        /// <returns>返回会员Id</returns>
        public async Task<int> ExistMemberAsync(string name, string psw, int merchantId)
        {
            string sql = $"SELECT Id FROM Users WHERE MerchantId = {merchantId} AND AccountName = @AccountName AND Pasw = '{psw}'";
            return await _dbConnection.ExecuteScalarAsync<int>(sql, new { AccountName = name });
        }

        /// <summary>
        /// 会员名字是否存在
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="name"></param>
        /// <returns>返回会员Id</returns>
        public async Task<int> ExistMemberNameAsync(int merchantId, string name)
        {
            string sql = $"SELECT ISNULL(Id,0) FROM Users WHERE MerchantId = {merchantId} AND AccountName = @AccountName ";
            return await _dbConnection.ExecuteScalarAsync<int>(sql, new { AccountName = name });
        }

        /// <summary>
        /// 获取商户所有代理
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Users>> GetAgentsAsync(int merchantId)
        {
            //string conditions = $"WHERE MerchantId={merchantId} AND Type={(int)Users.UserType.Agent}";
            //return await _dbConnection.GetListAsync<Users>(conditions);
            var ids = await _db.SetMembersAsync($"{MerchantAgentSet}{merchantId}");
            List<Users> users = new List<Users>();
            foreach (var id in ids)
            {
                var dStr = await _db.HashGetAsync(UsersEntityHash, id);
                users.Add(JsonHelper.JSONToObject<Users>(dStr));
            }
            return users;
        }

        public async Task<Users> GetByAccountNameAsync(int merchantId, string accountName)
        {
            string sql = $"SELECT * FROM {typeof(Users).Name} WHERE MerchantId={merchantId} AND AccountName = @AccountName";
            return await _dbConnection.QueryFirstOrDefaultAsync<Users>(sql, new { AccountName = accountName });
        }

        public async Task<Dictionary<int, int>> GetAgentIdAndMerchantIdDicAsync()
        {
            //string sql = "SELECT * FROM Users WHERE Type = 2";
            //return (await _dbConnection.QueryAsync<Users>(sql)).ToDictionary(t => t.Id, t => t.MerchantId);

            var strs = await _db.SetMembersAsync(MerchantAgentSet);
            Dictionary<int, int> dic = new Dictionary<int, int>();
            foreach (string str in strs)
            {
                var d = str.Split(",");
                dic.Add(d[1].To<int>(), d[0].To<int>());
            }
            return dic;
        }

        public async Task<Dictionary<int, int>> GetMerchantsAndAgentsDicAsync(int[] merchantIds)
        {
            //string sql = $"SELECT * FROM Users WHERE Type = 2 AND MerchantId in ({merchantIds.ToSplitByComma()})";
            //return (await _dbConnection.QueryAsync<Users>(sql)).ToDictionary(t => t.MerchantId, t => t.Id);

            var strs = await _db.SetMembersAsync(MerchantAgentSet);
            Dictionary<int, int> dic = new Dictionary<int, int>();
            foreach (string str in strs)
            {
                var d = str.Split(",");
                if (merchantIds.ToList().Contains(d[0].To<int>()))
                    dic.Add(d[0].To<int>(), d[1].To<int>());
            }
            return dic;
        }


        // TODO 放入缓存
        public async Task<Users> GetAsync(int merchantId, int userId)
        {
            string sql = $"SELECT * FROM Users WHERE MerchantId={merchantId} AND Id={userId}";
            return await _dbConnection.QueryFirstAsync<Users>(sql);
        }

        public async Task<List<int>> GetIdsAsync(int merchantId, List<int> groupIds)
        {
            var g = groupIds.ConvertAll<string>(x => x.ToString());
            string str = string.Join(",", g);
            string sql = $"SELECT Id FROM Users WHERE MerchantId={merchantId} AND GroupId in ({str})";
            return (await _dbConnection.QueryAsync<int>(sql)).ToList<int>();
        }

        public async Task<Dictionary<int, int>> GetBirthDictionaryAsync(int merchantId, DateTime dt)
        {
            string sql = $"SELECT Id,GroupId FROM Users WHERE MerchantId={merchantId} AND BirthDate!=@DefaultDate AND MONTH(BirthDate)={dt.Month} AND DAY(BirthDate)={dt.Day}";
            return (await _dbConnection.QueryAsync(sql, new { DefaultDate = DefaultString.DefaultDateTime })).ToDictionary(t => (int)t.Id, t => (int)t.GroupId);
        }

        // 获取代理链接配置字符串
        public async Task<Dictionary<int, string>> GetAgentsUrlsAsync()
        {
            //Dictionary<int, string> dic = new Dictionary<int, string>();
            //string sql = $"SELECT MerchantId,AgentSetting FROM Users WHERE Type={UserType.Agent.GetEnumValue()}";
            //var results = (await _dbConnection.QueryAsync(sql));
            //foreach (var row in results)
            //{
            //    var fields = row as IDictionary<string, object>;
            //    int merchantId = fields["MerchantId"].To<int>();
            //    string set = fields["AgentSetting"].To<string>();
            //    if (dic.Keys.Contains(merchantId))
            //    {
            //        var str = dic[merchantId];
            //        str += $",{set}";
            //        dic[merchantId] = str;
            //    }
            //    else
            //        dic.Add(merchantId, set);
            //}
            //return dic;

            Dictionary<int, string> dic = new Dictionary<int, string>();
            var strs = await _db.SetMembersAsync(MerchantAgentConfigSet);
            foreach (string st in strs)
            {
                var d = st.Split("(,)");
                int merchantId = d[0].To<int>();
                if (dic.Keys.Contains(merchantId))
                {
                    var str = dic[merchantId];
                    str += $",{d[2]}";
                    dic[merchantId] = str;
                }
                else
                    dic.Add(merchantId, d[2]);
            }
            return dic;
        }


        public async Task<Dictionary<int, string>> GetAgentSettingDicAsync(int merchantId)
        {
            //string sql = $"SELECT Id,AgentSetting FROM Users WHERE MerchantId={merchantId} AND Type={UserType.Agent.GetEnumValue()}";
            //return (await _dbConnection.QueryAsync(sql)).ToDictionary(t => (int)t.Id, t => (string)t.AgentSetting);

            Dictionary<int, string> dic = new Dictionary<int, string>();
            var strs = await _db.SetMembersAsync(MerchantAgentConfigSet);
            foreach (string st in strs)
            {
                var d = st.Split("(,)");
                int userId = d[1].To<int>();
                dic.TryAdd(userId, d[2]);
            }
            return dic;
        }


        public async Task<int> GetRegisterCountAsync(int agentId, DateTime startTime, DateTime dateTime)
        {
            string sql = $"SELECT COUNT(0) FROM Users WHERE AgentId = {agentId} AND CreateTime BETWEEN '{startTime}' AND '{dateTime}'";
            return await _dbConnection.ExecuteScalarAsync<int>(sql);
        }

        // 获取代理下所有的人员单位时间内注册数量
        public async Task<int> GetNewRegisterCountAsync(int siteId, int agentId, DateTime startTime, DateTime dateTime)
        {
            string sql = $@"WITH a AS (
                                SELECT SubId FROM [UserHierarchy] WHERE SiteId = {siteId} AND UserId = {agentId}
                                ),
                                b AS(
                                SELECT Id FROM Users WHERE  CreateTime > N'{startTime}' AND CreateTime <  N'{dateTime}'
                                )
                                SELECT COUNT(ID) No FROM a join b on a.SubId = b.Id";

            return await _dbConnection.ExecuteScalarAsync<int>(sql);
        }



        public async Task<Users> GetFromCacheAsync(int merchantId, int memberId)
        {
            var exitKey = await _db.HashExistsAsync(UsersEntityHash, memberId);
            if (!(exitKey)) return null;
            var dStr = await _db.HashGetAsync(UsersEntityHash, memberId);
            var d = JsonHelper.JSONToObject<Users>(dStr);
            if (d.MerchantId != merchantId) return null;
            return d;
        }
        public async Task<Users> GetFromCacheAsync(int memberId)
        {
            if (!(await _db.HashExistsAsync(UsersEntityHash, memberId))) return null;
            var d = await _db.HashGetAsync(UsersEntityHash, memberId);
            return JsonHelper.JSONToObject<Users>(d);
        }
        public async Task<int?> InsertWithCacheAsync(Users d)
        {
            var id = await _dbConnection.InsertAsync<Users>(d);
            if (id == null || id < 1) return id;
            d.Id = id.Value;
            await CacheAsync(d);
            return id;
        }
        private async Task CacheAsync(Users d)
        {
            await _db.HashSetAsync(UsersEntityHash, d.Id, d.ToJson());
            if (d.Type == UserType.Agent)
            {
                await _db.SetAddAsync($"{MerchantAgentSet}{d.MerchantId}", d.Id);
                await _db.SetAddAsync(MerchantAgentSet, $"{d.MerchantId},{d.Id}");
                await _db.SetAddAsync(MerchantAgentConfigSet, $"{d.MerchantId}(,){d.Id}(,){d.AgentSetting}");
            }
        }
        public async Task<int> UpdateWithCacheAsync(Users d)
        {
            var id = await _dbConnection.UpdateAsync<Users>(d);
            if (id < 1) return id;
            if (d.Type == UserType.Agent)
            {
                await _db.SetRemoveAsync($"{MerchantAgentSet}{d.MerchantId}", d.Id);
                await _db.SetRemoveAsync(MerchantAgentSet, $"{d.MerchantId}{d.Id}");
                await _db.SetRemoveAsync(MerchantAgentConfigSet, $"{d.MerchantId}(,){d.Id}(,){d.AgentSetting}");
            }
            await CacheAsync(d);
            return id;
        }



        public async Task MigrateSqlDbToRedisDbAsync()
        {
            var list = await _dbConnection.GetListAsync<Users>();
            foreach (var d in list)
                await CacheAsync(d);
        }


    }
}