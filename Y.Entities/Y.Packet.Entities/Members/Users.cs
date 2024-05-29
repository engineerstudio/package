using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Y.Infrastructure.Library.Core.WebInfrastructure.EnumeratedType;

namespace Y.Packet.Entities.Members
{
    /// <summary>
    /// Aaron
    /// 2020-08-30 15:00:55
    /// 用户基本信息表
    /// </summary>
    public partial class Users
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
        public Int32 MerchantId { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        [Required]
        [MaxLength(24)]
        public String AccountName { get; set; }

        /// <summary>
        /// 真实用户名
        /// </summary>
        [Required]
        [MaxLength(24)]
        public String IdName { get; set; }


        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [MaxLength(16)]
        public String Pasw { get; set; }

        /// <summary>
        /// 资金密码
        /// </summary>
        [Required]
        [MaxLength(16)]
        public String FPasw { get; set; }

        /// <summary>
        /// 用户组
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 GroupId { get; set; }

        /// <summary>
        /// 上级代理
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 AgentId { get; set; }

        /// <summary>
        /// 用户类型  会员/代理
        /// </summary>
        [Required]
        [MaxLength(3)]
        public UserType Type { get; set; }

        /// <summary>
        /// 代理配置
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string AgentSetting { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Required]
        [MaxLength(15)]
        public String Mobile { get; set; }

        /// <summary>
        ///  创建时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Required]
        [MaxLength(64)]
        public String Email { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        [MaxLength(3)]
        public Gender Gender { get; set; }

    }
}
