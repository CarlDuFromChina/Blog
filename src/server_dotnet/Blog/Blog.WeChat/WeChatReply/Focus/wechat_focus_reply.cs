using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember, Column, Description("名称")]
        public string name { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember, Column, Description("内容")]
        public string content { get; set; }

        /// <summary>
        /// 公众号
        /// </summary>
        [DataMember, Column, Description("公众号")]
        public string wechat { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        [DataMember, Column(name: "checked"), Description("启用")]
        public bool? @checked { get; set; }
    }
}
