using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Vips
{
    /// <summary>
    /// Aaron
    /// 2020-09-17 23:49:49
    /// 
    /// </summary>
    public partial class VipGroups
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
        public Int32 MerchantId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(12)]
        public String GroupName { get; set; }

        [Required]
        [MaxLength(64)]
        public string Img { get;set; }

        /// <summary>
        /// 是否开启， true开启， false未开启
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean Enabled { get; set; }

        /// <summary>
        /// 是否默认, true默认, false 非默认
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsDefault { get; set; }

        /// <summary>
        /// 充值配置
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public String PaySetting { get; set; }

        /// <summary>
        /// 提现设定
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public String WithdrawalSetting { get; set; }

        /// <summary>
        /// 分组满足条件
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public String GroupSetting { get; set; }

        /// <summary>
        /// 积分设置
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public String PointSetting { get; set; }

        /// <summary>
        /// 分组描述
        /// </summary>
        [Required]
        [MaxLength(64)]
        public String Description { get; set; }

        /// <summary>
        /// 排序序号
        /// </summary>
        [Required]
        [MaxLength(3)]
        public int SortNo { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime CreateTime { get; set; }


    }
}
