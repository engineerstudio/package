using System;
using System.Collections.Generic;
using System.Text;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Infrastructure.Entities.RequestModel
{
    public class TransferReq
    {
        /// <summary>
        ///  In,Out
        /// </summary>
        public TransType TransType { get; set; }

        /// <summary>
        /// 游戏类型
        /// </summary>
        public GameType Type { get; set; }

        /// <summary>
        /// 转账金额
        /// </summary>
        public decimal Money { get; set; }
    }
}
