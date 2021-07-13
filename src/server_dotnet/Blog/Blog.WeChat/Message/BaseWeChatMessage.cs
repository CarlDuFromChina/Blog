using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Blog.WeChat.Message
{

    /// <summary>
    /// 微信消息基类
    /// </summary>
    public class BaseWeChatMessage
    {
        public BaseWeChatMessage(XmlDocument xml)
        {
            var xmlNode = xml.SelectSingleNode("xml");
            ToUserName = xmlNode.SelectSingleNode("ToUserName").InnerText;
            FromUserName = xmlNode.SelectSingleNode("FromUserName").InnerText;
            CreateTime = xmlNode.SelectSingleNode("CreateTime").InnerText;
            MsgType = xmlNode.SelectSingleNode("MsgType").InnerText;

            if (MsgType.Trim() == "event")
            {
                EventName = xml.SelectSingleNode("xml").SelectSingleNode("Event").InnerText;
                EventKey = xml.SelectSingleNode("xml").SelectSingleNode("EventKey").InnerText;
            }
        }

        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }

        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId { get; set; }

        /// <summary>
        /// 事件名
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// 事件键
        /// </summary>
        public string EventKey { get; set; }
    }
}