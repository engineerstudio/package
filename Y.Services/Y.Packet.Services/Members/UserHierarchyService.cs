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
*│　描    述：用户层级关系                                                    
*│　作    者：Aaron                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-08-30 15:00:55                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Members                                  
*│　类    名： UserHierarchyService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Packet.Entities.Members;
using Y.Packet.Repositories.IMembers;
using Y.Packet.Services.IMembers;

namespace Y.Packet.Services.Members
{
    public class UserHierarchyService : IUserHierarchyService
    {
        private readonly IUserHierarchyRepository _repository;

        public UserHierarchyService(IUserHierarchyRepository repository)
        {
            _repository = repository;
        }



        /// <summary>
        /// 设置用户代理等级关系
        /// </summary>
        public async Task SetMemberHierarchy(int siteId, int userId, int agentId)
        {
            // 1. 获取到代理的所有上级
            //var list = await _repository.GetListAsync($" WHERE SiteId = {siteId} AND SubId = {agentId}");
            var list = await _repository.GetAgentMemberIdsFromCacheAsync(siteId, agentId);
            // 2. 每个上级插入一条数据，包含该代理
            UserHierarchy userHierarchy;
            foreach (var item in list)
            {
                if ((await _repository.ExistAsync(siteId, item.UserId, userId)) > 0) continue;
                userHierarchy = new UserHierarchy()
                {
                    SiteId = siteId,
                    UserId = item.UserId,
                    SubId = userId,
                    Level = item.Level + 1
                };
                await _repository.InsertWithCacheAsync(userHierarchy);
            }

            // 3. 新增一条自己的数据
            if ((await _repository.ExistAsync(siteId, userId, userId)) == 0)
            {
                userHierarchy = new UserHierarchy()
                {
                    SiteId = siteId,
                    UserId = userId,
                    SubId = userId,
                    Level = 0
                };
                await _repository.InsertWithCacheAsync(userHierarchy);
            }

        }

        public async Task<IEnumerable<int>> GetMemberSubHierarchy(int merchantId, int agentId)
        {
            return await _repository.GetSubMemberIdsFromCacheAsync(merchantId, agentId);
        }


    }
}