using Sixpence.EntityFramework.Broker;
using Sixpence.EntityFramework.Entity;
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
            _context = new EntityContext<robot>();
        }

        public RobotService(IPersistBroker broker)
        {
            _context = new EntityContext<robot>(Broker);
        }
        #endregion
    }
}
