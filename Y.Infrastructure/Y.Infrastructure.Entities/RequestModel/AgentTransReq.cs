using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Infrastructure.Entities.RequestModel
{
    public class AgentTransReq : PageModel
    {
        public string Name { get; set; }
        /// <summary>
        /// 客户的Id
        /// </summary>
        public int ToId { get; set; }
        /// <summary>
        /// 转账金额
        /// </summary>
        public decimal Point { get; set; }
    }
}
