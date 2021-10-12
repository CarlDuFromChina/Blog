using Sixpence.EntityFramework.Entity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Blog.Core.Module.Vertification.Mail
{
    [Entity("mail_vertification", "邮箱验证", true)]
    public partial class mail_vertification : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [Attr("mail_vertificationid", "邮箱验证id", AttrType.Varchar, 100)]
        public string mail_vertificationId
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

        [DataMember]
        [Attr("mail_address", "邮箱", AttrType.Varchar, 200)]
        public string mail_address { get; set; }

        /// <summary>
        /// 登录请求信息
        /// </summary>
        [DataMember]
        [Attr("login_request", "登录请求信息", AttrType.JToken)]
        public JToken login_request { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [DataMember, Attr("expire_time", "过期时间", AttrType.Timestamp, 6, true)]
        public DateTime? expire_time { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [DataMember, Attr("content", "消息内容", AttrType.Text)]
        public string content { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        [DataMember]
        [Attr("is_active", "是否激活", AttrType.Int4)]
        public bool is_active { get; set; }

        /// <summary>
        /// 激活类型
        /// </summary>
        [DataMember]
        [Attr("mail_type", "激活类型", AttrType.Varchar, 100)]
        public string mail_type { get; set; }
    }

    public enum MailType
    {
        Activation,
        ResetPassword
    }
}
