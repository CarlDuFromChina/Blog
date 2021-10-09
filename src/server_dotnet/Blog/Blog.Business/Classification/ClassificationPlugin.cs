using Blog.Core.Module.SysMenu;
using Sixpence.EntityFramework.Broker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Classification
{
    public class ClassificationPlugin : IPersistBrokerPlugin
    {
        public void Execute(PersistBrokerPluginContext context)
        {
            var data = context.Entity as classification;
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                case EntityAction.PostUpdate:
                    CreateOrUpdateMenu(context.Broker, data);
                    break;
                case EntityAction.PostDelete:
                    {
                        var menu = context.Broker.Retrieve<sys_menu>("SELECT * FROM sys_menu WHERE router = @code", new Dictionary<string, object>() { { "@code", context.Entity.GetAttributeValue<string>("code")} });
                        if (menu != null)
                        {
                            context.Broker.Delete(menu);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void CreateOrUpdateMenu(IPersistBroker broker, classification data)
        {
            var menu = broker.Retrieve<sys_menu>("SELECT * FROM sys_menu WHERE router = @code", new Dictionary<string, object>() { { "@code", $"blogs/{data.code}" } });
            if (menu != null)
            {
                menu.menu_Index = data.index;
                menu.name = data.name;
                broker.Update(menu);
            }
            else
            {
                menu = new sys_menu()
                {
                    Id = Guid.NewGuid().ToString(),
                    name = data.name,
                    parentid = "8201EFED-76E2-4CD1-A522-4803D52D4D92",
                    parentIdName = "博客管理",
                    router = $"blogs/{data.code}",
                    menu_Index = data.index,
                    stateCode = 1,
                    stateCodeName = "启用"
                };
                broker.Create(menu);
            }
        }
    }
}
