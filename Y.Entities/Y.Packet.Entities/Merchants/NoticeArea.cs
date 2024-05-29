using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Merchants
{
    /// <summary>
    /// Aaron
    /// 2021-05-09 10:31:28
    /// 
    /// </summary>
    public partial class NoticeArea
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        /// 商户Id
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MerchantId { get; set; }

        /// <summary>
        /// 类型 0:通知 1公告
        /// </summary>
        [Required]
        [MaxLength(5)]
        public NoticeType Type { get; set; }
        /// <summary>
        /// 通知/公告的发放对象
        /// </summary>
        [Required]
        public string GroupId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required]
        public String Description { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsDisplay { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean Deleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [MaxLength(19)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 首页显示
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean HomeDisplay { get; set; }

        /// <summary>
        /// 是否已经读过
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsBrowsed { get; set; }
    }
}
