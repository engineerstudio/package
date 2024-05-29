using System.Collections.Generic;

namespace Y.Infrastructure.Entities.ViewModel
{
    public class W27CenterPageInfoViewModel
    {
        public W27UserInfo User { get; set; }
        public W27FundsInfo Balance { get; set; }
        public W27Vips Vips { get; set; }
        public List<W27WithdrawalsRange> WithdrawalRange { get; set; }
    }

    public class W27UserInfo
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public string LastLoginIpAddr { get; set; }
        /// <summary>
        /// 总投注
        /// </summary>
        public decimal TotalBet { get; set; }
    }

    public class W27FundsInfo
    {
        /// <summary>
        /// 可用金额 / 可体现金额 / 钱包  不包含锁定
        /// </summary>
        public decimal Valid { get; set; }
        /// <summary>
        /// 总金额 包含锁定与游戏
        /// </summary>
        public decimal Center { get; set; }
        /// <summary>
        /// 锁定金额
        /// </summary>
        public decimal Lock { get; set; }
        /// <summary>
        /// 游戏金额
        /// </summary>
        public decimal Games { get; set; }
    }

    public class W27Vips
    {
        public int CurrentLevel { get; set; }
        public string CurrentLevelImage { get; set; }
        public int NextLevel { get; set; }
        public string NextLevelImage { get; set; }
    }

    public class W27WithdrawalsRange
    {
        public int SortNo { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
    }

}
