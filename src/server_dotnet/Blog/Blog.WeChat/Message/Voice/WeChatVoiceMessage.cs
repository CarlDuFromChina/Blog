using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Blog.WeChat.Message.Voice
{
    /// <summary>
    /// 语音消息
    /// </summary>
    public class WeChatVoiceMessage : BaseWeChatMessage
    {
        public WeChatVoiceMessage(XmlDocument xml) : base(xml)
        {

        }

        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 图片消息媒体id，可以调用获取临时素材接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 语音识别结果，UTF8编码
        /// </summary>
        public string Recognition { get; set; }
    }
}