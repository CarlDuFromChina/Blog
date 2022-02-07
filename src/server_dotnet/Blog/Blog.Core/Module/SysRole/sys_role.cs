using Sixpence.ORM.Entity;
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
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        [Column("name", "名称", DataType.Varchar, 100)]
        public string name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        [Column("description", "描述", DataType.Varchar, 200)]
        public string description { get; set; }

        /// <summary>
        /// 是否基础角色
        /// </summary>
        [DataMember]
        [Column("is_basic", "是否基础角色", DataType.Int4)]
        public bool is_basic { get; set; }

        /// <summary>
        /// 是否基础角色
        /// </summary>
        [DataMember]
        [Column("is_basic_name", "是否基础角色", DataType.Varchar, 100)]
        public string is_basic_name { get; set; }
        
        /// <summary>
        /// 继承角色
        /// </summary>
        [DataMember]
        [Column("parent_roleid", "继承角色", DataType.Varchar, 100)]
        public string parent_roleid { get; set; }

        /// <summary>
        /// 继承角色
        /// </summary>
        [DataMember]
        [Column("parent_roleid_name", "继承角色", DataType.Varchar, 100)]
        public string parent_roleid_name { get; set; }
    }
}

