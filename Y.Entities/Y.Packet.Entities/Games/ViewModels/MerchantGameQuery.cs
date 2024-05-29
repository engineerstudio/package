using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Games.ViewModels
{
    public class MerchantGameQuery : PageModel
    {
        /// <summary>
        /// 游戏类别字符串
        /// </summary>
        public string GameCategoryStr { get; set; }
        /// <summary>
        /// 游戏字符串
        /// </summary>
        public string GameTypeStr { get; set; }
        /// <summary>
        /// 源ID
        /// </summary>
        public string SourceId { get; set; }
        /// <summary>
        /// 游戏名称
        /// </summary>
        public string GameAccount { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string AccountName { get; set; }

        public int MerchantId { get; set; }
        public int MemberId { get; set; }
        /// <summary>
        /// 订单状态字符串
        /// </summary>
        public string OrderStatusStr { get; set; }

        /// <summary>
        /// 平台订单创建时间
        /// </summary>
        public DateTime? CreateStartTime { get; set; }
        public DateTime? CreateEndTime { get; set; }

        /// <summary>
        /// 游戏平台下单时间
        /// </summary>
        public DateTime? BetStartTime { get; set; }
        public DateTime? BetEndTime { get; set; }


    }
}
