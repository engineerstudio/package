using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Packet.Entities.Members;
using Y.Packet.Entities.Members.ViewModels;


namespace Y.Packet.Services.IMembers
{
    public interface INoticeInfoService
    {
        /// <summary>
        /// 保存通知
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="members"></param>
        /// <param name="noticeId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <returns>成功, 成功数目, 失败数目</returns>
        Task<(bool, int, int)> Save(int merchantId, List<int> members, int noticeId, string title, string description);

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="noticeId"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int merchantId, int noticeId);


        /// <summary>
        /// 获取通知列表
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        Task<(IEnumerable<NoticeInfo>, int)> GetPageList(NoticeInfoListQuery q);
    }
}