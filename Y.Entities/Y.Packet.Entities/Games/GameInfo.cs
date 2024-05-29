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
    public partial class GameInfo
    {
        /// <summary>
        /// 禁用, 启用, 维护
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        /// 游戏名称
        /// </summary>
        [Required]
        [MaxLength(18)]
        public String Name { get; set; }

        /// <summary>
        /// 游戏类型  真人 体育  
        /// </summary>
        [Required]
        [MaxLength(3)]
        public GameCategory CategoryType { get; set; }

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
        /// 游戏状态  开启  维护  删除
        /// </summary>
        [Required]
        [MaxLength(3)]
        public GameStatus Status { get; set; }

        /// <summary>
        /// 默认费率
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal DefaultRate { get; set; }

        /// <summary>
        /// 是否是免转钱包 
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsTransferWallet { get; set; }

        /// <summary>
        /// API日志时区
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 ApiTimeZone { get; set; }

        /// <summary>
        /// 游戏登录/API基础参数配置
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public String Config { get; set; }


    }
}
