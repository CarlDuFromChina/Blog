using Sixpence.Web;
using Sixpence.ORM.Entity;
using Sixpence.Web.Module.MessageRemind;
using Newtonsoft.Json;
using Sixpence.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sixpence.ORM.EntityManager;

namespace Blog.Business.Upvote
{
    public class UpvotePlugin : IEntityManagerPlugin
    {
        public void Execute(EntityManagerPluginContext context)
        {
            var entity = context.Entity;
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                    {
                        var data = entity as upvote;
                        var message = new message_remind()
                        {
                            id = Guid.NewGuid().ToString(),
                            name = $"{data.name}消息提醒",
                            is_read = false,
                            receiverId = data.object_ownerid,
                            receiverId_name = data.object_ownerid_name,
                            message_type = "upvote",
                            content = JsonConvert.SerializeObject(data)
                        };
                        context.EntityManager.Create(message);
                    }
                    break;
                case EntityAction.PreDelete:
                    {
                        var data = context.EntityManager.QueryFirst<upvote>(context.Entity.GetPrimaryColumn().Value);
                        var sql = @"
SELECT * FROM (
	SELECT *, content::jsonb ->> 'objectId' AS objectid
	FROM message_remind WHERE 1=1  AND message_type = 'upvote'
) t1
WHERE t1.objectid = @id";
                        var dataList = context.EntityManager.Query<message_remind>(sql, new Dictionary<string, object>() { { "@id", data.objectId } });
                        dataList.Each(item => context.EntityManager.Delete(item));
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
