using Blog.Core.Data;
using Blog.Core.Module.MessageRemind;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                default:
                    break;
            }
        }
    }
}
