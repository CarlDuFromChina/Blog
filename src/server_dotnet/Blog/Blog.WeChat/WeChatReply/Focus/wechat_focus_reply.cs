using Blog.Core.Data;
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
        [Attr("wechat_focus_replyid", "实体id", AttrType.Varchar, 100)]
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
        private string _content;
        [DataMember]
        [Attr("content", "内容", AttrType.Text)]
        public string content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
                SetAttributeValue("content", value);
            }
        }


        /// <summary>
        /// 公众号
        /// </summary>
        private string _wechat;
        [DataMember]
        [Attr("wechat", "公众号", AttrType.Varchar, 100)]
        public string wechat
        {
            get
            {
                return this._wechat;
            }
            set
            {
                this._wechat = value;
                SetAttributeValue("wechat", value);
            }
        }


        /// <summary>
        /// 启用
        /// </summary>
        private int? _checked;
        [DataMember]
        [Attr("checked", "启用", AttrType.Int4)]
        public int? @checked
        {
            get
            {
                return this._checked;
            }
            set
            {
                this._checked = value;
                SetAttributeValue("checked", value);
            }
        }

    }
}
