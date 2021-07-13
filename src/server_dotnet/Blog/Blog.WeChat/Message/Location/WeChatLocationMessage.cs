using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Blog.WeChat.Message.Location
{
    /// <summary>
    /// 微信地理位置消息
    /// </summary>
    public class WeChatLocationMessage : BaseWeChatMessage
    {
        public WeChatLocationMessage(XmlDocument xml) : base(xml)
        {

        }

        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Location_X { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Location_Y { get; set; }

        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public string Scale { get; set; }

        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }
    }
}