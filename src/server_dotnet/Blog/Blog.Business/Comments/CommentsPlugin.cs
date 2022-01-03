using Sixpence.ORM.Entity;
using Blog.Core.Module.MessageRemind;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sixpence.ORM.Broker;

namespace Blog.Comments
{
    public class CommentsPlugin : IPersistBrokerPlugin
    {
        public void Execute(PersistBrokerPluginContext context)
        {
            var broker = context.Broker;
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                    {
                        var data = context.Entity as comments;
                        var messageRemind = new message_remind()
                        {
                            Id = Guid.NewGuid().ToString(),
                            name = $"{data.name}消息提醒",
                            is_read = false,
                            is_readName = "否",
                            content = JsonConvert.SerializeObject(data),
                            message_type = data.comment_type
                        };
                        if (data.comment_type == "comment")
                        {
                            messageRemind.receiverId = data.object_ownerid;
                            messageRemind.receiverIdName = data.object_owneridName;
                        }
                        else if (data.comment_type == "reply")
                        {
                            messageRemind.receiverId = data.replyid;
                            messageRemind.receiverIdName = data.replyidName;
                        }
                        broker.Create(messageRemind);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
