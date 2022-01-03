
using Sixpence.ORM.Entity;
using System;
using System.Runtime.Serialization;


namespace Blog.WeChat.Robot
{
    [Entity("robot", "机器人")]
    public partial class robot : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [Attr("robotid", "实体id", DataType.Varchar, 100)]
        public string robotId
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
        /// 钩子地址
        /// </summary>
        [DataMember]
        [Attr("hook", "钩子地址", DataType.Varchar, 500)]
        public string hook { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [DataMember]
        [Attr("description", "说明", DataType.Varchar, 200)]
        public string description { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [DataMember]
        [Attr("robot_type", "类型", DataType.Varchar, 100)]
        public string robot_type { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [DataMember]
        [Attr("robot_typename", "类型名称", DataType.Varchar, 100)]
        public string robot_typeName { get; set; }
    }
}

