using Blog.Core.Module.SysConfig;
using Newtonsoft.Json;
using Sixpence.Common;
using Sixpence.Common.Utils;
using Sixpence.ORM.EntityManager;
using System.Collections.Generic;

namespace Blog.Business.Blog.Sync.Juejin
{
    public class SyncBlog2Juejin : ISyncBlog
    {
        public IEntityManager Manager { get; set; }

        public void Execute(string id)
        {
            if (Manager == null)
                Manager = EntityManagerFactory.GetManager();

            var data = Manager.QueryFirst<blog>(id);
            var cookie = new SysConfigService().GetValue("juejin_cookie")?.ToString();
            AssertUtil.CheckIsNullOrEmpty<SpException>(cookie, "博客同步到掘金需要先设置Cookie信息", "52669331-ED29-4E9B-8D2C-B5F671852BC1");
            CreateDraft(data, cookie);
        }

        private void CreateDraft(blog blog, string cookie)
        {
            var draft = new Draft() { title = blog.title, mark_content = blog.content };
            var postData = new Dictionary<string, string>();
            postData.Add("cookie", cookie);

            var response = HttpUtil.Post("https://api.juejin.cn/content_api/v1/article_draft/create?aid=2608&uuid=6980253040042001932", JsonConvert.SerializeObject(draft), postData);
            var resp = JsonConvert.DeserializeObject<JuejinResponse<JuejinDraftCreateResponseData>>(response);

            AssertUtil.CheckBoolean<SpException>(resp.err_no != 0, $"草稿创建失败，错误信息：{resp.err_msg}", "52669331-ED29-4E9B-8D2C-B5F671852BC1");
        }
    }
}
