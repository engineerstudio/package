using System;
using System.ComponentModel.DataAnnotations;

namespace Y.Infrastructure.Library.Core.LogsCoreController.Entity
{
    public class TaskDataForGames
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        ///  是否成功
        /// </summary>
        [Required]
        [MaxLength(50)]
        public bool Successed { get; set; }

        /// <summary>
        ///  请求地址
        /// </summary>
        [Required]
        [MaxLength(624)]
        public string Url { get; set; }

        /// <summary>
        ///  游戏类型
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string GameType { get; set; }

        /// <summary>
        ///  请求数据
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public String Requestd { get; set; }

        /// <summary>
        ///  相应数据
        /// </summary>
        [MaxLength(2147483647)]
        public String Responsed { get; set; }

        /// <summary>
        /// 游戏请求数据起始时间
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string GameReqTime { get; set; }

        /// <summary>
        /// 日志创建时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime CreateTime { get; set; }
    }
}