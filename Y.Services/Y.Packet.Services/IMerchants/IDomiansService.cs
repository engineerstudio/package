using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Y.Packet.Entities.Merchants;
using Y.Packet.Entities.Merchants.ViewModels;

namespace Y.Packet.Services.IMerchants
{
    public interface IDomiansService
    {
        Task<(bool, string)> InsertOrModifyAsync(DomiansInsertOrEditViewModel domain);


        (IEnumerable<Domains>, int) GetPageList(DomiansListQuery q);

        /// <summary>
        /// 是否存在地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<(bool, string, int)> ExistLikeAsync(string url);

        /// <summary>
        /// 获取回调字符串配置
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<string> GetCallbackUrlAsync(int merchantId);

        /// <summary>
        /// 删除域名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<(bool, string)> DelAsync(int id);
    }
}