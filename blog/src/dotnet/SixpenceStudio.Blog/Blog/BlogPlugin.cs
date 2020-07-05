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
                    var sql = @"
UPDATE sys_file SET objectid = @objectid WHERE sys_fileid in (in@ids)
";
                    var entity = context.Entity;
                    var id = entity.GetAttributeValue("Id")?.ToString();
                    var ids = GetType().GetProperty("images").GetValue(entity) as List<string>;
                    if (ids == null)
                    {
                        ids = new List<string>();
                    }
                    ids.Add(id);
                    context.Broker.Execute(sql, new Dictionary<string, object>() { { "@objectid", id }, { "in@ids", string.Join(",", ids) } });
                    break;
                default:
                    break;
            }
        }
    }
}
