
using Sixpence.ORM.Entity;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace Blog.Core.Auth.Privilege
{
    [Entity("sys_role_privilege", "角色权限", true)]
    public partial class sys_role_privilege : BaseEntity
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
        /// 角色id
        /// </summary>
        [DataMember, Column, Description("角色id")]
        public string sys_roleid { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        [DataMember, Column, Description("角色名")]
        public string sys_roleid_name { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        [DataMember, Column, Description("权限值")]
        public int? privilege { get; set; }

        /// <summary>
        /// 对象id
        /// </summary>
        [DataMember, Column, Description("对象id")]
        public string objectid { get; set; }

        /// <summary>
        /// 对象名
        /// </summary>
        [DataMember, Column, Description("对象名")]
        public string objectid_name { get; set; }

        /// <summary>
        /// 对象类型
        /// </summary>
        [DataMember, Column, Description("对象类型")]
        public string object_type { get; set; }
    }
}

