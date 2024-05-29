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
*│　描    述：用户登录日志                                                    
*│　作    者：Aaron                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2020-08-30 15:00:55                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Members                                  
*│　类    名： UsersLoginLogService                                    
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
    public class UsersLoginLogService : IUsersLoginLogService
    {
        private readonly IUsersLoginLogRepository _repository;

        public UsersLoginLogService(IUsersLoginLogRepository repository)
        {
            _repository = repository;
        }


        public async Task<int?> InsertAsync(bool sucess, int merchantId, int userId, string name, string ip, string rawreq)
        {

            var entiry = new UsersLoginLog()
            {
                Sucess = sucess,
                MerchantId = merchantId,
                UserId = userId,
                IP = ip,
                IPCn = "",
                IsLastLogin = true,
                LoginTime = DateTime.UtcNow.AddHours(8),
                RawData = rawreq
            };
            return   await _repository.InsertAsync(entiry);
        }

   

    }
}