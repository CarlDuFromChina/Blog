
using Sixpence.EntityFramework.Entity;
using System;
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
        [Attr("sys_role_privilegeid", "角色权限id", AttrType.Varchar, 100)]
        public string sys_role_privilegeId
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
        /// 角色id
        /// </summary>
        [DataMember]
        [Attr("sys_roleid", "角色id", AttrType.Varchar, 100)]
        public string sys_roleid { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        [DataMember]
        [Attr("sys_roleidname", "角色名", AttrType.Varchar, 100)]
        public string sys_roleidName { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        [DataMember]
        [Attr("privilege", "权限值", AttrType.Int4)]
        public int? privilege { get; set; }

        /// <summary>
        /// 对象id
        /// </summary>
        [DataMember]
        [Attr("objectid", "实体id", AttrType.Varchar, 100)]
        public string objectid { get; set; }

        /// <summary>
        /// 对象名
        /// </summary>
        [DataMember]
        [Attr("objectidname", "实体名", AttrType.Varchar, 100)]
        public string objectidName { get; set; }

        /// <summary>
        /// 对象类型
        /// </summary>
        [DataMember]
        [Attr("object_type", "对象类型", AttrType.Varchar, 100)]
        public string object_type { get; set; }
    }
}

