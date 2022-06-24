using Blog.Draft;
using Sixpence.Common.Utils;
using Sixpence.ORM.EntityManager;

namespace Blog.Business.Post
{
    public class PostPlugin : IEntityManagerPlugin
    {
        public void Execute(EntityManagerPluginContext context)
        {
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                case EntityAction.PostUpdate:
                    var id = context.Entity.GetPrimaryColumn().Value;
                    new DraftService(context.EntityManager).DeleteDataByPostId(id); // 删除草稿
                    MemoryCacheUtil.RemoveCacheItem(id);
                    break;
                default:
                    break;
            }
        }
    }
}
