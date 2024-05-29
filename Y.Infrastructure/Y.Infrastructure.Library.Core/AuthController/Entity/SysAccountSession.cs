using System;
using System.ComponentModel.DataAnnotations;

namespace Y.Infrastructure.Library.Core.AuthController.Entity
{
    /// <summary>
    /// Aaron
    /// 2020-08-24 02:23:05
    /// 
    /// </summary>
    public partial class SysAccountSession
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        /// 账户Id
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 AccountId { get; set; }

        /// <summary>
        /// 登录Session
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String Session { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime UpdateTime { get; set; }
    }
}