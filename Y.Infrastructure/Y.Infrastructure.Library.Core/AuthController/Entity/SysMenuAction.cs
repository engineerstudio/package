using System;
using System.ComponentModel.DataAnnotations;

namespace Y.Infrastructure.Library.Core.AuthController.Entity
{
    public partial class SysMenuAction
    {
        [Key] public Int32 Id { get; set; }

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
        [MaxLength(12)]
        public String Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required]
        [MaxLength(16)]
        public String Code { get; set; }

        /// <summary>
        /// 请求的链接地址
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String Url { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        [MaxLength(5)]
        public ActionsType ActionType { get; set; }

        /// <summary>
        /// 当前请求的数据类型 // 页面按钮?按钮? 列表
        /// </summary>
        [Required]
        [MaxLength(5)]
        public ActionsDataType DataType { get; set; }
    }
}