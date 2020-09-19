using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.Blog.FriendBlog
{
    public class FriendBlogService : EntityService<friend_blog>
    {
        #region 构造函数
        public FriendBlogService()
        {
            // 使用从库
            var broker = new PersistBroker(true);
            this._cmd = new EntityCommand<friend_blog>(broker);
        }

        public FriendBlogService(IPersistBroker broker)
        {
            this._cmd = new EntityCommand<friend_blog>(broker);
        }
        #endregion

        public override IList<EntityView<friend_blog>> GetViewList()
        {
            return new List<EntityView<friend_blog>>()
            {
                new EntityView<friend_blog>()
                {
                    Sql = $@"
SELECT
	friend_blogid,
	name,
	content,
	first_picture,
	createdon,
	modifiedon,
	author
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
