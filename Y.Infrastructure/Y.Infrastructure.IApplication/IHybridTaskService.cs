using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Y.Infrastructure.IApplication
{
    public interface IHybridTaskService
    {


        /// <summary>
        /// 创建代理日报表
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task CreateAgentDailyStatisticAsync(DateTime date);



        #region 活动任务

        /// <summary>
        /// [返水活动]
        /// </summary>
        /// <param name="date"></param>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        Task<string> ExecRebatePromoAsync(DateTime date, int merchantId);

        /// <summary>
        /// [绑定银行卡活动]
        /// </summary>
        /// <returns></returns>
        Task<string> ExecBankCardPromo();


        /// <summary>
        /// [代理返佣] 暂无
        /// </summary>
        /// <returns></returns>
        Task<string> ExecAgentRebatePromoAsync();


        /// <summary>
        /// [代理契约] 契约执行时间, 每周一凌晨四点. 每月1日凌晨四点
        /// </summary>
        /// <returns></returns>
        Task<string> ExecAgentContractRebatePromo();


        /// <summary>
        /// [注册优惠] 暂无
        /// </summary>
        /// <returns></returns>
        Task<string> ExecRegisterPromo();


        /// <summary>
        /// [每日签到]
        /// </summary>
        /// <returns></returns>
        Task<string> ExecDailyCheckInPromo();


        /// <summary>
        /// [周周签到]
        /// </summary>
        /// <returns></returns>
        Task<string> ExecWeeklyCheckInPromo();

        /// <summary>
        ///  [连续签到]
        /// </summary>
        /// <returns></returns>
        Task<string> ExecSeriesCheckInPromo();

        /// <summary>
        /// [充值优惠] 
        /// </summary>
        /// <returns></returns>
        Task<string> ExecRechargePromo();


        /// <summary>
        /// [幸运转盘]
        /// </summary>
        /// <returns></returns>
        Task<string> ExecTurnTablePromo();

        /// <summary>
        /// [邀请有礼]
        /// </summary>
        /// <returns></returns>
        Task<string> ExecInvitePromo();

        /// <summary>
        /// [救援金]
        /// </summary>
        /// <returns></returns>
        Task<string> ExecRescuePromo();
        #endregion



        #region VIP自动升级任务

        /// <summary>
        /// [VIP]充值升级
        /// </summary>
        /// <param name="merchantId"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        Task<(bool, string)> VipsUpgradeTask(int merchantId, int memberId);
        Task<(bool, string)> VipsUpgradeTaskAsync();

        /// <summary>
        /// [生日礼金]每天凌晨三点执行一次
        /// </summary>
        /// <returns></returns>
        Task VipBirthTask();


        /// <summary>
        /// [每月俸禄]每个月固定日期执行一次
        /// </summary>
        /// <returns></returns>
        Task VipSalaryTask();

        #endregion


    }
}
