using Blog.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WeChat.RobotMessageTask
{
    public class RobotMessageTaskController : EntityBaseController<robot_message_task, RobotMessageTaskService>
    {
        [HttpGet, Route("RunOnce")]
        public void RunOnce(string id)
        {
            new RobotMessageTaskService().RunOnce(id);
        }

        [HttpGet, Route("PauseJob")]
        public void PauseJob(string id)
        {
            new RobotMessageTaskService().PauseJob(id);
        }

        [HttpGet, Route("ResumeJob")]
        public void ResumeJob(string id)
        {
            new RobotMessageTaskService().ResumeJob(id);
        }
    }
}
