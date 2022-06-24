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
        [Column]
        public string name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        [Column]
        public string description { get; set; }

        /// <summary>
        /// 是否基础角色
        /// </summary>
        [DataMember]
        [Column]
        public bool? is_basic { get; set; }

        /// <summary>
        /// 是否基础角色
        /// </summary>
        [DataMember]
        [Column]
        public string is_basic_name { get; set; }
        
        /// <summary>
        /// 继承角色
        /// </summary>
        [DataMember]
        [Column]
        public string parent_roleid { get; set; }

        /// <summary>
        /// 继承角色
        /// </summary>
        [DataMember]
        [Column]
        public string parent_roleid_name { get; set; }
    }
}

