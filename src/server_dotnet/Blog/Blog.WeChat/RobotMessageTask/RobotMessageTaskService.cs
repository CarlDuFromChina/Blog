using Blog.Core.Auth;
using Blog.Core.Data;
using Blog.Core.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WeChat.RobotMessageTask
{
    public class RobotMessageTaskService : EntityService<robot_message_task>
    {
        #region 构造函数
        public RobotMessageTaskService()
        {
            _cmd = new EntityCommand<robot_message_task>();
        }

        public RobotMessageTaskService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<robot_message_task>(broker);
        }
        #endregion

        public new IEnumerable<robot_message_task> GetAllData()
        {
            return _cmd.GetAllEntity();
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
