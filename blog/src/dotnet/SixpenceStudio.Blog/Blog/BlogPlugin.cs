using SixpenceStudio.Blog.Draft;
using SixpenceStudio.Platform.Command;
using SixpenceStudio.Platform.Data;
using System.Collections.Generic;
using System.Linq;

namespace SixpenceStudio.Blog.Blog
{
    public class BlogPlugin : IEntityActionPlugin
    {
        public void Execute(Context context)
        {
            switch (context.Action)
            {
                case Action.PostCreate:
                case Action.PostUpdate:
                    var id = context.Entity.Id;
                    new DraftService(context.Broker).DeleteDataByBlogId(id); // 删除草稿
                    break;
                default:
                    break;
            }
        }
    }
}
