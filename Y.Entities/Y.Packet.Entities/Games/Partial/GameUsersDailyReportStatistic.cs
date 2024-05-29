using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Y.Infrastructure.Library.Core.YEntity;

namespace Y.Packet.Entities.Games
{
    /// <summary>
    /// Aaron
    /// 2020-09-08 23:59:35
    /// 
    /// </summary>
    partial class GameUsersDailyReportStatistic
    {

        public struct RebateTempModel
        {

            public Int32 UserId { get; set; }
            public GameType Type { get; set; }
            public decimal Reward { get; set; }
            public Int32 MerchantId { get; set; }
            public int PromotionId { get; set; }
            public string SourceId { get; set; }
        }


    }
}
