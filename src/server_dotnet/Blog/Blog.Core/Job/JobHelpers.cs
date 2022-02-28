﻿using Blog.Core.Auth;
using Sixpence.ORM.Entity;
using Sixpence.Common.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sixpence.Common;
using Sixpence.Common.IoC;
using System.Collections.Specialized;
using Sixpence.ORM.Driver;

namespace Blog.Core.Job
{
    /// <summary>
    /// Job帮助类
    /// </summary>
    public static class JobHelpers
    {
        static IScheduler sched;

        static JobHelpers()
        {
            var config = Sixpence.ORM.DBSourceConfig.Config;
            var properties = new NameValueCollection()
            {
                { "quartz.scheduler.instanceName", "MyClusteredScheduler"},
                { "quartz.scheduler.instanceId", "AUTO"},
                { "quartz.threadPool.type", "Quartz.Simpl.DefaultThreadPool, Quartz" },
                { "quartz.threadPool.threadCount", "25" },
                { "quartz.threadPool.threadPriority", "5" },
                { "quartz.jobStore.misfireThreshold", "60000" },
                { "quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz" },
                { "quartz.jobStore.driverDelegateType", GetDelegateType() },
                { "quartz.jobStore.tablePrefix", "QRTZ_" },
                { "quartz.jobStore.dataSource", "myDS" },
                { "quartz.jobStore.useProperties", "false" },
                { "quartz.dataSource.myDS.connectionString", config.ConnectionString },
                { "quartz.dataSource.myDS.provider", GetDbBDriver() },
                { "quartz.serializer.type", "json" }
            };
            var factory = new StdSchedulerFactory(properties);
            sched = factory.GetScheduler().Result;
        }

        private static string GetDbBDriver()
        {
            var dbType = Sixpence.ORM.DBSourceConfig.Config.DriverType.GetEnum<DriverType>();
            switch (dbType)
            {
                case DriverType.Postgresql:
                    return "Npgsql";
                case DriverType.Mysql:
                    return "MySql";
                default:
                    return null;
            }
        }

        private static string GetDelegateType()
        {
            var dbType = Sixpence.ORM.DBSourceConfig.Config.DriverType.GetEnum<DriverType>();
            switch (dbType)
            {
                case DriverType.Postgresql:
                    return "Quartz.Impl.AdoJobStore.PostgreSQLDelegate, Quartz";
                case DriverType.Mysql:
                    return "Quartz.Impl.AdoJobStore.SQLiteDelegate, Quartz";
                default:
                    return null;
            }
        }

        /// <summary>
        /// 注册作业
        /// </summary>
        public static void Start()
        {
            var logger = LogFactory.GetLogger("startup");
            var jobs = ServiceContainer.ResolveAll<IJob>().ToList();
            logger.Info($"共发现{jobs.Count}个Job待运行");
            jobs.Each(item => {
                if (item == null)
                {
                    return;
                }

                // 创建 Job
                var instance = item as JobBase;

                if (sched.CheckExists(instance.JobKey).Result)
                {
                    return;
                }

                var job = instance.GetJobBuilder().Build();
                job.JobDataMap.Add("User", UserIdentityUtil.GetSystem());

                var triggerBuilder = instance.GetTriggerBuilder();

                if (triggerBuilder != null)
                {
                    // 创建 trigger
                    ITrigger trigger = triggerBuilder.Build();
                    // 使用 trigger 规划执行任务 job
                    sched.ScheduleJob(job, trigger);
                    logger.Info($"作业[{instance.Name}]运行成功");
                    if (instance.DefaultTriggerState == TriggerState.Paused)
                    {
                        sched.PauseTrigger(trigger.Key);
                    }
                }
            });
            StartService();
        }

        /// <summary>
        /// 动态注册 job
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <param name="param"></param>
        /// <param name="cronExperssion"></param>
        public static void RegisterJob(DynamicJobBase job, BaseEntity entity, TriggerState state)
        {
            StartService();

            if (sched.CheckExists(job.JobKey).Result)
            {
                return;
            }

            var jobDetail = job.GetJobBuilder().Build();

            jobDetail.JobDataMap.Add("Entity", entity);
            jobDetail.JobDataMap.Add("User", UserIdentityUtil.GetAdmin());

            ITrigger trigger = job.GetTriggerBuilder()
                .Build();
            sched.ScheduleJob(jobDetail, trigger).Wait();

            if (state == TriggerState.Paused)
            {
                sched.PauseTrigger(trigger.Key);
            }
        }

        /// <summary>
        /// 立即执行
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <param name="context"></param>
        public static void RunOnceNow(string name, string group, IDictionary<string, object> context)
        {
            var jobKey = new JobKey(name, group);
            if (sched.CheckExists(jobKey).Result)
            {
                var jobDataMap = new JobDataMap(context);
                sched.TriggerJob(jobKey, jobDataMap);
            }
        }

        /// <summary>
        /// 立即执行
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public static void RunOnceNow(string name, string group)
        {
            var jobKey = new JobKey(name, group);
            if (sched.CheckExists(jobKey).Result)
            {
                sched.TriggerJob(jobKey).Wait();
            }
        }

        /// <summary>
        /// 删除job
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public static void DeleteJob(string name, string group)
        {
            if (sched.CheckExists(new JobKey(name, group)).Result)
            {
                sched.PauseJob(new JobKey(name, group)); // 停止任务
                sched.PauseTrigger(new TriggerKey(name, group)); // 停止触发器
                sched.UnscheduleJob(new TriggerKey(name, group)); // 移除触发器
                sched.DeleteJob(new JobKey(name, group)); // 删除任务
            }
        }

        /// <summary>
        /// 暂停 Job调度
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public static void PauseJob(string name, string group)
        {
            var triggerKey = new TriggerKey(name, group);
            sched.PauseTrigger(triggerKey);
        }

        /// <summary>
        /// 继续
        /// </summary>
        public static void ResumeJob(string name, string group)
        {
            var triggerKey = new TriggerKey(name, group);
            sched.ResumeTrigger(triggerKey);
        }

        /// <summary>
        /// 服务停止
        /// </summary>
        private async static void Shutdown()
        {
            if (!sched.IsShutdown)
            {
                await sched.Shutdown();
            }
        }

        /// <summary>
        /// 开启服务
        /// </summary>
        private async static void StartService()
        {
            if (!sched.IsStarted)
            {
                await sched.Start();
            }
        }

        /// <summary>
        /// 获取日志状态
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public static TriggerState GetJobStatus(string name, string group)
        {
            return sched.GetTriggerState(new TriggerKey(name, group)).Result;
        }
    }
}
