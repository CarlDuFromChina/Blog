using Sixpence.EntityFramework.Entity;
using Blog.Core.Store.SysFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sixpence.EntityFramework.Broker;

namespace Blog.Core.Module.Gallery
{
    public class GalleryPlugin : IPersistBrokerPlugin
    {
        public void Execute(PersistBrokerPluginContext context)
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
                    var data1 = context.Broker.Retrieve<sys_file>(obj.previewid);
                    var data2 = context.Broker.Retrieve<sys_file>(obj.imageid);
                    data1.objectId = obj.Id;
                    data2.objectId = obj.Id;
                    context.Broker.Update(data1);
                    context.Broker.Update(data2);
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
