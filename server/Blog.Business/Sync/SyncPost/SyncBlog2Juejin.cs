using Blog.Business.Entity;
using Blog.Business.Model;
using Blog.Business.Service;
using Newtonsoft.Json;
using Sixpence.ORM.EntityManager;

namespace Blog.Business.Sync.SyncPost
{
    public class SyncBlog2Juejin : ISyncPost
    {
        public void Execute(string id, object param)
        {
            var manager = EntityManagerFactory.GetManager();
            var service = new JuejinService();
            var data = manager.QueryFirst<Post>(id);

            var draft = service.CreateDraft(data, JsonConvert.DeserializeObject<JuejinSyncDto>(param?.ToString()));
            service.PublishDarft(draft.id);
        }
    }
}
