using Microsoft.Extensions.DependencyInjection;
using Y.Infrastructure.YTasks.Jobs;

namespace Y.Infrastructure.YTasks
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJob(this IServiceCollection services)
        {

            #region 游戏任务

            services.AddSingleton(new JobSchedule(
               jobType: typeof(GameOrdersWithParmsJobService),
               cronExpression: "0 0/2 * * * ?")); // 获取游戏日志  每一分钟执行一次

            #endregion


            #region 活动任务
            // // [返水活动]    每天凌晨10分执行一次
            //services.AddSingleton(new JobSchedule(
            //    jobType: typeof(RebatePromoJobService), cronExpression: "0 10 0 * * ? *")); //  "0 10 0 * * ? *"

            // // [绑定银行卡] 活动  每一分钟执行一次
            // services.AddSingleton(new JobSchedule(
            //jobType: typeof(BankCardPromoJobService), cronExpression: "0 0/1 * * * ?"));

            // // [每日签到] 活动  每一分钟执行一次
            // services.AddSingleton(new JobSchedule(
            //jobType: typeof(DailyCheckInPromoJobService), cronExpression: "0 0/1 * * * ?"));

            // // [首存活动] 活动  每一分钟执行一次
            // services.AddSingleton(new JobSchedule(
            //jobType: typeof(DepositFirstPromoJobService), cronExpression: "0 0/1 * * * ?"));

            // // [存款活动] 活动  每一分钟执行一次
            // services.AddSingleton(new JobSchedule(
            //jobType: typeof(DepositPromoJobService), cronExpression: "0 0/1 * * * ?"));

            // // [邀请有礼] 活动  每一分钟执行一次
            // services.AddSingleton(new JobSchedule(
            //jobType: typeof(DepositPromoJobService), cronExpression: "0 0/1 * * * ?"));

            //// [充值优惠] 活动  每一分钟执行一次
            //services.AddSingleton(new JobSchedule(
            //jobType: typeof(RechargePromoService), cronExpression: "0 0/2 * * * ?"));

            // // [注册优惠] 活动  每一分钟执行一次
            // services.AddSingleton(new JobSchedule(
            //jobType: typeof(RegisterPromoJobService), cronExpression: "0 0/1 * * * ?"));

            // // [连续签到] 活动  每天凌晨10分执行一次
            // services.AddSingleton(new JobSchedule(
            //jobType: typeof(SeriesCheckInPromoService), cronExpression: "0 10 0 * * ? *"));

            // // [幸运转盘] 活动  每一分钟执行一次
            // services.AddSingleton(new JobSchedule(
            //jobType: typeof(TurnTablePromoJobService), cronExpression: "0 0/1 * * * ?"));

            // // [每周签到] 活动   每周一凌晨30分执行一次
            // services.AddSingleton(new JobSchedule(
            //jobType: typeof(WeeklyCheckInPromoJobService), cronExpression: "0 30 0 ? * MON"));

            // [VIP自动晋级] 活动  每30分钟执行一次
            services.AddSingleton(new JobSchedule(
            jobType: typeof(VipUpgradePromoJobService), cronExpression: "0 0/1 * * * ?"));  // 该方法没有实现 ~

            // // [VIP生日礼金] 活动  每天凌晨一点钟分执行一次
            // services.AddSingleton(new JobSchedule(
            // jobType: typeof(VipBirthPromoJobService), cronExpression: "0 0 1 * * ? *"));

            // // [VIP每月俸禄] 活动  每月3号凌晨4点执行一次
            // services.AddSingleton(new JobSchedule(
            // jobType: typeof(VipMonthSalaryPromoJobService), cronExpression: "0 0 4 3 * ? *"));



            #endregion


            #region 报表任务



            services.AddSingleton(new JobSchedule(
               jobType: typeof(AgentDailyReportJobService),
               cronExpression: "0 0 1 * * ?")); // 代理日报表 每天凌晨1点执行

            services.AddSingleton(new JobSchedule(
               jobType: typeof(GameUserDailyReportJobService),
               cronExpression: "0 10 0 * * ? *")); // 游戏用户日报表 每天凌晨10分执行



            #endregion


            #region 充值任务

            services.AddSingleton(new JobSchedule(
                 jobType: typeof(DepositTimeExpiredJobService),
                 cronExpression: "0 0/5 * * * ?")); // 用户充值时间自动过期，每分钟执行一次


            #endregion


            #region 完结游戏回调任务

            services.AddSingleton(new JobSchedule(
                       jobType: typeof(GameLogsCallBackProcessJobService),
                       cronExpression: "0 0/2 * * * ?")); // 游戏日志完结后回调任务  每一分钟执行一次

            #endregion


        }
    }
}
