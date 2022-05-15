using Blog.Comments;
using Blog.Core.Auth;

using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Upvote
{
    public class UpvoteService : EntityService<upvote>
    {
        #region 构造函数
        public UpvoteService() : base() { }

        public UpvoteService(IEntityManager manager) : base(manager) { }
        #endregion

        public bool IsUp(string objectid)
        {
            var sql = @"
SELECT * FROM upvote
WHERE objectid = @objectid AND created_by = @ownerid";
            var data = Manager.QueryFirst<upvote>(sql, new Dictionary<string, object>() { { "@objectid", objectid }, { "@ownerid", UserIdentityUtil.GetCurrentUserId() } });
            return data != null;
        }
    }
}
