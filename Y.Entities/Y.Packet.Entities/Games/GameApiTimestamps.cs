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
    public partial class GameApiTimestamps
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
        public GameType Type { get; set; }

        /// <summary>
        ///  查询时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime QueryTime { get; set; }

        /// <summary>
        /// 保存到秒的时间戳
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Int64 Timestamps { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String Mark { get; set; }

        /// <summary>
        /// 类型字符串
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string TypeStr { get; set; }

    }
}
