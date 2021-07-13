using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WeChat.Robot
{
    public class RobotService : EntityService<robot>
    {
        #region 构造函数
        public RobotService()
        {
            _cmd = new EntityCommand<robot>();
        }

        public RobotService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<robot>(Broker);
        }
        #endregion
    }
}
