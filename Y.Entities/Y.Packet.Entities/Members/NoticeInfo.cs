using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Members
{
    /// <summary>
    /// Aaron
    /// 2021-05-09 10:31:29
    /// 
    /// </summary>
    public partial class NoticeInfo
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
        /// 会员Id
        /// </summary>
        [Required]
        [MaxLength(10)]
        public int MemberId { get; set; }

        /// <summary>
        /// 通知的Id
        /// </summary>
        [Required]
        [MaxLength(10)]
        public int NoticeAreaId { get; set; }

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
        /// 是否已读取
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsRead { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [MaxLength(19)]
        public DateTime CreateTime { get; set; }


    }
}
