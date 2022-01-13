using Quartz;
using Blog.WeChat.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Job;
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;

namespace Blog.WeChat.RobotMessageTask
{
    public class RobotMessageTaskJob : DynamicJobBase
    {
        public RobotMessageTaskJob() { }
        public RobotMessageTaskJob(string name, string group, string cron) : base(name, group, cron) { }

        public override void Executing(IJobExecutionContext context)
        {
            var entity = context.JobDetail.JobDataMap.Get("Entity") as robot_message_task;
            var Manager = EntityManagerFactory.GetManager();
            var robot = Manager.QueryFirst<robot>(entity.robotid);
            try
            {
                var client = RobotClientFacotry.GetClient(robot.robot_type, robot.hook);
                client.SendTextMessage(entity.content);
                Logger.Debug($"机器人[{robot.name}]发送了一条消息[{entity.content}]");
            }
            catch (Exception e)
            {
                Logger.Error($"机器人[{robot.name}]的消息[{entity.name}]发送失败", e);
                entity.job_state = "3";
                entity.job_stateName = "错误";
                Manager.Update(entity);
            }
        }
    }
}
