﻿using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Y.Infrastructure.Library.Core.Quartz
{
    public class QuartzUtil
    {
        private ISchedulerFactory sf = null;
        private IScheduler sched = null;

        //QuartzUtil()
        //{
        //    sf = new StdSchedulerFactory();
        //    sched = sf.GetScheduler();
        //    sched.Start();
        //}


        private static QuartzUtil instance = null;
        private static object obj = new object();

        private QuartzUtil()
        {
            sf = new StdSchedulerFactory();
            sched = sf.GetScheduler().Result;
            sched.Start();
        }

        public static QuartzUtil Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (obj)
                    {
                        if (instance == null)
                        {
                            instance = new QuartzUtil();
                        }
                    }
                }

                return instance;
            }
        }


        /// <summary>
        /// 添加Job 并且以定点的形式运行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JobName"></param>
        /// <param name="CronTime"></param>
        /// <param name="jobDataMap"></param>
        /// <returns></returns>
        public async Task<DateTimeOffset> AddJob<T>(string JobName, string CronTime, string jobData) where T : IJob
        {
            IJobDetail jobCheck = JobBuilder.Create<T>().WithIdentity(JobName, JobName + "_Group")
                .UsingJobData("jobData", jobData).Build();
            ICronTrigger CronTrigger =
                new CronTriggerImpl(JobName + "_CronTrigger", JobName + "_TriggerGroup", CronTime);
            return await sched.ScheduleJob(jobCheck, CronTrigger);
        }

        /// <summary>
        /// 添加Job 并且以定点的形式运行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JobName"></param>
        /// <param name="CronTime"></param>
        /// <returns></returns>
        public async Task<DateTimeOffset> AddJob<T>(string JobName, string CronTime) where T : IJob
        {
            return await AddJob<T>(JobName, CronTime, null);
        }

        /// <summary>
        /// 添加Job 并且以周期的形式运行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JobName"></param>
        /// <param name="SimpleTime">毫秒数</param>
        /// <returns></returns>
        public async Task<DateTimeOffset> AddJob<T>(string JobName, int SimpleTime) where T : IJob
        {
            return await AddJob<T>(JobName, DateTime.UtcNow.AddMilliseconds(1), TimeSpan.FromMilliseconds(SimpleTime));
        }

        /// <summary>
        /// 添加Job 并且以周期的形式运行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JobName"></param>
        /// <param name="SimpleTime">毫秒数</param>
        /// <returns></returns>
        public async Task<DateTimeOffset> AddJob<T>(string JobName, DateTimeOffset StartTime, int SimpleTime)
            where T : IJob
        {
            return await AddJob<T>(JobName, StartTime, TimeSpan.FromMilliseconds(SimpleTime));
        }

        /// <summary>
        /// 添加Job 并且以周期的形式运行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JobName"></param>
        /// <param name="SimpleTime"></param>
        /// <returns></returns>
        public async Task<DateTimeOffset> AddJob<T>(string JobName, DateTimeOffset StartTime, TimeSpan SimpleTime)
            where T : IJob
        {
            return await AddJob<T>(JobName, StartTime, SimpleTime, new Dictionary<string, object>());
        }

        /// <summary>
        /// 添加Job 并且以周期的形式运行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JobName"></param>
        /// <param name="StartTime"></param>
        /// <param name="SimpleTime">毫秒数</param>
        /// <param name="jobDataMap"></param>
        /// <returns></returns>
        public async Task<DateTimeOffset> AddJob<T>(string JobName, DateTimeOffset StartTime, int SimpleTime,
            string MapKey, object MapValue) where T : IJob
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add(MapKey, MapValue);
            return await AddJob<T>(JobName, StartTime, TimeSpan.FromMilliseconds(SimpleTime), map);
        }

        /// <summary>
        /// 添加Job 并且以周期的形式运行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JobName"></param>
        /// <param name="StartTime"></param>
        /// <param name="SimpleTime"></param>
        /// <param name="jobDataMap"></param>
        /// <returns></returns>
        public async Task<DateTimeOffset> AddJob<T>(string JobName, DateTimeOffset StartTime, TimeSpan SimpleTime,
            Dictionary<string, object> map) where T : IJob
        {
            IJobDetail jobCheck = JobBuilder.Create<T>().WithIdentity(JobName, JobName + "_Group").Build();
            jobCheck.JobDataMap.PutAll(map);
            ISimpleTrigger triggerCheck = new SimpleTriggerImpl(JobName + "_SimpleTrigger", JobName + "_TriggerGroup",
                StartTime,
                null,
                SimpleTriggerImpl.RepeatIndefinitely,
                SimpleTime);
            return await sched.ScheduleJob(jobCheck, triggerCheck);
        }

        /// <summary>
        /// 修改触发器时间,需要job名,以及修改结果
        /// CronTriggerImpl类型触发器
        /// </summary>
        public async Task UpdateTimeAsync(string jobName, string CronTime)
        {
            TriggerKey TKey = new TriggerKey(jobName + "_CronTrigger", jobName + "_TriggerGroup");
            CronTriggerImpl cti = await sched.GetTrigger(TKey) as CronTriggerImpl;
            cti.CronExpression = new CronExpression(CronTime);
            await sched.RescheduleJob(TKey, cti);
        }

        /// <summary>
        /// 修改触发器时间,需要job名,以及修改结果
        /// SimpleTriggerImpl类型触发器
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="SimpleTime">分钟数</param>
        public void UpdateTime(string jobName, int SimpleTime)
        {
            UpdateTime(jobName, TimeSpan.FromMinutes(SimpleTime));
        }

        /// <summary>
        /// 修改触发器时间,需要job名,以及修改结果
        /// SimpleTriggerImpl类型触发器
        /// </summary>
        public async void UpdateTime(string jobName, TimeSpan SimpleTime)
        {
            TriggerKey TKey = new TriggerKey(jobName + "_SimpleTrigger", jobName + "_TriggerGroup");
            SimpleTriggerImpl sti = await sched.GetTrigger(TKey) as SimpleTriggerImpl;
            sti.RepeatInterval = SimpleTime;
            await sched.RescheduleJob(TKey, sti);
        }

        /// <summary>
        /// 暂停所有Job
        /// 暂停功能Quartz提供有很多,以后可扩充
        /// </summary>
        public void PauseAll()
        {
            sched.PauseAll();
        }

        /// <summary>
        /// 恢复所有Job
        /// 恢复功能Quartz提供有很多,以后可扩充
        /// </summary>
        public void ResumeAll()
        {
            sched.ResumeAll();
        }

        /// <summary>
        /// 删除Job
        /// 删除功能Quartz提供有很多,以后可扩充
        /// </summary>
        /// <param name="JobName"></param>
        public void DeleteJob(string JobName)
        {
            JobKey jk = new JobKey(JobName, JobName + "_Group");
            sched.DeleteJob(jk);
        }

        /// <summary>
        /// 卸载定时器
        /// </summary>
        /// <param name="waitForJobsToComplete">是否等待job执行完成</param>
        public void Shutdown(bool waitForJobsToComplete)
        {
            sched.Shutdown(waitForJobsToComplete);
        }
    }
}