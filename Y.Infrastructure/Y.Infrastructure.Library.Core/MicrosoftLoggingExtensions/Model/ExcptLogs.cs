using System;
using System.ComponentModel.DataAnnotations;

namespace Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions.Model
{
    internal class ExcptLogs
    {
        [Key] public int Id { get; set; }


        /// <summary>
        /// 错误机器
        /// </summary>
        [Required]
        public string MachineName { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        [Required]
        public DateTime Logged { get; set; }

        /// <summary>
        /// 错误日志级别
        /// </summary>
        [Required]
        public string Level { get; set; }

        /// <summary>
        /// 详细错误信息
        /// </summary>
        [Required]
        public string Message { get; set; }

        /// <summary>
        /// 错误类型
        /// </summary>
        [Required]
        public string Logger { get; set; }

        /// <summary>
        /// 呼叫网站
        /// </summary>
        [Required]
        public string CallSite { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        [Required]
        public string Exception { get; set; }

        /// <summary>
        /// 追踪日志
        /// </summary>
        [Required]
        public string StackTrace { get; set; }
    }
}