using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core;
using Blog.Core.Job;
using Sixpence.Common.Logging;
using Blog.WeChat.RobotMessageTask;
using Sixpence.Common;

namespace Blog.WeChat
{
    public class Startup
    {
        public virtual void Configure()
        {
            var logger = LoggerFactory.GetLogger("startup");
            logger.Info("正在启动机器人作业...");
            try
            {
                new RobotMessageTaskService().GetAllData().Each(item =>
                {
                    JobHelpers.RegisterJob(new RobotMessageTaskJob(item.name, item.robotid_name, item.runtime), item, item.job_state.ToTriggerState());
                    logger.Info($"机器人[{item.robotid_name}]的[{item.name}]作业已启动");
                });
                logger.Info("机器人启动完毕");
            }
            catch (Exception e)
            {
                logger.Error("注册机器人失败", e);
                throw e;
            }
        }
    }
}
