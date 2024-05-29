using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y.Packet.Entities.Games
{
    /// <summary>
    /// Aaron
    /// 2020-09-08 23:59:35
    /// 
    /// </summary>
    public partial class GameInfo
    {
        public enum GameStatus
        {
            [Description("维护")]
            Maintains,
            [Description("正常")]
            Normal,
            [Description("下架")]
            Deleted,
            [Description("待定")]
            NotSet
        }





    }
}
