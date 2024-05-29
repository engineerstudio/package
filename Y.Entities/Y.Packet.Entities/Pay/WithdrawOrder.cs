using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Packet.Entities.Pay
{
    /// <summary>
    /// Aaron
    /// 2020-10-25 10:39:54
    /// 
    /// </summary>
    public partial class WithdrawOrder
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }


        [Required]
        [MaxLength(10)]
        public int MerchantId { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string WithdrawMerchantOrderId { get; set; }

        /// <summary>
        /// 会员Id
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MemberId { get; set; }


        /// <summary>
        /// 提现用户银行卡
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 UserBankId { get; set; }

        /// <summary>
        /// 出款渠道ID
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 WithdrawMerchantId { get; set; }

        /// <summary>
        /// 请求提现金额
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal ReqWithdrawAmount { get; set; }

        /// <summary>
        /// 实际提现金额
        /// </summary>
        [Required]
        [MaxLength(19)]
        public Decimal WithdrawAmount { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public PayOrderStatus Status { get; set; }

        /// <summary>
        /// 是否完成订单
        /// </summary>
        [Required]
        [MaxLength(1)]
        public Boolean IsFinish { get; set; }

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
        ///  订单提交IP
        /// </summary>
        [Required]
        [MaxLength(20)]
        public String IP { get; set; }

        /// <summary>
        ///  备注
        /// </summary>
        [Required]
        [MaxLength(20)]
        public String Marks { get; set; }
    }
}
