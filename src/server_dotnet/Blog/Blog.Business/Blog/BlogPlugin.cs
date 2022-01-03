using Blog.Draft;
using Sixpence.Common.Utils;
using Sixpence.ORM.Broker;
using Sixpence.ORM.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Business.Blog
{
    public class BlogPlugin : IPersistBrokerPlugin
    {
        public void Execute(PersistBrokerPluginContext context)
        {
            var entity = context.Entity as blog;
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                case EntityAction.PostUpdate:
                    var id = context.Entity.Id;
                    var sql = @"
UPDATE sys_file
SET objectid = @objectid
WHERE sys_fileid = @id
";
                    var paramList = new Dictionary<string, object>()
                    {
                        { "@id", entity.surfaceid },
                        { "@objectid", id }
                    };
                    context.Broker.Execute(sql, paramList);
                    new DraftService(context.Broker).DeleteDataByBlogId(id); // 删除草稿
                    MemoryCacheUtil.RemoveCacheItem(id);
                    break;
                default:
                    break;
            }
        }
    }
}
