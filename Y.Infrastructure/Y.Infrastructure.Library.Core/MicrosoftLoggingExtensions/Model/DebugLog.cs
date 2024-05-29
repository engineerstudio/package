using System;
using System.ComponentModel.DataAnnotations;

namespace Y.Infrastructure.Library.Core.MicrosoftLoggingExtensions.Model
{
    internal class DebugLog
    {
        [Key] public int Id { get; set; }

        /// <summary>
        /// 记录人
        /// </summary>
        [Required]
        public string Logger { get; set; } = "";

        /// <summary>
        /// 记录信息
        /// </summary>
        [Required]
        public string Message { get; set; } = "";

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        public string Mark { get; set; } = "";

        /// <summary>
        /// 记录时间
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; } = DateTime.UtcNow.AddHours(8);
    }
}