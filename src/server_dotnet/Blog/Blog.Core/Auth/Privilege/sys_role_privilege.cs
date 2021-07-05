
using Blog.Core.Data;
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
        private string _sys_roleid;
        [DataMember]
        [Attr("sys_roleid", "角色id", AttrType.Varchar, 100)]
        public string sys_roleid
        {
            get
            {
                return this._sys_roleid;
            }
            set
            {
                this._sys_roleid = value;
                SetAttributeValue("sys_roleid", value);
            }
        }


        /// <summary>
        /// 角色名
        /// </summary>
        private string _sys_roleidName;
        [DataMember]
        [Attr("sys_roleidname", "角色名", AttrType.Varchar, 100)]
        public string sys_roleidName
        {
            get
            {
                return this._sys_roleidName;
            }
            set
            {
                this._sys_roleidName = value;
                SetAttributeValue("sys_roleidName", value);
            }
        }

        /// <summary>
        /// 权限值
        /// </summary>
        private int? _privilege;
        [DataMember]
        [Attr("privilege", "权限值", AttrType.Int4)]
        public int? privilege
        {
            get
            {
                return this._privilege;
            }
            set
            {
                this._privilege = value;
                SetAttributeValue("privilege", value);
            }
        }

        /// <summary>
        /// 实体id
        /// </summary>
        private string _sys_entityid;
        [DataMember]
        [Attr("sys_entityid", "实体id", AttrType.Varchar, 100)]
        public string sys_entityid
        {
            get
            {
                return this._sys_entityid;
            }
            set
            {
                this._sys_entityid = value;
                SetAttributeValue("sys_entityid", value);
            }
        }


        /// <summary>
        /// 实体名
        /// </summary>
        private string _sys_entityidName;
        [DataMember]
        [Attr("sys_entityidname", "实体名", AttrType.Varchar, 100)]
        public string sys_entityidName
        {
            get
            {
                return this._sys_entityidName;
            }
            set
            {
                this._sys_entityidName = value;
                SetAttributeValue("sys_entityidName", value);
            }
        }

    }
}

