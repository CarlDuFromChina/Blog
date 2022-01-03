using Sixpence.ORM.Entity;
using Blog.Core.Job;
using Quartz;
using Sixpence.Common;
using System.Collections.Generic;
using System.Linq;
using Sixpence.ORM.Broker;
using Sixpence.Common.IoC;

namespace Blog.Core.Module.Job
{
    public class JobService : EntityService<job>
    {
        #region 构造函数
        public JobService()
        {
            _context = new EntityContext<job>();
        }

        public JobService(IPersistBroker broker)
        {
            _context = new EntityContext<job>(broker);
        }
        #endregion

        /// <summary>
        /// 删除job列表不存在的job
        /// </summary>
        /// <param name="jobNameList"></param>
        public void DeleteJob(List<string> jobNameList)
        {
            var sql = @"SELECT * FROM job WHERE name NOT IN (in@names)";
            var dataList = Broker.RetrieveMultiple<job>(sql, new Dictionary<string, object>() { { "in@names", string.Join(",", jobNameList) } });
            base.DeleteData(dataList.Select(item => item.Id).ToList());
        }

        /// <summary>
        /// 查询所有的job
        /// </summary>
        /// <returns></returns>
        public IList<job> GetDataList()
        {
            var sql = @"
SELECT * FROM job
ORDER BY name
";
            var dataList = Broker.RetrieveMultiple<job>(sql);
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
                    JobHelpers.RunOnceNow(job.Name, job.GetType().Namespace);
                }
            });
        }
    }
}