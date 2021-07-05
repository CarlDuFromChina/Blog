using Blog.Core.Auth;
using Blog.Core.Logging;
using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Job
{
    /// <summary>
    /// Job基类（所有动态Job继承该基类）
    /// </summary>
    [DisallowConcurrentExecution, DynamicJob]
    public abstract class DynamicJobBase : IJob
    {
        public DynamicJobBase() { }
        public DynamicJobBase(string name, string group, string cron)
        {
            JobKey = new JobKey(name, group);
            Name = name;
            ScheduleBuilder = CronScheduleBuilder.CronSchedule(cron);
        }

        public JobKey JobKey;

        /// <summary>
        /// 作业名
        /// </summary>
        public string Name;

        /// <summary>
        /// 日志
        /// </summary>
        protected ILog Logger => LogFactory.GetLogger("job_" + GetType().Name.ToLower());

        /// <summary>
        /// 默认触发器状态
        /// </summary>
        public virtual TriggerState DefaultTriggerState => TriggerState.Normal;

        public abstract void Executing(IJobExecutionContext context);
        public Task Execute(IJobExecutionContext context)
        {
            var user = context.JobDetail.JobDataMap.Get("User") as CurrentUserModel;
            return Task.Factory.StartNew(() =>
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                Logger.Debug($"作业：{Name} 开始执行");
                try
                {
                    UserIdentityUtil.SetCurrentUser(user);
                    Executing(context);
                }
                catch (Exception e)
                {
                    Logger.Error($"作业：{Name}执行异常", e);
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
                    .WithIdentity(JobKey.Name, JobKey.Group);
        }

        /// <summary>
        /// 计划生成
        /// </summary>
        public IScheduleBuilder ScheduleBuilder;
        public virtual TriggerBuilder GetTriggerBuilder()
        {
            if (ScheduleBuilder == null)
            {
                return null;
            }

            return TriggerBuilder
                    .Create()
                    .WithIdentity(JobKey.Name, JobKey.Group)
                    .WithSchedule(ScheduleBuilder)
                    .StartAt(SystemTime.UtcNow().AddSeconds(5));
        }
    }
}
