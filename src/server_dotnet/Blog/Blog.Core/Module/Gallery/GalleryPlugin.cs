using Sixpence.ORM.Entity;
using Blog.Core.Store.SysFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sixpence.ORM.EntityManager;

namespace Blog.Core.Module.Gallery
{
    public class GalleryPlugin : IEntityManagerPlugin
    {
        public void Execute(EntityManagerPluginContext context)
        {
            var obj = context.Entity as gallery;
            switch (context.Action)
            {
                case EntityAction.PreCreate:
                    break;
                case EntityAction.PreUpdate:
                    break;
                case EntityAction.PostCreate:
                case EntityAction.PostUpdate:
                    var data1 = context.EntityManager.QueryFirst<sys_file>(obj.previewid);
                    var data2 = context.EntityManager.QueryFirst<sys_file>(obj.imageid);
                    data1.objectId = obj.id;
                    data2.objectId = obj.id;
                    context.EntityManager.Update(data1);
                    context.EntityManager.Update(data2);
                    break;
                case EntityAction.PreDelete:
                    break;
                case EntityAction.PostDelete:
                    break;
                default:
                    break;
            }
        }
    }
}
