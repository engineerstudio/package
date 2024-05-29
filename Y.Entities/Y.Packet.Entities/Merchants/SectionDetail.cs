using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Merchants
{
    /// <summary>
    /// Aaron
    /// 2021-02-26 13:53:38
    /// 
    /// </summary>
    public partial class SectionDetail
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        /// 局部ID
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 SectionId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MerchantId { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [Required]
        [MaxLength(18)]
        public String Name { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        [Required]
        [MaxLength(18)]
        public String Alias { get; set; }

        /// <summary>
        /// 开启/禁用  
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean Enabled { get; set; }

        /// <summary>
        /// 是否菜单顶部显示图片
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsTopImg { get; set; }

        /// <summary>
        /// 是否存在子菜单 
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean HasSubMenu { get; set; }

        /// <summary>
        /// 上级key
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string SKey { get; set; }

        /// <summary>
        /// Pc图片链接地址
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String PcImgUrl { get; set; }

        /// <summary>
        /// H5、app图片链接地址
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String H5ImgUrl { get; set; }
        /// <summary>
        /// 页面链接地址
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String PageUrl { get; set; }

        /// <summary>
        /// 文本字符串
        /// </summary>
        [Required]
        public string Tcontent { get; set; }

        [Required]
        [MaxLength(10)]
        public int SortNo { get; set; }

    }
}
