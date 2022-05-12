using Blog.Draft;
using Sixpence.Common.Utils;
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Business.Blog
{
    public class BlogPlugin : IEntityManagerPlugin
    {
        public void Execute(EntityManagerPluginContext context)
        {
            var entity = context.Entity as blog;
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                case EntityAction.PostUpdate:
                    var id = context.Entity.PrimaryKey.Value;
                    new DraftService(context.EntityManager).DeleteDataByBlogId(id); // 删除草稿
                    MemoryCacheUtil.RemoveCacheItem(id);
                    break;
                default:
                    break;
            }
        }
    }
}
