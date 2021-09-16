using Sixpence.EntityFramework.Entity;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Blog.Core.Module.Role
{
    [Entity("sys_role", "角色", true)]
    public partial class sys_role : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [Attr("sys_roleid", "角色id", AttrType.Varchar, 100)]
        public string sys_roleId
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
        /// 描述
        /// </summary>
        [DataMember]
        [Attr("description", "描述", AttrType.Varchar, 200)]
        public string description { get; set; }

        /// <summary>
        /// 是否基础角色
        /// </summary>
        [DataMember]
        [Attr("is_basic", "是否基础角色", AttrType.Int4)]
        public bool is_basic { get; set; }

        /// <summary>
        /// 是否基础角色
        /// </summary>
        [DataMember]
        [Attr("is_basicname", "是否基础角色", AttrType.Varchar, 100)]
        public string is_basicName { get; set; }

        /// <summary>
        /// 是否系统实体
        /// </summary>
        [DataMember]
        [Attr("is_sys", "是否系统实体", AttrType.Int4)]
        public bool is_sys { get; set; }

        /// <summary>
        /// 是否系统实体
        /// </summary>
        [DataMember]
        [Attr("is_sysname", "是否系统实体", AttrType.Varchar, 100)]
        public string is_sysName { get; set; }

        /// <summary>
        /// 继承角色
        /// </summary>
        [DataMember]
        [Attr("parent_roleid", "继承角色", AttrType.Varchar, 100)]
        public string parent_roleid { get; set; }

        /// <summary>
        /// 继承角色
        /// </summary>
        [DataMember]
        [Attr("parent_roleidname", "继承角色", AttrType.Varchar, 100)]
        public string parent_roleidName { get; set; }
    }
}

