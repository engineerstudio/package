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
//                   佛祖保佑       永不宕机     永无BUG            //
////////////////////////////////////////////////////////////////////

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：Aaron                                            
*│　版    本：1.0    模板代码自动生成                                                
*│　创建时间：2021-05-09 10:31:29                             
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Y.Users.Service                                  
*│　类    名： NoticeInfoService                                    
*└──────────────────────────────────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Packet.Entities.Members;
using Y.Packet.Entities.Members.ViewModels;
using Y.Packet.Repositories.IMembers;
using Y.Packet.Services.IMembers;

namespace Y.Packet.Services.Members
{
    public class NoticeInfoService : INoticeInfoService
    {
        private readonly INoticeInfoRepository _repository;

        public NoticeInfoService(INoticeInfoRepository repository)
        {
            _repository = repository;
        }



        public async Task<(bool, int, int)> Save(int merchantId, List<int> members, int noticeId, string title, string description)
        {
            NoticeInfo info;
            int sucessCount = 0, failedCount = 0;
            foreach (int memberId in members)
            {
                info = new NoticeInfo();
                info.MerchantId = merchantId;
                info.MemberId = memberId;
                info.Title = title;
                info.Description = description;
                info.CreateTime = DateTime.UtcNow.AddHours(8);
                info.IsRead = false;
                info.NoticeAreaId = noticeId;
                var rt = await _repository.InsertAsync(info);
                if (rt != null && rt.Value > 0) sucessCount++;
                else failedCount++;
            }
            return (true, sucessCount, failedCount);

        }


        public async Task<bool> DeleteAsync(int merchantId, int noticeId)
        {
            var rt = (await _repository.DeleteAsync(merchantId, noticeId));
            return rt > 0;
        }


        public async Task<(IEnumerable<NoticeInfo>, int)> GetPageList(NoticeInfoListQuery q)
        {
            if (q.MerchantId == 0) return (null, 0);

            string conditions = $" WHERE 1=1 AND MerchantId={q.MerchantId} AND MemberId={q.MemberId}  ";

            return (_repository.GetListPaged(q.Page, q.Limit, conditions, "Id desc", null), await _repository.RecordCountAsync(conditions));
        }




    }
}