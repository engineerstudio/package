using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Infrastructure.Library.Core.MultiLanguage.Db
{
    /// <summary>
    /// Aaron
    /// 2022-10-12 11:08:11
    /// 
    /// </summary>
    public partial class Docs
    {
        /// <summary>
        ///  
        /// </summary>
        [Key]
        public Int32 Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string MerchantId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(2048)]
        public String Zhcn { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(2048)]
        public String Zhtw { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(4096)]
        public String En { get; set; }


    }
}
