
using Blog.Core.Data;
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
        [Attr("robotid", "实体id", AttrType.Varchar, 100)]
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
        private string _hook;
        [DataMember]
        [Attr("hook", "钩子地址", AttrType.Varchar, 500)]
        public string hook
        {
            get
            {
                return this._hook;
            }
            set
            {
                this._hook = value;
                SetAttributeValue("hook", value);
            }
        }


        /// <summary>
        /// 说明
        /// </summary>
        private string _description;
        [DataMember]
        [Attr("description", "说明", AttrType.Varchar, 200)]
        public string description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
                SetAttributeValue("description", value);
            }
        }


        /// <summary>
        /// 类型
        /// </summary>
        private string _robot_type;
        [DataMember]
        [Attr("robot_type", "类型", AttrType.Varchar, 100)]
        public string robot_type
        {
            get
            {
                return this._robot_type;
            }
            set
            {
                this._robot_type = value;
                SetAttributeValue("robot_type", value);
            }
        }


        /// <summary>
        /// 类型名称
        /// </summary>
        private string _robot_typeName;
        [DataMember]
        [Attr("robot_typename", "类型名称", AttrType.Varchar, 100)]
        public string robot_typeName
        {
            get
            {
                return this._robot_typeName;
            }
            set
            {
                this._robot_typeName = value;
                SetAttributeValue("robot_typeName", value);
            }
        }

    }
}

