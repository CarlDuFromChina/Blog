using Sixpence.ORM.Broker;
using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.idea
{
    public class IdeaSerivice : EntityService<idea>
    {
        #region 构造函数
        public IdeaSerivice()
        {
            _context = new EntityContext<idea>();
        }

        public IdeaSerivice(IPersistBroker broker)
        {
            _context = new EntityContext<idea>(broker);
        }
        #endregion

    }
}
