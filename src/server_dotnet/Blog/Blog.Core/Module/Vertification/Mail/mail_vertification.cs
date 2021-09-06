using Blog.Core.Data;
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

        private string _mail_address;
        [DataMember]
        [Attr("mail_address", "邮箱", AttrType.Varchar, 200)]
        public string mail_address
        {
            get
            {
                return this._mail_address;
            }
            set
            {
                this._mail_address = value;
                SetAttributeValue("mail_address", value);
            }
        }

        /// <summary>
        /// 登录请求信息
        /// </summary>
        private JToken _login_request;
        [DataMember]
        [Attr("login_request", "登录请求信息", AttrType.JToken)]
        public JToken login_request
        {
            get
            {
                return this._login_request;
            }
            set
            {
                this._login_request = value;
                SetAttributeValue("login_request", value);
            }
        }

        /// <summary>
        /// 过期时间
        /// </summary>
        private DateTime? _expire_time;
        [DataMember, Attr("expire_time", "过期时间", AttrType.Timestamp, 6, true)]
        public DateTime? expire_time
        {
            get
            {
                return this._expire_time;
            }
            set
            {
                this._expire_time = value;
                SetAttributeValue("expire_time", value);
            }
        }

        /// <summary>
        /// 消息内容
        /// </summary>
        private string _content;
        [DataMember, Attr("content", "消息内容", AttrType.Text)]
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
        /// 是否激活
        /// </summary>
        private bool _is_active;
        [DataMember]
        [Attr("is_active", "是否激活", AttrType.Int4)]
        public bool is_active
        {
            get
            {
                return this._is_active;
            }
            set
            {
                this._is_active = value;
                SetAttributeValue("is_active", value);
            }
        }
    }
}
