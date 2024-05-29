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
            [Description("���Զ�")]
            PC = 1,
            [Description("�ƶ���")]
            Mobile = 2
        }

        /// <summary>
        /// �����ĵ�����ʽ
        /// </summary>
        public enum KeyDetailType
        {
            [Description("��������")]
            Multi = 1,
            [Description("һ�������")]
            Single = 2,
            [Description("������ʽ")]
            Content = 3,
            [Description("Banner��ʽ")]
            Banner = 4
        }
    }
}
