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
    public partial class ActivityStatistic
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 SiteId { get; set; }

        /// <summary>
        ///  
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
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 ActivityIn { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 EffectiveIn { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal Reward { get; set; }


    }
}
