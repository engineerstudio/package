using System;
using System.ComponentModel.DataAnnotations;

namespace Y.Infrastructure.Library.Core.AuthController.Entity
{
    /// <summary>
    /// Aaron
    /// 2020-08-24 02:23:05
    /// 
    /// </summary>
    public partial class SysRole
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        ///  商户ID
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MerchantId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String RoleName { get; set; }

        /// <summary>
        /// 0:超管, 1:普通管理员
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 RoleType { get; set; }

        /// <summary>
        /// 是否系统默认
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsSystem { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsDelete { get; set; }

        /// <summary>
        /// 账户备注
        /// </summary>
        [Required]
        [MaxLength(128)]
        public String Remark { get; set; }

        /// <summary>
        /// 创建该角色的ID
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 CreatedId { get; set; }

        /// <summary>
        /// 创建时间
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