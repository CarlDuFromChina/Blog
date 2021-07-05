using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Blog.WeChat.Message.Image
{
    /// <summary>
    /// 图片消息
    /// </summary>
    public class WeChatImageMessage : BaseWeChatMessage
    {
        public WeChatImageMessage(XmlDocument xml) : base(xml)
        {

        }

        /// <summary>
        /// 图片链接（由系统生成）
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 图片消息媒体id，可以调用获取临时素材接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }
    }
}