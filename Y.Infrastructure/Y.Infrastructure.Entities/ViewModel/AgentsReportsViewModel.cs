using System;
using System.Collections.Generic;
using System.Text;

namespace Y.Infrastructure.Entities.ViewModel
{
    public class AgentsReportsViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } //用户名
        public string SubAgentNo { get; set; }// 下级代理数量
        public int SubUserNo { get; set; } // 下级人员总数
        public int NewUserNo { get; set; } // 新增
        public string Deposit { get; set; }// 存款
        public string Withdrawal { get; set; } //取款
        public string WaterFlow { get; set; } // 流水
        public string Loss { get; set; } // 盈亏
        public string Bet { get; set; } // 总投注
        public string ValidBet { get; set; } // 有效投注
        public string Point { get; set; }// 分润点位
        public string StartAt { get; set; }
        public string EndAt { get; set; }
        public List<AgentsReportsGamesViewModel> Games { get; set; }
    }

    public class AgentsReportsGamesViewModel
    {
        public string Name { get; set; }// 游戏名称
        public string Effective { get; set; }// 有效投注
        public string Loss { get; set; }// 盈亏
    }
}
