using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
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
        public RobotService() : base() { }

        public RobotService(IEntityManager manager) : base(manager) { }
        #endregion
    }
}
