using Newtonsoft.Json;
using Sixpence.Common;
using Sixpence.Common.Utils;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Business.Model;
using Blog.Business.Entity;
using Sixpence.Web.Service;
using Blog.Business.Config;
using Sixpence.Common.Logging;

namespace Blog.Business.Service
{
    public class JuejinService
    {
        public Dictionary<string, string> headers;
        private static volatile string juejinCookie = "";
        private static readonly object lockObj = new object();

        public JuejinService()
        {
            var configCookie = new SysConfigService().GetValue("juejin_cookie")?.ToString();
            headers = new Dictionary<string, string>() { { "cookie", GetJuejinCookie() ?? configCookie } };
        }

        public string GetJuejinCookie()
        {
            if (string.IsNullOrEmpty(juejinCookie))
            {
                lock (lockObj)
                {
                    if (string.IsNullOrEmpty(juejinCookie))
                    {
                        var config = ExternalSiteConfig.Config.Extricator;
                        var url = config.Host.TrimEnd('/');

                        if (!string.IsNullOrEmpty(url))
                        {
                            var sysConfig = new SysConfigService();
                            var user = sysConfig.GetValue("index_user")?.ToString();
                            try
                            {
                                juejinCookie = HttpUtil.Get($"{url}/api/juejin_cookie?token={config.Token}&user={user}");
                            }
                            catch (Exception ex)
                            {
                                LogUtil.Error(ex);
                            }
                        }
                    }
                }
            }

            return juejinCookie;
        }

        /// <summary>
        /// 获取API是否正常启用
        /// </summary>
        /// <returns></returns>
        public bool GetJuejinStatus()
        {
            if (headers.TryGetValue("cookie", out var value))
            {
                return !string.IsNullOrEmpty(value);
            }
            return false;
        }

        /// <summary>
        /// 查询分类
        /// </summary>
        /// <returns></returns>
        public List<JuejinCategory> QueryCategories()
        {
            if (!GetJuejinStatus()) return null;

            var param = new { };
            var response = HttpUtil.Post("https://api.juejin.cn/tag_api/v1/query_category_list?aid=2608&uuid=6980253040042001932", JsonConvert.SerializeObject(param), headers);
            var resp = JsonConvert.DeserializeObject<JuejinResponse<List<JuejinCategory>>>(response);
            if (resp.err_no != 0)
            {
                return null;
            }
            return resp.data;
        }


        /// <summary>
        /// 查询标签
        /// </summary>
        /// <returns></returns>
        public List<JuejinTagInfo> QueryTags(string keyWord = "")
        {
            if (!GetJuejinStatus()) return null;

            var param = new
            {
                cursor = "0",
                key_word = keyWord,
                limit = 10,
                sort_type = 1
            };

            var response = HttpUtil.Post("https://api.juejin.cn/tag_api/v1/query_tag_list", JsonConvert.SerializeObject(param), headers);
            var resp = JsonConvert.DeserializeObject<JuejinResponse<List<JuejinTagInfo>>>(response);
            if (resp.err_no != 0)
            {
                return null;
            }
            return resp.data;
        }

        /// <summary>
        /// 创建草稿
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        public JuejinDraftCreateResponseData CreateDraft(Post blog, JuejinSyncDto dto)
        {
            if (!GetJuejinStatus()) return null;

            AssertUtil.IsNullOrEmpty(dto.category_id, "博客分类不能为空");

            var draft = new DraftModel()
            {
                title = blog.title,
                mark_content = blog.content,
                brief_content = blog.brief,
                tag_ids = new List<string>(),
                category_id = dto.category_id
            };

            #region 匹配标签
            JsonConvert.DeserializeObject<List<string>>(blog?.tags?.ToString())
                .ForEach(tagName =>
                {
                    var datas = QueryTags(tagName); // 根据标签名查询标签
                    foreach (var item in datas)
                    {
                        // 再检查标签名是否吻合
                        if (tagName.Equals(item.tag.tag_name, StringComparison.OrdinalIgnoreCase))
                        {
                            draft.tag_ids.Add(item.tag_id);
                            return;
                        }
                    }
                });
            #endregion

            var response = HttpUtil.Post("https://api.juejin.cn/content_api/v1/article_draft/create?aid=2608&uuid=6980253040042001932", JsonConvert.SerializeObject(draft), headers);
            var resp = JsonConvert.DeserializeObject<JuejinResponse<JuejinDraftCreateResponseData>>(response);

            AssertUtil.IsTrue(resp.err_no != 0, $"草稿创建失败，错误信息：{resp.err_msg}");

            return resp.data;
        }

        /// <summary>
        /// 发布草稿
        /// </summary>
        /// <param name="id">草稿id</param>
        public void PublishDarft(string id)
        {
            if (!GetJuejinStatus()) return;

            var param = new
            {
                draft_id = id,
                sync_to_org = false,
                column_ids = new List<string>()
            };
            var response = HttpUtil.Post("https://api.juejin.cn/content_api/v1/article/publish?aid=2608&uuid=6897189016987272712", JsonConvert.SerializeObject(param), headers);
            var resp = JsonConvert.DeserializeObject<JuejinResponse<JuejinDraftPublish>>(response);

            AssertUtil.IsTrue(resp.err_no != 0, $"博客发布失败，错误信息：{resp.err_msg}");
        }
    }
}
