
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
        [Column("name", "名称", DataType.Varchar, 100)]
        public string name { get; set; }

        /// <summary>
        /// 接收人id
        /// </summary>
        [DataMember]
        [Column("receiverId", "接收人", DataType.Varchar, 100)]
        public string receiverId { get; set; }

        /// <summary>
        /// 接收人名称
        /// </summary>
        [DataMember]
        [Column("receiverIdName", "接收人", DataType.Varchar, 100)]
        public string receiverIdName { get; set; }

        /// <summary>
        /// 是否阅读
        /// </summary>
        [DataMember]
        [Column("is_read", "实体名", DataType.Int4)]
        public bool is_read { get; set; }


        /// <summary>
        /// 是否阅读
        /// </summary>
        [DataMember]
        [Column("is_readname", "实体名", DataType.Varchar, 100)]
        public string is_readName { get; set; }


        /// <summary>
        /// 消息内容
        /// </summary>
        [DataMember]
        [Column("content", "消息内容", DataType.Text)]
        public string content { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [DataMember]
        [Column("message_type", "消息类型", DataType.Varchar, 100)]
        public string message_type { get; set; }
    }
}

