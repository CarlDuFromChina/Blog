
using Sixpence.ORM.Entity;
using System;
using System.ComponentModel;
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
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember, Column, Description("名称")]
        public string name { get; set; }

        /// <summary>
        /// 钩子地址
        /// </summary>
        [DataMember, Column, Description("钩子地址")]
        public string hook { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [DataMember, Column, Description("说明")]
        public string description { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [DataMember, Column, Description("类型")]
        public string robot_type { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [DataMember, Column, Description("类型名称")]
        public string robot_type_name { get; set; }
    }
}

