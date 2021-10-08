
using Sixpence.EntityFramework.Entity;
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
        [Attr("robot_message_taskid", "实体id", AttrType.Varchar, 100)]
        public string robot_message_taskId
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
        /// 消息内容
        /// </summary>
        [DataMember]
        [Attr("content", "消息内容", AttrType.Text)]
        public string content { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        [DataMember]
        [Attr("runtime", "执行时间", AttrType.Varchar, 100)]
        public string runtime { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [DataMember]
        [Attr("message_type", "消息类型", AttrType.Varchar, 100)]
        public string message_type { get; set; }

        /// <summary>
        /// 消息类型名称
        /// </summary>
        [DataMember]
        [Attr("message_typename", "消息类型名称", AttrType.Varchar, 100)]
        public string message_typeName { get; set; }

        /// <summary>
        /// 机器人
        /// </summary>
        [DataMember]
        [Attr("robotid", "机器人", AttrType.Varchar, 100)]
        public string robotid { get; set; }

        /// <summary>
        /// 机器人名称
        /// </summary>
        [DataMember]
        [Attr("robotidname", "机器人名称", AttrType.Varchar, 100)]
        public string robotidName { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        [DataMember]
        [Attr("job_state", "任务状态", AttrType.Varchar, 100)]
        public string job_state { get; set; }

        /// <summary>
        /// 任务状态名称
        /// </summary>
        [DataMember]
        [Attr("job_statename", "任务状态名称", AttrType.Varchar, 100)]
        public string job_stateName { get; set; }
    }
}

