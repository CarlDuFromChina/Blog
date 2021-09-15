
using Blog.Core.Data;
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
        [Attr("wechat_keywordsid", "实体id", AttrType.Varchar, 100)]
        public string wechat_keywordsId
        {
            get
            {
                return this.Id;
            }
            set
            {
                this.Id = value;
            }
        }

        
        /// <summary>
        /// 回复内容
        /// </summary>
        [DataMember]
        [Attr("reply_content", "回复内容", AttrType.Text)]
        public string reply_content { get; set; }
    }
}

