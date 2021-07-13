using Blog.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WeChat.WeChatNews
{
    public class WeChatNewsController : EntityBaseController<wechat_news, WeChatNewsService>
    {
        /// <summary>
        /// 添加图文素材
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="thumb_media_id">图文消息的封面图片素材id（必须是永久mediaID）</param>
        /// <param name="author">作者</param>
        /// <param name="digest">图文消息的摘要，仅有单图文消息才有摘要，多图文此处为空。如果本字段为没有填写，则默认抓取正文前64个字。</param>
        /// <param name="show_cover_pic">是否显示封面，0为false，即不显示，1为true，即显示</param>
        /// <param name="content">图文消息的具体内容，支持HTML标签，必须少于2万字符，小于1M，且此处会去除JS,涉及图片url必须来源 "上传图文消息内的图片获取URL"接口获取。外部图片url将被过滤。</param>
        /// <param name="content_source_url">图文消息的原文地址，即点击“阅读原文”后的URL</param>
        /// <param name="need_open_comment">Uint32 是否打开评论，0不打开，1打开</param>
        /// <param name="only_fans_can_comment">Uint32 是否粉丝才可评论，0所有人可评论，1粉丝才可评论</param>
        [HttpGet]
        public void CreateData(string title, string thumb_media_id, string author, string digest, bool show_cover_pic, string content, string content_source_url, bool need_open_comment, bool only_fans_can_comment)
        {
            new WeChatNewsService().CreateData(title, thumb_media_id, author, digest, show_cover_pic, content, content_source_url, need_open_comment, only_fans_can_comment);
        }

        /// <summary>
        /// 获取图文素材
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public WeChatNewsResponse GetDataList(int pageIndex, int pageSize)
        {
            return new WeChatNewsService().GetDataList(pageIndex, pageSize);
        }
    }
}
