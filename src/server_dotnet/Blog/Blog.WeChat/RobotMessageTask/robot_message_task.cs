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
        [Column("name", "名称", DataType.Varchar, 100)]
        public string name { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [DataMember]
        [Column("content", "消息内容", DataType.Text)]
        public string content { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        [DataMember]
        [Column("runtime", "执行时间", DataType.Varchar, 100)]
        public string runtime { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [DataMember]
        [Column("message_type", "消息类型", DataType.Varchar, 100)]
        public string message_type { get; set; }

        /// <summary>
        /// 消息类型名称
        /// </summary>
        [DataMember]
        [Column("message_typename", "消息类型名称", DataType.Varchar, 100)]
        public string message_typeName { get; set; }

        /// <summary>
        /// 机器人
        /// </summary>
        [DataMember]
        [Column("robotid", "机器人", DataType.Varchar, 100)]
        public string robotid { get; set; }

        /// <summary>
        /// 机器人名称
        /// </summary>
        [DataMember]
        [Column("robotidname", "机器人名称", DataType.Varchar, 100)]
        public string robotidName { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        [DataMember]
        [Column("job_state", "任务状态", DataType.Varchar, 100)]
        public string job_state { get; set; }

        /// <summary>
        /// 任务状态名称
        /// </summary>
        [DataMember]
        [Column("job_statename", "任务状态名称", DataType.Varchar, 100)]
        public string job_stateName { get; set; }
    }
}

