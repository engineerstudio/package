using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Promotions
{
    /// <summary>
    /// Aaron
    /// 2020-07-26 00:31:01
    /// 
    /// </summary>
    public partial class PromotionsConfig
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        /// 活动类别
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 TagId { get; set; }

        /// <summary>
        /// 站点Id
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MerchantId { get; set; }

        /// <summary>
        /// 优惠类型
        /// </summary>
        [Required]
        [MaxLength(10)]
        public ActivityType AType { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public String Title { get; set; }


        /// <summary>
        /// Pc封面
        /// </summary>
        [Required]
        [MaxLength(64)]
        public String PcCover { get; set; }

        /// <summary>
        /// H5封面
        /// </summary>
        [Required]
        [MaxLength(64)]
        public String H5Cover { get; set; }

        [Required]
        [MaxLength(128)]
        public string IndexPageCover { get; set; }

        /// <summary>
        /// 活动描述
        /// </summary>
        [Required]
        public String Description { get; set; }

        /// <summary>
        /// 规则配置
        /// </summary>
        [Required]
        public string Config { get; set; }

        /// <summary>
        /// 是否开启
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean Enabled { get; set; }

        /// <summary>
        /// 是否前台显示
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean Visible { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 标记为已删除
        /// </summary>
        [Required]
        [MaxLength(1)]
        public bool Deleted { get; set; }

        [Required]
        [MaxLength(1)]
        public bool HomeDisplay { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        [Required]
        [MaxLength(10)]
        public int SortNo { get; set; }
    }
}
