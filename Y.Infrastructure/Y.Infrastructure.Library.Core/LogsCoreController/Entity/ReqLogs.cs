using System;
using System.ComponentModel.DataAnnotations;

namespace Y.Infrastructure.Library.Core.LogsCoreController.Entity
{
    /// <summary>
    /// Aaron
    /// 2021-08-20 15:21:28
    /// 
    /// </summary>
    public partial class ReqLogs
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        /// <summary>
        /// 请求商户号
        /// </summary>
        [Required]
        [MaxLength(10)]
        public Int32 MerchantId { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        [Required]
        [MaxLength(225)]
        public String ReqUri { get; set; }

        /// <summary>
        /// 请求内容详细
        /// </summary>
        [Required]
        [MaxLength(1073741823)]
        public String ReqDetails { get; set; }


        [Required] [MaxLength(23)] public DateTime ReqDatetime { get; set; }
    }
}