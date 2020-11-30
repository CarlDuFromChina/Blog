using SixpenceStudio.Core;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.FriendBlog
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

        public override IList<EntityView> GetViewList()
        {
            return new List<EntityView>()
            {
                new EntityView()
                {
                    Sql = $@"
SELECT
	friend_blogid,
	name,
	content,
	first_picture,
	createdon,
	modifiedon,
	author,
	description
FROM friend_blog
WHERE 1=1
",
                    ViewId = "F7A9536A-81E9-494F-9DF0-4AF323F1D5BC",
                    CustomFilter = new List<string> { "title" },
                    Name = "谢振国的博客",
                    OrderBy = "modifiedon desc"
                }
            };
        }
    }
}
