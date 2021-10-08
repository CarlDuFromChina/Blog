using Sixpence.EntityFramework.Entity;
using Blog.Core.Job;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sixpence.EntityFramework.Broker;

namespace Blog.WeChat.RobotMessageTask
{
    public class RobotMessageTaskPlugin : IPersistBrokerPlugin
    {
        public void Execute(PersistBrokerPluginContext context)
        {
            if (context.EntityName != "robot_message_task")
            {
                return;
            }

            var obj = context.Entity as robot_message_task;
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                case EntityAction.PostUpdate:
                    JobHelpers.RegisterJob(new RobotMessageTaskJob(obj.name, obj.robotidName, obj.runtime), obj, obj.job_state.ToTriggerState());
                    break;
                case EntityAction.PreCreate:
                    var jobState = new RobotMessageTaskJob().DefaultTriggerState.ToSelectOption();
                    obj.job_state = jobState.Value.ToString();
                    obj.job_stateName = jobState.Name;
                    break;
                case EntityAction.PreUpdate:
                case EntityAction.PostDelete:
                    JobHelpers.DeleteJob(obj.name, obj.robotidName);
                    break;
                default:
                    break;
            }
        }
    }
}
