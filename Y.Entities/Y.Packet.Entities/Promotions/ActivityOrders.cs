
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
    public partial class ActivityOrders
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        /// 站点Id
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MerchantId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 UserId { get; set; }

        /// <summary>
        /// 优惠活动Id 
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 PromotionId { get; set; }

        /// <summary>
        /// 优惠类型
        /// </summary>
        [Required]
        [MaxLength(10)]
        public ActivityType AType { get; set; }

        /// <summary>
        /// 派奖金额
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal Reward { get; set; }

        /// <summary>
        /// 状态. 成功、失败
        /// </summary>
        [Required]
        [MaxLength(10)]
        public ActivityOrderStatus Status { get; set; }

        /// <summary>
        /// 活动Ip
        /// </summary>
        [Required]
        [MaxLength(15)]
        public String Ip { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [MaxLength(50)]
        public String Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 派奖时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime RewardTime { get; set; }

        /// <summary>
        ///  创建日字符串
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string CreateDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        [MaxLength(128)]
        public String SourceId { get; set; }
    }
}
