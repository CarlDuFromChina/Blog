using SixpenceStudio.Blog.Draft;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.SysFile;
using System.Collections.Generic;
using System.Linq;

namespace SixpenceStudio.Blog.Blog
{
    public class BlogPlugin : IEntityActionPlugin
    {
        public void Execute(Context context)
        {
            var entity = context.Entity as blog;
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                case EntityAction.PostUpdate:
                    var id = context.Entity.Id;
                    var sql = @"
UPDATE sys_file SET objectid = @objectid WHERE sys_fileid = @id
";
                    var paramList = new Dictionary<string, object>()
                    {
                        { "@id", entity.surfaceid },
                        { "@objectid", id }
                    };
                    context.Broker.Execute(sql,  paramList);
                    new DraftService(context.Broker).DeleteDataByBlogId(id); // 删除草稿
                    break;
                case EntityAction.PostDelete:
                    new SysFileService(context.Broker).DeleteData(new List<string>() { entity.surfaceid });
                    break;
                default:
                    break;
            }
        }
    }
}
