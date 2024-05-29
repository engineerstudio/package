using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Games
{
    /// <summary>
    /// Aaron
    /// 2020-09-08 23:59:35
    /// 用户游戏信息表
    /// </summary>
    public partial class GameUsers
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        /// 游戏类型
        /// </summary>
        [Required]
        [MaxLength(3)]
        public string TypeStr { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MerchantId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MemberId { get; set; }

        /// <summary>
        /// 会员游戏名称
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String PlayerName { get; set; }

        /// <summary>
        /// 会员游戏密码
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String PlayerPsw { get; set; }

        /// <summary>
        /// 游戏余额
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal Balance { get; set; }

        /// <summary>
        /// 是否锁定禁止登录
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsLock { get; set; }

        /// <summary>
        /// 是否测试账户
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsTest { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime RegisterTime { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime LastLoginTime { get; set; }


    }
}
