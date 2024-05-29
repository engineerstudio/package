using System;
using System.ComponentModel.DataAnnotations;

namespace Y.Infrastructure.Library.Core.AuthController.Entity
{
    /// <summary>
    /// Aaron
    /// 2020-08-24 02:23:05
    /// 
    /// </summary>
    public partial class SysRolePermission
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 RoleId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MenuId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 Permission { get; set; }
    }
}