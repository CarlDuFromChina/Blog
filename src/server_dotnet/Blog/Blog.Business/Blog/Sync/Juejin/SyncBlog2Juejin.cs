using Blog.Business.Blog.Sync.Juejin.Model;
using Blog.Core.Module.SysConfig;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sixpence.Common;
using Sixpence.Common.Utils;
using Sixpence.ORM.EntityManager;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Business.Blog.Sync.Juejin
{
    public class SyncBlog2Juejin : ISyncBlog
    {
        public void Execute(string id, object param)
        {
            var manager = EntityManagerFactory.GetManager();
            var service = new JuejinService();
            var data = manager.QueryFirst<blog>(id);

            var draft = service.CreateDraft(data, param as JuejinSyncDto);
            service.PublishDarft(draft.id);
        }
    }
}
