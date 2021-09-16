using Blog.Core;
using Sixpence.EntityFramework.Entity;
using Blog.Core.Module.MessageRemind;
using Newtonsoft.Json;
using Sixpence.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sixpence.EntityFramework.Broker;

namespace Blog.Business.Upvote
{
    public class UpvotePlugin : IPersistBrokerPlugin
    {
        public void Execute(PersistBrokerPluginContext context)
        {
            var entity = context.Entity;
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                    {
                        var data = entity as upvote;
                        var message = new message_remind()
                        {
                            Id = Guid.NewGuid().ToString(),
                            name = $"{data.name}消息提醒",
                            is_read = false,
                            is_readName = "否",
                            receiverId = data.object_ownerid,
                            receiverIdName = data.object_owneridName,
                            message_type = "upvote",
                            content = JsonConvert.SerializeObject(data)
                        };
                        context.Broker.Create(message);
                    }
                    break;
                case EntityAction.PreDelete:
                    {
                        var data = context.Broker.Retrieve<upvote>(context.Entity.Id);
                        var sql = @"
SELECT * FROM (
	SELECT *, content::jsonb ->> 'objectId' AS objectid
	FROM message_remind WHERE 1=1  AND message_type = 'upvote'
) t1
WHERE t1.objectid = @id";
                        var dataList = context.Broker.RetrieveMultiple<message_remind>(sql, new Dictionary<string, object>() { { "@id", data.objectId } });
                        dataList.Each(item => context.Broker.Delete(item));
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
