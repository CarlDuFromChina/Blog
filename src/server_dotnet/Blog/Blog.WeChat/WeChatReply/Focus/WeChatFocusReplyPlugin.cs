#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/8 16:32:01
Description：
********************************************************/
#endregion
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WeChat.WeChatReply.Focus
{
    public class WeChatFocusReplyPlugin : IEntityManagerPlugin
    {
        public void Execute(EntityManagerPluginContext context)
        {
            switch (context.Action)
            {
                case EntityAction.PreCreate:
                case EntityAction.PreUpdate:
                    var entity = context.Entity;
                    if (entity.GetAttributeValue("checked") == null)
                    {
                        context.Entity["checked"] = false;
                    }
                    if (entity.GetAttributeValue("wechat") == null)
                    {
                        context.Entity["wechat"] = WeChatConfig.Config.Appid;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
