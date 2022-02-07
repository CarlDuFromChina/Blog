
using Sixpence.ORM.Entity;
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
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        [Column("name", "名称", DataType.Varchar, 100)]
        public string name { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        [DataMember]
        [Column("sys_roleid", "角色id", DataType.Varchar, 100)]
        public string sys_roleid { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        [DataMember]
        [Column("sys_roleid_name", "角色名", DataType.Varchar, 100)]
        public string sys_roleid_name { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        [DataMember]
        [Column("privilege", "权限值", DataType.Int4)]
        public int? privilege { get; set; }

        /// <summary>
        /// 对象id
        /// </summary>
        [DataMember]
        [Column("objectid", "实体id", DataType.Varchar, 100)]
        public string objectid { get; set; }

        /// <summary>
        /// 对象名
        /// </summary>
        [DataMember]
        [Column("objectid_name", "实体名", DataType.Varchar, 100)]
        public string objectid_name { get; set; }

        /// <summary>
        /// 对象类型
        /// </summary>
        [DataMember]
        [Column("object_type", "对象类型", DataType.Varchar, 100)]
        public string object_type { get; set; }
    }
}

