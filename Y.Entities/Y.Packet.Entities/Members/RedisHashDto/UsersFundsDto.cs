using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Y.Infrastructure.Library.Core.LuckyEntity.LuckyColorGames;
using Y.Infrastructure.Library.Core.LuckyEntity.Enums;
using Y.Infrastructure.Library.Core.LuckyEntity;

namespace Y.Packet.Entities.Members.RedisDb
{
    public class UsersFundsDto
    {
        public Int32 Id { get; set; }

        public Int32 MerchantId { get; set; }

        public Int32 UserId { get; set; }

        public long TotalFunds { get; set; }

        public long LockFunds { get; set; }

        public long TotalRechargedFunds { get; set; }

        public Int32 TotalRechargedFundsCount { get; set; }

        public long TotalWithdrawalFunds { get; set; }

        public Int32 TotalWithdrawalCount { get; set; }

        public long TotalBetFunds { get; set; }

        public long TotalProfitAndLoss { get; set; }

        public long PromotionsFunds { get; set; }

        public long OtherFunds { get; set; }

        public UsersFunds ToEntity()
        {
            return new UsersFunds()
            {
                Id = Id,
                MerchantId = MerchantId,
                UserId = UserId,
                TotalFunds = TotalFunds / 10000,
                LockFunds = LockFunds / 10000,
                TotalRechargedFunds = TotalRechargedFunds / 10000,
                TotalRechargedFundsCount = TotalRechargedFundsCount,
                TotalWithdrawalFunds = TotalWithdrawalFunds / 10000,
                TotalWithdrawalCount = TotalWithdrawalCount,
                TotalBetFunds = TotalBetFunds / 10000,
                TotalProfitAndLoss = TotalProfitAndLoss / 10000,
                PromotionsFunds = PromotionsFunds / 10000,
                OtherFunds = OtherFunds / 10000
            };
        }
    }
}
