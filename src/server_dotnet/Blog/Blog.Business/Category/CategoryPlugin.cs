using Sixpence.Web.Module.SysMenu;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Category
{
    public class CategoryPlugin : IEntityManagerPlugin
    {
        public void Execute(EntityManagerPluginContext context)
        {
            var data = context.Entity as category;
            switch (context.Action)
            {
                case EntityAction.PostCreate:
                case EntityAction.PostUpdate:
                    CreateOrUpdateMenu(context.EntityManager, data);
                    break;
                case EntityAction.PostDelete:
                    {
                        var menu = context.EntityManager.QueryFirst<sys_menu>("SELECT * FROM sys_menu WHERE router = @code", new Dictionary<string, object>() { { "@code", $"post/{context.Entity.GetAttributeValue<string>("code")}" } });
                        if (menu != null)
                        {
                            context.EntityManager.Delete(menu);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void CreateOrUpdateMenu(IEntityManager manager, category data)
        {
            var menu = manager.QueryFirst<sys_menu>("SELECT * FROM sys_menu WHERE router = @code", new Dictionary<string, object>() { { "@code", $"post/{data.code}" } });
            if (menu != null)
            {
                menu.menu_index = data.index;
                menu.name = data.name;
                manager.Update(menu);
            }
            else
            {
                menu = new sys_menu()
                {
                    id = Guid.NewGuid().ToString(),
                    name = data.name,
                    parentid = "8201EFED-76E2-4CD1-A522-4803D52D4D92",
                    parentid_name = "博客管理",
                    router = $"post/{data.code}",
                    menu_index = data.index,
                    statecode = true,
                    statecode_name = "启用"
                };
                manager.Create(menu);
            }
        }
    }
}
