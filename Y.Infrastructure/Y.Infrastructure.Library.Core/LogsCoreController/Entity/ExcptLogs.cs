using System;
using System.ComponentModel.DataAnnotations;

namespace Y.Infrastructure.Library.Core.LogsCoreController.Entity
{
    /// <summary>
    /// Aaron
    /// 2021-07-17 11:30:02
    /// 
    /// </summary>
    public partial class ExcptLogs
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
        [MaxLength(50)]
        public String MachineName { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime Logged { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(50)]
        public String Level { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public String Message { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [MaxLength(250)]
        public String Logger { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public String Callsite { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public String Exception { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [MaxLength(2147483647)]
        public String StackTrace { get; set; }
    }
}