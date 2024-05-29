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
*│　描    述：                                                    
*│　作    者：Aaron                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-10-18 22:53:28                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Users.Service                                  
*│　类    名： UsersSessionService                                    
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
    public class UsersSessionService : IUsersSessionService
    {
        private readonly IUsersSessionRepository _repository;

        public UsersSessionService(IUsersSessionRepository repository)
        {
            _repository = repository;
        }


        public async Task<(bool, UsersSession)> SaveSessionAsync(int userId)
        {
            var sessionId = await _repository.GetSessionIdAsync(userId);
            if (sessionId > 0) await _repository.DeleteWithCacheAsync(sessionId);

            var m = new UsersSession()
            {
                UserId = userId,
                Session = Guid.NewGuid().ToString("N"),
                UpdateTime = DateTime.UtcNow.AddHours(8)
            };
            var rt = await _repository.InsertWithCacheAsync(m);

            if (rt != null && rt.HasValue && rt.Value > 0) return (true, m);
            return (false, null);
        }


        public async Task<(bool, string)> DeleteAsync(int memberId)
        {
            var sessionId = await _repository.GetSessionIdAsync(memberId);

            if (sessionId > 0)
                await _repository.DeleteWithCacheAsync(sessionId);

            return (true, "已清理");
        }

        public async Task<(bool, int)> GetBySession(string session)
        {
            if (string.IsNullOrEmpty(session)) return (false, 0); // 702de36943294c58be4ee1fa0117deb1

            var userId = await _repository.GetUserIdAsync(session);

            if (userId > 0) return (true, userId);

            return (false, 0);
        }
    }
}