using System;
using System.Collections.Generic;
using System.Text;
using static Y.Packet.Entities.Promotions.PromotionsConfig;

namespace Y.Packet.Entities.Promotions.ViewModels
{
    public class PromotionsDetailsPage
    {
        public int Id { get; set; }
        public Int32 CategoryId { get; set; }
        public ActivityType AType { get; set; }
        public String Title { get; set; }
        public string Content { get; set; }
        public string Cover { get; set; }
        public string H5Cover { get; set; }
        public string IndexPageCover { get; set; }
        public string Config { get; set; }
        public Boolean Enabled { get; set; }
        public Boolean Visible { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public BonusType BonusType { get; set; }
        public BonusCalType BonusCalType { get; set; }
        public decimal BonusCalTypeValue { get; set; }
        public IPCheckType IPCheckType { get; set; }
        public WashType Wash { get; set; }
        public decimal WashValue { get; set; }
        public int TagId { get; set; }
        public int SortNo { get; set; }
    }
}
