
using Blog.Core.Data;
using System;
using System.Runtime.Serialization;

namespace Blog.Core.Module.MessageRemind
{
    [Entity("message_remind", "消息提醒", true)]
    public partial class message_remind : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [Attr("message_remindid", "实体id", AttrType.Varchar, 100)]
        public string message_remindId
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

        public string _receiverId;
        /// <summary>
        /// 接收人id
        /// </summary>
        [DataMember]
        [Attr("receiverId", "接收人", AttrType.Varchar, 100)]
        public string receiverId
        {
            get
            {
                return this._receiverId;
            }
            set
            {
                this._receiverId = value;
                SetAttributeValue("receiverId", value);
            }
        }

        public string _receiverIdName;
        /// <summary>
        /// 接收人名称
        /// </summary>
        [DataMember]
        [Attr("receiverIdName", "接收人", AttrType.Varchar, 100)]
        public string receiverIdName
        {
            get
            {
                return this._receiverIdName;
            }
            set
            {
                this._receiverIdName = value;
                SetAttributeValue("receiverIdName", value);
            }
        }

        /// <summary>
        /// 是否阅读
        /// </summary>
        private bool _is_read;
        [DataMember]
        [Attr("is_read", "实体名", AttrType.Int4)]
        public bool is_read
        {
            get
            {
                return this._is_read;
            }
            set
            {
                this._is_read = value;
                SetAttributeValue("is_read", value);
            }
        }


        /// <summary>
        /// 是否阅读
        /// </summary>
        private string _is_readName;
        [DataMember]
        [Attr("is_readname", "实体名", AttrType.Varchar, 100)]
        public string is_readName
        {
            get
            {
                return this._is_readName;
            }
            set
            {
                this._is_readName = value;
                SetAttributeValue("is_readName", value);
            }
        }


        /// <summary>
        /// 消息内容
        /// </summary>
        private string _content;
        [DataMember]
        [Attr("content", "消息内容", AttrType.Text)]
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

        private string _message_type;
        /// <summary>
        /// 消息类型
        /// </summary>
        [DataMember]
        [Attr("message_type", "消息类型", AttrType.Varchar, 100)]
        public string message_type
        {
            get
            {
                return this._message_type;
            }
            set
            {
                this._message_type = value;
                SetAttributeValue("message_type", value);
            }
        }
    }
}

