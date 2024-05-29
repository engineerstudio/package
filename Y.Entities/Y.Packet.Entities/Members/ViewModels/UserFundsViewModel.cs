using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Packet.Entities.Members.ViewModels
{
    public class UserFundsViewModel
    {
        public decimal TotalAmount { get; set; }
        public decimal AvailableAmount { get; set; }
        public decimal LockedAmount { get; set; }
        public bool IsAgent { get; set; }
    }
    public class UserFundsViewModelV2 : UserFundsViewModel
    {
        public decimal GamesAmount { get; set; }
        public decimal Level { get; set; }
        public string LevelImg { get; set; }
    }


    public class UserFundsViewModelV3 : UserFundsViewModel
    {
        public decimal Level { get; set; }
        public decimal DiffDeposit { get; set; }
        public decimal DiffWash { get; set; }
        public int Day { get; set; }
    }

}
