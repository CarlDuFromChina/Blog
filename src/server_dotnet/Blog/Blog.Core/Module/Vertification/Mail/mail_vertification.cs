using Sixpence.ORM.Entity;
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
        [PrimaryColumn]
        public string id { get; set; }

        [DataMember]
        [Column("mail_address", "邮箱", DataType.Varchar, 200)]
        public string mail_address { get; set; }

        /// <summary>
        /// 登录请求信息
        /// </summary>
        [DataMember]
        [Column("login_request", "登录请求信息", DataType.Jsonb)]
        public JToken login_request { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [DataMember, Column("expire_time", "过期时间", DataType.Timestamp, 6, true)]
        public DateTime? expire_time { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [DataMember, Column("content", "消息内容", DataType.Text)]
        public string content { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        [DataMember]
        [Column("is_active", "是否激活", DataType.Int4)]
        public bool is_active { get; set; }

        /// <summary>
        /// 激活类型
        /// </summary>
        [DataMember]
        [Column("mail_type", "激活类型", DataType.Varchar, 100)]
        public string mail_type { get; set; }
    }

    public enum MailType
    {
        Activation,
        ResetPassword
    }
}
