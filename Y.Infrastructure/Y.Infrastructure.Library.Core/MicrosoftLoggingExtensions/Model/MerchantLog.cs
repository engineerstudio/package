using System;
using System.ComponentModel.DataAnnotations;

namespace Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions.Model
{
    public class MerchantLog
    {
        [Key] public int Id { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        public int MerchantId { get; set; }

        /// <summary>
        /// 管理员Id
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public MerchantLogType Type { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 记录内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}