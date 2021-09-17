using Blog.Blog;
using Blog.Comments;
using Sixpence.EntityFramework.Broker;
using Sixpence.EntityFramework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Upvote
{
    public class UpvoteService : EntityService<upvote>
    {
        #region 构造函数
        public UpvoteService()
        {
            this._context = new EntityContext<upvote>();
        }

        public UpvoteService(IPersistBroker broker)
        {
            this._context = new EntityContext<upvote>(broker);
        }
        #endregion
    }
}
