using Blog.Core.Job;
using Quartz;
using Sixpence.Common;
using System.Collections.Generic;
using System.Linq;
using Sixpence.Common.IoC;
using Sixpence.ORM.EntityManager;
using Sixpence.Common.Utils;
using Blog.Core.Auth;

namespace Blog.Core.Module.Job
{
    public class JobService
    {
        private IEntityManager Manager = EntityManagerFactory.GetManager();

        /// <summary>
        /// 查询所有的job
        /// </summary>
        /// <returns></returns>
        public IList<job> GetDataList()
        {
            var sql = @"
SELECT
	qjd.job_name AS name,
	qjd.description,
	qt.prev_fire_time AS prev_fire_time_ticks,
	qt.next_fire_time AS next_fire_time_ticks,
	qt.trigger_state,
	qct.cron_expression
FROM qrtz_job_details AS qjd
LEFT JOIN qrtz_triggers AS qt ON qjd.job_name = qt.job_name
LEFT JOIN qrtz_cron_triggers AS qct ON qt.trigger_name = qct.trigger_name
";
            var dataList = Manager.Query<job>(sql).ToList();
            return dataList;
        }

        /// <summary>
        /// 运行一次job
        /// </summary>
        /// <param name="name"></param>
        public void RunOnceNow(string name)
        {
            ServiceContainer.ResolveAll<IJob>().Each(item =>
            {
                var job = item as JobBase;
                if (job.Name == name)
                {
                    var paramList = new Dictionary<string, object>() { { "User", UserIdentityUtil.GetCurrentUser() } };
                    JobHelpers.RunOnceNow(job.Name, job.GetType().Namespace, paramList);
                }
            });
        }

        /// <summary>
        /// 暂停Job
        /// </summary>
        /// <param name="jobName"></param>
        public void Pause(string jobName)
        {
            var job = ServiceContainer.ResolveAll<IJob>().FirstOrDefault(item => (item as JobBase).Name == jobName) as JobBase;
            AssertUtil.CheckNull<SpException>(job, $"未找到名为[{jobName}]作业", "");
            JobHelpers.PauseJob(job.Name, job.GetType().Namespace);
        }

        /// <summary>
        /// 重启job
        /// </summary>
        /// <param name="jobName"></param>
        public void Resume(string jobName)
        {
            var job = ServiceContainer.ResolveAll<IJob>().FirstOrDefault(item => (item as JobBase).Name == jobName) as JobBase;
            AssertUtil.CheckNull<SpException>(job, $"未找到名为[{jobName}]作业", "");
            JobHelpers.ResumeJob(job.Name, job.GetType().Namespace);
        }
    }
}