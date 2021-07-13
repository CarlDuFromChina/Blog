using Blog.Core.Data;
using Blog.Core.Module.MessageRemind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                            name = "评论提醒",
                            object_id = data.Id,
                            object_idName = data.name,
                            object_type = "comments",
                            object_typeName = "评论",
                            is_read = false,
                            is_readName = "否",
                            content = $"您的博客有一条新的评论：{data.comment}"
                        };
                        broker.Create(messageRemind);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
