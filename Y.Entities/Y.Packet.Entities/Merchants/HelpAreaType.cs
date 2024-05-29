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
    public partial class HelpAreaType
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        /// 商户ID
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MerchantId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(12)]
        public String Title { get; set; }

        /// <summary>
        /// 是否跳转链接,否：站内地址跳转， 是：其它链接地址
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsHref { get; set; }

        /// <summary>
        /// 是否开启
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsOpen { get; set; }

        /// <summary>
        /// 跳转链接
        /// </summary>
        [Required]
        [MaxLength(128)]
        public String Href { get; set; } = "/help";

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 SortNo { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string IconImg { get; set; }
    }
}
