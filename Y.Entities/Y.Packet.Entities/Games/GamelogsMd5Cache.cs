using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Games
{
    /// <summary>
    /// Aaron
    /// 2020-09-13 23:23:32
    /// 
    /// </summary>
    public partial class GamelogsMd5Cache
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
        [MaxLength(18)]
        public string GameTypeStr { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String SourceId { get; set; }

        /// <summary>
        ///  
        /// </summary>
        [Required]
        [MaxLength(32)]
        public String RawMd5 { get; set; }


    }
}
