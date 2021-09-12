using Blog.Core.Auth;
using Blog.Core.Data;
using Blog.Core.Logging;
using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Blog.Core.Job
{
    /// <summary>
    /// Job基类（所有Job继承该基类）
    /// </summary>
    [DisallowConcurrentExecution]
    public abstract class JobBase : IJob
    {
        /// <summary>
        /// 作业名
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 日志
        /// </summary>
        protected virtual ILog Logger => LogFactory.GetLogger("job");

        /// <summary>
        /// Job Key
        /// </summary>
        public virtual JobKey JobKey => new JobKey(Name, GetType().Namespace);

        /// <summary>
        /// 默认触发器状态
        /// </summary>
        public virtual TriggerState DefaultTriggerState => TriggerState.Normal;

        /// <summary>
        /// 作业描述
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// 任务
        /// </summary>
        public abstract void Executing(IJobExecutionContext context);

        /// <summary>
        /// 任务执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            var user = context.JobDetail.JobDataMap.Get("User") as CurrentUserModel;
            return Task.Factory.StartNew(() =>
            {
                Logger.Debug($"作业：{Name} 开始执行");

                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var broker = PersistBrokerFactory.GetPersistBroker();
                UserIdentityUtil.SetCurrentUser(user);
                try
                {
                    broker.ExecuteTransaction(() =>
                    {
                        Executing(context);
                        // 更新下次执行时间
                        var nextTime = JobHelpers.GetJobNextTime(Name);
                        var nextTimeSql = "";
                        var paramList = new Dictionary<string, object>() {
                            { "@time", DateTime.Now },
                            { "@name", Name }
                        };
                        paramList.Add("@nextTime", nextTime.UtcDateTime);
                        nextTimeSql = ", nextruntime = @nextTime";
                        broker.Execute($"UPDATE job SET lastruntime = @time {nextTimeSql} WHERE name = @name", paramList);
                    });
                }
                catch (Exception e)
                {
                    Logger.Error($"作业：{Name}执行异常", e);
                    throw e;
                }
                stopWatch.Stop();
                Logger.Debug($"作业：{Name} 执行结束，耗时{stopWatch.ElapsedMilliseconds}ms");
            });
        }

        /// <summary>
        /// 获取Job生成器
        /// </summary>
        /// <returns></returns>
        public virtual JobBuilder GetJobBuilder()
        {
            return JobBuilder.Create(GetType())
                    .WithIdentity(JobKey.Name, JobKey.Group)
                    .WithDescription(Description);
        }

        /// <summary>
        /// 计划生成
        /// </summary>
        public virtual IScheduleBuilder ScheduleBuilder => null;
        public virtual TriggerBuilder GetTriggerBuilder()
        {
            if (ScheduleBuilder == null)
            {
                return null;
            }

            return TriggerBuilder
                    .Create()
                    .WithIdentity(JobKey.Name, JobKey.Group)
                    .WithDescription(Description)
                    .WithSchedule(ScheduleBuilder)
                    .StartAt(SystemTime.UtcNow().AddSeconds(5));
        }
    }
}
