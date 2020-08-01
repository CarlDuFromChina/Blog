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
                    var entity = context.Entity;
                    var id = context.Entity.Id;
                    var sql = @"
UPDATE sys_file SET objectid = @objectid WHERE sys_fileid = @id
";
                    var paramList = new Dictionary<string, object>()
                    {
                        { "@id", entity.GetType().GetProperty("imageId")?.GetValue(entity)?.ToString() },
                        { "@objectid", id }
                    };
                    context.Broker.Execute(sql,  paramList);
                    new DraftService(context.Broker).DeleteDataByBlogId(id); // 删除草稿
                    break;
                default:
                    break;
            }
        }
    }
}
