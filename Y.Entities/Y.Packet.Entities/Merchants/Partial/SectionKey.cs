using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Merchants
{
    /// <summary>
    /// Aaron
    /// 2021-02-26 13:53:38
    /// 
    /// </summary>
    partial class SectionKey
    {
        public enum KeyType
        {
            [Description("电脑端")]
            PC = 1,
            [Description("移动端")]
            Mobile = 2
        }

        /// <summary>
        /// 子类别的弹出形式
        /// </summary>
        public enum KeyDetailType
        {
            [Description("多个子类别")]
            Multi = 1,
            [Description("一个子类别")]
            Single = 2,
            [Description("内容形式")]
            Content = 3,
            [Description("Banner形式")]
            Banner = 4
        }
    }
}
