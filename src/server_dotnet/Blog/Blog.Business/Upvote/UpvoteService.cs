using Blog.Blog;
using Blog.Comments;
using Blog.Core.Data;
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

        /// <summary>
        /// 点赞评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string UpvoteComment(string id)
        {
            var obj = Broker.Retrieve<comments>(id);
            var data = new upvote()
            {
                Id = Guid.NewGuid().ToString(),
                name = "评论点赞",
                objectId = id,
                objectIdName = obj.name,
                object_ownerid = obj.createdBy,
                object_owneridName = obj.createdByName,
                object_type = "评论"
            };
            return Broker.Create(data);
        }

        /// <summary>
        /// 点赞文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string UpvoteBlog(string id)
        {
            var obj = Broker.Retrieve<blog>(id);
            var data = new upvote()
            {
                Id = Guid.NewGuid().ToString(),
                name = "文章点赞",
                objectId = id,
                objectIdName = obj.name,
                object_ownerid = obj.createdBy,
                object_owneridName = obj.createdByName,
                object_type = "文章"
            };
            return Broker.Create(data);
        }
    }
}
