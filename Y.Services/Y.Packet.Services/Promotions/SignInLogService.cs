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
*│　创建时间：2021-05-06 07:38:35                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Packet.Services.Promotions                                  
*│　类    名： SignInLogService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Packet.Entities.Promotions;
using Y.Packet.Repositories.IPromotions;
using Y.Packet.Services.IPromotions;

namespace Y.Packet.Services.Promotions
{
    public class SignInLogService : ISignInLogService
    {
        private readonly ISignInLogRepository _repository;

        public SignInLogService(ISignInLogRepository repository)
        {
            _repository = repository;
        }


        public async Task<(bool, string)> InsertAsync(int merchantId, int memberId)
        {
            if (memberId == 0 || merchantId == 0) return (false, "参数错误");

            var log = new SignInLog()
            {
                MerchantId = merchantId,
                MemberId = memberId,
                CreateTime = DateTime.UtcNow.AddHours(8)
            };

            var rt = await _repository.InsertAsync(log);

            if (rt != null && rt.Value != 0) return (true, "保存成功");

            return (false, "保存失败");
        }


        /// <summary>
        /// [签到活动]获取上周的用户签到的字典 用户ID/签到的数量
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        public async Task<Dictionary<int, int>> GetTasksDicAsync(int merchantId)
        {
            return await _repository.GetTasksLastWeekDicAsync(merchantId);
        }



    }
}