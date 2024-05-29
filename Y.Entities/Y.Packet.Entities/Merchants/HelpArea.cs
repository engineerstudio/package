using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Merchants
{
    /// <summary>
    /// Aaron
    /// 2021-03-02 22:13:21
    /// 
    /// </summary>
    public partial class HelpArea
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        [MaxLength(5)]
        public Int32 TypeId { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MerchantId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [MaxLength(24)]
        public String Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        public String Tcontent { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 显示在首页
        /// </summary>
        [Required]
        [MaxLength(1)]
        public bool ShowIndexPage { get; set; }

        [Required]
        [MaxLength(24)]
        public string Alias { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 SortNo { get; set; }
    }
}
