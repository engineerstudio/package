using System;

namespace Y.Infrastructure.Library.Core.AuthController.Entity
{
    public class ManagerRoleAddOrModifyModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 角色名称
        public String RoleName { get; set; }

        /// <summary>
        /// 角色类型1超管2系管
        /// </summary>
        public Int32 RoleType { get; set; }

        /// <summary>
        /// 是否系统默认
        /// </summary>
        public Boolean IsSystem { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 系统标识字符串
        /// </summary>
        public string SysStr { get; set; }

        /// <summary>
        /// 菜单列表
        /// </summary>
        public int[] MenuIds { get; set; }

        public int MerchantId { get; set; }
    }
}