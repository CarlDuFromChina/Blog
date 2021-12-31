using Blog.Core.Config;
using Blog.WeChat;
using Blog.WeChat.Material;
using Blog.WeChat.WeChatNews;
using Sixpence.Common;
using Sixpence.Common.Utils;
using Sixpence.EntityFramework.Broker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Blog.Sync
{
    public class SyncBlog2WeChat : ISyncBlog
    {
        public IPersistBroker Broker { get; set; }

        public void Execute(string id)
        {
            if (Broker == null)
                Broker = PersistBrokerFactory.GetPersistBroker();

            Broker.ExecuteTransaction(() =>
            {
                blog data = Broker.Retrieve<blog>(id);
                var contentUrl = $"{SystemConfig.Config.Protocol}://{SystemConfig.Config.Domain}/#/blog/{id}";
                AssertUtil.CheckIsNullOrEmpty<SpException>(data.surfaceid, "请上传博客封面", "4365FB1F-2EE7-40CF-852C-F6CFA71E8DE2");

                // 如果封面素材未上传则创建封面素材
                string mediaId = Broker.Retrieve<wechat_material>("SELECT * FROM wechat_material WHERE sys_fileid = @id", new Dictionary<string, object>() { { "@id", data.surfaceid } })?.media_id;
                if (string.IsNullOrEmpty(mediaId))
                {
                    mediaId = new WeChatMaterialService(Broker).CreateData(MaterialType.image, data.big_surfaceid)?.media_id;
                }

                // 未创建图文素材则创建，已创建则更新
                if (string.IsNullOrEmpty(data.wechat_newsid))
                {
                    data.wechat_newsid = new WeChatNewsService(Broker).CreateData(data.title, mediaId, data.createdByName, "", true, data.html_content, contentUrl, true, false);
                }
                else
                {
                    // 如果图文素材已经被删除，则创建
                    var news = Broker.Retrieve<wechat_news>(data.wechat_newsid);
                    if (news == null)
                    {
                        data.wechat_newsid = new WeChatNewsService(Broker).CreateData(data.title, mediaId, data.createdByName, "", true, data.html_content, contentUrl, true, false);
                    }
                    else
                    {
                        news.html_content = data.html_content;
                        news.thumb_media_id = mediaId;
                        news.content_source_url = contentUrl;
                        new WeChatNewsService(Broker).UpdateData(news);
                    }
                }

                // 图文素材id回写到博客
                Broker.Update(data);
            });
        }
    }
}
