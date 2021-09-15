
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

        /// <summary>
        /// 接收人id
        /// </summary>
        [DataMember]
        [Attr("receiverId", "接收人", AttrType.Varchar, 100)]
        public string receiverId { get; set; }

        /// <summary>
        /// 接收人名称
        /// </summary>
        [DataMember]
        [Attr("receiverIdName", "接收人", AttrType.Varchar, 100)]
        public string receiverIdName { get; set; }

        /// <summary>
        /// 是否阅读
        /// </summary>
        [DataMember]
        [Attr("is_read", "实体名", AttrType.Int4)]
        public bool is_read { get; set; }


        /// <summary>
        /// 是否阅读
        /// </summary>
        [DataMember]
        [Attr("is_readname", "实体名", AttrType.Varchar, 100)]
        public string is_readName { get; set; }


        /// <summary>
        /// 消息内容
        /// </summary>
        [DataMember]
        [Attr("content", "消息内容", AttrType.Text)]
        public string content { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [DataMember]
        [Attr("message_type", "消息类型", AttrType.Varchar, 100)]
        public string message_type { get; set; }
    }
}

