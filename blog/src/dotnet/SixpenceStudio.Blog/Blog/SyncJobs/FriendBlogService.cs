using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.Blog.SyncJobs
{
    public class FriendBlogService : EntityService<friend_blog>
    {
        #region 构造函数
        public FriendBlogService()
        {
            this._cmd = new EntityCommand<friend_blog>();
        }

        public FriendBlogService(IPersistBroker broker)
        {
            this._cmd = new EntityCommand<friend_blog>(broker);
        }
        #endregion
    }
}
