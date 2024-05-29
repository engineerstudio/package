using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Packet.Entities.Pay
{
    /// <summary>
    /// Aaron
    /// 2020-10-16 23:12:38
    /// 
    /// </summary>
    public partial class PayOrder
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
        /// 会员Id
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MemberId { get; set; }

        /// <summary>
        /// 支付渠道Id
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 PayMerchantId { get; set; }

        /// <summary>
        /// 请求充值金额
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal ReqDepositAmount { get; set; }

        /// <summary>
        /// 实际充值金额
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal DepositAmount { get; set; }

        /// <summary>
        /// 是否完成订单
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsFinish { get; set; }

        [Required]
        [MaxLength(3)]
        public PayOrderStatus Status { get; set; }

        /// <summary>
        /// 订单创建时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 确认订单时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime ConfirmTime { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(20)]
        public String IP { get; set; }

        /// <summary>
        ///  商户返回的订单Id
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String PayMerchantOrderId { get; set; }

    }
}
