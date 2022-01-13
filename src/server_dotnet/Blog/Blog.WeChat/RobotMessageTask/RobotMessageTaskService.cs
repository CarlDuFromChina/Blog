using Blog.Core.Auth;
using Sixpence.ORM.Entity;
using Blog.Core.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Extensions;
using Sixpence.ORM.EntityManager;

namespace Blog.WeChat.RobotMessageTask
{
    public class RobotMessageTaskService : EntityService<robot_message_task>
    {
        #region 构造函数
        public RobotMessageTaskService() : base() { }

        public RobotMessageTaskService(IEntityManager manager) : base(manager) { }
        #endregion

        public new IEnumerable<robot_message_task> GetAllData()
        {
            return Repository.GetAllEntity();
        }

        public void RunOnce(string id)
        {
            var data = GetData(id);
            var paramList = new Dictionary<string, object>()
            {
                { "Entity", data },
                { "User", UserIdentityUtil.GetAdmin() }
            };

            JobHelpers.RunOnceNow(data.name, data.robotidName, paramList);
            var jobState = JobHelpers.GetJobStatus(data.name, data.robotidName).ToSelectOption();
            data.job_state = jobState.Value.ToString();
            data.job_stateName = jobState.Name;
            UpdateData(data);
        }

        public void PauseJob(string id)
        {
            var data = GetData(id);
            JobHelpers.PauseJob(data.name, data.robotidName);
            data.job_state = "1";
            data.job_stateName = "暂停";
            UpdateData(data);
        }

        public void ResumeJob(string id)
        {
            var data = GetData(id);
            JobHelpers.ResumeJob(data.name, data.robotidName);
            data.job_state = "0";
            data.job_stateName = "正常";
            UpdateData(data);
        }
    }
}
