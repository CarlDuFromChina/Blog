
using Sixpence.ORM.Entity;
using System;
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
        [DataMember]
        [Column("name", "名称", DataType.Varchar, 100)]
        public string name { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        [DataMember]
        [Column("reply_content", "回复内容", DataType.Text)]
        public string reply_content { get; set; }
    }
}

