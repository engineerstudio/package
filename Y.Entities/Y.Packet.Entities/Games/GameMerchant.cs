using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Packet.Entities.Games
{
    /// <summary>
    /// Aaron
    /// 2020-09-08 23:59:35
    /// 
    /// </summary>
    public partial class GameMerchant
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
        /// 游戏类型
        /// </summary>
        [Required]
        [MaxLength(3)]
        public GameType Type { get; set; }


        /// <summary>
        ///  游戏枚举的字符串
        /// </summary>
        [Required]
        [MaxLength(18)]
        public String TypeStr { get; set; }


        /// <summary>
        /// 游戏名称
        /// </summary>
        [Required]
        [MaxLength(12)]
        public String TypeDesc { get; set; }

        /// <summary>
        /// 是否启用：true(1)
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean Enabled { get; set; }

        /// <summary>
        /// 链接字符串配置
        /// </summary>
        [Required]
        [MaxLength(256)]
        public String Config { get; set; }

        /// <summary>
        /// 费率
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal Rate { get; set; }

        /// <summary>
        /// 是否启用：系统管理员操作 true(1)
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean SysEnabled { get; set; }

    }
}
