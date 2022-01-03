using Blog.Comments;
using Blog.Core.Auth;
using Sixpence.ORM.Broker;
using Sixpence.ORM.Entity;
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

        public bool IsUp(string objectid)
        {
            var sql = @"
SELECT * FROM upvote
WHERE objectid = @objectid AND createdby = @ownerid";
            var data = Broker.Retrieve<upvote>(sql, new Dictionary<string, object>() { { "@objectid", objectid }, { "@ownerid", UserIdentityUtil.GetCurrentUserId() } });
            return data != null;
        }
    }
}
