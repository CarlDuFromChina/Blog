using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.RecommendBlog
{
    public class RecommendBlogService : EntityService<recommend_blog>
    {
        #region 构造函数
        public RecommendBlogService()
        {
            _cmd = new EntityCommand<recommend_blog>();
        }

        public RecommendBlogService(IPersistBroker broker)
        {
            _cmd = new EntityCommand<recommend_blog>(broker);
        }
        #endregion


    }
}
