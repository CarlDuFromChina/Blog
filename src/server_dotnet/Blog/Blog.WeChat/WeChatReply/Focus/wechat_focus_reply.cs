using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WeChat.WeChatReply.Focus
{
    [Entity("wechat_focus_reply", "微信关注回复")]
    public partial class wechat_focus_reply : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [Attr("wechat_focus_replyid", "实体id", DataType.Varchar, 100)]
        public string wechat_focus_replyId
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
        /// 内容
        /// </summary>
        [DataMember]
        [Attr("content", "内容", DataType.Text)]
        public string content { get; set; }

        /// <summary>
        /// 公众号
        /// </summary>
        [DataMember]
        [Attr("wechat", "公众号", DataType.Varchar, 100)]
        public string wechat { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        [DataMember]
        [Attr("checked", "启用", DataType.Int4)]
        public int? @checked { get; set; }
    }
}
