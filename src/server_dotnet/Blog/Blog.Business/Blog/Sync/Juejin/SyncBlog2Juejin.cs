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
        public IEntityManager Manager { get; set; }

        public void Execute(string id)
        {
            if (Manager == null)
                Manager = EntityManagerFactory.GetManager();

            var data = Manager.QueryFirst<blog>(id);
            var cookie = new SysConfigService().GetValue("juejin_cookie")?.ToString();

            AssertUtil.CheckIsNullOrEmpty<SpException>(cookie, "博客同步到掘金需要先设置Cookie信息", "52669331-ED29-4E9B-8D2C-B5F671852BC1");

            var draft = CreateDraft(data, cookie);
            PublishDarft(draft.id, cookie);
        }

        /// <summary>
        /// 创建草稿
        /// </summary>
        /// <param name="blog"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        private JuejinDraftCreateResponseData CreateDraft(blog blog, string cookie)
        {
            var draft = new Draft()
            {
                title = blog.title,
                mark_content = blog.content,
                brief_content = blog.brief,
                tag_ids = new List<string>()
            };

            var blogTags = blog.tags?.ToObject<List<string>>();
            if (!blogTags.IsEmpty())
            {
                QueryTags().ForEach(item =>
                {
                    var tag = blogTags.Find(e => e.Equals(item.tag.tag_name, System.StringComparison.OrdinalIgnoreCase));
                    if (!string.IsNullOrEmpty(tag))
                    {
                        draft.tag_ids.Add(item.tag_id);
                    }
                });
            }

            var headers = new Dictionary<string, string>() { { "cookie", cookie } };
            var response = HttpUtil.Post("https://api.juejin.cn/content_api/v1/article_draft/create?aid=2608&uuid=6980253040042001932", JsonConvert.SerializeObject(draft), headers);
            var resp = JsonConvert.DeserializeObject<JuejinResponse<JuejinDraftCreateResponseData>>(response);

            AssertUtil.CheckBoolean<SpException>(resp.err_no != 0, $"草稿创建失败，错误信息：{resp.err_msg}", "52669331-ED29-4E9B-8D2C-B5F671852BC1");

            return resp.data;
        }

        /// <summary>
        /// 发布草稿
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cookie"></param>
        private void PublishDarft(string id, string cookie)
        {
            var param = new
            {
                draft_id = "",
                sync_to_org = false,
                column_ids = new List<string>()
            };
            var headers = new Dictionary<string, string>() { { "cookie", cookie } };
            var response = HttpUtil.Post("/content_api/v1/article/publish?aid=2608&uuid=6897189016987272712", JsonConvert.SerializeObject(param), headers);
            var resp = JsonConvert.DeserializeObject<JuejinResponse<JuejinDraftPublish>>(response);

            AssertUtil.CheckBoolean<SpException>(resp.err_no != 0, $"博客发布失败，错误信息：{resp.err_msg}", "52669331-ED29-4E9B-8D2C-B5F671852BC1");
        }

        /// <summary>
        /// 查询标签
        /// </summary>
        /// <returns></returns>
        private List<JuejinTagInfo> QueryTags()
        {
            var response = HttpUtil.Get("https://api.juejin.cn/tag_api/v1/query_tag_list?aid=2608&uuid=6897189016987272712");
            var resp = JsonConvert.DeserializeObject<JuejinResponse<List<JuejinTagInfo>>>(response);
            return resp.data;
        }
    }
}
