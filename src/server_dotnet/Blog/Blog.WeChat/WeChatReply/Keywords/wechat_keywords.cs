
using Sixpence.ORM.Entity;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace Blog.WeChat.WeChatReply.Keywords
{
    [Entity("wechat_keywords", "微信关键词回复")]
    public partial class wechat_keywords : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember, Column, Description("名称")]
        public string name { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        [DataMember, Column, Description("回复内容")]
        public string reply_content { get; set; }
    }
}

