using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Blog.WeChat.Message.Video
{
    /// <summary>
    /// 微信视频消息
    /// </summary>
    public class WeChatVideoMessage : BaseWeChatMessage
    {
        public WeChatVideoMessage(XmlDocument xml) : base(xml)
        {

        }

        /// <summary>
        /// 图片消息媒体id，可以调用获取临时素材接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string ThumbMediaId { get; set; }
    }
}