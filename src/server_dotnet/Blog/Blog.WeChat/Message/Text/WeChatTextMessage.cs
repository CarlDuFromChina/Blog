using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Blog.WeChat.Message.Text
{
    /// <summary>
    /// 文本消息
    /// </summary>
    public class WeChatTextMessage : BaseWeChatMessage
    {
        public WeChatTextMessage(XmlDocument xml) : base(xml)
        {
            if (MsgType.Trim() == "text")
            {
                Content = xml.SelectSingleNode("xml").SelectSingleNode("Content").InnerText;
            }
        }

        /// <summary>
        /// 发送的文本内容 
        /// </summary>
        public string Content { get; set; }
    }
}