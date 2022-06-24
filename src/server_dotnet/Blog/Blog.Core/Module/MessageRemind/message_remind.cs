
using Sixpence.ORM.Entity;
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
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        [Column]
        public string name { get; set; }

        /// <summary>
        /// 接收人id
        /// </summary>
        [DataMember]
        [Column]
        public string receiverId { get; set; }

        /// <summary>
        /// 接收人名称
        /// </summary>
        [DataMember]
        [Column]
        public string receiverId_name { get; set; }

        /// <summary>
        /// 是否阅读
        /// </summary>
        [DataMember]
        [Column]
        public bool? is_read { get; set; }


        /// <summary>
        /// 是否阅读
        /// </summary>
        [DataMember]
        [Column]
        public string is_read_name { get; set; }


        /// <summary>
        /// 消息内容
        /// </summary>
        [DataMember]
        [Column]
        public string content { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [DataMember]
        [Column]
        public string message_type { get; set; }
    }
}

