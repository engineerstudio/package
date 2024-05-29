using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Packet.Entities.Promotions.ViewModels
{
    public class PromotionsListPageQuery : PageModel
    {
        public int MerchantId { get; set; }
        public string AName { get; set; }
        public string AType { get; set; }
        /// <summary>
        /// 只显示未删除的
        /// </summary>
        public bool ShowDeletedPro { get; set; }


    }
}
