using Blog.Business.Post.Sync.Juejin.Model;
using Blog.Core.Module.SysConfig;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sixpence.Common;
using Sixpence.Common.Utils;
using Sixpence.ORM.EntityManager;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Business.Post.Sync.Juejin
{
    public class SyncBlog2Juejin : ISyncPost
    {
        public void Execute(string id, object param)
        {
            var manager = EntityManagerFactory.GetManager();
            var service = new JuejinService();
            var data = manager.QueryFirst<post>(id);

            var draft = service.CreateDraft(data, JsonConvert.DeserializeObject<JuejinSyncDto>(param?.ToString()));
            service.PublishDarft(draft.id);
        }
    }
}
