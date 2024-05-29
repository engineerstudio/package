using System;
using System.ComponentModel.DataAnnotations;

namespace Y.Infrastructure.Library.Core.AuthController.Entity
{
    /// <summary>
    /// Aaron
    /// 2020-08-24 02:23:05
    /// 
    /// </summary>
    public partial class SysMenu
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String DisplayName { get; set; }

        /// <summary>
        /// 图标地址
        /// </summary>
        [Required]
        [MaxLength(128)]
        public String IconClass { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        [Required]
        [MaxLength(128)]
        public String LinkUrl { get; set; }

        /// <summary>
        /// 排序数字
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 Sort { get; set; }

        /// <summary>
        /// 操作权限（按钮权限时使用）
        /// </summary>
        [Required]
        [MaxLength(2048)]
        public String Permission { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsDisplay { get; set; }

        /// <summary>
        /// 是否系统默认
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsSystem { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsDelete { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 ModifyId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime ModifyTime { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 CreatedId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime CreatedTime { get; set; }


        /// <summary>
        /// 系统标识字符串
        /// </summary>
        [Required]
        [MaxLength(18)]
        public String SysStr { get; set; }
    }
}