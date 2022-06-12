using Sixpence.ORM.Entity;
using System;
using System.Runtime.Serialization;


namespace Blog.WeChat.RobotMessageTask
{
    [Entity("robot_message_task", "机器人消息任务")]
    public partial class robot_message_task : BaseEntity
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
        /// 消息内容
        /// </summary>
        [DataMember]
        [Column]
        public string content { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        [DataMember]
        [Column]
        public string runtime { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [DataMember]
        [Column]
        public string message_type { get; set; }

        /// <summary>
        /// 消息类型名称
        /// </summary>
        [DataMember]
        [Column]
        public string message_type_name { get; set; }

        /// <summary>
        /// 机器人
        /// </summary>
        [DataMember]
        [Column]
        public string robotid { get; set; }

        /// <summary>
        /// 机器人名称
        /// </summary>
        [DataMember]
        [Column]
        public string robotid_name { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        [DataMember]
        [Column]
        public string job_state { get; set; }

        /// <summary>
        /// 任务状态名称
        /// </summary>
        [DataMember]
        [Column]
        public string job_state_name { get; set; }
    }
}

