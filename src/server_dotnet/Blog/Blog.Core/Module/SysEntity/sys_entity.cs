using Sixpence.ORM.Entity;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Blog.Core.Module.SysEntity
{
    [Entity("sys_entity", "实体", true)]
    [KeyAttributes("实体不能重复创建", "code")]
    public partial class sys_entity : BaseEntity
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
        /// 编码
        /// </summary>
        [DataMember, Column, Description("编码")]
        public string code { get; set; }

        /// <summary>
        /// 是否系统实体
        /// </summary>
        [DataMember, Column, Description("是否系统实体")]
        public bool? is_sys { get; set; }

        /// <summary>
        /// 是否系统实体
        /// </summary>
        [DataMember, Column, Description("是否系统实体")]
        public string is_sys_name { get; set; }
    }
}