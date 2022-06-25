using Sixpence.ORM.Entity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.ComponentModel;

namespace Blog.Core.Module.Vertification.Mail
{
    [Entity("mail_vertification", "邮箱验证", true)]
    public partial class mail_vertification : BaseEntity
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
        /// 邮箱地址
        /// </summary>
        [DataMember, Column, Description("邮箱地址")]
        public string mail_address { get; set; }

        /// <summary>
        /// 登录请求信息
        /// </summary>
        [DataMember, Column, Description("登录请求信息")]
        public JToken login_request { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [DataMember, Column, Description("过期时间")]
        public DateTime? expire_time { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [DataMember, Column, Description("消息内容")]
        public string content { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        [DataMember, Column, Description("是否激活")]
        public bool? is_active { get; set; }

        /// <summary>
        /// 激活类型
        /// </summary>
        [DataMember, Column, Description("激活类型")]
        public string mail_type { get; set; }
    }

    public enum MailType
    {
        Activation,
        ResetPassword
    }
}
