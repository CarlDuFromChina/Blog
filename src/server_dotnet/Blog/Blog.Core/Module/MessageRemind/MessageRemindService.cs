using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Module.MessageRemind
{
    public class MessageRemindService : EntityService<message_remind>
    {
        #region 构造函数
        public MessageRemindService()
        {
            _cmd = new EntityCommand<message_remind>();
        }

        public MessageRemindService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<message_remind>(broker);
        }
        #endregion
    }
}
